using System;

namespace TCPClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var client = new Client();

            client.SendMessage("Hello Fausto!");

            client.SendMessage("Testing");

            Console.ReadLine();
        }
    }
}
