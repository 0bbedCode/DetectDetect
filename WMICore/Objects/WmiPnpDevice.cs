using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiPnpDevice : IWmiObject
    {
        internal string Caption;
        internal string ClassGuid;
        internal string Description;
        internal string Manufacturer;
        internal string Name;
        internal string PNPClass;
        internal string PNPDeviceID;
        internal string Service;
        internal string SystemName;


        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Caption = obj.ToString(nameof(Caption));
                ClassGuid = obj.ToString(nameof(ClassGuid));
                Description = obj.ToString(nameof(Description));
                Manufacturer = obj.ToString(nameof(Manufacturer));
                Name = obj.ToString(nameof(Name));
                PNPClass = obj.ToString(nameof(PNPClass));
                PNPDeviceID = obj.ToString(nameof(PNPDeviceID));
                Service = obj.ToString(nameof(Service));
                SystemName = obj.ToString(nameof(SystemName));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[PnpDevice]=============").Append("\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(ClassGuid)}][").Append(ClassGuid).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(Manufacturer)}][").Append(Manufacturer).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(PNPClass)}][").Append(PNPClass).Append("]\n")
                .Append($"[{nameof(PNPDeviceID)}][").Append(PNPDeviceID).Append("]\n")
                .Append($"[{nameof(Service)}][").Append(Service).Append("]\n")
                .Append($"[{nameof(SystemName)}][").Append(SystemName).Append("]\n\n")
                .ToString();
        }
    }
}
