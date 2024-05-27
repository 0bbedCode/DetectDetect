using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore
{
    internal static class WmiConv
    {

        internal static bool ToBoolean(this ManagementObject ths, string name) {
            try
            {
                return Convert.ToBoolean(ths[name]);
            }
            catch { return false; }
        } 

        internal static int ToInt(this ManagementObject ths, string name)
        {
            try
            {
                return Convert.ToInt32(ths[name]);
            }
            catch { return -1337; }
        }

        internal static string ToString(this ManagementObject ths, string name)
        {
            try
            {
                return Convert.ToString(ths[name]);
            }
            catch { return "null"; }
        }
    }
}
