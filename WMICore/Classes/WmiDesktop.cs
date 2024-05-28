using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiDesktop : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Desktop";
        internal static readonly List<string> FIELDS = new List<string> {
            "Name",
            "IconTitleFaceName",
            "Wallpaper",
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
