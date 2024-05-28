using DetectDetect.Info;
using DetectDetect.Reporter;
using DetectDetect.Utils;
using DetectDetect.WMICore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect {
    internal class ReportWriter {
        internal static readonly string DIVIDER = "====================================================";
        private StringBuilder _report = new StringBuilder();
        public ReportWriter WriteTagHeader(string tag) {
            _report.Append($"==================[{tag}]==================");
            return this;
        }

        public ReportWriter Write(string message) { return Write(message, null); }
        public ReportWriter Write(string message, string header) {
            if (header != null)
                _report.Append(header);
            _report.Append(message);
            return this;
        }

        public ReportWriter NewLine() { return NewLine(1); }
        public ReportWriter NewLine(int count) {
            if (count == 1)
                _report.Append("\n");
            else if (count > 0) {
                for (int i = 0; i < count; i++)
                    _report.Append("\n");
            } return this;
        }

        public ReportWriter WriteHeader(string tag) {
            WriteDivider()
                .NewLine()
                .WriteTagHeader(tag)
                .NewLine()
                .WriteDivider()
                .NewLine(2);
            return this;
        }

        public ReportWriter WriteDivider() {
            _report.Append(DIVIDER);
            return this;
        }

        public ReportWriter WriteProcesses() {
            WriteHeader("Processes");
            ProcessReport.WriteProcesses(this);
            return this;
        }

        public ReportWriter WriteRegReport() {
            WriteHeader("Registry Test");
            RegReport.WriteRegReport(this);
            return this;
        }

        public ReportWriter WriteFilesReport() {
            WriteHeader("File Report");
            FileReport.WriteFileReport(this);
            return this;
        }

        public ReportWriter WriteEnvironmentVariables() {
            try {
                IDictionary environmentVariables = Environment.GetEnvironmentVariables();
                WriteHeader("System Environment Variables");
                foreach(DictionaryEntry v in environmentVariables) 
                    Write($"[{v.Key}][{v.Value}]").NewLine();
                
            } catch (Exception e) {
                Console.WriteLine("Env Vars Error: " + e);
            } return this;
        }

        public ReportWriter WriteBasicInfo() {
            WriteHeader("Basic Info")
                .Write($"[Username][{Sys.Username}]").NewLine()
                .Write($"[MachineName][{Sys.MachineName}]").NewLine()
                .Write($"[Domain][{Sys.Domain}]").NewLine()
                .Write($"[ProcessorCount][{Sys.ProcessorCount}]").NewLine()
                .Write($"[Wallpaper][{Sys.Wallpaper}]").NewLine()
                .Write($"[CurrentFile][{Sys.GetCurrentFileName()}]").NewLine();
            return this;
        }

        public ReportWriter WriteSystemInfoCommand() {
            WriteHeader("SystemInfo Command");
            Write(ShellUtils.ExecuteCommand("systeminfo")).NewLine();
            return this;
        }

        public ReportWriter WriteWmiTests() {
            WriteHeader("WMI Tests");
            string[] win32Counts = new string[] { "CacheMemory", "PhysicalMemory", "MemoryDevice", "MemoryArray", "PortConnector", "SMBIOSMemory", "PerfFormattedData_Counters_ThermalZoneInformation", "Fan", "VoltageProbe", "OperatingSystem" };
            string[] cimCounts = new string[] { "Memory", "Sensor", "NumericSensor", "TemperatureSensor", "PhysicalConnector", "Slot" };
            foreach (string w in win32Counts) 
                Write($"[>] Win32_{w}  COUNT:[{WmiController.InstanceCountEx(w)}]").NewLine();
            
            foreach (string c in cimCounts) 
                Write($"[>] CIM_{c}  COUNT:[{WmiController.InstanceCountEx(c, false)}]").NewLine();
          
            return this;
        }

        public ReportWriter WriteSystemInformationClass() {
            WriteHeader(".NET System Information Class");
            SysInfoClassDumper.WriteToReport(this);
            return this;
        }

        public ReportWriter WriteNetCards() {
            WriteHeader("Network Cards");
            foreach (var n in NetCard.GetCards()) 
                Write(n.ToString()).NewLine();
            
            return this;
        }

        public ReportWriter WriteReportsWmi() {
            try {
                List<Type> wmiClasses = ReflectUtils.GetTypesInheritInterface(null, typeof(IWmiClassReader));
                if(wmiClasses.Count > 0) {
                    foreach(var wmiClass in wmiClasses) {
                        try {
                            IWmiClassReader instance = (IWmiClassReader)Activator.CreateInstance(wmiClass);
                            WriteDivider()
                                .NewLine()
                                .WriteTagHeader(instance.GetClassName())
                                .NewLine()
                                .WriteDivider()
                                .NewLine()
                                .Write(ShellUtils.ExecutePowershellWmi(instance.GetClassName()))
                                .NewLine()
                                .WriteDivider()
                                .NewLine(2);

                            IEnumerable<ManagementObject> objs = WmiController.GetObjects(instance.GetClassName());
                            if(objs != null && objs.Count() > 0) {
                                int count = 1;
                                foreach(var classObj in objs) {
                                    if(classObj != null) {
                                        try {
                                            Write($"==================[Instance <{instance.GetClassName()}> ({count})]==================").NewLine();
                                            count++;
                                            foreach (var fld in instance.GetFields()) 
                                                Write($"[{fld}][{classObj.ToString(fld)}]").NewLine();
                                            
                                        } catch(Exception exx) { Console.WriteLine("Error ToString or Something WMI... " + exx + " obj=" + wmiClass.FullName); }
                                    }
                                }
                            }
                        } catch(Exception ex) { Console.WriteLine("Error With WMI Object: " + ex + " obj=" + wmiClass.FullName); }
                    }
                }
            }catch(Exception e) { Console.WriteLine("Error writing WMI Report: " + e); }
            NewLine(2);
            return this;
        }

        public override string ToString() => _report.ToString();
    }
}
