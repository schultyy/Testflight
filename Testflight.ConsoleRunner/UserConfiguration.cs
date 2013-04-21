using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testflight.Core;
using Testflight.Core.Build;

namespace Testflight.ConsoleRunner
{
    public class UserConfiguration
    {
        public string Name { get; set; }

        public string BaseDirectory { get; set; }

        public string SolutionFile { get; set; }

        public BuildConfiguration BuildConfiguration { get; set; }

        public string Target { get; set; }
    }
}
