using System;
using System.Linq;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;

    private T[] elements;
    private int startIndex;
    private int endIndex;

    public int Count { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
        startIndex = 0;
        endIndex = 0;
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length - 1)
        {
            Resize();
        }
        this.elements[endIndex] = element;
        endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++;
    }

    private void Resize()
    {
        var copy = new T[this.elements.Length * 2];
        this.CopyAllElements(copy);
        this.elements = copy;
        this.startIndex = 0;
        this.endIndex = this.Count;
    }

    private void CopyAllElements(T[] newArray)
    {
        int sourceIndex = this.startIndex;
        int destinationIndex = 0;
        for (int i = 0; i < this.Count; i++)
        {
            newArray[destinationIndex] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.elements.Length;
            destinationIndex++;
        }
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var result = this.elements[startIndex];
        this.elements[startIndex] = default(T);
        this.startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;
        return result;
    }

    public T[] ToArray()
    {
        var result = new T[this.Count];
        this.CopyAllElements(result);
        return result;
    }
}


public class Example
{
    public static void Main()
    {
        // Arrange
        int elementsCount = 20;
        int initialCapacity = 1;

        // Act
        CircularQueue<int> queue = new CircularQueue<int>(initialCapacity);
        for (int i = 0; i < elementsCount; i++)
        {
            queue.Enqueue(i);
        }

        // Assert
        Console.WriteLine("{0} {1}", elementsCount, queue.Count);
        for (int i = 0; i < elementsCount; i++)
        {
            int elementFromQueue = queue.Dequeue();
            Console.WriteLine("{0} {1}", i, elementFromQueue);
        }
        Console.WriteLine("{0} {1}", 0, queue.Count);

        //CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
