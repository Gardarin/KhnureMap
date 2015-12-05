using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Model
{
    public class Node
    {
        public string Name;
        public int X;
        public int Y;
        public List<Node> Nodes;

        public Node(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
            Nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
    }
}
