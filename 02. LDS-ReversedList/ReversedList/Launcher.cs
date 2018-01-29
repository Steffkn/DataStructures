namespace ReversedList
{
    using System;

    public class Launcher
    {
        public static void Main()
        {
            for (int i = 10; i >= 0; i--)
            {
                Console.WriteLine(i);
            }

            var list = new ReversedList<int>();
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);

            Console.WriteLine("Count " + list.Count);
            Console.WriteLine("Capacity" + list.Capacity);

            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            list.RemoveAt(7);

            Console.WriteLine("Count " + list.Count);
            Console.WriteLine("Capacity " + list.Capacity);
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

        }
    }

}
