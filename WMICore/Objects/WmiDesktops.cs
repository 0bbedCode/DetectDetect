using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiDesktops : IWmiObject
    {
        private List<WmiDesktop> _desktops = new List<WmiDesktop>();

        public static WmiDesktops Get()
        {
            WmiDesktops desktops = new WmiDesktops();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_Desktop");
            if (objs == null) return desktops;
            desktops.Init(objs);
            return desktops;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiDesktop p = new WmiDesktop();
                p.Init(obj);
                _desktops.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiDesktop P in _desktops)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
