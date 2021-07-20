using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Ships.Navigation {
    public interface IThrustVectors {
        void Register(ThrusterBehaviour thruster);

        void Unregister(ThrusterBehaviour thruster);

        void SetGraphics(Vector2 directionScalar);
    }
}