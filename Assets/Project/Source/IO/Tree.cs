using System.IO;
using UnityEngine;

namespace Exa.IO {
    public static class Tree {
        static Tree() {
        #if UNITY_EDITOR
            var info = new DirectoryInfo(Application.persistentDataPath);
            var newName = $"{info.Name}Dev";
            // ReSharper disable once PossibleNullReferenceException
            Root = new RootNode(info.Parent.FullName, newName);
        #else
            Root = new RootNode(Application.persistentDataPath);
        #endif
        }

        public static RootNode Root { get; }

        public class RootNode : IDirectoryNode {
            private readonly string rootFolder;

            public RootNode(string rootFolder) {
                this.rootFolder = rootFolder;
                IOUtils.EnsureCreated(GetPath());

                Blueprints = new DirectoryNode(this, "blueprints");
                Settings = new DirectoryNode(this, "settings");
                Thumbnails = new DirectoryNode(this, "thumbnails");
                DefaultThumbnails = new DirectoryNode(this, "defaultThumbnails");
                CustomSoundTracks = new DirectoryNode(this, "customSoundTracks");
            }

            public RootNode(string rootFolder, string postFix)
                : this(IOUtils.CombinePath(rootFolder, postFix)) { }

            public DirectoryNode Blueprints { get; }
            public DirectoryNode Settings { get; }
            public DirectoryNode Thumbnails { get; }
            public DirectoryNode DefaultThumbnails { get; }
            public DirectoryNode CustomSoundTracks { get; }

            public string GetPath() {
                return rootFolder;
            }
        }
    }
}