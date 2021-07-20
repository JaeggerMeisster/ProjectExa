using System.Collections.Generic;
using System.IO;

namespace Exa.IO {
    public class DirectoryNode : IDirectoryNode {
        private readonly string name;
        protected IDirectoryNode parent;

        public DirectoryNode(IDirectoryNode parent, string name) {
            this.parent = parent;
            this.name = name;
            IOUtils.EnsureCreated(GetPath());
        }

        public string GetPath() {
            return IOUtils.CombinePath(parent.GetPath(), name);
        }

        public IEnumerable<string> GetFiles(string pattern) {
            return Directory.GetFiles(GetPath(), pattern);
        }

        public static implicit operator string(DirectoryNode node) {
            return node.GetPath();
        }
    }
}