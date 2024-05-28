using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiAccount : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Account";
        internal static readonly List<string> FIELDS = new List<string> {
            "Domain",
            "Name",
            "Caption",
            "Description",
            "LocalAccount",
            "SID",
            "SIDType",
            "Status"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
