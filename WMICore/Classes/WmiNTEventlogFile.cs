using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiNTEventlogFile : IWmiClassReader {
        internal static readonly string CLASS = "Win32_NTEventlogFile";
        internal static readonly List<string> FIELDS = new List<string> {
            "Name",
            "Archive",
            "Caption",
            "CreationDate",
            "Description",
            "Drive",
            "EightDotThreeFileName",
            "Encrypted",
            "Extension",
            "FileName",
            "FileSize",
            "FileType",
            "FSName",
            "Hidden",
            "InstallDate",
            "LastAccessed",
            "LastModified",
            "LogfileName",
            "MaxFileSize",
            "NumberOfRecords",
            "OverWritePolicy",
            "Path",
            "Readable",
            "Status",
            "System",
            "Writeable"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
