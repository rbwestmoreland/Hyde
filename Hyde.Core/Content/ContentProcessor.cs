using System;
using System.IO;
using Hyde.Core.Configuration;

namespace Hyde.Core.Content
{
    public class ContentProcessor : IContentProcessor
    {
        private IContentIndex ContentIndex { get; set; }

        public ContentProcessor(IContentIndex contentIndex)
        {
            if (contentIndex == null)
            {
                throw new ArgumentNullException("contentIndex");
            }

            ContentIndex = contentIndex;
        }

        public void Process()
        {
            var outputPathBase = Path.Combine(ContentIndex.ConfigurationSettings.Destination, "_site");
            ContentIndex.Index();
            DeleteDirectory(outputPathBase);

            foreach (var other in ContentIndex.Other)
            {
                var relativePath = other.Path.Substring(ContentIndex.ConfigurationSettings.Source.Length).TrimStart(Path.DirectorySeparatorChar);

                if (other.HasFrontMatter)
                {
                }
                else
                {
                    var outputPath = Path.Combine(outputPathBase, relativePath);
                    Copy(other.Path, outputPath);
                }
            }

            foreach (var post in ContentIndex.Posts)
            {
                
            }
        }

        private void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private void WriteToDisk(string path, string text)
        {
        }

        private void Copy(string sourcePath, string destinationPath)
        {
            var directory = Path.GetDirectoryName(destinationPath);
            Directory.CreateDirectory(directory);
            File.Copy(sourcePath, destinationPath, true);
        }
    }
}
