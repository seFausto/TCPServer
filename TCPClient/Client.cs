using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    public class Client
    {
        public Client()
        {
            var client = new TcpClient();
            var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

            client.Connect(serverEndPoint);

            var clientStream = client.GetStream();

            var encoder = new ASCIIEncoding();
            var buffer = encoder.GetBytes("Hello Server");


            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

			Console.WriteLine (buffer.ToString());

        }
    }
}

