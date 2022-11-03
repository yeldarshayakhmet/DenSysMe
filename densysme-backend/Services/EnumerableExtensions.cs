namespace Services;

public static class EnumerableExtensions
{
    public static void AddElement<TKey, T>(this IDictionary<TKey, IEnumerable<T>> source, TKey key, T element)
    {
        if (source.ContainsKey(key))
        {
            if (source[key] is List<T>)
                ((List<T>)source[key]).Add(element);
            else
                source[key] = source[key].Append(element);
        }
        else
            source[key] = new List<T> { element };
    }
}