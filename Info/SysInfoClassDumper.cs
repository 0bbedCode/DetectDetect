using DetectDetect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectDetect.Info {
    internal class SysInfoClassDumper {
        internal static void WriteToReport(ReportWriter writer) {
            try {
                var props = ReflectUtils.GetPublicStaticProperties(typeof(SystemInformation));
                if(props.Count > 0) 
                    foreach(var p  in props) 
                        writer.Write($"[{p.Name}][{ReflectUtils.GetPropertyValue(p)}]").NewLine();
            }catch(Exception e) {
                Console.WriteLine("Failed to Write System Information Class to report: " + e); ;
            }
        }
    }
}
