using UnityEngine;

namespace Exa.Debugging {
    public interface IDebugDragable {
        Vector2 GetDebugDraggerPosition();

        void SetDebugDraggerGlobals(Vector2 position, Vector2 velocity);

        void Rotate(float degrees);
    }
}