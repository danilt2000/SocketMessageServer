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

			// Получаем сообщение
			Socket clientSocket = serverSocket.Accept();
			int received;
			while (true)
			{

				byte[] buffer = new byte[1024];


				try
				{
					received = clientSocket.Receive(buffer);
				}
				catch (Exception)
				{
					received = 0;
				}



				if (received == 0)
				{

					clientSocket.Shutdown(SocketShutdown.Both);

					clientSocket.Close();

					clientSocket = serverSocket.Accept();

				}
				// Преобразуем байты обратно в строку
				string receivedMessage = Encoding.UTF8.GetString(buffer, 0, received);

				// Выводим сообщение
				if (receivedMessage != string.Empty)
				{
					Console.WriteLine("Received: " + receivedMessage);
				}




			}

			// Закрываем сокеты
			clientSocket.Shutdown(SocketShutdown.Both);
			clientSocket.Close();







		}
	}
}