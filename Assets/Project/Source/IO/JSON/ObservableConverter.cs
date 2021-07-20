using System;
using Exa.Types.Binding;
using Newtonsoft.Json;

namespace Exa.IO.Json {
    internal class ObservableConverter<TObservable, TData> : JsonConverter<TObservable>
        where TObservable : Observable<TData>
        where TData : class {
        public override TObservable ReadJson(
            JsonReader reader,
            Type objectType,
            TObservable existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        ) {
            var data = serializer.Deserialize(reader, typeof(TData)) as TData;

            return Activator.CreateInstance(typeof(TObservable), data) as TObservable;
        }

        public override void WriteJson(JsonWriter writer, TObservable value, JsonSerializer serializer) {
            serializer.Serialize(writer, value.Data);
        }
    }
}