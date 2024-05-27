using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Xml.Linq;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiProcesses : IWmiObject
    {
        private List<WmiProcess> _processes = new List<WmiProcess>();

        public static WmiProcesses Get()
        {
            WmiProcesses process = new WmiProcesses();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_Process");
            if (objs == null) return process;
            process.Init(objs);
            return process;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements) { 
            foreach(ManagementObject obj in  elements)
            {
                WmiProcess p = new WmiProcess();
                p.Init(obj);
                if(p.ExecutablePath != null && !p.ExecutablePath.ToLower().Contains("system32"))
                    _processes.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj)  { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(WmiProcess P in _processes)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
