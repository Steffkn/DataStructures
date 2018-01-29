namespace _01.ReverseNumbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseNumbers
    {
        static void Main()
        {
            var inputList = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            Stack<int> numbersStack = new Stack<int>(inputList);

            Console.WriteLine(String.Join(" ", numbersStack));
        }
    }
}
