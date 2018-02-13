using System;

public class HeapExample
{
    static void Main()
    {

        // Arrange
        int[] arr = new int[] { 5, 1, 4, 2, 13, 6, 52, 80, 41, 17, 1, 0 };

        // Act
        Heap<int>.Sort(arr);

        // Assert
        int[] exp = new int[] { -2, 1, 5 };

        Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        heap.Insert(5);
        heap.Insert(8);
        heap.Insert(1);
        heap.Insert(3);
        heap.Insert(12);
        heap.Insert(-4);

        Console.WriteLine("Heap elements (max to min):");
        while (heap.Count > 0)
        {
            var max = heap.Pull();
            Console.WriteLine(max);
        }
    }
}
