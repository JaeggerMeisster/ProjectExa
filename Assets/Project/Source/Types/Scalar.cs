using System;
using UnityEngine;

namespace Exa.Data {
    [Serializable]
    public struct Scalar : IFormattable {
        [SerializeField] private float scalar;

        public Scalar(float value) {
            if (value < 0f || value > 1f) {
                throw new ArgumentOutOfRangeException(nameof(value), $"value {value} must be between 0 and 1");
            }
            
            scalar = value;
        }

        public string ToString(string format, IFormatProvider formatProvider) {
            return scalar.ToString(format, formatProvider);
        }

        public float GetValue(float real) {
            return real * scalar;
        }

        public Vector2 GetValue(Vector2 real) {
            return real * scalar;
        }

        public static implicit operator Scalar(float a) {
            return new Scalar(a);
        }

        public static implicit operator float(Scalar a) {
            return a.scalar;
        }

    #region Operators
        public static bool operator >(Scalar a, Scalar b) {
            return a.scalar > b.scalar;
        }

        public static bool operator <(Scalar a, Scalar b) {
            return a.scalar < b.scalar;
        }

        public static Scalar operator -(Scalar a, Scalar b) {
            return new Scalar(a.scalar - b.scalar);
        }

        public static Scalar operator +(Scalar a, Scalar b) {
            return new Scalar(a.scalar + b.scalar);
        }
    #endregion
        
        public override string ToString() {
            return scalar.ToString();
        }
    }
}