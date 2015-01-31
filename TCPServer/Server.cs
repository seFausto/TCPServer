using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPServer
{
	public class Server
	{
		const int Max_Message_Size = 256;

		readonly int _port = 3000;

		TcpListener _tcpListener;
		Thread _listenThread;

		public Server()
		{
			_tcpListener = new TcpListener(IPAddress.Any, _port);
			_listenThread = new Thread(new ThreadStart(ListenForClients));
			_listenThread.Start();
		}

		void HandleClientComm(object obj)
		{
			var tcpClient = (TcpClient)obj;
			var clientStream = tcpClient.GetStream();

			var message = new byte[Max_Message_Size];
			var bytesRead = 0;

			while (true)
			{
				bytesRead = 0;
				try
				{
					bytesRead = clientStream.Read(message, 0, Max_Message_Size);
				}
				catch (Exception ex)
				{
					Debug.Print("{0} - {1}", "Error", ex.Message);
					break;
				}

				var encoder = new ASCIIEncoding();
				var decodedMessage = encoder.GetString (message);
				Debug.Print(decodedMessage);


				//This means the client discoonected
				if (decodedMessage.ToLower() == "disconnect")
				{
					Debug.Print ("Client has disconnected");
					break;
				}



				byte[] buffer = encoder.GetBytes("Hello Client!");
				clientStream.Write(buffer, 0, buffer.Length);
				clientStream.Flush();

			}


			tcpClient.Close();

		}

		void ListenForClients()
		{
			_tcpListener.Start();

			while (true)
			{
				var client = _tcpListener.AcceptTcpClient();

				var clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
				clientThread.Start(client);
			}
		}


	}
}

