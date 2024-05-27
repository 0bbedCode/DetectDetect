using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiDesktop : IWmiObject
    {
        internal string Name;
        internal string Wallpaper;


        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Name = obj.ToString(nameof(Name));
                Wallpaper = obj.ToString(nameof(Wallpaper));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Desktop]=============").Append("\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(Wallpaper)}][").Append(Wallpaper).Append("]\n\n")
                .ToString();
        }
    }
}
