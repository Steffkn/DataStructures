using System;
using System.Collections.Generic;
using System.Linq;

namespace _06.SequenceN_M
{
    public class SequenceNtoM
    {
        static void Main()
        {
            int[] input = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int number = input[0];
            int mumber = input[1];

            var items = new Queue<Node<int>>();
            items.Enqueue(new Node<int>(number));

            while (items.Count > 0)
            {
                var item = items.Dequeue();
                if (item.Value < mumber)
                {
                    items.Enqueue(new Node<int>(item.Value + 1, item));
                    items.Enqueue(new Node<int>(item.Value + 2, item));
                    items.Enqueue(new Node<int>(item.Value * 2, item));
                }
                else if (item.Value == mumber)
                {
                    PrintResult(item);
                    return;
                }
            }
        }

        private static void PrintResult(Node<int> item)
        {
            var list = new List<int>();
            while (item != null)
            {
                list.Add(item.Value);
                item = item.PrevNode;
            }
            list.Reverse();
            Console.WriteLine(String.Join(" -> ", list));
        }

        public class Node<T>
        {
            public T Value { get; set; }

            public Node<T> PrevNode { get; set; }

            public Node(T value, Node<T> prevNode = null)
            {
                this.Value = value;
                this.PrevNode = prevNode;
            }
        }
    }

}
