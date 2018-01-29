using System.Collections.Generic;

namespace _04.RemoveOddOccurrences
{
    using System;
    using System.Linq;

    public class RemoveOddOccurrences
    {
        public static void Main()
        {
            var numbersList = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            for (int i = 0; i < numbersList.Count;)
            {
                int number = numbersList[i];
                int count = numbersList.Count(n => n == number);

                if (count % 2 != 0)
                {
                    numbersList.RemoveAll(n => n == number);
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            Console.WriteLine(String.Join(" ", numbersList));
        }
    }
}
