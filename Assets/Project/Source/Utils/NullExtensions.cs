namespace Exa.Utils {
    public static class NullExtensions {
        public static bool IsNotNull<T>(this T input, out T output)
            where T : class {
            output = input;

            return input.IsNotNull();
        }

        public static bool IsNotNull<T>(this T input)
            where T : class {
            return input != null;
        }
    }
}