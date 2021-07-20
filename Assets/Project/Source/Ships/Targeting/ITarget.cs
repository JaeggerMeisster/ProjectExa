using UnityEngine;

namespace Exa.Ships.Targeting {
    public interface ITarget {
        /// <summary>
        ///     Gets the world position of the target (not the delta)
        /// </summary>
        /// <param name="current">Current position</param>
        Vector2 GetPosition(Vector2 current);
    }

    public interface IWeaponTarget : ITarget {
        bool GetTargetValid();
    }
}