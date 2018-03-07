using System.Collections.Generic;

public interface ILimitedMemoryCollection<K, V> : IEnumerable<KeyValuePair<K, V>>
{
    int Capacity { get; }

    int Count { get; }

    void Set(K key, V value);

    V Get(K key);
}
