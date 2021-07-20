using System;
using System.Collections.Generic;
using System.IO;
using UCommandConsole.Exceptions;

namespace UCommandConsole {
    public class CommandParser {
        public CommandParserContext context;

        private StringReader reader;

        public CommandParser(string value, CommandParserContext parserContext) {
            context = parserContext;
            Parse(value);
        }

        public bool IsEOF { get; private set; }
        public bool CurrentCharIsNegated { get; private set; }
        public char CurrentChar { get; private set; }

        public void Parse(string value) {
            reader = new StringReader(value);
            NextChar();
        }

        public T AsValue<T>(CustomTypeParser<T> typeParser = null) {
            return (T) AsValue(typeof(T), typeParser);
        }

        public object AsValue(Type valueType, TypeParser typeParser = null) {
            try {
                return (typeParser ?? context.GetDefaultTypeParser(valueType)).ParseAsObject(this);
            } catch (KeyNotFoundException e) {
                throw new ArgumentException($"Type {valueType} is not supported", e);
            }
        }

        public bool Matches(char expectedChar, bool removePadding = false) {
            if (removePadding) {
                RemovePadding();
            }

            return expectedChar == CurrentChar && !CurrentCharIsNegated;
        }

        public void Require(char requiredChar, bool removePadding = false) {
            var foundChar = Matches(requiredChar, removePadding);

            NextChar();

            if (!foundChar) {
                throw new InputFormatException($"Required char [{requiredChar}] but received [{CurrentChar}]");
            }
        }

        public void RemovePadding() {
            while (char.IsWhiteSpace(CurrentChar) && !IsEOF) {
                NextChar();
            }
        }

        public char NextChar() {
            // Go to next character and get wether or not the character marks EOF
            var charIsValid = ReadChar();
            CurrentCharIsNegated = false;

            // If the character is a backslash, ignore it
            if (CurrentChar == '\\') {
                charIsValid = ReadChar();
                CurrentCharIsNegated = true;
            }

            IsEOF = !charIsValid;

            return CurrentChar;
        }

        private bool ReadChar() {
            var charInt = reader.Read();
            var charIsValid = charInt >= 0;

            CurrentChar = charIsValid
                ? (char) charInt
                : '\0';

            return charIsValid;
        }
    }
}