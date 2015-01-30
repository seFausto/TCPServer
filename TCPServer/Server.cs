using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace TCPServer
{
	public class Server
	{
		const int Max_Message_Size = 256;

		readonly int _port = 3000;

		TcpListener _tcpListener;
		Thread _listenThread;

		public Server ()
		{
			_tcpListener = new TcpListener (IPAddress.Any, _port);
			_listenThread = new Thread (new ThreadStart (ListenForClients));

		}

		void HandleClientComm (object obj)
		{
			var tcpClient = (TcpClient)obj;
			var clientStream = tcpClient.GetStream ();

			var message = new byte[Max_Message_Size];
			var bytesRead = 0;

			while (true)
			{
				bytesRead = 0;

				try
				{
					bytesRead = clientStream.Read (message, 0, Max_Message_Size);

				}
				catch (Exception ex)
				{
					Debug.Print ("{0} - {1}", "Error", ex.Message);
				}

				//This means the client discoonected
				if (bytesRead == 0)
				{
					break;
				}

				var encoder = new ASCIIEncoding ();
				Debug.Print (encoder.GetString (message, 0, bytesRead));

			}

			tcpClient.Close ();

		}

		void ListenForClients ()
		{
			_tcpListener.Start ();

			while (true)
			{
				var client = _tcpListener.AcceptTcpClient ();

				var clientThread = new Thread (new ParameterizedThreadStart (HandleClientComm));
				clientThread.Start (client);
			}
		}
	}
}

