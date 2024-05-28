using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect
{
    internal class ShellUtils {
        internal static string ExecuteCommand(string command) {
            try {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/C {command}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (!string.IsNullOrEmpty(error)) 
                    return error;
                
                process.Close();
                return output;
            } catch(Exception e) {
                Console.WriteLine("Error executing Command: " + e);
                return "";
            }
        }
    
        internal static string ExecutePowershellWmi(string className) {
            try {
                string powerShellCommand = $"Get-WmiObject -Class {className}";
                // Create a process to run PowerShell
                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-NoProfile -Command \"{powerShellCommand}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (!string.IsNullOrEmpty(error)) 
                    return error;
                
                process.Close();
                return output;
            } catch(Exception e) {
                Console.WriteLine("Error executing Powershell: " + e);
                return "";
            }
        }
    }
}
