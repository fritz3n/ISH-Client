using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISH_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Userhash:");
            string User = Console.ReadLine();

            ISH_Client cli = new ISH_Client(User);
            cli.CopyFromConsTask();
        }
    }
}
