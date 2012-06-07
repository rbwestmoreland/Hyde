using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hyde.Core.Configuration;
using System.IO;

namespace Hyde.Core.ContentProcessor
{
    internal class ContentIndex
    {
        private IConfigurationSettings ConfigurationSettings { get; set; }
        public IEnumerable<string> AllFiles { get; private set; }
        public IEnumerable<Include> Includes { get; private set; }
        public IEnumerable<Layout> Layouts { get; private set; }
        public IEnumerable<Post> Posts { get; private set; }
        public IEnumerable<string> Other { get; private set; }

        public ContentIndex(IConfigurationSettings configurationSettings)
        {
            if (configurationSettings == null)
            {
                throw new ArgumentNullException("configurationSettings");
            }

            ConfigurationSettings = configurationSettings;
        }

        public void Analyze()
        {
            AllFiles = GetAllFiles(ConfigurationSettings.Source);
            Includes = GetAllIncludes(AllFiles);
            Layouts = GetAllLayouts(AllFiles);
            Posts = GetAllPosts(AllFiles);
            Other = AllFiles
                .Except(Includes.Select(i => i.Path))
                .Except(Layouts.Select(l => l.Path))
                .Except(Posts.Select(p => p.Path))
                .Except(new string[] { ConfigurationSettings.Path })
                .Except(ConfigurationSettings.Exclude);
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

        private IEnumerable<Include> GetAllIncludes(IEnumerable<string> paths)
        {
            var includes = new List<Include>();

            foreach (var path in paths)
            {
                var relativePath = path.Substring(ConfigurationSettings.Source.Length).TrimStart('\\');
                if (relativePath.StartsWith("_includes"))
                {
                    includes.Add(new Include(path));
                }
            }

            return includes;
        }

        private IEnumerable<Layout> GetAllLayouts(IEnumerable<string> paths)
        {
            var includes = new List<Layout>();

            foreach (var path in paths)
            {
                var relativePath = path.Substring(ConfigurationSettings.Source.Length).TrimStart('\\');
                if (relativePath.StartsWith("_layouts"))
                {
                    includes.Add(new Layout(path));
                }
            }

            return includes;
        }

        private IEnumerable<Post> GetAllPosts(IEnumerable<string> paths)
        {
            var includes = new List<Post>();

            foreach (var path in paths)
            {
                var relativePath = path.Substring(ConfigurationSettings.Source.Length).TrimStart('\\');
                if (relativePath.StartsWith("_posts"))
                {
                    includes.Add(new Post(path));
                }
            }

            return includes;
        }
    }
}
