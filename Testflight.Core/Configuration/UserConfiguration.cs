using Testflight.Core.Build;

namespace Testflight.Core.Configuration
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
