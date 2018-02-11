using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Node root;
    private Dictionary<T, Node> nodesByValue;

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.nodesByValue = new Dictionary<T, Node>();
        nodesByValue.Add(root, this.root);
    }

    public int Count
    {
        get { return nodesByValue.Count; }
    }

    public void Add(T element, T child)
    {
        if (!nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        if (nodesByValue.ContainsKey(child))
        {
            throw new ArgumentException();
        }

        Node parentNode = this.nodesByValue[element];
        Node childNode = new Node(child, parentNode);
        parentNode.Children.Add(childNode);
        this.nodesByValue.Add(child, childNode);
    }

    public void Remove(T element)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        if (this.root.Value.Equals(element))
        {
            throw new InvalidOperationException();
        }

        Node toRemove = this.nodesByValue[element];
        foreach (var child in toRemove.Children)
        {
            child.Parent = toRemove.Parent;
        }

        toRemove.Parent.Children.AddRange(toRemove.Children);
        toRemove.Parent.Children.Remove(toRemove);
        nodesByValue.Remove(toRemove.Value);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!nodesByValue.ContainsKey(item))
        {
            throw new ArgumentException();
        }

        return nodesByValue[item].Children.Select(x => x.Value);
    }

    public T GetParent(T item)
    {
        if (!nodesByValue.ContainsKey(item))
        {
            throw new ArgumentException();
        }

        if (nodesByValue[item].Parent == null)
        {
            return default(T);
        }

        return nodesByValue[item].Parent.Value;
    }

    public bool Contains(T value)
    {
        return this.nodesByValue.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        var result = new HashSet<T>(this.nodesByValue.Keys);
        result.IntersectWith(other.nodesByValue.Keys);
        return result;
    }

    // bfs
    public IEnumerator<T> GetEnumerator()
    {
        var queue = new Queue<Node>();
        var current = this.root;
        queue.Enqueue(current);

        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            yield return current.Value;
            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Node
    {
        public T Value { get; set; }

        public Node Parent { get; set; }

        public List<Node> Children { get; set; }

        public Node(T value, Node parent = null)
        {
            this.Value = value;
            this.Children = new List<Node>();
            this.Parent = parent;
        }
    }
}
