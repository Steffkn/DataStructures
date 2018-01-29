using System;

namespace _04.LinkedStack
{
    public class LinkedStack<T>
    {
        private Node<T> firstNode;

        public int Count { get; private set; }

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var result = this.firstNode.value;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return result;
        }

        public T[] ToArray()
        {
            T[] tempArray = new T[this.Count];
            var current = this.firstNode;
            int i = 0;
            while (current != null)
            {
                tempArray[i] = current.value;
                current = current.NextNode;
                i++;
            }

            return tempArray;
        }

        private class Node<T>
        {
            public T value;

            public Node<T> NextNode { get; set; }

            public Node(T value, Node<T> nextNode = null)
            {
                this.value = value;
                this.NextNode = nextNode;
            }
        }
    }
}
