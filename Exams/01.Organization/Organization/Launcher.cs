using System;

public class Launcher
{
    public static void Main()
    {
        var org = new Organization();
        var person = new Person("aaa", 213);
        org.Add(person);
        Console.WriteLine(org.Count);
        org.Add(person);
        Console.WriteLine(org.Count);
        org.Contains(person);
        org.ContainsByName(person.Name);
        org.GetAtIndex(0);
        org.GetWithNameSize(3);
        org.GetByName("aaa");
        org.FirstByInsertOrder();
        org.SearchWithNameSize(2,3);
        org.PeopleByInsertOrder();
        
    }
}
