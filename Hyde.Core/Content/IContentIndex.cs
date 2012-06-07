using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hyde.Core.Configuration;
using System.IO;

namespace Hyde.Core.Content
{
    public interface IContentIndex
    {
        IConfigurationSettings ConfigurationSettings { get; }
        IEnumerable<string> AllFiles { get; }
        IEnumerable<Include> Includes { get; }
        IEnumerable<Layout> Layouts { get; }
        IEnumerable<Post> Posts { get; }
        IEnumerable<ContentBase> Other { get; }

        void Analyze();
    }
}
