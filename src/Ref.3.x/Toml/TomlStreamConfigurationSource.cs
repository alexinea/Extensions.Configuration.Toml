using Microsoft.Extensions.Configuration;

namespace Alexinea.Extensions.Configuration.Toml {
    /// <summary>
    /// Represents a YAML file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class TomlStreamConfigurationSource : StreamConfigurationSource {
        /// <summary>
        /// Builds the <see cref="TomlStreamConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="TomlStreamConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
            => new TomlStreamConfigurationProvider(this);
    }
}