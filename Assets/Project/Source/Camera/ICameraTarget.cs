using UnityEngine;

namespace Exa.Camera {
    public interface ICameraTarget {
        float ZoomScale { get; }

        bool TargetValid { get; }

        Vector2 GetWorldPosition();

        float GetCalculatedOrthoSize();

        void OnScroll(float yScroll);
    }
}