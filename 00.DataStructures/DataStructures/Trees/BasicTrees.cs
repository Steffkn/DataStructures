using System;
using System.Collections.Generic;
using System.Linq;

public class BasicTrees
{
    static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    static void Main()
    {
        ReadTree();
        var sum = int.Parse(Console.ReadLine());
        SubtreesWithGivenSum(sum);
    }

    static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }

    static void AddEdge(int parent, int child)
    {
        Tree<int> parentNode = GetTreeNodeByValue(parent);
        Tree<int> childNode = GetTreeNodeByValue(child);

        parentNode.Children.Add(childNode);
        childNode.Parent = parentNode;
    }

    static void ReadTree()
    {
        int nodeCount = int.Parse(Console.ReadLine());
        for (int i = 1; i < nodeCount; i++)
        {
            var edge = Console.ReadLine().Split(' ');
            AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
        }
    }

    static Tree<int> GetRootNode()
    {
        return nodeByValue.Values
            .FirstOrDefault(x => x.Parent == null);
    }

    static void PrintTree()
    {
        GetRootNode().Print();
    }

    static void PrintLeafs()
    {
        var leafs = nodeByValue.Values
            .Where(x => x.Children.Count == 0)
            .Select(x => x.Value)
            .OrderBy(x => x);

        Console.WriteLine("Leaf nodes: {0}", String.Join(" ", leafs));
    }

    static void PrintMiddleNodes()
    {
        var nodes = nodeByValue.Values
            .Where(x => x.Parent != null && x.Children.Count > 0)
            .Select(x => x.Value)
            .OrderBy(x => x)
            .ToList();

        Console.WriteLine("Middle nodes: {0}", String.Join(" ", nodes));
    }

    static void DeepestNode()
    {
        var leafs = nodeByValue.Values
            .Where(x => x.Children.Count == 0);

        int mostParentsCount = 0;
        Tree<int> deepestNode = leafs.FirstOrDefault();

        foreach (var leaf in leafs)
        {
            int parentsCount = 0;
            var currentLeaf = leaf;
            while (currentLeaf.Parent != null)
            {
                currentLeaf = currentLeaf.Parent;
                parentsCount++;
            }

            if (mostParentsCount < parentsCount)
            {
                mostParentsCount = parentsCount;
                deepestNode = leaf;
            }
        }

        if (deepestNode != null)
        {
            Console.WriteLine("Deepest node: {0}", deepestNode.Value);
        }
    }

    static void LongestPath()
    {
        var leafs = nodeByValue.Values
            .Where(x => x.Children.Count == 0);

        int mostParentsCount = 0;
        Tree<int> deepestNode = leafs.FirstOrDefault();
        Stack<int> stack = new Stack<int>();
        foreach (var leaf in leafs)
        {
            int parentsCount = 0;
            var currentLeaf = leaf;
            while (currentLeaf.Parent != null)
            {
                currentLeaf = currentLeaf.Parent;
                parentsCount++;
            }

            if (mostParentsCount < parentsCount)
            {
                mostParentsCount = parentsCount;
                deepestNode = leaf;
            }
        }

        while (deepestNode != null)
        {
            stack.Push(deepestNode.Value);
            deepestNode = deepestNode.Parent;
        }

        Console.WriteLine("Longest path: {0}", String.Join(" ", stack));
    }

    static void PathsWithGivenSum(int sum)
    {
        var results = new List<List<int>>();
        var leafs = nodeByValue.Values
            .Where(x => x.Children.Count == 0);

        Stack<int> stack = new Stack<int>();

        foreach (var leaf in leafs)
        {
            int currentSum = 0;
            var currentLeaf = leaf;
            while (currentLeaf != null)
            {
                currentSum += currentLeaf.Value;
                currentLeaf = currentLeaf.Parent;
            }

            if (sum == currentSum)
            {
                Tree<int> deepestNode = leaf;
                while (deepestNode != null)
                {
                    stack.Push(deepestNode.Value);
                    deepestNode = deepestNode.Parent;
                }
                results.Add(stack.ToList());
                stack.Clear();
            }
        }

        Console.WriteLine("Paths of sum {0}:", sum);
        foreach (var result in results)
        {
            Console.WriteLine(String.Join(" ", result));
        }
    }

    static void SubtreesWithGivenSum(int sum)
    {
        Console.WriteLine("Subtrees of sum {0}:", sum);

        foreach (var tree in nodeByValue.Values)
        {
            var list = tree.OrderBFS();

            int currentSum = 0;
            foreach (int i in list)
            {
                currentSum += i;
            }

            if (currentSum == sum)
            {
                Console.WriteLine(string.Join(" ", tree.OrderBFS()));
            }
        }
    }

    public class Tree<T>
    {
        /// <summary>
        /// Holds the value of the current leaf
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Children of the current leaf
        /// </summary>
        public IList<Tree<T>> Children { get; private set; }

        public Tree<T> Parent { get; set; }

        /// <summary>
        /// Tree constructor
        /// </summary>
        /// <param name="value">Value for the new leaf</param>
        /// <param name="children">Children of this leaf (if any)</param>
        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
        }

        /// <summary>
        /// Print the tree structure
        /// </summary>
        /// <param name="indent">Current indentention (default: 0)</param>
        public void Print(int indent = 0)
        {
            Console.Write(new string(' ', 2 * indent));
            Console.WriteLine(this.Value);
            foreach (var child in Children)
            {
                child.Print(indent + 1);
            }
        }

        /// <summary>
        /// Traverse the tree
        /// </summary>
        /// <param name="action">Action to be executed with current leaf</param>
        public void Each(Action<T> action)
        {
            action(this.Value);
            foreach (var child in Children)
            {
                child.Each(action);
            }
        }

        /// <summary>
        /// Traverse the tree with DFS
        /// </summary>
        /// <returns>Returns all values of the tree</returns>
        public IEnumerable<T> OrderDFS()
        {
            var result = new List<T>();
            this.DFS(this, result);
            return result;
        }

        /// <summary>
        /// DFS method used for traversing the tree
        /// </summary>
        /// <param name="tree">Tree current root</param>
        /// <param name="result">Results so far</param>
        private void DFS(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                this.DFS(child, result);
            }

            result.Add(tree.Value);
        }

        /// <summary>
        /// Traverse the tree with BFS
        /// </summary>
        /// <returns>Returns all values of the tree</returns>
        public IEnumerable<T> OrderBFS()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                result.Add(item.Value);

                foreach (var child in item.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }
    }
}

