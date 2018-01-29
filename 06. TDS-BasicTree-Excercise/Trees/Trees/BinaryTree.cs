using System;

public class BinaryTree<T>
{
    /// <summary>
    /// Holds the value of the current leaf
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Left child of this leaf (if it exists)
    /// </summary>
    public BinaryTree<T> LeftChild { get; set; }

    /// <summary>
    /// Right child of this leaf (if it exists)
    /// </summary>
    public BinaryTree<T> RightChild { get; }

    /// <summary>
    /// Tree constructor.
    /// </summary>
    /// <param name="value">This leaf's value</param>
    /// <param name="leftChild">This leaf's left child (nullable)</param>
    /// <param name="rightChild">This leaf's right child (nullable)</param>
    public BinaryTree(T value,
        BinaryTree<T> leftChild = null,
        BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
    }

    /// <summary>
    /// Prints the tree in pre order
    /// </summary>
    /// <param name="indent">Current indentention (default: 0)</param>
    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.Value);
        LeftChild?.PrintIndentedPreOrder(indent + 1);
        RightChild?.PrintIndentedPreOrder(indent + 1);
    }

    /// <summary>
    /// Traverse the tree in order (left, root, right)
    /// </summary>
    /// <param name="action">Action to be executed with current leaf</param>
    public void EachInOrder(Action<T> action)
    {
        LeftChild?.EachInOrder(action);
        action(this.Value);
        RightChild?.EachInOrder(action);
    }

    /// <summary>
    /// Traverse the tree in post order (left, right, root)
    /// </summary>
    /// <param name="action">Action to be executed with current leaf</param>
    public void EachPostOrder(Action<T> action)
    {
        LeftChild?.EachPostOrder(action);
        RightChild?.EachPostOrder(action);
        action(this.Value);
    }
}
