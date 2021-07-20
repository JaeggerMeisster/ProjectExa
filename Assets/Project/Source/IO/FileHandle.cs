using System;
using System.IO;

namespace Exa.IO {
    /// <summary>
    ///     Provides a reference to a file that may have its name updated
    /// </summary>
    public class FileHandle {
        private readonly ISerializableItem item;
        private readonly Func<string, string> pathFactory;
        private readonly Action<string> serializationFactory;

        public FileHandle(
            ISerializableItem item,
            Func<string, string> pathFactory,
            Action<string> serializationFactory,
            bool generatePath = true
        ) {
            this.item = item;
            this.pathFactory = pathFactory;
            this.serializationFactory = serializationFactory;

            if (generatePath) {
                CurrentPath = pathFactory(item.ItemName);
            }
        }

        public string CurrentPath { get; set; }

        public string TargetPath {
            get => pathFactory(item.ItemName);
        }

        public bool PathIsDirty {
            get => CurrentPath != TargetPath;
        }

        public void Delete() {
            if (CurrentPath != "" && File.Exists(CurrentPath)) {
                File.Delete(CurrentPath);
            }
        }

        public void Refresh() {
            if (PathIsDirty) {
                Delete();
            }

            CurrentPath = TargetPath;
            serializationFactory(CurrentPath);
        }
    }
}