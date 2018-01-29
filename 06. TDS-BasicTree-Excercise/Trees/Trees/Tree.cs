using System;
using System.Collections.Generic;

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