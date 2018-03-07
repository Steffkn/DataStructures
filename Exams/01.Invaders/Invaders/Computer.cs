using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    private int energy;
    public HashSet<Invader> invaders;

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }

        this.Energy = energy;
        this.invaders = new HashSet<Invader>();
    }

    public int Energy
    {
        private set
        {
            if (value < 0)
            {
                this.energy = 0;
            }

            this.energy = value;
        }
        get
        {
            if (this.energy < 0)
            {
                return 0;
            }

            return this.energy;
        }
    }

    public void Skip(int turns)
    {
        foreach (var invader in invaders)
        {
            invader.Distance -= turns;
        }

        foreach (var invader in this.invaders.Where(t => t.Distance <= 0))
        {
            this.Energy -= invader.Damage;
        }

        this.invaders.RemoveWhere(i => i.Distance <= 0);
    }

    public void AddInvader(Invader invader)
    {
        invaders.Add(invader);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        foreach (var invader in this.invaders.OrderBy(i => i).Take(count))
        {
            this.invaders.Remove(invader);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        this.invaders.RemoveWhere(i => i.Distance <= radius);
    }

    public IEnumerable<Invader> Invaders()
    {
        foreach (var invader in invaders)
        {
            yield return invader;
        }
    }
}
