using System;
using System.Linq;

public class Program
{
    static void Main()
    {
        var input = Console.ReadLine();
        HashTable<char, int> table = new HashTable<char, int>();

        for (int i = 0; i < input.Length; i++)
        {
            if (!table.ContainsKey(input[i]))
            {
                table.Add(input[i], 0);
            }

            table[input[i]]++;
        }

        foreach (var ch in table.OrderBy(t => t.Key))
        {
            Console.WriteLine($"{ch.Key}: {ch.Value} time/s");
        }
    }
}
