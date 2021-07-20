using System;
using System.Collections;
using System.Collections.Generic;

namespace Exa.Types.Binding {
    /// <summary>
    ///     Base class for virtual collection that notifies observers of model changes
    ///     <para>
    ///         This is preferable to just an observable<List<TData>> because it only needs to refresh views that are changed
    ///     </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ObservableCollection<T> : IObservableEnumerable<T>, ICollection<T> {
        private List<T> collection;

        public ObservableCollection() {
            collection = new List<T>();
        }

        public ObservableCollection(List<T> collection) {
            this.collection = collection;
        }

        public int Count {
            get => collection.Count;
        }

        public bool IsReadOnly {
            get => false;
        }

        public virtual void Add(T item) {
            collection.Add(item);

            foreach (var observer in Observers) {
                observer.OnAdd(item);
            }
        }

        public virtual void Clear() {
            collection.Clear();

            foreach (var observer in Observers)
            foreach (var item in this) {
                observer.OnRemove(item);
            }
        }

        public virtual bool Contains(T item) {
            return collection.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex) {
            collection.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(T item) {
            var removed = collection.Remove(item);

            if (removed) {
                foreach (var observer in Observers) {
                    observer.OnRemove(item);
                }
            }

            return removed;
        }

        public List<ICollectionObserver<T>> Observers { get; } = new List<ICollectionObserver<T>>();

        public virtual IEnumerator<T> GetEnumerator() {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return collection.GetEnumerator();
        }

        /// <summary>
        ///     Add an observer to the observer collection
        /// </summary>
        /// <param name="observer"></param>
        public virtual void Register(ICollectionObserver<T> observer) {
            if (Observers.Contains(observer)) {
                return;
            }

            Observers.Add(observer);
        }

        /// <summary>
        ///     Remove an observer from the observer collection
        /// </summary>
        /// <param name="observer"></param>
        public virtual void Unregister(ICollectionObserver<T> observer) {
            if (!Observers.Contains(observer)) {
                return;
            }

            Observers.Remove(observer);
        }
    }
}