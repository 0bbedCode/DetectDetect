﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiProcess : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Process";
        internal static readonly List<string> FIELDS = new List<string> {
            "Caption",
            "CommandLine",
            "CreationDate",
            "CSName",
            "Description",
            "ExecutablePath",
            "HandleCount",
            "Name",
            "OSName",
            "ProcessId"};

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
