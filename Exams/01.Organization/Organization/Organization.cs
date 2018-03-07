using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organization : IOrganization
{
    private List<Person> people;
    private HashSet<Person> mryshlqci;
    private HashSet<string> mryshlqciName;

    public Organization()
    {
        people = new List<Person>();
        mryshlqci = new HashSet<Person>();
        mryshlqciName = new HashSet<string>();
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var person in people)
        {
            yield return person;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count => people.Count;

    public bool Contains(Person person)
    {
        return mryshlqci.Contains(person);
    }

    public bool ContainsByName(string name)
    {
        return mryshlqciName.Contains(name);
    }

    public void Add(Person person)
    {
        people.Add(person);
        this.mryshlqci.Add(person);
        this.mryshlqciName.Add(person.Name);
    }

    public Person GetAtIndex(int index)
    {
        if (index < this.Count && index >= 0)
        {
            return people[index];
        }

        throw new IndexOutOfRangeException();
    }

    public IEnumerable<Person> GetByName(string name)
    {
        return this.people.Where(x => x.Name == name);
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        return this.people.Take(count);
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        return this.people.Where(x => x.Name.Length >= minLength && x.Name.Length <= maxLength);
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        var result = this.people.Where(x => x.Name.Length == length); ;
        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.people;
    }
}