namespace _02.SortWords
{
    using System;
    using System.Linq;

    class SortWords
    {
        static void Main()
        {
            var wordsList = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            wordsList.Sort();
            Console.WriteLine(String.Join(" ", wordsList));
        }
    }
}
