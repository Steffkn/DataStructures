using System;
using System.Collections.Generic;
using System.Linq;

public class PlayWithTrees
{
    static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    static void Main()
    {
        ReadTree();
        var sum = long.Parse(Console.ReadLine());
        if (nodeByValue.Values.Count == 1)
        {
            Console.WriteLine("Subtrees of sum {0}:", sum);
            if (sum == nodeByValue.Values.FirstOrDefault().Value)
            {
                Console.WriteLine(sum);
                return;
            }
        }
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
            var edge = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

    static void SubtreesWithGivenSum(long sum)
    {
        Console.WriteLine("Subtrees of sum {0}:", sum);
        foreach (var tree in nodeByValue.Values)
        {
            var list = tree.OrderBFS();

            long currentSum = 0;
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
}
