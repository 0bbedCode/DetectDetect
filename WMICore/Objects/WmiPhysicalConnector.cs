using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiPhysicalConnector : IWmiObject
    {
        internal string Caption;
        internal string Description;
        internal string ExternalReferenceDesignator;
        internal string InternalReferenceDesignator;
        internal string Name;
        internal string SlotDesignation;

        public virtual void Init(IEnumerable<ManagementObject> elements) { }

        public virtual void Init(ManagementObject obj)
        {
            try
            {
                if (obj == null) return;
                Caption = obj.ToString(nameof(Caption));
                Description = obj.ToString(nameof(Description));
                ExternalReferenceDesignator = obj.ToString(nameof(ExternalReferenceDesignator));
                InternalReferenceDesignator = obj.ToString(nameof(InternalReferenceDesignator));
                Name = obj.ToString(nameof(Name));
                SlotDesignation = obj.ToString(nameof(SlotDesignation));

            }
            catch { }
        }

        public virtual string ConvertToString()
        {
            return new StringBuilder()
                .Append(" =============[Physicial Connector]=============").Append("\n")
                .Append($"[{nameof(Caption)}][").Append(Caption).Append("]\n")
                .Append($"[{nameof(Description)}][").Append(Description).Append("]\n")
                .Append($"[{nameof(ExternalReferenceDesignator)}][").Append(ExternalReferenceDesignator).Append("]\n")
                .Append($"[{nameof(InternalReferenceDesignator)}][").Append(InternalReferenceDesignator).Append("]\n")
                .Append($"[{nameof(Name)}][").Append(Name).Append("]\n")
                .Append($"[{nameof(SlotDesignation)}][").Append(SlotDesignation).Append("]\n")
                .ToString();
        }
    }
}
