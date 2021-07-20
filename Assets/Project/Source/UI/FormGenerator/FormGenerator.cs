using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI {
    public class FormGenerator : MonoBehaviour {
        [SerializeField] private Transform controlsContainer;

        public void GenerateForm<T>(ModelDescriptor<T> modelDescriptor) {
            controlsContainer.DestroyChildren();
            modelDescriptor.GenerateView(controlsContainer);
        }
    }
}