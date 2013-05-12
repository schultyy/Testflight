using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testflight.Core.Build
{
    public class TargetResult : ITargetResult
    {
        public Exception Exception { get; set; }
        public ResultCode Result { get; set; }
        public string Component { get; set; }
    }
}
