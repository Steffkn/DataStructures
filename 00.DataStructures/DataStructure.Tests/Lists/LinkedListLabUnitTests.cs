using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LinkedListLabUnitTests
{
    [TestMethod]
    public void AddFirst_ShouldIncreaseCount()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.AddFirst(1);

        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void AddLast_ShouldIncreaseCount()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.AddLast(1);

        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void AddFirst_ShouldAddCorrectElement()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.AddFirst(1);

        foreach (int item in list)
        {
            Assert.AreEqual(1, item);
        }
    }

    [TestMethod]
    public void AddLast_ShouldAddCorrectElement()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.AddLast(1);

        foreach (int item in list)
        {
            Assert.AreEqual(1, item);
        }
    }

    [TestMethod]
    public void AddFirst_MultipleElements_ShouldAddCorrectElements()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        for (int i = 0; i < 100; i++)
        {
            list.AddFirst(i);
        }

        int expected = 99;
        foreach (int item in list)
        {
            Assert.AreEqual(expected--, item);
        }
    }

    [TestMethod]
    public void AddLast_MultipleElements_ShouldAddCorrectElements()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        for (int i = 0; i < 100; i++)
        {
            list.AddLast(i);
        }

        int expected = 0;
        foreach (int item in list)
        {
            Assert.AreEqual(expected++, item);
        }
    }

    [TestMethod]
    public void RemoveFirst_SingleElement_ShouldDecreaseCount()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.AddFirst(1);
        list.AddFirst(2);
        list.RemoveFirst();

        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void RemoveLast_SingleElement_ShouldDecreaseCount()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.AddFirst(1);
        list.AddFirst(2);
        list.RemoveLast();

        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void RemoveFirst_MultipleElements_ShouldRemoveCorrectly()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        for (int i = 0; i < 100; i++)
        {
            list.AddLast(i);
        }

        for (int i = 0; i < 100; i++)
        {
            Assert.AreEqual(i, list.RemoveFirst());
        }

        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void RemoveLast_MultipleElements_ShouldRemoveCorrectly()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        for (int i = 0; i < 100; i++)
        {
            list.AddFirst(i);
        }

        for (int i = 0; i < 100; i++)
        {
            Assert.AreEqual(i, list.RemoveLast());
        }

        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void RemoveFirst_OnEmptyList_ShouldThrow()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.RemoveFirst();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void RemoveLast_OnEmptyList_ShouldThrow()
    {
        LinkedListLab<int> list = new LinkedListLab<int>();

        list.RemoveLast();
    }
}
