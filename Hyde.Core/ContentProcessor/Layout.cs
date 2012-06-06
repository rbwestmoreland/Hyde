using System;

namespace Hyde.Core.ContentProcessor
{
    internal class Layout
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public FrontMatter FrontMatter { get; private set; }

        public Layout(string name, string path)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            Path = path;
            Name = name;
            ParseExtension(name);
            FrontMatter = new FrontMatter(path);
        }

        private void ParseExtension(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            try
            {
                Extension = System.IO.Path.GetExtension(name);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid post file extension");
            }
        }
    }
}
