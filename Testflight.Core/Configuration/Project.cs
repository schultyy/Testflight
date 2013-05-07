using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testflight.Core.Configuration
{
    public class Project
    {
        public string Name { get; set; }

        public UserConfiguration[] Configurations { get; set; }
    }
}
