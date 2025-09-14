using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace qlocktwo
{
    internal class UDP
    {
        private static readonly string Ip = DataExchange.instance.deskpiIP;
        private static readonly int ListenPort = DataExchange.instance.deskpiListenPort;
        private static readonly int SendPort = DataExchange.instance.deskpiSendPort;
        private static bool _startup = true;
        public static void GetTemperature()
        {

            // wait 10s on startup to make sure network is up and running
            if (_startup == true)
            {
                _startup = false;
                Thread.Sleep(10000);
            }

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress deskpi = IPAddress.Parse(Ip);
            const string trigger = "clock";
            
            while (true)
            {
                byte[] sendbuf = Encoding.ASCII.GetBytes(trigger);
                IPEndPoint ep = new IPEndPoint(deskpi, SendPort);
                s.SendTo(sendbuf, ep);
                Console.WriteLine($"Message sent to the address {deskpi}");
                StartListener();
            }   

        }


        private static void StartListener(bool protActive = true)
        {
            UdpClient listener = new UdpClient(ListenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, ListenPort);
            listener.Client.ReceiveTimeout = 5000;

            try
            {
                Console.WriteLine("Waiting for response");
                byte[] bytes = listener.Receive(ref groupEP);
                DataExchange.instance.roomtemperature = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                Console.WriteLine($"Received response from {groupEP}\n");

                if (protActive)
                {
                    string path = DataExchange.instance.pathProt;
                    // write received string to file
                    FileUtility.Append($"{path}room_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt", DataExchange.instance.roomtemperature);
                }
                
                System.Threading.Thread.Sleep(10000);
            }
            catch (SocketException e)
            {
                DataExchange.instance.roomtemperature = "NoData";
                Console.WriteLine(e);
		        System.Threading.Thread.Sleep(10000);
            }
            finally
            {
                listener.Close();
            }
        }

    }
}
