using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace PokerRoomManager
{
    public class RoomManager
    {
        public static string RoomLocation;
        public static string RoomName;
        public static string MachineIp;

        static RoomManager()
        {
            RoomLocation = ConfigurationManager.AppSettings["SocketServerLocation"];
            RoomName = ConfigurationManager.AppSettings["SocketServerName"];
            MachineIp = ConfigurationManager.AppSettings["MachineIp"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static List<Room> GetActiveRoomList()
        {
            List<Room> roomList = new List<Room>();
            List<int> pids = new List<int>();

            foreach (Process p in Process.GetProcessesByName(RoomName))
            {
                pids.Add(p.Id);
            }

            foreach (int pid in pids)
            {
                using (NamedPipeClientStream npc = new NamedPipeClientStream(".", "wss" + pid, PipeDirection.In))
                {
                    try
                    {
                        npc.Connect(1500);
                        using (StreamReader sr = new StreamReader(npc))
                        {
                            string data = sr.ReadToEnd();
                            string[] chunks = data.Split(':');
                            if (chunks.Length != 2) continue;
                            roomList.Add(new Room() { Name = chunks[0], Port = int.Parse(chunks[1]), Pid = pid });
                        }
                    }
                    catch (TimeoutException)
                    {
                        continue; // Since this one timed out, we don't care about it, so move on to the next one.
                    }
                }
            }

            return roomList;
        }

        public static void StartRoom(string name, int port, string pass)
        {
            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(RoomLocation, RoomName + ".exe"),
                    WindowStyle = ProcessWindowStyle.Minimized,
                    Arguments = name + " " + port + " " + pass,
                    UseShellExecute = true,                    
                }
            };
            try
            {
                p.Start();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public static bool ShutdownRoom(int pid)
        {
            try
            {
                Process p = Process.GetProcessById(pid);

                try
                {
                    p.Kill();
                    return true;
                }
                catch
                {
                    return false; // Couldn't kill it for some reason.
                }
            }
            catch (ArgumentException)
            {
                return false; // Invalid pid.
            }
        }
    }
}