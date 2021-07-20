using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.IO.Json {
    public class Vector2IntConverter : JsonConverter<Vector2Int> {
        public static readonly string[] SEPARATORS = {
            "(",
            ", ",
            ")"
        };

        public override Vector2Int ReadJson(
            JsonReader reader,
            Type objectType,
            Vector2Int existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        ) {
            var str = (string) reader.Value;
            var vector = str.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);

            return new Vector2Int {
                x = Convert.ToInt32(vector.First()),
                y = Convert.ToInt32(vector.Last())
            };
        }

        public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }
    }
}