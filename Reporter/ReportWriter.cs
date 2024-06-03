using DetectDetect.Info;
using DetectDetect.Native;
using DetectDetect.Reporter;
using DetectDetect.Utils;
using DetectDetect.WMICore;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetectDetect {
    internal class ReportWriter {
        internal static readonly string DIVIDER = "====================================================";
        private StringBuilder _report = new StringBuilder();

        private ConcurrentBag<Task<string>> _tasks = new ConcurrentBag<Task<string>>();


        public ReportWriter WriteTagHeader(string tag, StringBuilder sb = null) {
            var sbSelected = sb ?? _report;
            sbSelected.Append($"==================[{tag}]==================");
            return this;
        }

        public ReportWriter Write(string message, StringBuilder sb = null) { return Write(message, null, sb); }
        public ReportWriter Write(string message, string header, StringBuilder sb = null) {
            var sbSelected = sb ?? _report;
            if (header != null)
                sbSelected.Append(header);
            sbSelected.Append(message);
            return this;
        }

        public ReportWriter NewLine(StringBuilder sb = null) { return NewLine(1, sb); }
        public ReportWriter NewLine(int count, StringBuilder sb = null) {
            var sbSelected = sb ?? _report;
            if (count == 1)
                sbSelected.Append("\n");
            else if (count > 0) {
                for (int i = 0; i < count; i++)
                    sbSelected.Append("\n");
            } return this;
        }

        public ReportWriter WriteHeader(string tag, StringBuilder sb = null) {
            WriteDivider(sb)
                .NewLine(sb)
                .WriteTagHeader(tag, sb)
                .NewLine(sb)
                .WriteDivider(sb)
                .NewLine(2, sb);
            return this;
        }

        public ReportWriter WriteDivider(StringBuilder sb = null) {
            var sbSelected = sb ?? _report;
            sbSelected.Append(DIVIDER);
            return this;
        }

        public ReportWriter WriteSecurity() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                WriteHeader("Security Results", sb);
                try {
                    Write($"[BuildCommDCBAndTimeouts][{Win32.KERNEL32.Invoke<bool>("BuildCommDCBAndTimeouts", "jhl46745fghb", null, null)}]", sb).NewLine(2, sb);
                } catch { }
                SecurityReport.WriteSecurity(this, sb);
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteProcesses() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                WriteHeader("Processes", sb);
                ProcessReport.WriteProcesses(this, sb);
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteRegReport() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader("Registry Test", sb);
                    RegReport.WriteRegReport(this, sb);
                } catch { }
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteFilesReport() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader("File Report", sb);
                    FileReport.WriteFileReport(this, sb);
                } catch { }
                return sb.ToString();
            }));
            return this;
        }

        public ReportWriter WriteEnvironmentVariables() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    IDictionary environmentVariables = Environment.GetEnvironmentVariables();
                    WriteHeader("System Environment Variables", sb);
                    foreach (DictionaryEntry v in environmentVariables)
                        Write($"[{v.Key}][{v.Value}]", sb).NewLine(sb);
                } catch { }
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteBasicInfo() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader("Basic Info", sb)
                        .Write($"[Username][{Sys.Username}]", sb).NewLine(sb)
                        .Write($"[MachineName][{Sys.MachineName}]", sb).NewLine(sb)
                        .Write($"[Domain][{Sys.Domain}]", sb).NewLine(sb)
                        .Write($"[ProcessorCount][{Sys.ProcessorCount}]", sb).NewLine(sb)
                        .Write($"[Wallpaper][{Sys.Wallpaper}]", sb).NewLine(sb)
                        .Write($"[CurrentFile][{Sys.GetCurrentFileName()}]", sb).NewLine(sb);
                } catch { }
                return sb.ToString();
            }));  return this;
        }

        public ReportWriter WriteSystemInfoCommand() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader("SystemInfo Command", sb);
                    Write(ShellUtils.ExecuteCommand("systeminfo"), sb).NewLine(sb);
                } catch { }
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteWmiTests() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader("WMI Tests", sb);
                    string[] win32Counts = new string[] { "CacheMemory", "PhysicalMemory", "MemoryDevice", "MemoryArray", "PortConnector", "SMBIOSMemory", "PerfFormattedData_Counters_ThermalZoneInformation", "Fan", "VoltageProbe", "OperatingSystem" };
                    string[] cimCounts = new string[] { "Memory", "Sensor", "NumericSensor", "TemperatureSensor", "PhysicalConnector", "Slot" };
                    foreach (string w in win32Counts)
                        Write($"[>] Win32_{w}  COUNT:[{WmiController.InstanceCountEx(w)}]", sb).NewLine(sb);

                    foreach (string c in cimCounts)
                        Write($"[>] CIM_{c}  COUNT:[{WmiController.InstanceCountEx(c, false)}]", sb).NewLine(sb);

                } catch { }
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteSystemInformationClass() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader(".NET System Information Class", sb);
                    SysInfoClassDumper.WriteToReport(this, sb);
                } catch { }
                return sb.ToString();
            }));  return this;
        }

        public ReportWriter WriteNetCards() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    WriteHeader("Network Cards", sb);
                    foreach (var n in NetCard.GetCards())
                        Write(n.ToString(), sb).NewLine(sb);
                } catch { }
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteReportsWmiPowerShell() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    List<Type> wmiClasses = ReflectUtils.GetTypesInheritInterface(null, typeof(IWmiClassReader));
                    if (wmiClasses.Count > 0) {
                        foreach (var wmiClass in wmiClasses) {
                            try {
                                IWmiClassReader instance = (IWmiClassReader)Activator.CreateInstance(wmiClass);
                                WriteDivider(sb)
                                    .NewLine(sb)
                                    .WriteTagHeader(instance.GetClassName(), sb)
                                    .NewLine(sb)
                                    .WriteDivider(sb)
                                    .NewLine(sb)
                                    .Write(ShellUtils.ExecutePowershellWmi(instance.GetClassName()), sb)
                                    .NewLine(sb)
                                    .WriteDivider(sb)
                                    .NewLine(2, sb);
                            } catch (Exception ex) { Console.WriteLine("Error With WMI Object: " + ex + " obj=" + wmiClass.FullName); }
                        }
                    } NewLine(2, sb);
                } catch (Exception e) { Console.WriteLine("Error writing WMI Report: " + e); }
                return sb.ToString();
            })); return this;
        }

        public ReportWriter WriteReportsWmi() {
            _tasks.Add(Task.Run(() => {
                var sb = new StringBuilder();
                try {
                    List<Type> wmiClasses = ReflectUtils.GetTypesInheritInterface(null, typeof(IWmiClassReader));
                    if (wmiClasses.Count > 0) {
                        foreach (var wmiClass in wmiClasses) {
                            try {
                                IWmiClassReader instance = (IWmiClassReader)Activator.CreateInstance(wmiClass);
                                WriteDivider(sb)
                                    .NewLine(sb)
                                    .WriteTagHeader(instance.GetClassName(), sb)
                                    .NewLine(sb)
                                    .WriteDivider(sb)
                                    .NewLine(2, sb);

                                IEnumerable<ManagementObject> objs = WmiController.GetObjects(instance.GetClassName());
                                if (objs != null && objs.Count() > 0) {
                                    int count = 1;
                                    foreach (var classObj in objs) {
                                        if (classObj != null) {
                                            try {
                                                Write($"==================[Instance <{instance.GetClassName()}> ({count})]==================", sb).NewLine(sb);
                                                count++;
                                                foreach (var fld in instance.GetFields())
                                                    Write($"[{fld}][{classObj.ToString(fld)}]", sb).NewLine(sb);

                                            } catch (Exception exx) { Console.WriteLine("Error ToString or Something WMI... " + exx + " obj=" + wmiClass.FullName); }
                                        }
                                    }
                                }
                            } catch (Exception ex) { Console.WriteLine("Error With WMI Object: " + ex + " obj=" + wmiClass.FullName); }
                        }
                    } NewLine(2, sb);
                } catch (Exception e) { Console.WriteLine("Error writing WMI Report: " + e); }
                return sb.ToString();
            })); return this;
        }


        public ReportWriter Wait() {
            try {
                Task.WhenAll(_tasks).Wait();
                foreach (var tsk in _tasks)  _report.AppendLine(tsk.Result);
            } catch { }
            return this;
        }

        public override string ToString() => _report.ToString();
    }
}
