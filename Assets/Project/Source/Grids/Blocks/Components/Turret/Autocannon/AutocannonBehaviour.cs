using System;
using Exa.Weapons;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.Components {
    public class AutocannonBehaviour : AutoFireTurret<AutocannonData> {
        [SerializeField] private AutocannonPart[] parts;
        private int currentPoint;

        public override void Fire() {
            switch (data.cycleMode) {
                case CycleMode.Cycling:
                    parts[currentPoint].Fire(data.damage, data.range);
                    currentPoint++;
                    currentPoint %= parts.Length;

                    break;

                case CycleMode.Volley:
                    foreach (var part in parts) {
                        part.Fire(data.damage, data.range);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnAdd() {
            var animTime = GetAnimTime();

            if (Parent.BlockContext == 0) {
                throw new InvalidOperationException("Parent's block context must be set");
            }

            var damageMask = ~Parent.BlockContext;

            foreach (var part in parts) {
                part.Setup(animTime, Parent, damageMask);
            }
        }

        private float GetAnimTime() {
            return data.cycleMode switch {
                CycleMode.Cycling => data.firingRate * 1.5f,
                CycleMode.Volley => data.firingRate * 0.75f,
                _ => throw new Exception()
            };
        }
    }
}