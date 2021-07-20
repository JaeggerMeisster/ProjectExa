using System.Collections.Generic;
using System.Linq;
using Exa.Utils;
using UnityEngine;

namespace Exa.Drawing {
    public static partial class Texture2DExtensions {
        public static Texture2D CallbackCircularSuperSampledDraw<T>(this Texture2D tex, Vector2 centre, SuperSamplingArgs<T> samplingArgs) {
            foreach (var pixel in tex.EnumeratePixels()) {
                var samples = GetSamples(pixel, samplingArgs.sampleSize)
                    .Select(point => samplingArgs.sampler(point, centre - point))
                    .ToList();

                samplingArgs.applier(pixel, samplingArgs.samplesAverageFactory(samples));
            }

            tex.Apply();

            return tex;
        }

        public static IEnumerable<Vector2> GetSamples(Vector2Int origin, int count) {
            var size = 1f / count;
            var halfSize = size / 2f;

            for (var x = 0; x < count; x++)
            for (var y = 0; y < count; y++) {
                yield return new Vector2 {
                    x = origin.x + halfSize + x * size,
                    y = origin.y + halfSize + y * size
                };
            }
        }
    }
}