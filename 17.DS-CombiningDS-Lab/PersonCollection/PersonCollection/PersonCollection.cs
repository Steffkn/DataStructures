using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> peopleByEmail;
    private Dictionary<string, SortedSet<Person>> peopleByEmailDomain;
    private Dictionary<string, SortedSet<Person>> peopleByNameAndTown;
    private OrderedDictionary<int, SortedSet<Person>> peopleByAge;
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> peopleByTownAndAge;

    public PersonCollection()
    {
        this.peopleByEmail = new Dictionary<string, Person>();
        this.peopleByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        this.peopleByNameAndTown = new Dictionary<string, SortedSet<Person>>();
        this.peopleByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.peopleByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }
        var person = new Person()
        {
            Email = email,
            Name = name,
            Age = age,
            Town = town
        };

        this.peopleByEmail.Add(email, person);

        var emailDomain = this.ExtractEmailDomain(email);
        this.peopleByEmailDomain.AppendValueToKey(emailDomain, person);

        var nameAndTown = this.CombineNameAndTown(name, town);
        this.peopleByNameAndTown.AppendValueToKey(nameAndTown, person);

        this.peopleByAge.AppendValueToKey(age, person);

        // add by {town + age}
        this.peopleByTownAndAge.EnsureKeyExists(town);
        this.peopleByTownAndAge[town].AppendValueToKey(age, person);

        return true;
    }

    public int Count => this.peopleByEmail.Count;

    public Person FindPerson(string email)
    {
        Person person;
        var personExists = this.peopleByEmail.TryGetValue(email, out person);
        return person;
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (person == null)
        {
            return false;
        }
        var personDeleted = this.peopleByEmail.Remove(email);

        var emailDomain = this.ExtractEmailDomain(email);
        this.peopleByEmailDomain[emailDomain].Remove(person);

        var nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
        this.peopleByNameAndTown[nameAndTown].Remove(person);

        this.peopleByAge[person.Age].Remove(person);

        this.peopleByTownAndAge[person.Town][person.Age].Remove(person);
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.peopleByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        string nameAndTown = this.CombineNameAndTown(name, town);
        return this.peopleByNameAndTown.GetValuesForKey(nameAndTown);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var peopleInRange = this.peopleByAge.Range(startAge, true, endAge, true);
        foreach (var personByAge in peopleInRange)
        {
            foreach (var person in personByAge.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.peopleByTownAndAge.ContainsKey(town))
        {
            yield break;
        }

        var peopleInRange = this.peopleByTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (var personByAge in peopleInRange)
        {
            foreach (var person in personByAge.Value)
            {
                yield return person;
            }
        }
    }

    private string ExtractEmailDomain(string email)
    {
        return email.Split('@')[1];
    }

    private string CombineNameAndTown(string name, string town)
    {
        return $"{name}|!|{town}";
    }
}
