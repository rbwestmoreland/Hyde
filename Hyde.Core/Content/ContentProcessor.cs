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
            ContentIndex.Analyze();

            foreach (var other in ContentIndex.Other)
            {
                var relativePath = other.Path.Substring(ContentIndex.ConfigurationSettings.Source.Length);
                var outputPath = Path.Combine(ContentIndex.ConfigurationSettings.Destination, relativePath);
            }

            foreach (var post in ContentIndex.Posts)
            {
                
            }
        }
    }
}
