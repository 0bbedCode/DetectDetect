using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiLogicalDisk : IWmiObject
    {
        internal string DeviceID;
        internal string Caption;
        internal string Description;
        internal string FreeSpace;
        internal string Name;
        internal string Size;
        internal string VolumeName;
        internal string VolumeSerialNumber;


        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                DeviceID = obj.ToString(nameof(DeviceID));
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                FreeSpace = obj.ToString(nameof(FreeSpace));
                Name = obj.ToString(nameof(Name));
                Size = obj.ToString(nameof(Size));
                VolumeName = obj.ToString(nameof(VolumeName));
                VolumeSerialNumber = obj.ToString(nameof(VolumeSerialNumber));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Logical Disk]=============").Append("\n")
                .Append($"[{nameof(DeviceID)}][").Append(DeviceID).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(FreeSpace)}][").Append(FreeSpace).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(Size)}][").Append(Size).Append("]\n")
                .Append($"[{nameof(VolumeName)}][").Append(VolumeName).Append("]\n")
                .Append($"[{nameof(VolumeSerialNumber)}][").Append(VolumeSerialNumber).Append("]\n\n")
                .ToString();
        }
    }
}
