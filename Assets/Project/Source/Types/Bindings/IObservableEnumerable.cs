using System.Collections.Generic;

namespace Exa.Types.Binding {
    /// <summary>
    ///     Provides
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObservableEnumerable<T> : IEnumerable<T> {
        List<ICollectionObserver<T>> Observers { get; }
    }
}