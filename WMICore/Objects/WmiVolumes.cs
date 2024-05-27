using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiVolumes : IWmiObject
    {
        private List<WmiVolume> _volumes = new List<WmiVolume>();

        public static WmiVolumes Get()
        {
            WmiVolumes servs = new WmiVolumes();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_Volume");
            if (objs == null) return servs;
            servs.Init(objs);
            return servs;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiVolume p = new WmiVolume();
                p.Init(obj);
                _volumes.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiVolume P in _volumes)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
