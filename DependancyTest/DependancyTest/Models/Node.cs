using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependancyTest.Models
{
    class Node
    {
        public string name;
        public bool Executed = false;

        public List<Node> Edges = new List<Node>();

        public Node(string newName)
        {
            this.name = newName;
        }

        public void addEdge (Node newEdge)
        {
            this.Edges.Add(newEdge);
        }
    }
}
