using System;
using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

[Flags]
public enum AnimationMode {
    Height = 1 << 0,
    Width = 1 << 1,
    Both = Height | Width
}

public class ElementTracker : MonoBehaviour {
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private AnimationMode animationMode = AnimationMode.Both;

    private Tween currentAnimation;

    private Vector2 targetSize {
        get => targetRect.rect.size;
    }

    public void TrackOnce() {
        TrackOnce(true);
    }

    public void TrackOnce(bool animate) {
        (animate ? (Action<Vector2>) Animate : SetSize)(targetSize);
    }

    public void Hide() {
        Animate(Vector2.zero);
    }

    private void SetSize(Vector2 size) {
        currentAnimation?.Kill();

        switch (animationMode) {
            case AnimationMode.Both:
                layoutElement.SetPreferredSize(size);

                break;
            case AnimationMode.Height:
                layoutElement.preferredHeight = size.x;

                break;
            case AnimationMode.Width:
                layoutElement.preferredWidth = size.y;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Animate(Vector2 size) {
        SelectTween(size).Replace(ref currentAnimation);
    }

    private Tween SelectTween(Vector2 size) {
        return animationMode switch {
            AnimationMode.Both => layoutElement.DOPreferredSize(size, duration),
            AnimationMode.Height => layoutElement.DOPreferredHeight(size.y, duration),
            AnimationMode.Width => layoutElement.DOPreferredWidth(size.x, duration),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}