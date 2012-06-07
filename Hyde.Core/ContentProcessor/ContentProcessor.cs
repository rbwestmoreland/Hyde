using System;
using System.IO;
using Hyde.Core.Configuration;

namespace Hyde.Core.ContentProcessor
{
    public class ContentProcessor : IContentProcessor
    {
        private IConfigurationSettings ConfigurationSettings { get; set; }

        public ContentProcessor(IConfigurationSettings configurationSettings)
        {
            if (configurationSettings == null)
            {
                throw new ArgumentNullException("configurationSettings");
            }

            ConfigurationSettings = configurationSettings;
        }

        public void Process()
        {
            var contentIndex = new ContentIndex(ConfigurationSettings);
            contentIndex.Analyze();

            foreach (var other in contentIndex.Other)
            {
                var relativePath = other.Substring(ConfigurationSettings.Source.Length);
                var outputPath = Path.Combine(ConfigurationSettings.Destination, relativePath);
                var fileText = Process(other);
                WriteToDisk(outputPath, fileText);
            }

            foreach (var post in contentIndex.Posts)
            {
                
            }
        }

        private string Process(string path)
        {
            var extensionsToProcess = new string[] { ".html", ".markdown", ".md", ".textile" };
            var frontMatter = default(FrontMatter);

            var processedFileText = File.ReadAllText(path);
            var extension = Path.GetExtension(path);

            if (FrontMatter.TryParse(path, out frontMatter))
            {
                throw new NotImplementedException();
            }

            return processedFileText;
        }

        private void WriteToDisk(string path, string fileText)
        {
            throw new NotImplementedException();
        }
    }
}
