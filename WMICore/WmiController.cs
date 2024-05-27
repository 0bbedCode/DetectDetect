using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Management.Instrumentation;
using System.Management;

namespace DetectDetect.WMICore
{
    internal class WmiController
    {
        internal static int InstanceCount(string classLocation, bool isWin32 = true) {
            int count = 0;
            try
            {
                classLocation = FixClass(classLocation, isWin32);
                count = new ManagementClass(classLocation).GetInstances().Count;
            }
            catch { }
            return count;
        }

        internal static int InstanceCountEx(string classLocation, bool isWin32 = true)
        {
            try
            {
                classLocation = FixClass(classLocation, isWin32);
                IEnumerable<ManagementObject> members = (new ManagementObjectSearcher($"select * from {classLocation}").Get().Cast<ManagementObject>());
                return members.Count();
            }
            catch { }
            return 0;
        }

        internal static IEnumerable<ManagementObject> GetObjects(string classLocation, bool isWin32 = true)
        {
            try
            {
                classLocation = FixClass(classLocation, isWin32);
                return (new ManagementObjectSearcher($"select * from {classLocation}").Get().Cast<ManagementObject>());
            }
            catch { }
            return null;
        } 

        private static string FixClass(string classPath, bool isWin32 = true)
        {
            if (classPath.StartsWith("CIM")) return classPath;
            if (!isWin32) return "CIM_" + classPath;
            if (classPath.StartsWith("Win32")) return classPath;
            return "Win32_" + classPath;
        }
    }
}
