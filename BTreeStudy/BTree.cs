using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeStudy
{
    public class BTree
    {
        private TreeNode rootNode;

        public TreeNode RootNode
        {
            get { return rootNode; }
        }

        public BTree()
        {
            this.rootNode = null;
        }

        public void AddDataToTree(int numbers)
        {
            TreeNode node = new TreeNode(numbers);
            if (this.rootNode == null)
            {
                this.rootNode = node;
            }
            else
            {
                TreeNode currentNode = this.rootNode;
                this.compareData(currentNode, node);
            }
        }

        private void compareData(TreeNode currentNode, TreeNode newNode)
        {
            if (newNode.NodeValue <= currentNode.NodeValue)
            {
                if (currentNode.LeftNode == null)
                {
                    currentNode.LeftNode = newNode;
                }
                else
                {
                    currentNode = currentNode.LeftNode;
                    this.compareData(currentNode, newNode);
                }
            }
            else
            {
                if (currentNode.RightNode == null)
                {
                    currentNode.RightNode = newNode;
                }
                else
                {
                    currentNode = currentNode.RightNode;
                    this.compareData(currentNode, newNode);
                }
            }
        }

        public void PreOrder(TreeNode node)
        {
            if (node != null)
            { 
                Console.Write(node.NodeValue + "\t");
                this.PreOrder(node.LeftNode);
                this.PreOrder(node.RightNode);
            }
            else
            { 
                
            }
        }
        public void MidOrder(TreeNode node)
        {
            if (node != null)
            {
                this.MidOrder(node.LeftNode);
                Console.Write(node.NodeValue + "\t");
                this.MidOrder(node.RightNode);
            }
            else { }
        }

        public void AfterOrder(TreeNode node)
        {
            if (node != null)
            {
                this.AfterOrder(node.LeftNode);
                this.AfterOrder(node.RightNode);
                Console.Write(node.NodeValue + "\t");
            }
            else { }
        }
        private int z = 0;
        private int d = 0;
        public void Print(TreeNode node)
        {
            
            if (node != null)
            {
                Console.WriteLine(node.NodeValue);
                if(node.LeftNode != null)
                {
                    
                    Console.WriteLine("Current Node: " + node.NodeValue);
                    Console.Write("Next Left: ");
                    Print(node.LeftNode);
                }
                if (node.RightNode != null)
                {
                    Console.WriteLine("Current Node: " + node.NodeValue);
                    Console.Write("Next Right: ");
                    Print(node.RightNode);
                }
            }
        }

        public int GetDepth(TreeNode node)
        {
            z++;
            if (d < z)
            {
                d = z;
            }
            if (node.LeftNode != null)
            {
                    
                GetDepth(node.LeftNode);
                z--;
            }
            if (node.RightNode != null)
            {
                    
                GetDepth(node.RightNode);
                z--;
            }
            return d;                  
        }
    }
}
