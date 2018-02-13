using System;
using System.Runtime.CompilerServices;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;
        for (int i = n / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, n);
        }

        Console.WriteLine(string.Join(" ", arr));
        for (int i = n - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void HeapifyDown(T[] heap, int index, int border)
    {
        int parentIndex = index;
        while (parentIndex < border / 2)
        {
            int childIndex = parentIndex * 2 + 1;

            if (childIndex + 1 < border && IsGreather(heap, childIndex, childIndex + 1))
            {
                childIndex++;
            }

            int compare = heap[parentIndex].CompareTo(heap[childIndex]);

            if (compare < 0)
            {
                Swap(heap, childIndex, parentIndex);
            }
            parentIndex = childIndex;

            /*if (!IsLess(heap, child, index)) break;
            Swap(heap, index, child);
            index = child;*/
        }

    }

    private static void Swap(T[] heap, int parentIndex, int index)
    {
        T temp = heap[parentIndex];
        heap[parentIndex] = heap[index];
        heap[index] = temp;
    }

    private static bool IsGreather(T[] heap, int left, int right)
    {
        return heap[left].CompareTo(heap[right]) < 0;
    }
}
