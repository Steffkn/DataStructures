namespace _03.ArrayStack
{
    using System;

    public class Launcher
    {
        static void Main()
        {
            ArrayStack<int> astack = new ArrayStack<int>();

            for (int i = 0; i < 10; i++)
            {
                astack.Push(i);
            }

            Console.WriteLine(astack.Count);
            var arr = astack.ToArray();

            Console.WriteLine(arr.Length);

            for (int i = 0; i < arr.Length; i++)
            {
                astack.Pop();
            }

            Console.WriteLine(astack.Count);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

        }
    }
}
