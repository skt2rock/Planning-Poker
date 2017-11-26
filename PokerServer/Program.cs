using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Text.RegularExpressions;
using Fleck;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Net.Sockets;

namespace WebSocketServer
{
    /// <summary>
    /// Entry class for the poker server. Handles named pipe communications, as well as web socket server setup.
    /// </summary>
    class Program
    {
        public static string MachineIp;
        
        /// <summary>
        /// The card server to use for this server instance.
        /// </summary>
        static CardServer cs;

        /// <summary>
        /// The web socket server instance.
        /// </summary>
        static Fleck.WebSocketServer server;

        /// <summary>
        /// The human-readable name of this server. This is displayed on the poker server manager page, as well as the client page during the connection process.
        /// </summary>
        static string Name;

        /// <summary>
        /// The port number that this server will run on. The port must be greater than 1000 so as not to collide with standard application ports in that range.
        /// </summary>
        static int Port;

        /// <summary>
        /// Entry point for the application.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments for this console application are as follows:
        /// 
        /// Param 1: Server name. Cannot contain spaces. Valid characters are A-Z, a-z, 0-9, _, -, and .
        /// Param 2: Port number. Must be between 1000 and 65535.
        /// Param 3: Admin password. Cannot contain spaces.
        /// 
        /// A sample run might look like this, if invoked manually: C:\> PokerServer.exe MyName 8500 MyPass
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {

                args = new string[4];
                args[0] = "FC";
                args[1] = "8000";
                args[2] = "abcd";
                args[3] = "127.0.0.1";

                Console.WriteLine("Enter Room Name:");
                args[0] = Console.ReadLine();
                Console.WriteLine("Enter Port No Bet 8000 & 9000:");
                args[1] = Console.ReadLine();
                Console.WriteLine("Enter Room Password:");
                args[2] = Console.ReadLine();
                Console.WriteLine("Enter Hosting IP or D for auto detect:");
                args[3] = Console.ReadLine();
            }

            int portNumber;
            if (!int.TryParse(args[1], out portNumber))
            {
                ExitWithError("Port number is invalid.");
            }
            if (portNumber < 1000 || portNumber > 65535)
            {
                ExitWithError("Port number is outside valid range [1000-65535].");
            }
            if (!Regex.IsMatch(args[0], @"^[a-zA-Z0-9_\-.]+$"))
            {
                ExitWithError("Name is invalid. Valid name characters are A-Z, a-z, 0-9, _, -, and .");
            }

            Name = args[0];
            Port = portNumber;
            if (args[3].Equals("D"))
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    throw new Exception("Network not available.");
                }

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                MachineIp = host
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            }
            else
            {
                MachineIp = args[3];
            }

            // Set up the named pipe server and the initial async connection listener.
            NamedPipeServerStream nps = new NamedPipeServerStream("wss" + Process.GetCurrentProcess().Id, PipeDirection.Out, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            nps.BeginWaitForConnection(PipeHandler, nps);

            cs = new CardServer(args[2]);

            try
            {
                //server = new Fleck.WebSocketServer("ws://" + Environment.MachineName + ":" + Port + "/");
                server = new Fleck.WebSocketServer("ws://" + MachineIp + ":" + Port + "/");                
                server.Start(s => cs.NewClient((WebSocketConnection)s));                
                Console.WriteLine("PID: "+ Process.GetCurrentProcess().Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            while (true)
            {
                Console.ReadKey();
            }
        }

        static void PipeHandler(IAsyncResult res)
        {
            NamedPipeServerStream nps = res.AsyncState as NamedPipeServerStream;

            // End the current connection.
            nps.EndWaitForConnection(res);

            string data = Name + ":" + Port + "\r\n";

            nps.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
            nps.Flush();
            nps.WaitForPipeDrain();

            nps.Disconnect();

            // Start the new connection.
            nps.BeginWaitForConnection(PipeHandler, nps);
        }

        static void ExitWithError(string error)
        {
            Console.WriteLine(error);
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
