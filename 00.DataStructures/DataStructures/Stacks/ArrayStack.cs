using System;

namespace _03.ArrayStack
{
    public class ArrayStack<T>
    {
        private T[] elements;
        private int count;

        public int Count
        {
            get
            {
                return this.count;
            }

            private set
            {
                if (value < 0 || value > this.elements.Length)
                {
                    throw new IndexOutOfRangeException("Count property cannot be negative or bigger than array size!");
                }

                this.count = value;
            }
        }

        private const int InitialCapacity = 16;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
        }

        public void Push(T element)
        {
            if (this.Count == this.elements.Length - 1)
            {
                this.Grow();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Poping from empty stack!");
            }

            if (this.elements.Length > this.Count * 2)
            {
                this.Trim();
            }

            this.Count--;
            var result = this.elements[this.Count];
            this.elements[this.Count] = default(T);
            return result;
        }

        public T[] ToArray()
        {
            T[] tempArray = new T[this.Count];

            for (int i = this.Count - 1, j = 0; i >= 0; i--, j++)
            {
                tempArray[j] = this.elements[i];
            }

            return tempArray;
        }

        private void Grow()
        {
            var tempArray = new T[this.elements.Length * 2];

            for (int i = 0; i < this.Count; i++)
            {
                tempArray[i] = this.elements[i];
            }

            this.elements = tempArray;
        }

        private void Trim()
        {
            var tempArray = new T[this.elements.Length / 2];

            for (int i = 0; i < this.Count; i++)
            {
                tempArray[i] = this.elements[i];
            }

            this.elements = tempArray;
        }
    }
}
