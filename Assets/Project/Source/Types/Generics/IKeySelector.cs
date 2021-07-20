namespace Exa.Types.Generics {
    /// <summary>
    ///     Supports an object that provides a key that may change
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeySelector<T> {
        T Key { get; }
    }
}