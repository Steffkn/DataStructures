using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    public LinkedList<T> ByInsertion { get; set; }

    private OrderedBag<LinkedListNode<T>> ByAscending { get; set; }

    private OrderedBag<LinkedListNode<T>> ByDescending { get; set; }

    public FirstLastList()
    {
        this.ByAscending = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.ByInsertion = new LinkedList<T>();
        this.ByDescending = new OrderedBag<LinkedListNode<T>>((x, y) => y.Value.CompareTo(x.Value));
    }

    public int Count
    {
        get { return this.ByInsertion.Count; }
    }

    public void Add(T element)
    {
        LinkedListNode<T> node = new LinkedListNode<T>(element);
        this.ByAscending.Add(node);
        this.ByDescending.Add(node);
        this.ByInsertion.AddLast(element);
    }

    public void Clear()
    {
        this.ByAscending.Clear();
        this.ByDescending.Clear();
        this.ByInsertion.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (!IsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = ByInsertion.First;
        int index = 0;
        while (index < count)
        {
            yield return current.Value;
            current = current.Next;
            index++;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (!IsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = ByInsertion.Last;
        int index = 0;
        while (index < count)
        {
            yield return current.Value;
            current = current.Previous;
            index++;
        }
    }


    public IEnumerable<T> Max(int count)
    {
        if (!IsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.ByDescending.Take(count).Select(x => x.Value);
    }

    public IEnumerable<T> Min(int count)
    {
        if (!IsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.ByAscending.Take(count).Select(x => x.Value);
    }

    public int RemoveAll(T element)
    {

        LinkedListNode<T> node = new LinkedListNode<T>(element);

        foreach (var item in ByAscending.Range(node,true,node,true))
        {
            this.ByInsertion.Remove(item.Value);
        }
        int count = this.ByAscending.RemoveAllCopies(node);
        this.ByDescending.RemoveAllCopies(node);

        return count;
    }

    private bool IsInBounds(int count)
    {
        return count >= 0 && count <= this.Count;
    }
}
