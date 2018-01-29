namespace _05.CountOfOccurrences
{
    using System;
    using System.Linq;

    public class CountOfOccurrences
    {
        public static void Main()
        {
            var numbersList = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var distinctNumbersList = numbersList.Distinct();

            foreach (int number in distinctNumbersList)
            {
                int count = numbersList.Count(n => n == number);

                Console.WriteLine("{0} -> {1} times", number, count);
            }
        }
    }
}
