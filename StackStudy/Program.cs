using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            StringStack ss = new StringStack(10);
            string[] data = {"asdasd", "bsdfsdf", "cbxcb", "dcbxcb", "eetwet", "fwrwqr", "gsdfsdf", "hpioiopi", "iojkljkl" };
            foreach (string s in data)
            {
                ss.Push(s);
            }

            ss.Print();

            Console.WriteLine(ss.Pop());
            Console.WriteLine(ss.Pop());
            Console.WriteLine(ss.GetTopValue());
            ss.Print();
            ss.Clear();
            ss.Print();
            Console.ReadLine();
        }
    }
}
