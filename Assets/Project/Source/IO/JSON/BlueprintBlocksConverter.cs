using System;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Exa.IO.Json {
    public class BlueprintBlocksConverter : JsonConverter<BlueprintGrid> {
        public static readonly string[] SEPARATORS = {
            "(",
            ",",
            ")"
        };

        public override BlueprintGrid ReadJson(
            JsonReader reader,
            Type objectType,
            BlueprintGrid existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        ) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            var blocks = new BlueprintGrid();

            foreach (var pair in JObject.Load(reader).Unpack()) {
                var vector = pair.key.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);

                var key = new Vector2Int {
                    x = Convert.ToInt32(vector[0]),
                    y = Convert.ToInt32(vector[1])
                };

                var value = pair.value.ToObject<BlueprintBlock>(serializer);

                blocks.Add(new ABpBlock(key, value));
            }

            return blocks;
        }

        public override void WriteJson(JsonWriter writer, BlueprintGrid value, JsonSerializer serializer) {
            writer.WriteStartObject();

            foreach (var pair in value) {
                writer.WritePropertyName(pair.GridAnchor.ToShortString());
                serializer.Serialize(writer, pair.BlueprintBlock);
            }

            writer.WriteEndObject();
        }
    }
}