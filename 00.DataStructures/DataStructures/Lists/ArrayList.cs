using System;

public class ArrayList<T>
{
    private T[] array;

    public ArrayList(int count = 2)
    {
        this.array = new T[count];
        this.Count = 0;
    }

    public int Count { get; set; }

    public T this[int index]
    {
        get
        {
            if (index >= 0 && index < array.Length)
            {
                return this.array[index];
            }

            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        set
        {
            if (index >= 0 && index < array.Length)
            {
                this.array[index] = value;
            }

            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void Add(T item)
    {
        if (this.array.Length > this.Count)
        {
            this.array[this.Count] = item;
        }
        else
        {
            var tempArray = new T[this.array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }
            this.array = tempArray;
            this.array[this.Count] = item;
        }

        this.Count++;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= array.Length)
        {
            throw new ArgumentOutOfRangeException();
        }

        T result = default(T);

        var tempArray = this.Count < (this.array.Length / 4) ? 
            new T[this.array.Length / 2] : 
            new T[this.array.Length];

        for (int i = 0, j = 0; i < array.Length; i++, j++)
        {
            if (i == index)
            {
                j--;
                this.Count--;
                result = array[i];
            }
            else
            {
                tempArray[j] = array[i];
            }
        }

        this.array = tempArray;

        return result;
    }
}
