using System;

namespace Hyde.Core.Content
{
    public class Post : ContentBase
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public string Title { get; private set; }

        public Post(string path)
            : base(path)
        {
            Year = ParseYear(path);
            Month = ParseMonth(path);
            Day = ParseDay(path);
            Title = ParseTitle(path);
        }

        private int ParseYear(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            if (path.Length < 10)
            {
                throw new ArgumentException("path");
            }

            try
            {
                var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                var yearString = fileName.Substring(0, 4);
                var year = int.Parse(yearString);
                return year;
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse year");
            }
        }

        private int ParseMonth(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            if (path.Length < 10)
            {
                throw new ArgumentException("path");
            }

            try
            {
                var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                var monthString = fileName.Substring(5, 2);
                var month = int.Parse(monthString);
                return month;
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse month");
            }
        }

        private int ParseDay(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            if (path.Length < 10)
            {
                throw new ArgumentException("path");
            }

            try
            {
                var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                var dayString = fileName.Substring(8, 2);
                var day = int.Parse(dayString);
                return day;
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse day");
            }
        }

        private string ParseTitle(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            if (path.Length < 12)
            {
                throw new ArgumentException("path");
            }

            try
            {
                var nameWithoutDate = System.IO.Path.GetFileNameWithoutExtension(path).Substring(11);
                return System.IO.Path.GetFileNameWithoutExtension(nameWithoutDate);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse title");
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
