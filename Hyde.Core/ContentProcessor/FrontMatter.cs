using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace Hyde.Core.ContentProcessor
{
    internal class FrontMatter
    {
        public string Path { get; private set; }
        public string Title { get; private set; }
        public string Layout { get; private set; }
        public string Permalink { get; private set; }
        public bool Published { get; private set; }
        public string[] Categories { get; private set; }
        public string[] Tags { get; private set; }
        public IDictionary<string, string> Other { get; private set; }

        public FrontMatter(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            Path = path;
            Published = true;
            Categories = new string[] { };
            Tags = new string[] { };
            Other = new Dictionary<string, string>();
            Parse(path);
        }

        protected virtual void Parse(string path)
        {
            try
            {
                var yamlStream = new YamlStream();
                var fileText = File.ReadAllText(path);
                var regex = new Regex(@"^(---\s)([\s\S]+?)(\s---)");
                var yamlFrontMatter = regex.Match(fileText).Value;

                if (regex.IsMatch(fileText))
                {
                    try
                    {
                        using (var stringReader = new StringReader(yamlFrontMatter))
                        {
                            yamlStream.Load(stringReader);
                        }

                        var mapping = (YamlMappingNode)yamlStream.Documents[0].RootNode;

                        foreach (var node in mapping.Children)
                        {
                            switch (node.Key.ToString().ToLower())
                            {
                                case "title":
                                    Title = node.Value.ToString();
                                    break;
                                case "layout":
                                    Layout = node.Value.ToString();
                                    break;
                                case "permalink":
                                    Permalink = node.Value.ToString();
                                    break;
                                case "published":
                                    Published = !node.Value.ToString().Equals(bool.FalseString);
                                    break;
                                default:
                                    Other.Add(node.Key.ToString().ToLower(), node.Value.ToString().ToLower());
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static bool TryParse(string path, out FrontMatter frontMatter)
        {
            var success = false;
            frontMatter = null;

            try
            {
                frontMatter = new FrontMatter(path);
                success = true;
            }
            catch (Exception) { }

            return success;
        }
    }
}
