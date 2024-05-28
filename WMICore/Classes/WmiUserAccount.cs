using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiUserAccount : IWmiClassReader {
        internal static readonly string CLASS = "Win32_UserAccount";
        internal static readonly List<string> FIELDS = new List<string> {
            "Domain",
            "Name",
            "AccountType",
            "Caption",
            "Description",
            "Disabled",
            "FullName",
            "SID",
            "SIDType",
            "Status"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
