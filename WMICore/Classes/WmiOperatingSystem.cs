using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiOperatingSystem : IWmiClassReader {
        internal static readonly string CLASS = "Win32_OperatingSystem";
        internal static readonly List<string> FIELDS = new List<string> { 
            "BootDevice", 
            "BuildNumber", 
            "BuildType", 
            "Caption", 
            "CodeSet", 
            "CountryCode", 
            "CSName", 
            "CurrentTimeZone", 
            "Debug", 
            "Description", 
            "FreePhysicalMemory", 
            "FreeSpaceInPagingFiles", 
            "FreeVirtualMemory", 
            "InstallDate", 
            "LastBootUpTime", 
            "LocalDateTime", 
            "Locale", 
            "Manufacturer", 
            "MaxNumberOfProcesses", 
            "MaxProcessMemorySize", 
            "Name", 
            "NumberOfLicensedUsers",
            "NumberOfProcesses",
            "NumberOfUsers", 
            "Organization", 
            "OSArchitecture", 
            "OSProductSuite", 
            "OSType", 
            "PortableOperatingSystem", 
            "Primary", 
            "ProductType", 
            "RegisteredUser", 
            "SerialNumber", 
            "ServicePackMajorVersion", 
            "ServicePackMinorVersion", 
            "Status", 
            "SystemDevice", 
            "SystemDirectory", 
            "SystemDrive", 
            "TotalVirtualMemorySize", 
            "TotalVisibleMemorySize", 
            "Version", 
            "WindowsDirectory" };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
