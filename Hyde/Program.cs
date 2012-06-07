using System;
using Hyde.Core.Configuration;
using Hyde.Core.Content;

namespace Hyde
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Hyde requires arguments.");
                Console.WriteLine("Hyde.exe <configuration-path>");
                Console.WriteLine("Paths that contain spaces should be surrounded by double-quotes.");
                Console.ReadKey();
                return -1;
            }

            //Load configuration
            Console.WriteLine("Loading configuration...");
            var configurationPath = args[0];
            var configurationSettings = new ConfigurationSettings(configurationPath);
            Console.WriteLine("Loading configuration complete.");

            //Index content
            Console.WriteLine("Indexing source content...");
            var contentIndex = new ContentIndex(configurationSettings);
            contentIndex.Index();
            Console.WriteLine("Indexing source content complete.");

            //Process content
            Console.WriteLine("Processing source content...");
            var contentProcessor = new ContentProcessor(contentIndex);
            contentProcessor.Process();
            Console.WriteLine("Processing source content complete.");

            //Write to destination
            //todo

            return 0;
        }
    }
}
