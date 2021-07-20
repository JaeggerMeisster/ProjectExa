using UnityEngine;

namespace Exa.Math {
    public static class QuaternionExtensions {
        public static Quaternion Multiply(this Quaternion quaternion, float scalar) {
            return new Quaternion(
                (float) (quaternion.x * (double) scalar),
                (float) (quaternion.y * (double) scalar),
                (float) (quaternion.z * (double) scalar),
                (float) (quaternion.w * (double) scalar)
            );
        }

        public static Quaternion RequiredRotation(Quaternion from, Quaternion to) {
            var requiredRotation = to * Quaternion.Inverse(from);

            // Flip the sign if w is negative.
            // This makes sure we always rotate the shortest angle to match the desired rotation.
            if (requiredRotation.w < 0.0f) {
                requiredRotation.x *= -1.0f;
                requiredRotation.y *= -1.0f;
                requiredRotation.z *= -1.0f;
                requiredRotation.w *= -1.0f;
            }

            return requiredRotation;
        }

        public static Quaternion Subtract(this Quaternion lhs, Quaternion rhs) {
            return new Quaternion(
                (float) (lhs.x - (double) rhs.x),
                (float) (lhs.y - (double) rhs.y),
                (float) (lhs.z - (double) rhs.z),
                (float) (lhs.w - (double) rhs.w)
            );
        }
    }
}