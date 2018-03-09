using System;

public class LinkedQueue<T>
{
    private QueueNode<T> headNode;

    private QueueNode<T> tailNode;

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        if (this.Count == 0)
        {
            this.headNode = new QueueNode<T>(element);
            this.tailNode = this.headNode;
        }
        else
        {
            var newTailNode = new QueueNode<T>(element);
            newTailNode.PrevNode = this.tailNode;
            this.tailNode.NextNode = newTailNode;
            this.tailNode = newTailNode;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var result = this.headNode.Value;
        this.headNode = this.headNode.NextNode;
        this.headNode.PrevNode = null;
        return result;
    }

    public T[] ToArray()
    {
        T[] tempArray = new T[this.Count];
        var current = this.headNode;
        int i = 0;
        while (current != null)
        {
            tempArray[i] = current.Value;
            current = current.NextNode;
            i++;
        }

        return tempArray;
    }

    private class QueueNode<T>
    {
        public T Value { get; private set; }

        public QueueNode<T> NextNode { get; set; }

        public QueueNode<T> PrevNode { get; set; }

        public QueueNode(T value, QueueNode<T> nextNode = null, QueueNode<T> prevNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
            this.PrevNode = prevNode;
        }
    }
}
