using System.Collections.Generic;

namespace Exa.Utils {
    public static class IComparerExtensions {
        public static IComparer<T> ReverseComparer<T>(this IComparer<T> comparer) {
            return new ReversedComparer<T>(comparer);
        }
    }

    public class ReversedComparer<T> : IComparer<T> {
        private readonly IComparer<T> originalComparer;

        public ReversedComparer(IComparer<T> originalComparer) {
            this.originalComparer = originalComparer;
        }

        public int Compare(T x, T y) {
            return -originalComparer.Compare(x, y);
        }
    }
}