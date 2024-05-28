using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiPhysicalConnector : IWmiClassReader {
        internal static readonly string CLASS = "CIM_PhysicalConnector";
        internal static readonly List<string> FIELDS = new List<string> {
            "Tag",
            "Caption",
            "Description",
            "ExternalReferenceDesignator",
            "InternalReferenceDesignator",
            "Name",
            "PortType"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
