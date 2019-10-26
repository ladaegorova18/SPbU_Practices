using System;
using System.Net.Sockets;
using System.Text;

namespace ConsoleClient
{
    class Program
    {
        private const int port = 8888;
        private const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.WriteLine("Введите свое имя:");
            var userName = Console.ReadLine();
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                var stream = client.GetStream();
                while (true)
                {
                    Console.WriteLine(userName + ":");
                    var message = Console.ReadLine();
                    message = String.Format("{0}, {1}", userName, message);
                    var data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    data = new byte[64];
                    var buider = new StringBuilder();
                    var bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        buider.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    message = buider.ToString();
                    Console.WriteLine("Сервер: " + message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }
}
