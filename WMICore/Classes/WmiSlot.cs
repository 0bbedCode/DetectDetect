using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiSlot : IWmiClassReader {
        internal static readonly string CLASS = "CIM_Slot";
        internal static readonly List<string> FIELDS = new List<string> {
            "Tag",
            "BusNumber",
            "Caption",
            "CurrentUsage",
            "Description",
            "DeviceNumber",
            "FunctionNumber",
            "MaxDataWidth",
            "Name",
            "PMESignal",
            "Shared",
            "SlotDesignation",
            "Status"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
