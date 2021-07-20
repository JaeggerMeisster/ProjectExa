using System;
using System.IO;
using Exa.IO;

namespace Exa.Data {
    /// <summary>
    ///     Provides base functionality for a settings object that is serialized and stored in the data directory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SaveableSettings<T> : ISettings
        where T : class, IEquatable<T> {
        /// <summary>
        ///     Default setting values
        /// </summary>
        public abstract T DefaultValues { get; }

        /// <summary>
        ///     Current setting values
        /// </summary>
        public T Values { get; set; }

        protected abstract string Key { get; }

        /// <summary>
        ///     Applies the current values to the client
        /// </summary>
        public abstract void Apply();

        public virtual void Save() {
            var path = Tree.Root.Settings.CombineWith($"{Key}.json");
            IOUtils.JsonSerializeToPath(Values, path, SerializationMode.Settings);
        }

        public virtual void Load() {
            var path = Tree.Root.Settings.CombineWith($"{Key}.json");

            Values = File.Exists(path)
                ? DeserializeValues(path) ?? DefaultValues
                : DefaultValues;
        }

        public abstract T Clone();

        protected virtual T DeserializeValues(string path) {
            return IOUtils.JsonDeserializeFromPath<T>(path, SerializationMode.Settings);
        }
    }
}