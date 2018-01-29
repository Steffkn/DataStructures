using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

class Launcher
{
    public static void Main()
    {
        LinkedList<int> list = new LinkedList<int>();

        for (int i = 0; i < 10; i++)
        {
            list.AddFirst(i);
            Console.WriteLine("{0} {1}", i, list.Head.Value);
        }

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("{0} {1}", i, list.RemoveLast());
        }

        Console.WriteLine("{0} {1}", 0, list.Count);
    }
}
