using System;
using System.Collections;
using System.Collections.Generic;

public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
{
    private Dictionary<K, V> collection;
    private LinkedList<K> linkedCollection;
    private readonly int _capacity;

    public LimitedMemoryCollection(int capacity)
    {
        this.collection = new Dictionary<K, V>(capacity);
        this.linkedCollection = new LinkedList<K>();
        this._capacity = capacity;
    }

    public int Capacity => this._capacity;

    public int Count => this.collection.Count;

    public V Get(K key)
    {
        if (!collection.ContainsKey(key))
        {
            throw new ArgumentException();
        }

        var result = collection[key];
        linkedCollection.Remove(key);
        linkedCollection.AddLast(key);

        return result;
    }


    public void Set(K key, V value)
    {
        if (collection.ContainsKey(key))
        {
            if (collection.Count == this.Capacity)
            {
                var oldest = linkedCollection.First;
                linkedCollection.RemoveFirst();
                collection.Remove(oldest.Value);
                collection[key] = value;
                linkedCollection.AddLast(key);
            }
            else
            {
                collection[key] = value;
                linkedCollection.Remove(key);
                linkedCollection.AddLast(key);
            }
        }
        else
        {
            if (collection.Count == this.Capacity)
            {
                var oldest = linkedCollection.First;
                linkedCollection.RemoveFirst();
                collection.Remove(oldest.Value);
                collection.Add(key, value);
                linkedCollection.AddLast(key);
            }
            else
            {
                collection.Add(key, value);
                linkedCollection.AddLast(key);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        var item = linkedCollection.First;
        while (item != null)
        {
            yield return new KeyValuePair<K, V>(item.Value, collection[item.Value]);
            item = item.Next;
        }
    }
}
