﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Hyde.Core.Configuration
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        public string Path { get; private set; }
        public string Source { get; private set; }
        public string Destination { get; private set; }
        public string Permalink { get; private set; }
        public IEnumerable<string> Exclude { get; private set; }

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

            Path = configurationPath;
            Source = System.IO.Path.GetDirectoryName(configurationPath);
            Destination = System.IO.Path.GetDirectoryName(configurationPath);
            Exclude = new List<string>();
            Parse(configurationPath);
        }

        private void Parse(string configurationPath)
        {
            var yamlStream = new YamlStream();
            var fileText = File.ReadAllText(configurationPath);
            using (var stringReader = new StringReader(fileText))
            {
                yamlStream.Load(stringReader);
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
                    case "permalink":
                        Permalink = entry.Value.ToString();
                        break;
                    case "exclude":
                        Exclude = ((YamlSequenceNode)entry.Value).Children.Select(n => System.IO.Path.Combine(Source, n.ToString()));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
