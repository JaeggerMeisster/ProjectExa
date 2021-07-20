namespace Exa.IO {
    public interface IDirectoryNode {
        public string GetPath();
    }

    public static class IDirectoryNodeExtensions {
        public static string CombineWith(this IDirectoryNode node, string fileName) {
            return IOUtils.CombinePath(node.GetPath(), fileName);
        }
    }
}