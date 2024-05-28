using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiBaseBoard : IWmiClassReader {
        internal static readonly string CLASS = "Win32_BaseBoard ";
        internal static readonly List<string> FIELDS = new List<string> {
            "Tag",
            "Caption",
            "Description",
            "HostingBoard",
            "HotSwappable",
            "Manufacturer",
            "Name",
            "PoweredOn",
            "Product",
            "Removable",
            "Replaceable",
            "RequiresDaughterBoard",
            "SerialNumber",
            "Version"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
