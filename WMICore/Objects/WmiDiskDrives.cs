using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiDiskDrives : IWmiObject
    {
        private List<WmiDiskDrive> _drives = new List<WmiDiskDrive>();

        public static WmiDiskDrives Get()
        {
            WmiDiskDrives drives = new WmiDiskDrives();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_DiskDrive");
            if (objs == null) return drives;
            drives.Init(objs);
            return drives;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiDiskDrive p = new WmiDiskDrive();
                p.Init(obj);
                _drives.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiDiskDrive P in _drives)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
