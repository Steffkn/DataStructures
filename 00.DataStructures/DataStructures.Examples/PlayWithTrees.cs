using System;
using System.Linq;

public class PlayWithTrees
{
    static void Main()
    {
        BinarySearchTreeLab();
        BinaryHeap();
        Tree();
        Dictionary();
        BalancedOrderedSet();
        HashTable();
        CircularQueue();
    }

    private static void Dictionary()
    {
        var dictionary = new Dictionary<string, int>();

        dictionary.Add("abc", 5);
        dictionary.Add("abcd", 10);
        dictionary.Add("abce", 15);
        dictionary.Add("abcf", 20);

        foreach (var kvp in dictionary.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{kvp.Key}, {kvp.Value}");
        }
    }

    private static void Tree()
    {
        var tree =
            new Tree<int>(7,
                new Tree<int>(19,
                    new Tree<int>(1),
                    new Tree<int>(12),
                    new Tree<int>(31)),
                new Tree<int>(21),
                new Tree<int>(14,
                    new Tree<int>(23),
                    new Tree<int>(6)));

        Console.WriteLine("Tree (indented):");
        tree.Print();

        Console.Write("Tree nodes:");
        tree.Each(c => Console.Write(" " + c));
        Console.WriteLine();

        Console.WriteLine();

        var binaryTree =
            new BinaryTree<string>("*",
                new BinaryTree<string>("+",
                    new BinaryTree<string>("3"),
                    new BinaryTree<string>("2")),
                new BinaryTree<string>("-",
                    new BinaryTree<string>("9"),
                    new BinaryTree<string>("6")));

        Console.WriteLine("Binary tree (indented, pre-order):");
        binaryTree.PrintIndentedPreOrder();

        Console.Write("Binary tree nodes (in-order):");
        binaryTree.EachInOrder(c => Console.Write(" " + c));
        Console.WriteLine();

        Console.Write("Binary tree nodes (post-order):");
        binaryTree.EachPostOrder(c => Console.Write(" " + c));
        Console.WriteLine();
    }

    public static void BinaryHeap()
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

    public static void BinarySearchTreeLab()
    {
        BinarySearchTreeLab<int> bst = new BinarySearchTreeLab<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        bst.EachInOrder(Console.WriteLine);
    }

    static void BalancedOrderedSet()
    {
        var set = new BalancedOrderedSet<int>();
        set.Add(17);
        set.Add(9);
        set.Add(12);
        set.Add(19);
        set.Add(6);
        set.Add(25);
        Console.WriteLine(set.Count);
        set.Remove(12);
        Console.WriteLine(set.Contains(9));
        Console.WriteLine(set.Contains(12));
        Console.WriteLine(set.Count);
        foreach (var item in set)
        {
            Console.WriteLine(item);
        }
    }
    static void HashTable()
    {
        HashTable<string, int> grades = new HashTable<string, int>();

        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        grades.Add("Peter", 3);
        grades.Add("Maria", 6);
        grades["George"] = 5;
        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        grades.AddOrReplace("Peter", 33);
        grades.AddOrReplace("Tanya", 4);
        grades["George"] = 55;
        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        Console.WriteLine("Keys: " + string.Join(", ", grades.Keys));
        Console.WriteLine("Values: " + string.Join(", ", grades.Values));
        Console.WriteLine("Count = " + string.Join(", ", grades.Count));
        Console.WriteLine("--------------------");

        grades.Remove("Peter");
        grades.Remove("George");
        grades.Remove("George");
        Console.WriteLine("Grades:" + string.Join(",", grades));
        Console.WriteLine("--------------------");

        Console.WriteLine("ContainsKey[\"Tanya\"] = " + grades.ContainsKey("Tanya"));
        Console.WriteLine("ContainsKey[\"George\"] = " + grades.ContainsKey("George"));
        Console.WriteLine("Grades[\"Tanya\"] = " + grades["Tanya"]);
        Console.WriteLine("--------------------");
    }

    public static void CircularQueue()
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
