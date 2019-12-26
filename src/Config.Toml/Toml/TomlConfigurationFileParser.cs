using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Nett;

namespace Alexinea.Extensions.Configuration.Toml {
    internal class TomlConfigurationFileParser : ITomlObjectVisitor {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _context = new Stack<string>();
        private string _currentPath;

        public static IDictionary<string, string> Parse(Stream input)
            => new TomlConfigurationFileParser().ParseStream(input);

        private IDictionary<string, string> ParseStream(Stream stream) {
            _data.Clear();
            _context.Clear();

            Nett.Toml.ReadStream(stream).Visit(this);
            
            return _data;
        }

        void PushContext(string context) {
            this._context.Push(context);
            _currentPath = ConfigurationPath.Combine(this._context.Reverse());
        }

        void PopContext() {
            _context.Pop();
            _currentPath = ConfigurationPath.Combine(this._context.Reverse());
        }

        public void Visit(TomlTable table) {
            foreach (var row in table.Rows) {
                PushContext(row.Key);
                row.Value.Visit(this);
                PopContext();
            }
        }

        public void Visit(TomlTableArray tableArray) {
            for (int i = 0; i < tableArray.Count; i++) {
                PushContext(i.ToString());
                tableArray[i].Visit(this);
                PopContext();
            }
        }

        public void Visit(TomlInt i) => _data[_currentPath] = i.Value.ToString(CultureInfo.InvariantCulture);

        public void Visit(TomlFloat f) => _data[_currentPath] = f.Value.ToString(CultureInfo.InvariantCulture);

        public void Visit(TomlBool b) => _data[_currentPath] = b.Value.ToString(CultureInfo.InvariantCulture);

        public void Visit(TomlString s) => _data[_currentPath] = s.Value;
        
        public void Visit(TomlDuration ts) => _data[_currentPath] = ts.Value.ToString("c", CultureInfo.InvariantCulture);

        public void Visit(TomlOffsetDateTime dt) => _data[_currentPath] = dt.Value.ToString(CultureInfo.InvariantCulture);

        public void Visit(TomlLocalDate ld) => _data[_currentPath] = ld.Value.ToString(CultureInfo.InvariantCulture);

        public void Visit(TomlLocalDateTime ldt) => _data[_currentPath] = ldt.Value.ToString(CultureInfo.InvariantCulture);

        public void Visit(TomlLocalTime lt) => _data[_currentPath] = lt.Value.ToString("c", CultureInfo.InvariantCulture);

        public void Visit(TomlArray a) {
            for (int i = 0; i < a.Length; i++) {
                PushContext(i.ToString());
                a[i].Visit(this);
                PopContext();
            }
        }
    }
}