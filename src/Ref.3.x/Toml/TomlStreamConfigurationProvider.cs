using System.IO;
using Microsoft.Extensions.Configuration;

namespace Alexinea.Extensions.Configuration.Toml {
    /// <summary>
    /// Loads configuration key/values from a toml stream into a provider.
    /// </summary>
    public class TomlStreamConfigurationProvider : StreamConfigurationProvider {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">The <see cref="TomlStreamConfigurationSource"/>.</param>
        public TomlStreamConfigurationProvider(TomlStreamConfigurationSource source) : base(source) { }

        /// <summary>
        /// Loads toml configuration key/values from a stream into a provider.
        /// </summary>
        /// <param name="stream">The toml <see cref="Stream"/> to load configuration data from.</param>
        public override void Load(Stream stream) {
            Data = TomlConfigurationFileParser.Parse(stream);
        }
    }
}