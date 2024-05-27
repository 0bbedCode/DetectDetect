using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiPhysicalConnectors : IWmiObject
    {
        private List<WmiPhysicalConnector> _connectors = new List<WmiPhysicalConnector>();

        public static WmiPhysicalConnectors Get()
        {
            WmiPhysicalConnectors process = new WmiPhysicalConnectors();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("CIM_PhysicalConnector");
            if (objs == null) return process;
            process.Init(objs);
            return process;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiPhysicalConnector p = new WmiPhysicalConnector();
                p.Init(obj);
                _connectors.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiPhysicalConnector P in _connectors)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
