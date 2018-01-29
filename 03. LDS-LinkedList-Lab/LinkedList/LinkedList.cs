using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Next { get; set; }
    }

    public Node Head { get; private set; }

    public Node Tail { get; private set; }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        if (this.Count == 0)
        {
            this.Head = new Node(item);
            this.Tail = new Node(item);
        }
        else
        {
            var second = new Node(this.Head.Value);
            second.Next = this.Head.Next;

            this.Head.Value = item;
            this.Head.Next = second;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        if (this.Count == 0)
        {
            this.Head = new Node(item);
            this.Tail = Head;
        }
        else
        {
            var newTailNode = new Node(item);
            this.Tail.Next = newTailNode;
            this.Tail = newTailNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var value = this.Head.Value;
        this.Head = this.Head.Next;
        this.Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var value = this.Tail.Value;
        var newTailNode = this.GetSecondToLast();
        newTailNode.Next = null;

        this.Tail = newTailNode;
        this.Count--;
        return value;
    }

    private Node GetSecondToLast()
    {
        var current = this.Head;
        var secondToLast = this.Head;

        while (current.Next != null)
        {
            secondToLast = current;
            current = current.Next;
        }

        return secondToLast;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.Head;

        while (current != null)
        {
            var result = current.Value;
            current = current.Next;
            yield return result;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
