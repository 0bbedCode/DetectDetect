using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiBus : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Bus";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "BusNum",
            "BusType",
            "Caption",
            "Description",
            "Name"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
