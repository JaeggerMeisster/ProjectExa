using System.Collections.Generic;
using UnityEngine;

namespace Exa.Types.Binding {
    /// <summary>
    ///     Controls views for a collection of models
    ///     <para>
    ///         Handles instantiating views, binding model observables to their corresponding views, and deleting views
    ///     </para>
    /// </summary>
    /// <typeparam name="TView">View type, must be an observer of <see cref="{TModel}" /></typeparam>
    /// <typeparam name="TContainer">Model observable type</typeparam>
    /// <typeparam name="TModel">Model type</typeparam>
    public class ViewBinder<TView, TContainer, TModel> : AbstractCollectionObserver<TContainer>
        where TView : MonoBehaviour, IObserver<TModel>
        where TContainer : Observable<TModel>
        where TModel : class {
        [SerializeField] protected Transform viewContainer;
        [SerializeField] protected GameObject viewPrefab;

        protected Dictionary<TContainer, TView> views = new Dictionary<TContainer, TView>();

        /// <summary>
        ///     Add observable
        /// </summary>
        /// <param name="value"></param>
        public override void OnAdd(TContainer value) {
            CreateView(value, viewContainer);
        }

        /// <summary>
        ///     Add Observable with specified view container
        /// </summary>
        /// <param name="value"></param>
        /// <param name="container"></param>
        protected virtual TView CreateView(TContainer value, Transform container) {
            var blockObject = Instantiate(viewPrefab, container);
            var view = blockObject.GetComponent<TView>();
            view.OnUpdate(value.Data);
            value.Register(view);
            views.Add(value, view);

            return view;
        }

        /// <summary>
        ///     Clear views
        /// </summary>
        public override void OnClear() {
            foreach (var key in views.Keys) {
                OnRemove(key);
            }

            views = new Dictionary<TContainer, TView>();
        }

        /// <summary>
        ///     Remove view
        /// </summary>
        /// <param name="value"></param>
        public override void OnRemove(TContainer value) {
            var view = views[value];
            value.Unregister(view);
            Destroy(view.gameObject);
        }

        public TView GetView(TContainer container) {
            return views[container];
        }
    }
}