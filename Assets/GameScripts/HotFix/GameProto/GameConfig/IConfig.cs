namespace GameConfig
{
    public interface IConfig<TKey, TValue>
    {
        TValue GetOrDefault(TKey key);
        TValue Get(TKey key);
        TValue this[TKey key] { get; }
    }
}