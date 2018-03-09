using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AVLLABTests
{
    [TestMethod]
    public void TraverseInOrder_AfterSingleInsert()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(1);

        // Act
        List<int> nodes = new List<int>();
        AVLLab.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void TraverseInOrder_AfterMultipleInserts()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(2);
        AVLLab.Insert(1);
        AVLLab.Insert(3);

        // Act
        List<int> nodes = new List<int>();
        AVLLab.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1, 2, 3 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Contains_ExistingElement_ShouldReturnTrue()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(2);
        AVLLab.Insert(1);
        AVLLab.Insert(3);

        // Act
        // Assert
        Assert.IsTrue(AVLLab.Contains(1));
        Assert.IsTrue(AVLLab.Contains(2));
        Assert.IsTrue(AVLLab.Contains(3));
    }

    [TestMethod]
    public void Contains_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(2);
        AVLLab.Insert(1);
        AVLLab.Insert(3);

        // Act
        bool contains = AVLLab.Contains(5);

        // Assert
        Assert.IsFalse(contains);
    }

    [TestMethod]
    public void Height_RootRight()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(1);
        AVLLab.Insert(2);

        // Act
        // Assert
        Assert.AreEqual(2, AVLLab.Root.Height);
        Assert.AreEqual(1, AVLLab.Root.Right.Height);
    }

    [TestMethod]
    public void Height_RootLeft()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(2);
        AVLLab.Insert(1);

        // Act
        // Assert
        Assert.AreEqual(2, AVLLab.Root.Height);
        Assert.AreEqual(1, AVLLab.Root.Left.Height);
    }

    [TestMethod]
    public void Height_RootLeftRight()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(2);
        AVLLab.Insert(1);
        AVLLab.Insert(3);

        // Act
        // Assert
        Assert.AreEqual(2, AVLLab.Root.Height);
        Assert.AreEqual(1, AVLLab.Root.Left.Height);
        Assert.AreEqual(1, AVLLab.Root.Right.Height);
    }

    [TestMethod]
    public void Rebalance_RootShouldHaveHeightTwo()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        AVLLab.Insert(1);
        AVLLab.Insert(2);
        AVLLab.Insert(3);

        // Assert
        Assert.AreEqual(2, AVLLab.Root.Height);
    }

    [TestMethod]
    public void Rebalance_TestHeightOneNodes()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        for (int i = 1; i < 10; i++)
        {
            AVLLab.Insert(i);
        }

        // Assert
        Assert.AreEqual(1, AVLLab.Root.Left.Left.Height); // 1
        Assert.AreEqual(1, AVLLab.Root.Left.Right.Height); // 3
        Assert.AreEqual(1, AVLLab.Root.Right.Left.Height); // 5
        Assert.AreEqual(1, AVLLab.Root.Right.Right.Left.Height); // 7
        Assert.AreEqual(1, AVLLab.Root.Right.Right.Right.Height); // 9
    }

    [TestMethod]
    public void Rebalance_TestHeightTwoNodes()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        for (int i = 1; i < 10; i++)
        {
            AVLLab.Insert(i);
        }

        // Assert
        Assert.AreEqual(2, AVLLab.Root.Left.Height); // 2
        Assert.AreEqual(2, AVLLab.Root.Right.Right.Height); // 8
    }

    [TestMethod]
    public void Rebalance_TestHeightThreeNodes()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        for (int i = 1; i < 10; i++)
        {
            AVLLab.Insert(i);
        }

        // Assert
        Assert.AreEqual(3, AVLLab.Root.Right.Height); // 6
    }

    [TestMethod]
    public void Rebalance_TestHeightFourNodes()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();
        for (int i = 1; i < 10; i++)
        {
            AVLLab.Insert(i);
        }

        // Assert
        Assert.AreEqual(4, AVLLab.Root.Height); // 4
    }

    [TestMethod]
    public void Rebalance_SingleRight()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();

        // Act
        AVLLab.Insert(3);
        AVLLab.Insert(2);
        AVLLab.Insert(1);

        // Assert
        Assert.AreEqual(2, AVLLab.Root.Value);
    }

    [TestMethod]
    public void Rebalance_SingleLeft()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();

        // Act
        AVLLab.Insert(1);
        AVLLab.Insert(2);
        AVLLab.Insert(3);

        // Assert
        Assert.AreEqual(2, AVLLab.Root.Value);
    }

    [TestMethod]
    public void Rebalance_DoubleRight()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();

        // Act
        AVLLab.Insert(5);
        AVLLab.Insert(2);
        AVLLab.Insert(4);

        // Assert
        Assert.AreEqual(4, AVLLab.Root.Value);
        Assert.AreEqual(2, AVLLab.Root.Height);
        Assert.AreEqual(1, AVLLab.Root.Left.Height);
        Assert.AreEqual(1, AVLLab.Root.Right.Height);
    }

    [TestMethod]
    public void Rebalance_DoubleLeft()
    {
        // Arrange
        AVLLab<int> AVLLab = new AVLLab<int>();

        // Act
        AVLLab.Insert(5);
        AVLLab.Insert(7);
        AVLLab.Insert(6);

        // Assert
        Assert.AreEqual(6, AVLLab.Root.Value);
        Assert.AreEqual(2, AVLLab.Root.Height);
        Assert.AreEqual(1, AVLLab.Root.Left.Height);
        Assert.AreEqual(1, AVLLab.Root.Right.Height);
    }

    [TestMethod, Timeout(400)]
    public void Performance_Insert_Contains()
    {
        var AVLLab = new AVLLab<int>();

        for (int i = 0; i < 100000; i++)
        {
            AVLLab.Insert(i);
        }

        for (int i = 0; i < 100000; i++)
        {
            Assert.IsTrue(AVLLab.Contains(i));
        }
    }
}
