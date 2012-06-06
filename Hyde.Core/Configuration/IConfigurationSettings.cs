using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyde.Core.Configuration
{
    public interface IConfigurationSettings
    {
        string Path { get; }
        string Source { get; }
        string Destination { get; }
        string Permalink { get; }
        IEnumerable<string> Exclude { get; }
    }
}
