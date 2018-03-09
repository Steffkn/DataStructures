using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Prev { get; set; }

        public Node Next { get; set; }
    }

    public Node Head { get; private set; }

    public Node Tail { get; private set; }

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.Head = new Node(element);
            this.Tail = Head;
        }
        else
        {
            var newHeadNode = new Node(element);
            newHeadNode.Next = this.Head;
            this.Head.Prev = newHeadNode;
            this.Head = newHeadNode;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.Head = new Node(element);
            this.Tail = Head;
        }
        else
        {
            var newTailNode = new Node(element);
            newTailNode.Prev = this.Tail;
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

        var result = this.Head.Value;
        this.Count--;

        if (Count == 0)
        {
            this.Tail = null;
            this.Head = null;
        }
        else
        {
            this.Head = this.Head.Next;
            this.Head.Prev = null;
        }

        return result;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var result = this.Tail.Value;
        this.Count--;
        if (Count == 0)
        {
            this.Tail = null;
            this.Head = null;
        }
        else
        {
            this.Tail = this.Tail.Prev;
            this.Tail.Next = null;
        }

        return result;
    }

    public void ForEach(Action<T> action)
    {
        var current = this.Head;

        while (current != null)
        {
            action.Invoke(current.Value);
            current = current.Next;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.Head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        var result = new T[this.Count];
        var current = this.Head;
        int i = 0;
        while (current != null)
        {
            result[i] = current.Value;
            current = current.Next;
            i++;
        }

        return result;
    }
}
