using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace Exa.IO {
    public static class ZipUtils {
        public static IEnumerable<ZipEntry> Filter(this IEnumerable<ZipEntry> entries, string filter) {
            return entries.Where(entry => Path.GetExtension(entry.FileName) == filter);
        }

        public static IEnumerable<ZipEntry> Filter(this IEnumerable<ZipEntry> entries, IEnumerable<string> filters) {
            return entries.Where(
                entry => {
                    var extension = Path.GetExtension(entry.FileName);

                    return filters.Any(filter => extension == filter);
                }
            );
        }

        public static MemoryStream GetStream(this ZipEntry entry) {
            var stream = new MemoryStream();
            entry.Extract(stream);
            stream.Position = 0;

            return stream;
        }

        public static T ReadJson<T>(this ZipEntry entry, SerializationMode mode) {
            var json = Encoding.UTF8.GetString(entry.GetStream().ToArray());

            return IOUtils.JsonDeserializeWithSettings<T>(json, mode);
        }
    }
}