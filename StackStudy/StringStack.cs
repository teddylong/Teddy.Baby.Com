using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackStudy
{
    public class StringStack
    {
        private int top;

        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private string[] data;

        public string this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
        public StringStack(int length)
        {
            top = -1;
            size = length;
            data = new string[length];
        }
        public int GetLength()
        {
            return top + 1;
        }

        public bool IsEmpty()
        {
            return (top == -1);
        }

        public bool IsFull()
        {
            if (size == top + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            top = -1;
        }

        public void Push(string item)
        {
            if (IsFull())
            {
                Console.WriteLine("Full!");
                return;
            }
            else
            {
                data[++top] = item;
            }
        }
        public string Pop()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Empty!");
                return null;
            }
            else
            {
                string temp = data[top];
                --top;
                return temp;
            }
        }

        public string GetTopValue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Empty!");
                return null;
            }
            else
            {
                return data[top];
            }
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Empty!");
                return;
            }
            else
            {
                for (int i=0;i<size;i++)
                {
                    Console.WriteLine(i + ": " + data[i]);
                }
            }
        }
    }
}
