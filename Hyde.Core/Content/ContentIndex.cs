using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hyde.Core.Configuration;
using System.IO;

namespace Hyde.Core.Content
{
    public class ContentIndex : IContentIndex
    {
        public IConfigurationSettings ConfigurationSettings { get; private set; }
        public IEnumerable<string> AllFiles { get; private set; }
        public IEnumerable<Include> Includes { get; private set; }
        public IEnumerable<Layout> Layouts { get; private set; }
        public IEnumerable<Post> Posts { get; private set; }
        public IEnumerable<ContentBase> Other { get; private set; }

        public ContentIndex(IConfigurationSettings configurationSettings)
        {
            if (configurationSettings == null)
            {
                throw new ArgumentNullException("configurationSettings");
            }

            ConfigurationSettings = configurationSettings;
        }

        public void Index()
        {
            AllFiles = GetAllFiles(ConfigurationSettings.Source);
            Includes = GetAllIncludes(AllFiles);
            Layouts = GetAllLayouts(AllFiles);
            Posts = GetAllPosts(AllFiles);
            Other = GetAllOtherContent(AllFiles, Includes, Layouts, Posts);
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
                var relativePath = path.Substring(ConfigurationSettings.Source.Length);
                if (relativePath.StartsWith("\\_includes\\"))
                {
                    includes.Add(new Include(path));
                }
            }

            return includes;
        }

        private IEnumerable<Layout> GetAllLayouts(IEnumerable<string> paths)
        {
            var layouts = new List<Layout>();

            foreach (var path in paths)
            {
                var relativePath = path.Substring(ConfigurationSettings.Source.Length);
                if (relativePath.StartsWith("\\_layouts\\"))
                {
                    layouts.Add(new Layout(path));
                }
            }

            return layouts;
        }

        private IEnumerable<Post> GetAllPosts(IEnumerable<string> paths)
        {
            var posts = new List<Post>();

            foreach (var path in paths)
            {
                var relativePath = path.Substring(ConfigurationSettings.Source.Length);
                if (relativePath.StartsWith("\\_posts\\"))
                {
                    posts.Add(new Post(path));
                }
            }

            return posts;
        }

        private IEnumerable<ContentBase> GetAllOtherContent(IEnumerable<string> allFiles, IEnumerable<Include> includes, IEnumerable<Layout> layouts, IEnumerable<Post> posts)
        {
            var otherContent = new List<ContentBase>();

            var paths = allFiles
                .Except(includes.Select(i => i.Path))
                .Except(layouts.Select(l => l.Path))
                .Except(posts.Select(p => p.Path))
                .Except(new string[] { ConfigurationSettings.Path })
                .Except(ConfigurationSettings.Exclude);

            foreach (var path in paths)
            {
                otherContent.Add(new ContentBase(path));
            }

            return otherContent;
        }
    }
}
