using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!~");
            for (int i = 0; i < 5000; i++)
            {

                Console.WriteLine(i);
                
            }
            Console.ReadLine();

        }
    }
}
