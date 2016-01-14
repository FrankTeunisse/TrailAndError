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
        public status Status = status.New;

        public List<Node> Edges = new List<Node>();

        public Node(string newName)
        {
            this.name = newName;
        }

        public void addEdge (Node newEdge)
        {
            this.Edges.Add(newEdge);
        }

        public bool CanStart
        {
            get
            {
                bool AllDependenciesReady = true;
                foreach(var Depedency in Edges)
                {
                    AllDependenciesReady = AllDependenciesReady && (Depedency.Status == status.Completed);
                }
                return AllDependenciesReady;
            }
        }
    }

    enum status  {New, Qued, Working, Completed}
}
