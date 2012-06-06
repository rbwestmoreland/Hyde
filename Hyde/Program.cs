﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var configurationPath = args[0];
            var configuration = new ConfigurationSettings(configurationPath);

            var contentProcessor = new ContentProcessor(configuration.Source, configuration.Destination);
            contentProcessor.Process();

            return 0;
        }
    }
}