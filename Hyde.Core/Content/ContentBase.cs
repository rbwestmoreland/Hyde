using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyde.Core.Content
{
    public class ContentBase
    {
        public string Path { get; private set; }
        public string Extension { get; private set; }
        public FrontMatter FrontMatter { get; private set; }
        public bool HasFrontMatter { get { return FrontMatter != null; } }

        public ContentBase(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            Path = path;
            Extension = ParseExtension(path);

            var frontMatter = default(FrontMatter);
            FrontMatter.TryParse(path, out frontMatter);
            FrontMatter = frontMatter;
        }

        private string ParseExtension(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            try
            {
                return System.IO.Path.GetExtension(path);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse extension");
            }
        }
    }
}
