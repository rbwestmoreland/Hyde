using System;

namespace Hyde.Core.Content
{
    public class Layout : ContentBase
    {
        public string Name { get; private set; }

        public Layout(string path)
            : base(path)
        {
            Name = ParseName(path);
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
    }
}
