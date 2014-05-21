using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree tree = new BTree();
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            foreach (int i in data)
            {
                tree.AddDataToTree(i);
            }
            Console.WriteLine("Pre Order: ");
            tree.PreOrder(tree.RootNode);
            Console.WriteLine("\n" + "Middle Order: ");
            tree.MidOrder(tree.RootNode);
            Console.WriteLine("\n" + "After Order: ");
            tree.AfterOrder(tree.RootNode);

            Console.WriteLine("\n" + "Print: ");
            tree.Print(tree.RootNode);
            Console.WriteLine("Depth: " + tree.GetDepth(tree.RootNode));

            BTree tree1 = new BTree();
            int[] data1 = { 4, 8, 10, 34, 17, 1, 45, 3 };
            foreach (int i in data1)
            {
                tree1.AddDataToTree(i);
            }

            tree1.Print(tree1.RootNode);
            Console.WriteLine("Depth: " + tree1.GetDepth(tree1.RootNode));
            Console.ReadLine();
        }
    }
}
