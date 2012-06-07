using System;

namespace Hyde.Core.ContentProcessor
{
    internal class Layout
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public FrontMatter FrontMatter { get; private set; }

        public Layout(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            Path = path;
            Name = ParseName(path);
            Extension = ParseExtension(path);
            FrontMatter = new FrontMatter(path);
        }

        private string ParseName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            try
            {
                return System.IO.Path.GetFileNameWithoutExtension(path);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse name");
            }
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
