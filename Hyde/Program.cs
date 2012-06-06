using System;
using Hyde.Core.Configuration;
using Hyde.Core.ContentProcessor;

namespace Hyde
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Hyde requires arguments.");
                Console.WriteLine("Hyde.exe <configuration-file-path>");
                Console.WriteLine("Paths that contain spaces should be surrounded by double-quotes.");
                Console.ReadKey();
                return -1;
            }

            var configurationFilePath = args[0];
            var configuration = new ConfigurationSettings(configurationFilePath);

            var contentProcessor = new ContentProcessor(configuration);
            contentProcessor.Process();

            return 0;
        }
    }
}
