using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.Math;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.UI.Settings {
    /// <summary>
    ///     A list of resolutions, sorted by resolution size and refresh rate
    /// </summary>
    public class Resolutions : IEnumerable<Resolution> {
        private readonly int[] acceptedRefreshRates = {
            60,
            75,
            80,
            90,
            100,
            120,
            144,
            165,
            180,
            240
        };
        private readonly List<Resolution> resolutions;

        public Resolutions() {
            var filteredResolutions = Screen
                .resolutions
                .Where(IsAcceptedRefreshRate)
                .Where(IsAcceptedRatio);

            // If the are no selected resolutions,
            if (!filteredResolutions.Any()) {
                filteredResolutions = Screen.resolutions;
            }

            resolutions = new List<Resolution>(filteredResolutions);

            resolutions.Sort(
                (x, y) => {
                    int GetValue(Resolution res) {
                        return res.width * res.height + res.refreshRate;
                    }

                    return GetValue(x) - GetValue(y);
                }
            );
        }

        public IEnumerator<Resolution> GetEnumerator() {
            return resolutions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public Resolution GetHighestSupportedResolution() {
            return resolutions.Last();
        }

        public Resolution GetLowestSupportedResolution() {
            return resolutions.First();
        }

        public IEnumerable<ILabeledValue<object>> GetResolutionLabels() {
            return this.Select(
                res =>
                    new LabeledValue<object>($"{res.width}x{res.height}", res) as ILabeledValue<object>
            );
        }

        public IEnumerable<ILabeledValue<object>> GetRefreshRateLabels() {
            return this.Select(res => res.refreshRate)
                .Distinct()
                .OrderByDescending(res => res)
                .Select(
                    res =>
                        new LabeledValue<object>($"{res} hz", res) as ILabeledValue<object>
                );
        }

        private bool IsAcceptedRefreshRate(Resolution resolution) {
            return acceptedRefreshRates.Contains(resolution.refreshRate);
        }

        private bool IsAcceptedRatio(Resolution resolution) {
            var ratio = MathUtils.GetRatio(
                new Vector2Int {
                    x = resolution.width,
                    y = resolution.height
                }
            );

            return
                IsRatio(ratio, 21, 9) ||
                IsRatio(ratio, 16, 10) ||
                IsRatio(ratio, 16, 9) ||
                IsRatio(ratio, 5, 4) ||
                IsRatio(ratio, 4, 3);
        }

        private bool IsRatio(Vector2Int ratio, int width, int height) {
            return ratio.x == width && ratio.y == height;
        }
    }
}