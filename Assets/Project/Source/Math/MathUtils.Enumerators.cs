using System.Collections.Generic;
using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        /// <summary>
        ///     Enumerate through a range of intergers, if the value is negative the enumeration will go backwards
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static IEnumerable<int> Range(int value) {
            if (value > 0) {
                for (var i = 0; i < value; i++) {
                    yield return i;
                }
            } else {
                for (var i = 0; i > value; i--) {
                    yield return i;
                }
            }
        }

        /// <summary>
        ///     Enumerate through a square of vectors
        /// </summary>
        /// <param name="sizeX">Width</param>
        /// <param name="sizeY">Height</param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> EnumerateVectors(int sizeX, int sizeY) {
            foreach (var x in Range(sizeX))
            foreach (var y in Range(sizeY)) {
                yield return new Vector2Int(x, y);
            }
        }

        /// <summary>
        ///     Enumerate through a square of vectors from the given offset
        /// </summary>
        /// <param name="sizeX">Horizontal offset</param>
        /// <param name="sizeY">Vertical offset</param>
        /// <param name="offsetX">Width</param>
        /// <param name="offsetY">Height</param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> EnumerateVectors(int sizeX, int sizeY, int offsetX, int offsetY) {
            foreach (var x in Range(sizeX))
            foreach (var y in Range(sizeY)) {
                yield return new Vector2Int(x + offsetX, y + offsetY);
            }
        }

        /// <inheritdoc cref="EnumerateVectors(int, int)" />
        public static IEnumerable<Vector2Int> EnumerateVectors(Vector2Int size) {
            return EnumerateVectors(size.x, size.y);
        }

        /// <inheritdoc cref="EnumerateVectors(int, int, int, int)" />
        public static IEnumerable<Vector2Int> EnumerateVectors(Vector2Int size, Vector2Int offset) {
            return EnumerateVectors(size.x, size.y, offset.x, offset.y);
        }
    }
}