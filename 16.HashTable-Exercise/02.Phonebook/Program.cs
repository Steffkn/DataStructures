using System;

class Program
{
    static void Main()
    {
        var phoneNumbers = new HashTable<string, string>();

        string input = string.Empty;
        while ((input = Console.ReadLine()) != "search")
        {
            var phoneArgs = input.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (!phoneNumbers.ContainsKey(phoneArgs[0]))
            {
                phoneNumbers[phoneArgs[0]] = phoneArgs[1];
            }
        }

        while ((input = Console.ReadLine()) != "end")
        {
            if (!phoneNumbers.ContainsKey(input))
            {
                Console.WriteLine($"Contact {input} does not exist.");
            }
            else
            {
                Console.WriteLine($"{input} -> {phoneNumbers[input]}");
            }
        }
    }
}
