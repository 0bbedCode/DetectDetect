using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiLogicalDisks : IWmiObject
    {
        private List<WmiLogicalDisk> _disks = new List<WmiLogicalDisk>();

        public static WmiLogicalDisks Get()
        {
            WmiLogicalDisks disks = new WmiLogicalDisks();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_LogicalDisk");
            if (objs == null) return disks;
            disks.Init(objs);
            return disks;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiLogicalDisk p = new WmiLogicalDisk();
                p.Init(obj);
                _disks.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiLogicalDisk P in _disks)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
