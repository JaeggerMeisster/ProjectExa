using System;
using System.Collections.Generic;
using Exa.Math;
using UnityEngine;

namespace Exa.Utils {
    public class Timer {
        private readonly Dictionary<float, Action> callbackDict;
        private float currentTime;

        public Timer() {
            callbackDict = new Dictionary<float, Action>();
        }

        public void Tick(float? time = null) {
            var delta = time ?? Time.deltaTime;

            callbackDict
                .WhereKeys(callbackTime => currentTime.Between(callbackTime, callbackTime + delta))
                .ForEach(callback => callback());

            currentTime += delta;
        }

        public void OnTime(float time, Action callback) {
            callbackDict.Add(time, callback);
        }
    }
}