using System;
using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks {
    /// <summary>
    ///     Supports converting a template component to the runtime data container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITemplatePartial<out T> : IBlockComponentContainer
        where T : struct, IBlockComponentValues {
        T ToBaseComponentValues();
    }

    public interface IBlockComponentContainer {
        Type GetDataType();
    }

    public static class ITemplatePartialExtensions {
        public static bool GetDataTypeIsOf<TType>(this IBlockComponentContainer partial)
            where TType : IBlockComponentValues {
            return partial.GetDataTypeIsOf(typeof(TType));
        }

        public static bool GetDataTypeIsOf(this IBlockComponentContainer partial, Type type) {
            return type.IsAssignableFrom(partial.GetDataType());
        }
    }
}