using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiProcess : IWmiObject
    {
        internal string Caption;
        internal string CommandLine;
        internal string CSName;
        internal string Description;
        internal string ExecutablePath;
        internal string Name;
        internal string OSName;
        internal int ProcessId;

        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Caption = obj.ToString(nameof(Caption));
                CommandLine = obj.ToString(nameof(CommandLine));
                CSName = obj.ToString(nameof(CSName));
                Description = obj.ToString(nameof(Description));
                ExecutablePath = obj.ToString(nameof(ExecutablePath));
                Name = obj.ToString(nameof(Name));
                OSName = obj.ToString(nameof(OSName));
                ProcessId = obj.ToInt(nameof(ProcessId));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Process]=============").Append("\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(CommandLine)}][").Append(CommandLine).Append("]\n")
                .Append($"[{nameof(CSName)}][").Append(CSName).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(ExecutablePath)}][").Append(ExecutablePath).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(OSName)}][").Append(OSName).Append("]\n")
                .Append($"[{nameof(ProcessId)}][").Append(ProcessId).Append("]\n\n")
                .ToString();
        }
    }
}
