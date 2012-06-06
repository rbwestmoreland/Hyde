using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var filePaths = GetAllFiles(ConfigurationSettings.Source);
            var includes = GetAllIncludes(filePaths);
            var layouts = GetAllLayouts(filePaths);
            var posts = GetAllPosts(filePaths);
            var others = filePaths
                .Except(includes.Select(i => i.Path))
                .Except(layouts.Select(l => l.Path))
                .Except(posts.Select(p => p.Path))
                .Except(new string[] { ConfigurationSettings.Path })
                .Except(ConfigurationSettings.Exclude);

            foreach (var other in others)
            {
                var relativePath = other.Substring(ConfigurationSettings.Source.Length);
                var outputPath = Path.Combine(ConfigurationSettings.Destination, relativePath);
                var fileText = Process(other);
                WriteToDisk(outputPath, fileText);
            }

            foreach (var post in posts)
            {
                
            }
        }

        private IEnumerable<string> GetAllFiles(string directory)
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

        private IEnumerable<Include> GetAllIncludes(IEnumerable<string> filePaths)
        {
            var includes = new List<Include>();

            foreach (var filePath in filePaths)
            {
                var relativePath = filePath.Substring(ConfigurationSettings.Source.Length).TrimStart('\\');
                if (relativePath.StartsWith("_includes"))
                {
                    var path = filePath;
                    var name = relativePath.Replace("_includes\\", string.Empty);
                    includes.Add(new Include(name, path));
                }
            }

            return includes;
        }

        private IEnumerable<Layout> GetAllLayouts(IEnumerable<string> filePaths)
        {
            var includes = new List<Layout>();

            foreach (var filePath in filePaths)
            {
                var relativePath = filePath.Substring(ConfigurationSettings.Source.Length).TrimStart('\\');
                if (relativePath.StartsWith("_layouts"))
                {
                    var path = filePath;
                    var name = relativePath.Replace("_layouts\\", string.Empty);
                    includes.Add(new Layout(name, path));
                }
            }

            return includes;
        }

        private IEnumerable<Post> GetAllPosts(IEnumerable<string> filePaths)
        {
            var includes = new List<Post>();

            foreach (var filePath in filePaths)
            {
                var relativePath = filePath.Substring(ConfigurationSettings.Source.Length).TrimStart('\\');
                if (relativePath.StartsWith("_posts"))
                {
                    var path = filePath;
                    var name = relativePath.Replace("_posts\\", string.Empty);
                    includes.Add(new Post(name, path));
                }
            }

            return includes;
        }

        private IEnumerable<string> OutputPaths(Post post)
        {
            throw new NotImplementedException();
        }

        private string Process(string filePath)
        {
            var extensionsToProcess = new string[] { ".html", ".markdown", ".md", ".textile" };
            var frontMatter = default(FrontMatter);

            var processedFileText = File.ReadAllText(filePath);
            var extension = Path.GetExtension(filePath);

            if (FrontMatter.TryParse(filePath, out frontMatter))
            {
                throw new NotImplementedException();
            }

            return processedFileText;
        }

        private void WriteToDisk(string filePath, string fileText)
        {
            throw new NotImplementedException();
        }
    }
}
