using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkListStudy
{
    public class Node
    {
        private int nodeValue;

        public int NodeValue
        {
            get { return nodeValue; }
            set { nodeValue = value; }
        }

        private Node nextNode;

        public Node NextNode
        {
            get { return nextNode; }
            set { nextNode = value; }
        }

        public Node()
        {
            this.nextNode = null;
        }
        public Node(int value)
        {
            this.nodeValue = value;
            this.nextNode = null;
        }
        public Node(Node next)
        {
            this.nextNode = next;
        }


    }
}
