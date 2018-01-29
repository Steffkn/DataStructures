namespace _02.CalculateSequence
{
    using System;
    using System.Collections.Generic;

    public class CalculateSequence
    {
        static void Main()
        {
            int inputN = int.Parse(Console.ReadLine());
            var numbersQueue = new Queue<int>();
            numbersQueue.Enqueue(inputN);

            var list = new List<int>();

            while (list.Count + numbersQueue.Count < 50)
            {
                int currentNumber = numbersQueue.Dequeue();
                numbersQueue.Enqueue(currentNumber + 1);
                numbersQueue.Enqueue(2 * currentNumber + 1);
                numbersQueue.Enqueue(currentNumber + 2);
                list.Add(currentNumber);
            }

            while (list.Count != 50)
            {
                list.Add(numbersQueue.Dequeue());
            }

            numbersQueue.Clear();

            Console.WriteLine(String.Join(", ", list));
        }
    }
}
