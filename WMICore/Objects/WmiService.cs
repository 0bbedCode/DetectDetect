using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiService : IWmiObject
    {
        internal string Name;
        internal string Caption;
        internal string Description;
        internal string DisplayName;
        internal string PathName;
        internal int ProcessId;
        internal string StartName;


        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Name = obj.ToString(nameof(Name));
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                DisplayName = obj.ToString(nameof(DisplayName));
                PathName = obj.ToString(nameof(PathName));
                ProcessId = obj.ToInt(nameof(ProcessId));
                StartName = obj.ToString(nameof(StartName));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Service]=============").Append("\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(DisplayName)}][").Append(DisplayName).Append("]\n")
                .Append($"[{nameof(PathName)}][").Append(PathName).Append("]\n")
                .Append($"[{nameof(ProcessId)}][").Append(ProcessId).Append("]\n")
                .Append($"[{nameof(StartName)}][").Append(StartName).Append("]\n\n")
                .ToString();
        }
    }
}
