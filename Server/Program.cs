using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_New
{
    class Program
    {
        static void Main(string[] args)
        {
            EndPoint iep = new IPEndPoint(IPAddress.Any, 9595);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iep);
            socket.Listen(5);

            Socket clientSocket = socket.Accept();

            bool End = true;

            while (End)
            {
                string str = Console.ReadLine();

                byte[] msgBytes = Encoding.ASCII.GetBytes(str);

                clientSocket.Send(msgBytes);

                byte[] msgByte = new byte[1024];

                int msgSize = clientSocket.Receive(msgByte);

                string msgStr = Encoding.ASCII.GetString(msgByte, 0, msgSize);

                Console.WriteLine(msgStr);

                if (msgStr == "Bye")
                {
                    End = false;
                }

            }

            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
