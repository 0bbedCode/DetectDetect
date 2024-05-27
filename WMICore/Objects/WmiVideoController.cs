using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiVideoController : IWmiObject
    {
        internal string DeviceID;
        internal string AdapterCompatibility;
        internal string AdapterDACType;
        internal string Caption;
        internal string Description;
        internal string DriverDate;
        internal string DriverVersion;
        internal string InstalledDisplayDrivers;
        internal string Name;
        internal string PNPDeviceID;
        internal string VideoModeDescription;
        internal string VideoProcessor;


        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                DeviceID = obj.ToString(nameof(DeviceID));
                AdapterCompatibility = obj.ToString(nameof(AdapterCompatibility));
                AdapterDACType = obj.ToString(nameof(AdapterDACType));
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                DriverDate = obj.ToString(nameof(DriverDate));
                DriverVersion = obj.ToString(nameof(DriverVersion));
                InstalledDisplayDrivers = obj.ToString(nameof(InstalledDisplayDrivers));
                Name = obj.ToString(nameof(Name));
                PNPDeviceID = obj.ToString(nameof(PNPDeviceID));
                VideoModeDescription = obj.ToString(nameof(VideoModeDescription));
                VideoProcessor = obj.ToString(nameof(VideoProcessor));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Video Controllers]=============").Append("\n")
                .Append($"[{nameof(DeviceID)}][").Append(DeviceID).Append("]\n")
                .Append($"[{nameof(AdapterCompatibility)}][").Append(AdapterCompatibility).Append("]\n")
                .Append($"[{nameof(AdapterDACType)}][").Append(AdapterDACType).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(DriverDate)}][").Append(DriverDate).Append("]\n")
                .Append($"[{nameof(DriverVersion)}][").Append(DriverVersion).Append("]\n")
                .Append($"[{nameof(InstalledDisplayDrivers)}][").Append(InstalledDisplayDrivers).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(PNPDeviceID)}][").Append(PNPDeviceID).Append("]\n")
                .Append($"[{nameof(VideoModeDescription)}][").Append(VideoModeDescription).Append("]\n")
                .Append($"[{nameof(VideoProcessor)}][").Append(VideoProcessor).Append("]\n\n")
                .ToString();
        }
    }
}
