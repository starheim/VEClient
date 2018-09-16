using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ.Sockets;
using NetMQ;


namespace VEClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new RequestSocket(">tcp://206.189.9.238:5555"))
            {
                String serverMessage;

                using (var process1 = new Process())
                {
                    process1.StartInfo.FileName = @"..\..\..\VEChat\bin\Debug\VEChat.exe";
                    process1.Start();
                }

                Console.WriteLine("Visual educator client.");
                //Connecting to server
                server.SendFrame("");
                //Receiving connection confirmation
                serverMessage = server.ReceiveFrameString();

                Console.WriteLine(serverMessage);

                server.SendFrame("");
                //Confirmation both parties are connected
                serverMessage = server.ReceiveFrameString();

                Console.WriteLine(serverMessage);

                Console.WriteLine("Press enter to initiate the examination");

                Console.ReadLine();

                server.SendFrame("");

                serverMessage = server.ReceiveFrameString();

                Console.WriteLine(serverMessage);

                server.SendFrame("");

                serverMessage = server.ReceiveFrameString();

                Console.WriteLine(serverMessage);

                Console.ReadLine();
            }
            

            


        }
    }
}
