using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UdpBroadcastSender
{
    class Program
    {
        public static int Humid = 50;
        private static readonly Random Getrandom = new Random();

        public static int GetRandomNumber()
        {
            if (Humid == 100)
            {
                lock (Getrandom) // synchronize
                {
                    Humid = Getrandom.Next(Humid - 1, Humid + 0);
                    return Humid;
                }
            }
            else if (Humid == 0)
            {
                lock (Getrandom) // synchronize
                {
                    Humid = Getrandom.Next(Humid - 0, Humid + 2);
                    return Humid;
                }
            }
            else
            {
                lock (Getrandom) // synchronize
                {
                    Humid = Getrandom.Next(Humid - 1, Humid + 2);
                    return Humid;
                }
            }
            
        }
        public const int Port = 8400;
        static void Main()
        {
            
            UdpClient socket = new UdpClient();
            socket.EnableBroadcast = true; // IMPORTANT
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, Port);
            while (true)
            {
                string message = "" + Humid;
                byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
                socket.Send(sendBuffer, sendBuffer.Length, endPoint);
                Console.WriteLine("Message sent to broadcast address {0} port {1}", endPoint.Address, Port);
                Thread.Sleep(5000);
                GetRandomNumber();
            }
        }
    }
}
