using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enterprise : IEnterprise
{
    private Dictionary<Guid, Employee> employeeDict = new Dictionary<Guid, Employee>();
    private DateTime date = DateTime.Now;

    public int Count => this.employeeDict.Count;

    public void Add(Employee employee)
    {
        if (!employeeDict.ContainsKey(employee.Id))
        {
            employeeDict.Add(employee.Id, employee);
        }
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        return employeeDict.Values.Where(x => x.Position == position && x.Salary >= minSalary);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!this.employeeDict.ContainsKey(guid))
        {
            return false;
        }

        this.employeeDict[guid].FirstName = employee.FirstName;
        this.employeeDict[guid].LastName = employee.LastName;
        this.employeeDict[guid].Salary = employee.Salary;
        this.employeeDict[guid].HireDate = employee.HireDate;
        this.employeeDict[guid].Position = employee.Position;
        return true;
    }

    public bool Contains(Guid guid)
    {
        return this.employeeDict.ContainsKey(guid);
    }

    public bool Contains(Employee employee)
    {
        return this.employeeDict.ContainsKey(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!employeeDict.ContainsKey(guid))
        {
            return false;
        }

        return this.employeeDict.Remove(guid);
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!employeeDict.ContainsKey(guid))
        {
            throw new ArgumentException();
        }

        return this.employeeDict[guid];
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!employeeDict.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        return this.employeeDict[guid].Position;
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        var result = this.employeeDict.Values.Where(x => x.Position.Equals(position));
        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var result = this.employeeDict.Values.Where(x => x.Salary >= minSalary);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        var result = this.employeeDict.Values.Where(x => x.Position == position && x.Salary.Equals(salary));
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var result = false;
        foreach (var keyValuePair in this.employeeDict)
        {
            if (keyValuePair.Value.HireDate.AddMonths(months) <= date)
            {
                keyValuePair.Value.Salary = keyValuePair.Value.Salary * ((percent / 100.0) + 1);
                result = true;
            }
        }

        return result;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        return this.employeeDict.Values.Where(x => x.FirstName == firstName);
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        return this.employeeDict.Values.Where(x => x.FirstName == firstName && x.LastName == lastName && x.Position == position);
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        return this.employeeDict.Values.Where(x => positions.Any(p => p == x.Position));
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        return this.employeeDict.Values.Where(x => x.Salary >= minSalary && x.Salary <= maxSalary);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var employee in employeeDict)
        {
            yield return employee.Value;
        }
    }
}

