using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore
{
    public interface IWmiObject
    {
        void Init(IEnumerable<ManagementObject> elements);
        void Init(ManagementObject obj);
        string ConvertToString();
    }
}
