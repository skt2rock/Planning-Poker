using Newtonsoft.Json.Linq;
using PokerRoomManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RoomManagerForms
{
    public partial class Form1 : Form
    {
        public static List<Room> ActiveRoomList { get; set; }

        public Form1()
        {
            InitializeComponent();

            BindRoomList();

            MessageSuccess.Text = "";
            MessageFail.Text = "";
            ClientUrl.Text = "";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateRoom();
        }

        private void BindRoomList()
        {
            ActiveRoomList = RoomManager.GetActiveRoomList();
            RoomContainer.DataSource = ActiveRoomList;
            PortNumber.Text = CurrentPortNumber.ToString();
        }

        private int CurrentPortNumber
        {
            get
            {
                return 8000 + ActiveRoomList.Count;
            }
        }

        private void CreateRoom()
        {

            if (ActiveRoomList.Where(s => s.Name == RoomName.Text || s.Port == int.Parse(PortNumber.Text)).Any())
            {
                Error("Unable to create room: A room with the same name or port number is already running.");
                return;
            }

            RoomManager.StartRoom(RoomName.Text, int.Parse(PortNumber.Text), Password.Text, IP.Text);

            Thread.Sleep(500); // Give it a second to start up before we refresh the room list.

            BindRoomList();

            RoomName.Text = "";
            PortNumber.Text = CurrentPortNumber.ToString();
            Password.Text = "";

            Success("The room has been started.");
        }

        private void Success(string message)
        {
            MessageSuccess.Visible = true;
            MessageFail.Visible = false;
            MessageSuccess.Text = message;
        }

        private void Error(string message)
        {
            MessageFail.Visible = true;
            MessageSuccess.Visible = false;
            MessageFail.Text = message;
        }

        #region WEB HOST
        // A simple thread based web server to host the web client.

        static HttpListener _httpListener = new HttpListener();
        Thread _responseThread;
        static string WebSiteLocation;
        static string HostingUrl;
        static string DefaultFile;
        static string RoomListTrigger;

        private void ClientUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(HostingUrl);
        }

        private void btnHostWebClient_Click(object sender, EventArgs e)
        {
            // Checks if Web Clinet Folder path points to the correct folder.
            if (!File.Exists(WebSiteLocation + @"\" + DefaultFile))
            {
                MessageBox.Show("The Web Client folder does not have the default file. Please point to the right Web Client Folder.", "Wrong Folder", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                // Confirms Re-Hosting if already hosted before.
                if (_httpListener.IsListening)
                {
                    DialogResult dr = MessageBox.Show(text: "Are you sure, you want to restart hosting the web client?", caption: "Warning", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning);
                    if (dr.ToString().Equals("Yes"))
                    {
                        HostWebClient(true);
                    }
                }
                else
                {
                    HostWebClient(false);
                }
            }
        }

        private void HostWebClient(bool isRestart)
        {
            MessageWebHost.Text = "Starting Web Client Host...";
            RoomListTrigger = WebHostRoomListTrigger.Text;
            DefaultFile = WebHostDefaultFile.Text;
            try
            {
                RoomManager.MachineIp = IP.Text;
                HostingUrl = UrlToHostAt.Text;
                if (isRestart)
                {
                    _httpListener.Stop();
                    _responseThread.Abort();
                    _responseThread.Join();

                }
                _httpListener.Prefixes.Add(HostingUrl);
                _httpListener.Start(); // start web server (Run application as Administrator!)
                MessageWebHost.Text = isRestart ? "Client Re-Hosted @:" : "Client Hosted @:";
                ClientUrl.Text = HostingUrl;
                _responseThread = new Thread(ResponseThread);
                _responseThread.IsBackground = true;
                _responseThread.Start(); // start the response thread
            }
            catch (Exception ex)
            {
                MessageWebHost.Text = "Failed to start host, Need admin priviledge to run.";
            }
        }

        static void ResponseThread()
        {
            while (true)
            {
                try
                {
                    HttpListenerContext context = _httpListener.GetContext(); // get a context

                    // Now, you'll find the request URL in context.Request.Url
                    string requestedPathUrl = context.Request.Url.PathAndQuery;

                    // Removes all querry string parameters. Since the webhost simply reads the file, it cannot handle query strings.           
                    if (requestedPathUrl.Contains("?"))
                    {
                        requestedPathUrl = requestedPathUrl.Remove(requestedPathUrl.IndexOf("?"));
                    }

                    //Converts web path to file path
                    string filePath = WebSiteLocation + requestedPathUrl.Replace("/", @"\");
                    
                    if (requestedPathUrl == "/")
                    {
                        filePath += DefaultFile;
                    }
                    else if (requestedPathUrl.StartsWith(RoomListTrigger))
                    {
                        var obj = new JObject(new JProperty("machineName", RoomManager.MachineIp), new JProperty("servers", JArray.FromObject(RoomManager.GetActiveRoomList())));
                        byte[] _responseArray = Encoding.UTF8.GetBytes(obj.ToString());
                        RespondToRequestWith(context, _responseArray);
                        continue; // skips the loop to avoid opening the trigger file.
                    }

                    if (File.Exists(filePath))
                    {
                        byte[] _responseArray = File.ReadAllBytes(filePath);
                        RespondToRequestWith(context, _responseArray);
                    }
                    else
                    {
                        byte[] _responseArray = Encoding.UTF8.GetBytes("File not found, please check the URL, or ask admin to point to the right Web Client Folder");
                        RespondToRequestWith(context, _responseArray);
                    }
                }
                catch (HttpListenerException ex)
                {
                    // Thrown when previous thread is still running and listening to the same socket.
                    continue;
                }
            }
        }

        private static void RespondToRequestWith(HttpListenerContext context, byte[] _responseArray)
        {
            context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
            context.Response.KeepAlive = false; // set the KeepAlive bool to false
            context.Response.Close(); // close the connection                                      
        }



        #endregion

        private void RemoveRoom(string RoomName)
        {
            try
            {
                Room sts = ActiveRoomList.Where(se => se.Name == RoomName).First();
                if (RoomManager.ShutdownRoom(sts.Pid))
                {
                    Success("Room \"" + sts.Name + "\" has been closed.");
                }
                else
                {
                    Error("There was a problem in closing the room \"" + sts.Name + "\".");
                }
            }
            catch
            {
                Error("Unable to locate a Room with that name.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RoomListTrigger = WebHostRoomListTrigger.Text;
            DefaultFile = WebHostDefaultFile.Text;
            WebSiteLocation = WebClientFolderPath.Text = Directory.GetCurrentDirectory() + @"\PokerWebClient";
            PokerServerFilePath.Text = Directory.GetCurrentDirectory() + @"\PokerServer.exe";
            PopulateRoomManager();
            AutoDetectMachineIP();
            HostingUrl = UrlToHostAt.Text = "http://" + RoomManager.MachineIp + ":5000/";
            this.ActiveControl = UrlToHostAt;
            BindRoomList();
        }

        private void RefreshRoomList_Click(object sender, EventArgs e)
        {
            BindRoomList();
        }

        private void DeleteRoom_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveRoom(ActiveRoomList[RoomContainer.SelectedRows[0].Index].Name);
            }
            catch (Exception ex) { }
            BindRoomList();
        }

        private void btnAutoDetectIP_Click(object sender, EventArgs e)
        {
            AutoDetectMachineIP();
        }

        private void AutoDetectMachineIP()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageFail.Text = "Network not available";
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            string ipAddress = host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            this.IP.Text = RoomManager.MachineIp = ipAddress;
        }

        private void btnBrowsePokerServerFile_Click(object sender, EventArgs e)
        {
            FileDialogPokerServer.ShowDialog();
        }

        private void FileDialogPokerServer_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PokerServerFilePath.Text = FileDialogPokerServer.FileName;
            PopulateRoomManager();
        }

        private void PopulateRoomManager()
        {
            try
            {
                RoomManager.PokerServerFolderLocation = PokerServerFilePath.Text.Remove(PokerServerFilePath.Text.LastIndexOf(@"\"));
                RoomManager.PokerServerFileName = PokerServerFilePath.Text.Remove(0, PokerServerFilePath.Text.LastIndexOf(@"\") + 1).Replace(".exe", string.Empty);
                FailMessagePokerServerFileStatus.Visible = false;
            }
            catch (Exception ex)
            {
                FailMessagePokerServerFileStatus.Visible = true;
                FailMessagePokerServerFileStatus.Text = "Poker Server File Path not valid";
            }
        }

        private void btnWebClientPath_Click(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowserWebClient.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.WebClientFolderPath.Text = this.FolderBrowserWebClient.SelectedPath;
            }
        }

        private void WebClientFolderPath_TextChanged(object sender, EventArgs e)
        {
            WebSiteLocation = WebClientFolderPath.Text;
        }

        private void UrlToHostAt_TextChanged(object sender, EventArgs e)
        {
            HostingUrl = UrlToHostAt.Text;
        }

        private void PokerServerFilePath_TextChanged(object sender, EventArgs e)
        {
            PopulateRoomManager();
        }
    }
}
