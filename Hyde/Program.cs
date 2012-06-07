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

            var configurationPath = args[0];
            var configurationSettings = new ConfigurationSettings(configurationPath);

            var contentIndex = new ContentIndex(configurationSettings);

            var contentProcessor = new ContentProcessor(contentIndex);
            contentProcessor.Process();

            return 0;
        }
    }
}
