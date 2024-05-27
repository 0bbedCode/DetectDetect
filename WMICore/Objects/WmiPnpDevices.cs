using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiPnpDevices : IWmiObject
    {
        private List<WmiPnpDevice> _devices = new List<WmiPnpDevice>();

        public static WmiPnpDevices Get()
        {
            WmiPnpDevices devices = new WmiPnpDevices();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_PnPEntity");
            if (objs == null) return devices;
            devices.Init(objs);
            return devices;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiPnpDevice p = new WmiPnpDevice();
                p.Init(obj);
                _devices.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiPnpDevice P in _devices)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
