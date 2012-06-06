using System;
using System.IO;

namespace Hyde.Core.ContentProcessor
{
    internal class Post
    {
        public string Path { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public string Title { get; private set; }
        public string Extension { get; private set; }
        public FrontMatter FrontMatter { get; private set; }

        public Post(string name, string path)
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
            ParseDate(name);
            ParseTitle(name);
            ParseExtension(name);
            FrontMatter = new FrontMatter(path);
        }

        private void ParseDate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length < 10)
            {
                throw new ArgumentException("name");
            }

            try
            {
                var yearString = name.Substring(0, 4);
                var monthString = name.Substring(5, 2);
                var dayString = name.Substring(8, 2);

                Year = int.Parse(yearString);
                Month = int.Parse(monthString);
                Day = int.Parse(dayString);

                var date = new DateTime(Year, Month, Day);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid post date format");
            }
        }

        private void ParseTitle(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length < 12)
            {
                throw new ArgumentException("name");
            }

            try
            {
                var nameWithoutDate = name.Substring(11);
                Title = System.IO.Path.GetFileNameWithoutExtension(nameWithoutDate);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid post title format");
            }
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
