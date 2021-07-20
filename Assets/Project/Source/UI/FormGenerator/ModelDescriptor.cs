using UnityEngine;

namespace Exa.UI {
    /// <summary>
    ///     Base class for objects that generate a form view and store form data
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ModelDescriptor<TModel> {
        public abstract TModel FromDescriptor();

        public abstract void GenerateView(Transform container);
    }
}