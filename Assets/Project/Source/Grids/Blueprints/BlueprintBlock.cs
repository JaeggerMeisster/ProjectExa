using System;
using System.Collections.Generic;
using System.ComponentModel;
using Exa.Grids.Blocks;
using Exa.Math;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    [Serializable]
    public struct BlueprintBlock : IEquatable<BlueprintBlock> {
        public string id;
        [DefaultValue(false)] public bool flippedX;
        [DefaultValue(false)] public bool flippedY;
        [JsonIgnore] private int rotation;

        public int Rotation {
            get => MathUtils.NormalizeAngle04(rotation);
            set => rotation = value;
        }

        [JsonIgnore]
        public int Direction {
            get => (Vector2Int.right.Rotate(Rotation) * FlipVector).GetRotation();
        }

        [JsonIgnore]
        public Vector2Int FlipVector {
            get => new Vector2Int {
                x = flippedX ? -1 : 1,
                y = flippedY ? -1 : 1
            };
        }

        [JsonIgnore]
        public BlockTemplate Template {
            get {
                if (!S.Blocks.blockTemplatesDict.ContainsKey(id)) {
                    throw new KeyNotFoundException($"Block template with id: {id} doesn't exist");
                }

                return S.Blocks.blockTemplatesDict[id];
            }
        }

        public bool Equals(BlueprintBlock other) {
            return
                id == other.id &&
                flippedX == other.flippedX &&
                flippedY == other.flippedY &&
                rotation == other.rotation;
        }

        public Quaternion GetDirection() {
            return Quaternion.Euler(0, 0, Direction * 90f);
        }

        public Vector2Int CalculateSizeDelta() {
            var area = Template.size.Rotate(Rotation);

            if (flippedX) {
                area.x = -area.x;
            }

            if (flippedY) {
                area.y = -area.y;
            }

            return area;
        }

        public void SetSpriteRendererFlips(SpriteRenderer spriteRenderer) {
            spriteRenderer.flipX = Rotation % 2 == 0
                ? flippedX
                : flippedY;

            spriteRenderer.flipY = Rotation % 2 == 0
                ? flippedY
                : flippedX;
        }

        public override string ToString() {
            return $"{id}:{rotation}";
        }

        public override bool Equals(object obj) {
            return obj is BlueprintBlock other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = id != null ? id.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ flippedX.GetHashCode();
                hashCode = (hashCode * 397) ^ flippedY.GetHashCode();
                hashCode = (hashCode * 397) ^ rotation;

                return hashCode;
            }
        }
    }
}