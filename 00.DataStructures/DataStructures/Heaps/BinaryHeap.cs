using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(Parent(index), index))
        {
            this.Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void Swap(int index, int parentIndex)
    {
        T temp = heap[index];
        heap[index] = heap[parentIndex];
        heap[parentIndex] = temp;
    }

    private bool IsLess(int parent, int index)
    {
        int compare = heap[parent].CompareTo(heap[index]);
        if (compare < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }


    public T Peek()
    {
        return this.heap[0];
    }

    public T Pull()
    {
        // check if heap is empty
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T item = this.heap[0];

        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);

        this.HeapifyDown(0);

        return item;
    }

    private void HeapifyDown(int current)
    {
        while (current < this.heap.Count / 2)
        {
            int child = 2 * current + 1;
            if (HasRight(child) && IsLess(child, child + 1))
            {
                child = child + 1;
            }

            if (!IsLess(current, child)) break;
            this.Swap(current, child);
            current = child;
        }
    }

    private bool HasRight(int child)
    {
        return this.Count > 2 * child + 2;
    }
}
