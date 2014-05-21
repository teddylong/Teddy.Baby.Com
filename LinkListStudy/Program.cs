using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkListStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkList l = new LinkList();

            Node[] data = { new Node(2), new Node(5),  new Node(3),new Node(6),new Node(4),new Node(8),new Node(12),new Node(10),new Node(11)};
            foreach (Node temp in data)
            {
                l.Append(temp);
            }

            l.Print();

            l.Delete(6);
            l.Print();

            l.Insert(12, new Node(38));
            l.Print();

            Console.WriteLine(l.LocateElement(data[2]));
            Console.WriteLine("Length: " + l.GetLength().ToString());
            Console.WriteLine("Value: " + l.GetElement(4).NodeValue + "  Next Node Value: " + l.GetElement(4).NextNode.NodeValue);

            Console.ReadLine();
        }
    }
}
