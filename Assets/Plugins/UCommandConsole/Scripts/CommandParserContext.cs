using System;
using System.Collections.Generic;
using UCommandConsole.TypeParsers;
using UnityEngine;

namespace UCommandConsole {
    public class CommandParserContext : MonoBehaviour {
        protected Dictionary<Type, TypeParser> defaultParsers;
        protected Dictionary<Type, TypeParser> parserInstances;

        protected virtual void Awake() {
            defaultParsers = new Dictionary<Type, TypeParser>();
            parserInstances = new Dictionary<Type, TypeParser>();

            AddDefaultParser<FloatParser>();
            AddDefaultParser<StringParser>();
            AddDefaultParser<Vector2Parser>();
            AddDefaultParser<Vector3Parser>();
        }

        public void AddDefaultParser<T>()
            where T : TypeParser {
            var typeParser = Activator.CreateInstance<T>();
            defaultParsers.Add(typeParser.Target, typeParser);
            parserInstances.Add(typeof(T), typeParser);
        }

        public TypeParser GetDefaultTypeParser(Type parserType) {
            return defaultParsers[parserType];
        }

        public TypeParser GetTypeParser(Type parserType) {
            if (!parserInstances.ContainsKey(parserType)) {
                var typeParser = Activator.CreateInstance(parserType) as TypeParser;
                parserInstances.Add(parserType, typeParser);
            }

            return parserInstances[parserType];
        }
    }
}