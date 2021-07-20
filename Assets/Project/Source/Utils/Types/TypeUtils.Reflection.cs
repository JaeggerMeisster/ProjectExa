using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Exa.Utils {
    public static class TypeUtils {
        public static IEnumerable<Func<TObject, TMember>> GetPropertyGetters<TObject, TMember>(Type objectType = null) {
            if (objectType == null) {
                objectType = typeof(TObject);
            } else {
                if (!typeof(TObject).IsAssignableFrom(objectType)) {
                    throw new Exception("Type mismatch");
                }
            }

            var memberType = typeof(TMember);

            foreach (var propertyInfo in objectType.GetProperties()) {
                if (memberType.IsAssignableFrom(propertyInfo.PropertyType)) {
                    var input = Expression.Parameter(typeof(object), "input");
                    var getMethod = propertyInfo.GetGetMethod();
                    var instance = Expression.Convert(input, objectType);
                    var methodCall = Expression.Call(instance, getMethod);

                    yield return Expression.Lambda<Func<TObject, TMember>>(methodCall, input).Compile();
                }
            }
        }
    }
}