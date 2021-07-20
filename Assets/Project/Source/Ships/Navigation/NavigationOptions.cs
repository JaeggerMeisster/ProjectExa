using System;
using Exa.Data;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Ships.Navigation {
    public enum NavigationType {
        Simple,
        Directional,
        NewDirectional
    }

    public class NavigationOptions : MonoBehaviour {
        [Header("Options")]
        public NavigationType navigationType;

        public INavigation GetNavigation(GridInstance gridInstance, Blueprint blueprint) {
            return navigationType switch {
                NavigationType.Simple => new SimpleNavigation(gridInstance, this, new Scalar(1)),
                NavigationType.Directional => new DirectionalNavigation(gridInstance, this, new Scalar(1)),
                _ => throw new ArgumentOutOfRangeException(nameof(navigationType))
            };
        }
    }
}