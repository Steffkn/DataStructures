using System;
using System.Collections.Generic;

public class BinarySearchTreeLab<T> where T : IComparable<T>
{
    private Node root;

    public BinarySearchTreeLab()
    {

    }

    private BinarySearchTreeLab(Node root)
    {
        if (root == null)
        {
            return;
        }

        this.Copy(root);
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.LeftChild);
        this.Copy(node.RightChild);
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }

        Node parent = null;
        Node current = this.root;
        while (current != null)
        {
            parent = current;
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.LeftChild;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.RightChild;
            }
            else
            {
                break;
            }
        }

        var newNode = new Node(value);
        if (value.CompareTo(parent.Value) < 0)
        {
            parent.LeftChild = newNode;
        }
        else if (value.CompareTo(parent.Value) > 0)
        {
            parent.RightChild = newNode;
        }
    }

    public bool Contains(T value)
    {
        Node current = this.root;
        while (current != null)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.LeftChild;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.RightChild;
            }
            else
            {
                break;
            }
        }

        return current != null;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node parent = null;
        Node minNode = this.root;
        while (minNode.LeftChild != null)
        {
            parent = minNode;
            minNode = minNode.LeftChild;
        }

        if (parent == null)
        {
            this.root = minNode.RightChild;
        }
        else
        {
            parent.LeftChild = minNode.RightChild;
        }
    }

    public BinarySearchTreeLab<T> Search(T value)
    {
        Node current = this.root;
        while (current != null)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.LeftChild;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.RightChild;
            }
            else
            {
                break;
            }
        }

        if (current == null)
        {
            return null;
        }
        else
        {
            return new BinarySearchTreeLab<T>(current);
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();
        this.Range(this.root, queue, startRange, endRange);
        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.LeftChild, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.RightChild, queue, startRange, endRange);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }
        this.EachInOrder(node.LeftChild, action);
        action(node.Value);
        this.EachInOrder(node.RightChild, action);
    }

    /// <summary>
    /// Node - basic structure
    /// </summary>
    private class Node
    {
        /// <summary>
        /// Holds the value of the current leaf
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Left child of this leaf (if it exists)
        /// </summary>
        public Node LeftChild { get; set; }

        /// <summary>
        /// Right child of this leaf (if it exists)
        /// </summary>
        public Node RightChild { get; set; }

        /// <summary>
        /// Tree constructor.
        /// </summary>
        /// <param name="value">This leaf's value</param>
        /// <param name="leftChild">This leaf's left child (nullable)</param>
        /// <param name="rightChild">This leaf's right child (nullable)</param>
        public Node(T value,
            Node leftChild = null,
            Node rightChild = null)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        // Arrange
        BinarySearchTreeLab<int> bst = new BinarySearchTreeLab<int>();
        bst.Insert(1);

        // Act
        bst.DeleteMin();
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { };
    }
}
