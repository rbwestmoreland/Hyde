using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace Hyde.Core.ContentProcessor
{
    public class ContentProcessor : IContentProcessor
    {
        private string SourcePath { get; set; }
        private string DestinationPath { get; set; }

        public ContentProcessor(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentNullException("sourcePath");
            }

            if (string.IsNullOrWhiteSpace(destinationPath))
            {
                throw new ArgumentNullException("destinationPath");
            }

            SourcePath = sourcePath;
            DestinationPath = destinationPath;
        }

        public void Process()
        {
            var filePaths = GetAllFiles(SourcePath);
            foreach (var filePath in filePaths)
            {
                var relativePath = filePath.Substring(SourcePath.Length);
                var shouldProcess = ShouldProcess(filePath);
            }
        }

        protected virtual IEnumerable<string> GetAllFiles(string directory)
        {
            var files = new List<string>();

            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(directory);
            }

            files.AddRange(Directory.GetFiles(directory));

            foreach (var childDirectory in Directory.GetDirectories(directory))
            {
                files.AddRange(GetAllFiles(childDirectory));
            }

            return files;
        }

        protected virtual bool ShouldProcess(string filePath)
        {
            var shouldProcess = false;

            try
            {
                var yamlStream = new YamlStream();
                var fileText = File.ReadAllText(filePath);
                using (var fileReader = new StringReader(fileText))
                {
                    yamlStream.Load(fileReader);
                }

                var mapping = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                shouldProcess = true;
            }
            catch (Exception)
            {
            }

            return shouldProcess;
        }
    }
}
