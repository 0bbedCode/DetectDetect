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
    internal class WmiVolume : IWmiObject
    {
        internal string DeviceID;
        internal string Capacity;
        internal string Caption;
        internal string FreeSpace;
        internal string Label;
        internal string Name;
        internal string SerialNumber;

        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                DeviceID = obj.ToString(nameof(DeviceID));
                Capacity = obj.ToString(nameof(Capacity));
                Caption = obj.ToString(nameof(Caption));
                FreeSpace = obj.ToString(nameof(FreeSpace));
                Label = obj.ToString(nameof(Label));
                Name = obj.ToString(nameof(Name));
                SerialNumber = obj.ToString(nameof(SerialNumber));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Volumes]=============").Append("\n")
                .Append($"[{nameof(DeviceID)}][").Append(DeviceID).Append("]\n")
                .Append($"[{nameof(Capacity)}][").Append(Capacity).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(FreeSpace)}][").Append(FreeSpace).Append("]\n")
                .Append($"[{nameof(Label)}][").Append(Label).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(SerialNumber)}][").Append(SerialNumber).Append("]\n\n")
                .ToString();
        }
    }
}
