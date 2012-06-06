using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace Hyde.Core.Configuration
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        public string Source { get; private set; }
        public string Destination { get; private set; }

        public ConfigurationSettings(string configurationPath)
        {
            if (string.IsNullOrWhiteSpace(configurationPath))
            {
                throw new ArgumentNullException("configurationPath");
            }

            if (!File.Exists(configurationPath))
            {
                throw new FileNotFoundException("File not found", configurationPath);
            }

            Source = Path.GetDirectoryName(configurationPath);
            Destination = Path.Combine(Source, "_site");

            Parse(configurationPath);
        }

        private void Parse(string configurationPath)
        {
            var yamlStream = new YamlStream();
            var fileText = File.ReadAllText(configurationPath);
            using (var fileReader = new StringReader(fileText))
            {
                yamlStream.Load(fileReader);
            }

            var mapping = (YamlMappingNode)yamlStream.Documents[0].RootNode;

            foreach (var entry in mapping.Children)
            {
                switch (entry.Key.ToString().ToLower())
                {
                    case "source":
                        Source = entry.Value.ToString();
                        break;
                    case "destination":
                        Destination = entry.Value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
