using Newtonsoft.Json.Linq;
using PokerRoomManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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

            RoomManager.StartRoom(RoomName.Text, int.Parse(PortNumber.Text), Password.Text);

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
        static string WebSiteLocation = ConfigurationManager.AppSettings["WebSiteLocation"];
        static string HostingUrl = ConfigurationManager.AppSettings["HostingUrl"];
        static string DefaultFile = ConfigurationManager.AppSettings["DefaultFile"];
        static string RoomListTrigger = ConfigurationManager.AppSettings["RoomListTrigger"];

        private void ClientUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(HostingUrl);
        }

        private void btnHostWebClient_Click(object sender, EventArgs e)
        {
            MessageWebHost.Text = "Starting Web Client Host...";
            try
            {
                HostingUrl = UrlToHostAt.Text; // comment this line to read url from app config
                _httpListener.Prefixes.Add(HostingUrl);
                _httpListener.Start(); // start web server (Run application as Administrator!)
                MessageWebHost.Text = "Client Hosted @:";
                ClientUrl.Text = HostingUrl;
                Thread _responseThread = new Thread(ResponseThread);
                _responseThread.IsBackground = true;
                _responseThread.Start(); // start the response thread
            }
            catch (Exception ex)
            {                
                MessageWebHost.Text = "Failed to start host, try again.";
            }
        }

        static void ResponseThread()
        {
            while (true)
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
                    context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
                    context.Response.KeepAlive = false; // set the KeepAlive bool to false
                    context.Response.Close(); // close the connection
                    continue; // skips the loop to avoid opening the trigger file.
                }

                if (File.Exists(filePath))
                {
                    byte[] _responseArray = File.ReadAllBytes(filePath);
                    context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
                    context.Response.KeepAlive = false; // set the KeepAlive bool to false
                    context.Response.Close(); // close the connection                                      
                }
            }
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
            this.ActiveControl = UrlToHostAt;
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
    }
}
