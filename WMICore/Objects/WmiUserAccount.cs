using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiUserAccount : IWmiObject
    {

        internal string Domain;
        internal string Name;
        internal string Caption;
        internal string Description;
        internal string FullName;
        internal string SID;

        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Domain = obj.ToString(nameof(Domain));
                Name = obj.ToString(nameof(Name));
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                FullName = obj.ToString(nameof(FullName));
                SID = obj.ToString(nameof(SID));
            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[User]=============").Append("\n")
                .Append($"[{nameof(Domain)}][").Append(Domain).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(FullName)}][").Append(FullName).Append("]\n")
                .Append($"[{nameof(SID)}][").Append(SID).Append("]\n\n")
                .ToString();
        }
    }
}
