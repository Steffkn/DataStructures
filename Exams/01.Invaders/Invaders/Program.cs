using System;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {
        Computer computer = new Computer(100);

        for (int i = 0; i < 10; i++)
        {
            var invader = new Invader(10, 10 + i);
            computer.AddInvader(invader);
        }

        Console.WriteLine("{0} {1} {2}", 100, computer.Energy, "Wrong energy");
        Console.WriteLine("{0} {1} {2}", 10, computer.Invaders().Count(), "Wrong count");

        computer.Skip(5);

        Console.WriteLine("{0} {1} {2}", 100, computer.Energy, "Wrong energy");
        Console.WriteLine("{0} {1} {2}", 10, computer.Invaders().Count(), "Wrong count");

        computer.Skip(5);

        Console.WriteLine("{0} {1} {2}", 90, computer.Energy, "Wrong energy");
        Console.WriteLine("{0} {1} {2}", 9, computer.Invaders().Count(), "Wrong count");

        computer.Skip(5);

        Console.WriteLine("{0} {1} {2}", 40, computer.Energy, "Wrong energy");
        Console.WriteLine("{0} {1} {2}", 4, computer.Invaders().Count(), "Wrong count");

        computer.Skip(5);

        Console.WriteLine("{0} {1} {2}", 0, computer.Energy, "Wrong energy");
        Console.WriteLine("{0} {1} {2}", 0, computer.Invaders().Count(), "Wrong count");
    }
}
