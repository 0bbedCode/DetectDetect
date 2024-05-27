using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiVideoControllers : IWmiObject
    {
        private List<WmiVideoController> _videoControllers = new List<WmiVideoController>();

        public static WmiVideoControllers Get()
        {
            WmiVideoControllers videos = new WmiVideoControllers();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_VideoController");
            if (objs == null) return videos;
            videos.Init(objs);
            return videos;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiVideoController p = new WmiVideoController();
                p.Init(obj);
                _videoControllers.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiVideoController P in _videoControllers)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
