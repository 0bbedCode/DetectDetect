using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore {
    internal interface IWmiClassReader {
        List<string> GetFields();
        string GetClassName();
    }
}
