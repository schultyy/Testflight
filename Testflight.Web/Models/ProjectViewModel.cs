using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestFlight.Model;

namespace Testflight.Web.Models
{
    public class ProjectViewModel
    {
        public string Name { get; set; }

        public Configuration[] Configurations { get; set; }
    }
}