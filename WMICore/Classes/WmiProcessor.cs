using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiProcessor : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Processor";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Architecture",
            "AssetTag",
            "Availability",
            "Caption",
            "CpuStatus",
            "CurrentClockSpeed",
            "CurrentVoltage",
            "AddressWidth",
            "DataWidth",
            "Description",
            "ExtClock",
            "Family",
            "L2CacheSize",
            "L3CacheSize",
            "L3CacheSpeed",
            "Level", 
            "LoadPercentage", 
            "Manufacturer", 
            "MaxClockSpeed",
            "Name",
            "NumberOfCores",
            "NumberOfEnabledCore",
            "NumberOfLogicalProcessors",
            "PortNumber", 
            "ProcessorId",
            "ProcessorType",
            "Role",
            "SerialNumber", 
            "SocketDesignation",
            "Status",
            "StatusInfo",
            "SystemName",
            "ThreadCount",
            "UpdateMethod",
            "Version",
            "VirtualizationFirmwareEnabled",
            "VMMonitorModeExtensions"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
