using System;

namespace Exa.Utils {
    public static class EnumExtensions {
        /// <summary>
        ///     Type constrained version of <see cref="Enum.HasFlag(Enum)" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasValue<T>(this T value, T flag)
            where T : Enum {
            return value.HasFlag(flag);
        }

        public static bool HasAnyValue<T>(this T value, T flags)
            where T : Enum {
            return ((int) (object) value & (int) (object) flags) != 0;
        }

        public static T SetFlag<T>(this T value, T flag, bool set)
            where T : Enum {
            var valueInt = (int) (object) value;
            var flagInt = (int) (object) flag;

            return (T) (object) (set ? valueInt | flagInt : valueInt & ~flagInt);
        }
    }
}