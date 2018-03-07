using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class TextEditor : ITextEditor
{
    private Trie<BigList<char>> usersStrings;

    private Trie<Stack<BigList<char>>> usersStack;

    public TextEditor()
    {
        this.usersStrings = new Trie<BigList<char>>();
        this.usersStack = new Trie<Stack<BigList<char>>>();
    }

    public void Clear(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }
        this.usersStack.GetValue(username).Push(new BigList<char>(this.usersStrings.GetValue(username)));
        this.usersStrings.Insert(username, new BigList<char>());
    }

    public void Delete(string username, int startIndex, int length)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        this.usersStack.GetValue(username).Push(new BigList<char>(this.usersStrings.GetValue(username)));

        this.usersStrings.GetValue(username).RemoveRange(startIndex, length);
    }

    public void Insert(string username, int index, string text)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        this.usersStack.GetValue(username).Push(new BigList<char>(this.usersStrings.GetValue(username)));

        this.usersStrings.GetValue(username).InsertRange(index, text);
    }

    public int Length(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return 0;
        }

        return this.usersStrings.GetValue(username).Count();
    }

    public void Login(string username)
    {
        this.usersStrings.Insert(username, new BigList<char>());
        this.usersStack.Insert(username, new Stack<BigList<char>>());
    }

    public void Logout(string username)
    {
        this.usersStrings.Delete(username);
        this.usersStack.Delete(username);
    }

    public void Prepend(string username, string text)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }
        this.usersStack.GetValue(username).Push(this.PrintBigList(username));
        this.usersStrings.GetValue(username).AddRangeToFront(text);
    }

    public string Print(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return null;
        }

        return string.Join("", this.PrintBigList(username));
    }

    private BigList<char> PrintBigList(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return new BigList<char>();
        }

        var list = this.usersStrings.GetValue(username);

        return new BigList<char>(list);
    }

    public void Substring(string username, int startIndex, int length)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var userString = this.usersStrings.GetValue(username);

        var userStack = this.usersStack.GetValue(username);

        userStack.Push(new BigList<char>(userString));

        var newString = new BigList<char>();

        int endIndex = startIndex + length;
        startIndex = Math.Max(0, startIndex);
        endIndex = Math.Min(userString.Count, endIndex);

        for (int i = startIndex; i < endIndex; i++)
        {
            newString.Add(userString[i]);
        }

        this.usersStrings.Insert(username, newString);
    }

    public void Undo(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }
        Environment.Exit(0);
        var userStack = this.usersStack.GetValue(username);

        if (userStack.Count == 0) return;

        var lastString = userStack.Pop();

        this.usersStrings.Insert(username, new BigList<char>(lastString));
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        Queue<string> queue = new Queue<string>();

        foreach (var user in this.usersStrings.GetByPrefix(prefix))
        {
            queue.Enqueue(user);
        }

        return queue;
    }
}