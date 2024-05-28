using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reggy {
    internal class RegKey : IDisposable {
        private static readonly string _wow64 = "Wow6432Node";

        private RegistryKey _baseKey;
        private RegistryKey _key;

        internal RegHive Hive;
        internal string Path;
        internal string Name;

        internal bool Is64 {
            get {
                if (!string.IsNullOrEmpty(Path) && Path.IndexOf(_wow64, 0, StringComparison.OrdinalIgnoreCase) != -1)
                    return false;
                else if (!string.IsNullOrEmpty(Name) && Name.IndexOf(_wow64, 0, StringComparison.OrdinalIgnoreCase) != -1)
                    return false;
                else return Environment.Is64BitProcess;
            }
        }

        internal RegKey(RegKey baseKey, string subKey, bool writable = false) {
            if(!subKey.Equals("...")) {
                _baseKey = baseKey._baseKey;
                Hive = baseKey.Hive;
                Name = subKey;
                if (!"...".Equals(baseKey.Name)) {
                    if ("...".Equals(baseKey.Path)) {
                        Path = baseKey.Name;
                    } else {
                        Path = baseKey.Path + "\\" + baseKey.Name;
                    }
                } else Path = "...";
                InternalOpen(writable);
            }
        }

        internal RegKey(string fullPath, bool writable = false) {
            InitKey(fullPath);
            InternalOpen(writable);
        }

        internal RegKey OpenSubkey(string subKey, bool writable = false) => new RegKey(this, subKey, writable);

        internal bool Exists() => _key != null;

        internal IEnumerable<RegKey> EnumKeys() {
            foreach (var s in GetSubkeyNames())
                if (!string.IsNullOrEmpty(s))
                    yield return new RegKey(this, s);
        }

        internal List<string> GetSubkeyNames() {
            List<string> names = new List<string>();
            try {
                if(_key != null)
                    names.AddRange(_key.GetSubKeyNames());
                else if(_baseKey != null)
                    names.AddRange(_baseKey.GetSubKeyNames());
            } catch { }
            return names;
        }

        internal Dictionary<string, string> GetAllValues() {
            Dictionary<string, string> vs = new Dictionary<string, string>();
            try {
                if(_key != null) {
                    foreach(var v in _key.GetValueNames()) 
                        vs.Add(v, GetValue(v));
                }
            }catch { }
            return vs;
        }

        internal bool SubKeyExists(string subKey) {
            try {
                if (_key != null) {
                    foreach (var s in _key.GetSubKeyNames())
                        if (s.Equals(subKey, StringComparison.OrdinalIgnoreCase))
                            return true;
                } 
                else if(_baseKey != null) {
                    foreach (var s in _baseKey.GetSubKeyNames())
                        if (s.Equals(subKey, StringComparison.OrdinalIgnoreCase))
                            return true;
                }
            } catch { }
            return false;
        }

        internal bool ValueExists(string valueName) {
            try {
                if(_key != null) {
                    foreach (var s in _key.GetValueNames())
                        if (s.Equals(valueName, StringComparison.OrdinalIgnoreCase))
                            return true;
                } else {
                    foreach (var s in _baseKey.GetValueNames())
                        if (s.Equals(valueName, StringComparison.OrdinalIgnoreCase))
                            return true;
                }
            } catch { }
            return false;
        }

        public string GetValue(string valueName) {
            try {
                if(_key != null) {
                    object value = _key.GetValue(valueName, "noValueButYesKey");
                    RegistryValueKind regVKind = _key.GetValueKind(valueName);
                    if (value.GetType() == typeof(string)) return value.ToString();
                    if (regVKind == RegistryValueKind.String || regVKind == RegistryValueKind.ExpandString) return value.ToString();
                    if (regVKind == RegistryValueKind.DWord) return Convert.ToString((int)value);
                    if (regVKind == RegistryValueKind.QWord) return Convert.ToString((long)value);
                    if (regVKind == RegistryValueKind.Binary) return StrUtils.BytesToHexString((byte[])value);
                    if (regVKind == RegistryValueKind.MultiString) return string.Join("", (string[])value);
                }
            } catch { }
            return "";
        }

        private void InternalOpen(bool writable) {
            if(Hive != null) {
                try {
                    if (_baseKey == null)
                        _baseKey = RegistryKey.OpenBaseKey(Hive.EnumNet, Is64 ? RegistryView.Registry64 : RegistryView.Registry32);
                    if (_baseKey != null) {
                        if(_key == null) {
                            StringBuilder sb = new StringBuilder();
                            bool hasPath = false;
                            if (!"...".Equals(Path)) {
                                sb.Append(Path);
                                hasPath = true;
                            }

                            if (!"...".Equals(Name)) {
                                if (hasPath) sb.Append("\\");
                                sb.Append(Name);
                            }

                            string pth = sb.ToString().Trim('\\');
                            if(!string.IsNullOrEmpty(pth))
                                _key = _baseKey.OpenSubKey(pth, writable);
                        }
                    }
                } catch { }
            }
        }

        private void InitKey(string fullPath) {
            if (string.IsNullOrEmpty(fullPath)) return;
            fullPath = fullPath.Trim(' ', '\\');
            Hive = RegHive.GetHive(fullPath);
            if(Hive != null) {
                if(fullPath.Contains("\\")) {
                    string[] split = fullPath.Split('\\');
                    if(split.Length > 1 && split.Length != 2) {
                        StringBuilder path = new StringBuilder();
                        for (int i = 1; i < split.Length -1; i++) {
                            path.Append(split[i]);
                            if(i != split.Length -1) {
                                path.Append("\\");
                            }
                        }

                        Path = path.ToString().Trim('\\');
                        Name = split[split.Length - 1];
                    }
                    else if(split.Length == 2) {
                        Path = "...";
                        Name = split[1];
                    }
                } else {
                    Path = "..";
                    Name = "..";
                }
            }
        }

        ~RegKey() {
            Dispose();
        }

        private bool IsDisposed = false;

        private SafeFileHandle SafeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public virtual void Dispose() {
            if (!IsDisposed) {
                try {
                    if (_baseKey != null) _baseKey.Dispose();
                    if (_key != null) _key.Dispose();
                } catch { }
                IsDisposed = true;
                SafeHandle.Dispose();
                GC.Collect();
                GC.SuppressFinalize(this);
            }
        }
    }
}
