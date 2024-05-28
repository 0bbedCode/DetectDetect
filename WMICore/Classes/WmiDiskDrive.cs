using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiDiskDrive : IWmiClassReader {
        internal static readonly string CLASS = "Win32_DiskDrive";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Caption",
            "Description",
            "FirmwareRevision",
            "Index",
            "InterfaceType",
            "Manufacturer",
            "MediaLoaded",
            "MediaType",
            "Model",
            "Name",
            "Partitions",
            "PNPDeviceID",
            "SCSILogicalUnit",
            "SCSIBus",
            "SCSIPort",
            "SCSITargetId",
            "SerialNumber",
            "Signature",
            "Size",
            "Status",
            "TotalCylinders",
            "TotalTracks"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
