using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    Dictionary<int, Transaction> transactions;

    public Chainblock()
    {
        this.transactions = new Dictionary<int, Transaction>();
    }

    public int Count => this.transactions.Count;

    public void Add(Transaction tx)
    {
        this.transactions.Add(tx.Id, tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!this.transactions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.transactions[id].Status = newStatus;
    }

    public bool Contains(Transaction tx)
    {
        return this.transactions.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return this.transactions.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        var result = this.transactions.Values.Where(x => x.Amount >= lo && x.Amount <= hi);

        if (!result.Any())
        {
            return Enumerable.Empty<Transaction>();
        }

        return result;
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return this.transactions.Values.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        var result = this.transactions.Values.Where(x => x.Status == status).OrderByDescending(x => x.Amount);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.Select(x => x.To);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        var result = this.transactions.Values.Where(x => x.Status == status).OrderByDescending(x => x.Amount);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.Select(x => x.From);
    }

    public Transaction GetById(int id)
    {
        if (!this.transactions.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.transactions[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = this.transactions.Values
            .Where(x => x.To.Equals(receiver) && x.Amount >= lo && x.Amount < hi)
            .OrderByDescending(x => x.Amount).ThenBy(x => x.Id);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.transactions.Values.Where(x => x.To.Equals(receiver));

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.transactions.Values.Where(x => x.From.Equals(sender) && x.Amount > amount);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.transactions.Values.Where(x => x.From.Equals(sender));

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        var result = this.transactions.Values
            .Where(x => x.Status.Equals(status));

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        var result = this.transactions.Values
            .Where(x => x.Status.Equals(status) && x.Amount <= amount);
        if (!result.Any())
        {
            return Enumerable.Empty<Transaction>();
        }

        return result.OrderByDescending(x => x.Amount);
    }

    public void RemoveTransactionById(int id)
    {
        if (!this.transactions.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        this.transactions.Remove(id);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var item in this.transactions)
        {
            yield return item.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

