using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeStudy
{
    public class TreeNode
    {
        private int nodeValue;
        private TreeNode leftNode;
        private TreeNode rightNode;

        public TreeNode(int nodevalue)
        {
            nodeValue = nodevalue;
            leftNode = null;
            rightNode = null;
        }

        public int NodeValue
        {
            get { return this.nodeValue; }
        }

        public TreeNode LeftNode
        {
            get { return this.leftNode; }
            set
            {
                if (this.leftNode == null)
                {
                    this.leftNode = value;
                }
            }
        }

        public TreeNode RightNode
        {
            get { return this.rightNode; }
            set
            {
                if (this.rightNode == null)
                {
                    this.rightNode = value;
                }
            }
        }
    }
}
