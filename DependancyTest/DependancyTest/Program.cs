using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependancyTest.Models;

namespace DependancyTest
{
    class Program
    {
        static Node a = new Node("a");
        static Node b = new Node("b");
        static Node c = new Node("c");
        static Node d = new Node("d");
        static Node e = new Node("e");

        static void Main(string[] args)
        {
            a.addEdge(b);
            a.addEdge(d);
            b.addEdge(c);
            b.addEdge(e);
            c.addEdge(d);
            c.addEdge(e);

            //test_dep_resolve();
            startNextTask();
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }

        static void execute(Node task)
        {
            Console.WriteLine(task.name + " Executed");
            task.Executed = true;
            startNextTask();
        }

        static void startNextTask()
        {
            var executionOrder = new List<Node>();
            dep_resolve4(a, executionOrder);
            if (executionOrder.Count != 0)
                execute(executionOrder.First());
        }

        static void test_dep_resolve()
        {
            var executionOrder = new List<Node>();
            dep_resolve4(a, executionOrder);
            Console.WriteLine("---- This is the execution order ----");
            foreach (var edge in executionOrder)
                Console.WriteLine(edge.name);

            Console.WriteLine("---- Making the graph invallid and trying again ----");
            d.addEdge(b);

            var executionOrder2 = new List<Node>();
            dep_resolve4(a, executionOrder2);
        }

        static void dep_resolve4(Node node, List<Node> Resolved, List<Node> Unresolved = null)
        {
            //Console.WriteLine(node.name);
            Unresolved = Unresolved ?? new List<Node>(); // Create a new list if no list was provided (for the first call)
            Unresolved.Add(node);
            foreach (var edge in node.Edges)
            {
                if (!Resolved.Any(x => x.name == edge.name)) //If this item is not in our list yet (we can do this with Resolved.contains too)
                {
                    if (Unresolved.Any(y => y.name == edge.name)) //If we are already looking for this dependancy
                        throw new Exception("Circular reference detected"); //We have a problem: 

                    dep_resolve4(edge, Resolved, Unresolved); //Add the item to our list
                }
            }
            if(!node.Executed)
                Resolved.Add(node);
            Unresolved.Remove(node);
        }
    }
}
