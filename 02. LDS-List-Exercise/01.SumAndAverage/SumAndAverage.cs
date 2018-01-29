namespace _01.SumAndAverage
{
    using System;
    using System.Linq;

    public class SumAndAverage
    {
        static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var sum = numbers.Sum();
            var avg = numbers.Average();

            Console.WriteLine("Sum={0}; Average={1:f2}", sum, avg);
        }
    }
}
