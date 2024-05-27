using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiOperatingSystem : IWmiObject
    {

        internal static WmiOperatingSystem Get() { 
            var os = new WmiOperatingSystem();
            var insts = WmiController.GetObjects("Win32_OperatingSystem");
            if(insts != null)
            {
                os.Init(insts.First());
            }

            return os;
        }

        internal string BootDevice;
        internal string BuildNumber;
        internal string Caption;
        internal string CountryCode;
        internal int CurrentTimeZone;
        internal bool Debug;
        internal int FreePhysicalMemory;
        internal string Manufacturer;
        internal string Name;
        internal int NumberOfUsers;
        internal string Organization;
        internal string RegisteredUser;
        internal string SerialNumber;
        internal string SystemDevice;
        internal string SystemDirectory;
        internal string Version;
        internal string WindowsDirectory;

        internal string InstallDate;
        internal string LastBootUpTime;
        internal string LocalDateTime;

        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                BootDevice = obj.ToString(nameof(BootDevice));
                BuildNumber = obj.ToString(nameof(BuildNumber));
                Caption = obj.ToString(nameof(Caption));
                CountryCode = obj.ToString(nameof(CountryCode));
                CurrentTimeZone = obj.ToInt(nameof(CurrentTimeZone));
                Debug = obj.ToBoolean(nameof(Debug));
                FreePhysicalMemory = obj.ToInt(nameof(FreePhysicalMemory));
                Manufacturer = obj.ToString(nameof(Manufacturer));
                Name = obj.ToString(nameof(Name));
                NumberOfUsers = obj.ToInt(nameof(NumberOfUsers));
                Organization = obj.ToString(nameof(Organization));
                RegisteredUser = obj.ToString(nameof(RegisteredUser));
                SerialNumber = obj.ToString(nameof(SerialNumber));
                SystemDevice = obj.ToString(nameof(SystemDevice));
                SystemDirectory =   obj.ToString(nameof(SystemDirectory));
                Version = obj.ToString(nameof(Version));

                InstallDate = obj.ToString(nameof(InstallDate));
                LastBootUpTime = obj.ToString(nameof(LastBootUpTime));
                LocalDateTime = obj.ToString(nameof(LocalDateTime));

                WindowsDirectory = obj.ToString(nameof(WindowsDirectory));
            }catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append($"[{nameof(BootDevice)}][").Append(BootDevice).Append("]\n")
                .Append($"[{nameof(BuildNumber)}][").Append(BuildNumber).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(CountryCode)}][").Append(CountryCode).Append("]\n")
                .Append($"[{nameof(CurrentTimeZone)}][").Append(CurrentTimeZone).Append("]\n")
                .Append($"[{nameof(Debug)}][").Append(Debug).Append("]\n")
                .Append($"[{nameof(FreePhysicalMemory)}][").Append(FreePhysicalMemory).Append("]\n")
                .Append($"[{nameof(Manufacturer)}][").Append(Manufacturer).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(NumberOfUsers)}][").Append(NumberOfUsers).Append("]\n")
                .Append($"[{nameof(Organization)}][").Append(Organization).Append("]\n")
                .Append($"[{nameof(RegisteredUser)}][").Append(RegisteredUser).Append("]\n")
                .Append($"[{nameof(SerialNumber)}][").Append(SerialNumber).Append("]\n")
                .Append($"[{nameof(SystemDevice)}][").Append(SystemDevice).Append("]\n")
                .Append($"[{nameof(SystemDirectory)}][").Append(SystemDirectory).Append("]\n")
                .Append($"[{nameof(Version)}][").Append(Version).Append("]\n")

                .Append($"[{nameof(InstallDate)}][").Append(InstallDate).Append("]\n")
                .Append($"[{nameof(LastBootUpTime)}][").Append(LastBootUpTime).Append("]\n")
                .Append($"[{nameof(LocalDateTime)}][").Append(LocalDateTime).Append("]\n")

                .Append($"[{nameof(WindowsDirectory)}][").Append(WindowsDirectory).Append("]\n")
                .ToString();
        }
    }

}
