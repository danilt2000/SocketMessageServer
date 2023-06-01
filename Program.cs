using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SocketWorkSecond
{
	internal class Program
	{
		static void Main(string[] args)
		{

			// Создаем клиентский сокет
			// Создаем серверный сокет
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Привязываем его к определенному IP-адресу и порту
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 12345);
			serverSocket.Bind(endPoint);

			// Начинаем прослушивание
			serverSocket.Listen(10);
			// Принимаем клиента

			while (true)
			{
				Socket clientSocket = serverSocket.Accept();
				Console.WriteLine("User Join");
				Thread clientThread = new Thread(() => HandleClient(clientSocket));
				clientThread.Start();
			}

			void HandleClient(Socket clientSocket)
			{
				byte[] buffer = new byte[1024];
				int received;
				while (true)
				{
					try
					{
						received = clientSocket.Receive(buffer);
					}
					catch (Exception)
					{
						Console.WriteLine("Client disconnected forcefully.");
						break;
					}

					if (received == 0)
					{
						Console.WriteLine("Client disconnected gracefully.");
						break;
					}

					string receivedMessage = Encoding.UTF8.GetString(buffer, 0, received);
					if (receivedMessage != string.Empty)
					{
						Console.WriteLine("Received: " + receivedMessage);
					}
				}

				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
			}





		}

		



	}
}