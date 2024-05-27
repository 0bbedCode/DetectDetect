using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiCompterSystem : IWmiObject
    {

        internal static WmiCompterSystem Get()
        {
            var os = new WmiCompterSystem();
            var insts = WmiController.GetObjects("Win32_ComputerSystem");
            if (insts != null)
            {
                os.Init(insts.First());
            }

            return os;
        }

        internal string Name;
        internal string Caption;
        internal string Description;
        internal string DNSHostName;
        internal string Domain;
        internal string Manufacturer;
        internal string Model;
        internal string SystemFamily;
        internal string TotalPhysicalMemory;
        internal string UserName;
        internal string Workgroup;

        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Name = obj.ToString(nameof(Name));
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                DNSHostName = obj.ToString(nameof(DNSHostName));
                Domain = obj.ToString(nameof(Domain));
                Manufacturer = obj.ToString(nameof(Manufacturer));
                Model = obj.ToString(nameof(Model));
                SystemFamily = obj.ToString(nameof(SystemFamily));
                TotalPhysicalMemory = obj.ToString(nameof(TotalPhysicalMemory));
                UserName = obj.ToString(nameof(UserName));
                Workgroup = obj.ToString(nameof(Workgroup));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[ComputerSystem]=============").Append("\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(DNSHostName)}][").Append(DNSHostName).Append("]\n")
                .Append($"[{nameof(Domain)}][").Append(Domain).Append("]\n")
                .Append($"[{nameof(Manufacturer)}][").Append(Manufacturer).Append("]\n")
                .Append($"[{nameof(Model)}][").Append(Model).Append("]\n")
                .Append($"[{nameof(SystemFamily)}][").Append(SystemFamily).Append("]\n")
                .Append($"[{nameof(TotalPhysicalMemory)}][").Append(TotalPhysicalMemory).Append("]\n")
                .Append($"[{nameof(UserName)}][").Append(UserName).Append("]\n")
                .Append($"[{nameof(Workgroup)}][").Append(Workgroup).Append("]\n\n")
                .ToString();
        }
    }
}
