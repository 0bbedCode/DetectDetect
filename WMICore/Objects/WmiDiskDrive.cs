using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiDiskDrive : IWmiObject
    {

        internal string DeviceID;
        internal string Caption;
        internal string Description;
        internal string FirmwareRevision;
        internal string InterfaceType;
        internal string Manufacturer;
        internal string Model;
        internal string Name;
        internal int Paritions;
        internal string PNPDeviceID;
        internal string SerialNumber;
        internal string Signature;
        internal string Size;


        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                DeviceID = obj.ToString(nameof(DeviceID));
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                FirmwareRevision = obj.ToString(nameof(FirmwareRevision));
                InterfaceType = obj.ToString(nameof(InterfaceType));
                Manufacturer = obj.ToString(nameof(Manufacturer));
                Model = obj.ToString(nameof(Model));
                Name = obj.ToString(nameof(Name));
                Paritions = obj.ToInt(nameof(Paritions));
                PNPDeviceID = obj.ToString(nameof(PNPDeviceID));
                SerialNumber = obj.ToString(nameof(SerialNumber));
                Signature = obj.ToString(nameof(Signature));
                Size = obj.ToString(nameof(Size));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Disk Drive]=============").Append("\n")
                .Append($"[{nameof(DeviceID)}][").Append(DeviceID).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(FirmwareRevision)}][").Append(FirmwareRevision).Append("]\n")
                .Append($"[{nameof(InterfaceType)}][").Append(InterfaceType).Append("]\n")
                .Append($"[{nameof(Manufacturer)}][").Append(Manufacturer).Append("]\n")
                .Append($"[{nameof(Model)}][").Append(Model).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(Paritions)}][").Append(Paritions).Append("]\n")
                .Append($"[{nameof(PNPDeviceID)}][").Append(PNPDeviceID).Append("]\n")
                .Append($"[{nameof(SerialNumber)}][").Append(SerialNumber).Append("]\n")
                .Append($"[{nameof(Signature)}][").Append(Signature).Append("]\n")
                .Append($"[{nameof(Size)}][").Append(Size).Append("]\n\n")
                .ToString();
        }
    }
}
