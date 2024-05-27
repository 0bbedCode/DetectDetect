using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiServices : IWmiObject
    {
        private List<WmiService> _services = new List<WmiService>();

        public static WmiServices Get()
        {
            WmiServices servs = new WmiServices();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_Service");
            if (objs == null) return servs;
            servs.Init(objs);
            return servs;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiService p = new WmiService();
                p.Init(obj);
                _services.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiService P in _services)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
