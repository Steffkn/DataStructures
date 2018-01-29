using System.Collections;
using System.Collections.Generic;
using System;

public class ReversedList<T> : IEnumerable<T>
{
    private const int DefaulthCapasity = 2;
    private T[] items;
    private int capacity;

    public ReversedList(int capasity = DefaulthCapasity)
    {
        this.items = new T[capasity];
        this.Capacity = capasity;
    }

    public int Count { get; set; }

    public int Capacity { get => this.items.Length; private set => this.capacity = value; }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.items[this.Count - index - 1];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.items[this.Count - index - 1] = value;
        }
    }

    public void Add(T item)
    {
        if (this.items.Length == this.Count)
        {
            this.Resize();
        }

        this.items[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        if (index >= this.Count || index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        T element = this.items[index];
        this.items[index] = default(T);
        this.Shift(index);
        this.Count--;

        if (this.Count <= this.items.Length / 4)
        {
            this.Shrink();
        }

        return element;
    }

    private void Shrink()
    {
        T[] copy = new T[this.items.Length / 2];
        for (int i = 0; i < this.Count; i++)
        {
            copy[i] = this.items[i];
        }
        this.items = copy;
    }

    private void Shift(int index)
    {
        for (int i = this.Count - index - 1; i < this.Count - 1; i++)
        {
            this.items[i] = this.items[i + 1];
        }
    }

    private void Resize()
    {
        T[] copy = new T[this.items.Length * 2];

        for (int i = 0; i < this.items.Length; i++)
        {
            copy[i] = this.items[i];
        }
        this.items = copy;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return this.items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
