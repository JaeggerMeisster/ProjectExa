namespace Exa.Types.Binding {
    /// <summary>
    ///     Interface to support sending push-based notification of changes in a collection
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public interface ICollectionObserver<T> {
        void OnAdd(T value);

        void OnRemove(T value);
    }
}