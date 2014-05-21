using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkListStudy
{
    public class LinkList
    {
        private Node head;

        public Node Head
        {
            get { return head; }
            set { head = value; }
        }

        public LinkList()
        {
            head = null;
        }

        public Node GetElement(int nodeValue)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Empty!");
                return null;
            }
            Node root = this.head;
            while (root.NextNode != null)
            {
                if (root.NodeValue == nodeValue)
                {
                    return root;
                }
                else
                {
                    root = root.NextNode;
                }
            }
            Console.WriteLine("Not Found!");
            return null;
        }

        public void Append(Node newNode)
        {
            if (IsEmpty())
            {
                this.head = newNode;
                return;
            }
            else
            {
                Node root = this.head;
                while (root.NextNode != null)
                {
                    root = root.NextNode;
                }
                root.NextNode = newNode;
            }
        }

        public bool IsEmpty()
        {
            return (this.head == null);
        }

        public void Clear()
        {
            this.head = null;
        }

        public Node Delete(int i)
        {
            if (IsEmpty() || i < 0)
            {
                Console.WriteLine("Empty!");
                return null;
            }
            else
            {
                Node root = this.head;
                while (root.NextNode != null)
                {
                    if (root.NextNode.NodeValue == i)
                    {
                        root.NextNode = root.NextNode.NextNode;
                        return root.NextNode;
                    }
                    else
                    {
                        root = root.NextNode;
                    }
                }
                Console.WriteLine("Not Found!");
                return null;
            }
        }
        public void Insert(int i,Node newNode)
        {
            if (IsEmpty() || i < 0)
            {
                Console.WriteLine("Empty!");
                return;
            }
            else
            {
                Node root = this.head;
                while (root.NextNode != null)
                {
                    if (root.NextNode.NodeValue == i)
                    {
                        Node temp = root.NextNode.NextNode;
                        root.NextNode.NextNode = newNode;
                        newNode.NextNode = temp;
                        return;
                    }
                    else
                    {
                        root = root.NextNode;
                    }
                }
                Console.WriteLine("Not Found!");
            }
        }

        public int LocateElement(Node node)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Empty!");
                return 0;
            }
            else if (this.head == node)
            {
                return 1;
            }
            else
            { 
                Node root = this.head;
                int i = 1;
                while (root.NextNode != null)
                {
                    i++;
                    if (root.NextNode == node)
                    {
                        return i;
                    }
                    else
                    {
                        root = root.NextNode;
                    }
                }
                Console.WriteLine("Not Found!");
                return 0;
            }
        }

        public int GetLength()
        {
            if (IsEmpty())
            {
                return 0;
            }
            else
            {
                int i = 1;
                Node root = this.head;
                while(root.NextNode != null)
                {
                    i++;
                    root = root.NextNode;
                }
                return i;
            }
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Empty");
            }
            else
            {
                Node root = this.head;
                Console.WriteLine("Root: " + root.NodeValue);

                while (root.NextNode != null)
                { 
                    Console.Write("Next: ");
                    Console.WriteLine(root.NextNode.NodeValue);
                    root = root.NextNode;
                }
            }
        }
    }
}
