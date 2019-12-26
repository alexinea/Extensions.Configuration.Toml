using System.IO;
using Microsoft.Extensions.Configuration;

namespace Alexinea.Extensions.Configuration.Toml {
    /// <summary>
    /// A Toml file based <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public class TomlConfigurationProvider : FileConfigurationProvider {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">The <see cref="TomlConfigurationSource"/>.</param>
        public TomlConfigurationProvider(TomlConfigurationSource source) : base(source) { }

        /// <summary>
        /// Loads Toml configuration key/values from a stream into a provider.
        /// </summary>
        /// <param name="stream">The toml <see cref="Stream"/> to load configuration data from.</param>
        public override void Load(Stream stream) {
            Data = TomlConfigurationFileParser.Parse(stream);
        }
    }
}