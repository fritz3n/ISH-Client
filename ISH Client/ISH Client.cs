using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ISH_Client
{
    class ISH_Client
    {
        TcpClient client;
        NetworkStream stream;
        StreamWriter write;
        StreamReader read;

        Stream InStream;
        Stream OutStream;

        int[] CursorPos;

        public ISH_Client(string User)
        {
            client = new TcpClient("hllm.ddns.net", 3234);

            Console.WriteLine("Connected!");

            stream = client.GetStream();

            write = new StreamWriter(stream, Encoding.ASCII);
            read = new StreamReader(stream, Encoding.ASCII);

            write.AutoFlush = true;

            stream.Write(Encoding.ASCII.GetBytes(User),0,User.Length);

            InStream = Console.OpenStandardInput();
            OutStream = Console.OpenStandardOutput();

            Thread.Sleep(1000);

            new Task(() => { CopyFromTcpTask(); }).Start();
            //new Task(() => { CopyFromConsTask(); }).Start();
        }

        private void CopyFromTcpTask()
        {
            while (true)
            {
                Console.Write((char)read.Read());
                CursorPos = new int[]{ Console.CursorLeft, Console.CursorTop};
            }
        }

        public void CopyFromConsTask()
        {
            while (true)
            { 
                int s = Console.Read();

                Console.SetCursorPosition(CursorPos[0], CursorPos[1]);
                //Console.CursorLeft -= CursorPos;

                write.Write((char)s);
            }
        }
    }
}
