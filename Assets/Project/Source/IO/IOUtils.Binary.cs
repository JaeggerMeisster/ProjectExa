using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exa.IO {
    public static partial class IOUtils {
        public static bool TryBinaryDeserializeFromPath<T>(out T result, string filePath)
            where T : class {
            if (!File.Exists(filePath)) {
                result = null;

                return false;
            }

            using (var stream = File.OpenRead(filePath)) {
                var formatter = new BinaryFormatter();
                result = (T) formatter.Deserialize(stream);

                return true;
            }
        }

        public static T BinaryDeserializeFromPath<T>(string filePath) {
            using (var stream = File.OpenRead(filePath)) {
                var formatter = new BinaryFormatter();

                return (T) formatter.Deserialize(stream);
            }
        }

        public static T BinaryDeserializeFromPath<T>(params string[] paths) {
            return BinaryDeserializeFromPath<T>(CombinePathWithDataPath(paths));
        }

        public static void BinarySerializeToPath(object value, string filePath) {
            if (!File.Exists(filePath)) {
                File.Create(filePath);
            }

            using (var stream = File.OpenWrite(filePath)) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
            }
        }
    }
}