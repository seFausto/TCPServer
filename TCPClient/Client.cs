using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    public class Client
    {
        NetworkStream _clientStream;

        public Client()
        {
            var client = new TcpClient();
            var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

            client.Connect(serverEndPoint);


            _clientStream = client.GetStream();

        }


        public void SendMessage(string message)
        {
            var encoder = new ASCIIEncoding();
            var buffer = encoder.GetBytes("Hello Server");

            _clientStream.Write(buffer, 0, buffer.Length);
            _clientStream.Flush();
        }
    }
}

