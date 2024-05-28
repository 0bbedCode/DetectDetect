using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiBiosElement : IWmiClassReader {
        internal static readonly string CLASS = "CIM_BIOSElement";
        internal static readonly List<string> FIELDS = new List<string> {
            "Name",
            "SoftwareElementID",
            "SoftwareElementState",
            "TargetOperatingSystem",
            "Version",
            "BIOSVersion",
            "Caption",
            "CurrentLanguage",
            "Description",
            "PrimaryBIOS",
            "RealeaseDate",
            "SerialNumber",
            "SMBIOSBIOSVersion",
            "SMBIOSMajorVersion",
            "SMBIOSMinorVersion",
            "SMBIOSPresent",
            "Status",
            "SystemBiosMajorVersion",
            "SystemBiosMinorVersion"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
