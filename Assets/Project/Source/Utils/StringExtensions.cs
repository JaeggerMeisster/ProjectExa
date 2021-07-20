using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Exa.Utils {
    public static class StringExtensions {
        public static char GetRandomChar() {
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            var r = new Random();
            var i = r.Next(chars.Length);

            return chars[i];
        }

        public static void AppendLineIndented(this StringBuilder sb, string line, int tabs = 0) {
            var tabbedString = new string('\t', tabs);
            var newString = line.Replace("\n", $"\n{tabbedString}").TrimEnd();
            sb.AppendLine($"{tabbedString}{newString}");
        }

        public static string Format(this string format, params object[] args) {
            return string.Format(format, args);
        }

        public static string GetUniqueName(string name, IEnumerable<string> existingNames) {
            string NextName(string currentName, int number) {
                if (number != 0) {
                    currentName = $"{currentName}{number}";
                }

                return existingNames.Contains(currentName)
                    ? NextName(currentName, number + 1)
                    : currentName;
            }

            return NextName(name, 0);
        }

        public static void LogInstanceString(this Object obj) {
            Debug.Log(GetInstanceString(obj));
        }

        public static string GetInstanceString(this Object obj) {
            return $"{obj.name} : {obj.GetInstanceID()}";
        }

        public static string ToGenericString(this Type type) {
            if (type.IsGenericType) {
                var retType = new StringBuilder();
                var parentType = type.FullName.Split('`');
                // We will build the type here.
                var arguments = type.GetGenericArguments();
                var argList = new StringBuilder();

                foreach (var t in arguments) {
                    // Let's make sure we get the argument list.
                    var arg = ToGenericString(t);

                    if (argList.Length > 0) {
                        argList.AppendFormat(", {0}", arg);
                    } else {
                        argList.Append(arg);
                    }
                }

                if (argList.Length > 0) {
                    retType.AppendFormat("{0}<{1}>", parentType[0], argList);
                }

                return retType.ToString();
            }

            return type.ToString();
        }

        public static string ToProperCase(this string value) {
            // If there are 0 or 1 characters, just return the string.
            if (value == null) {
                return value;
            }

            if (value.Length < 2) {
                return value.ToUpper();
            }

            // Start with the first character.
            var result = new StringBuilder(value.Substring(0, 1).ToUpper());

            // Add the remaining characters.
            for (var i = 1; i < value.Length; i++) {
                if (char.IsUpper(value[i])) {
                    result.Append(" ");
                }
                    

                result.Append(value[i]);
            }

            return result.ToString();
        }
    }
}