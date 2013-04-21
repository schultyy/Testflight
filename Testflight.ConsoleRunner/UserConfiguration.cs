using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testflight.Core;

namespace Testflight.ConsoleRunner
{
    public class UserConfiguration
    {
        public string Name { get; set; }

        public string SolutionFile { get; set; }

        public BuildConfiguration BuildConfiguration { get; set; }

        public string Target { get; set; }
    }
}
