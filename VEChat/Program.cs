using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ.Sockets;
using NetMQ;


namespace VEChat
{
    class Program
    {
        public static void ChatReceiveThread()
        {
            String incomingMessage;

            using (var VEReceiveSocket = new RequestSocket(">tcp://206.189.9.238:5563"))
            {
                VEReceiveSocket.SendFrame("");

                while (true)
                {
                    incomingMessage = VEReceiveSocket.ReceiveFrameString();
                    Console.WriteLine(incomingMessage);
                    VEReceiveSocket.SendFrame("");
                }
            }
        }

        static void Main(string[] args)
        {
            String outgoingMessage;

            using (var VESendSocket = new RequestSocket(">tcp://206.189.9.238:5560"))
            {
                ThreadStart threadRef = new ThreadStart(ChatReceiveThread);

                Thread thread = new Thread(threadRef);

                thread.Start();

                Console.WriteLine("Connecting to chat");
                VESendSocket.SendFrame("");
                String answer = VESendSocket.ReceiveFrameString();
                Console.WriteLine(answer);

                while (true)
                {
                    outgoingMessage = Console.ReadLine();

                    VESendSocket.SendFrame(outgoingMessage);

                    answer = VESendSocket.ReceiveFrameString();
                }
            }
        }
    }
}
