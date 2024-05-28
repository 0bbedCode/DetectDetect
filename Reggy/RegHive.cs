using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reggy {
    internal class RegHive {
        internal string NameFull { private set; get; }
        internal string NameShort { private set; get; }
        internal string NameRegini { private set; get; }
        internal IntPtr HandleSafe { private set; get; }
        internal UIntPtr HandleUnsafe { private set; get; }
        internal RegistryHive EnumNet { private set; get; }

        internal static RegHive LOCAL_MACHINE = Create("HKEY_LOCAL_MACHINE", "HKLM", "\\Registry\\machine", new IntPtr(-2147483646), new UIntPtr(0x80000002u), RegistryHive.LocalMachine);
        internal static RegHive CURRENT_USER = Create("HKEY_CURRENT_USER", "HKCU", "\\Registry\\user", new IntPtr(-2147483647), new UIntPtr(0x80000001u), RegistryHive.CurrentUser);
        internal static RegHive USERS = Create("HKEY_USERS", "HKU", "\\Registry\\user", new IntPtr(-2147483645), new UIntPtr(0x80000003u), RegistryHive.Users);
        internal static RegHive CLASSES_ROOT = Create("HKEY_CLASSES_ROOT", "HKCR", "\\Registry\\root", new IntPtr(-2147483648), new UIntPtr(0x80000000u), RegistryHive.ClassesRoot);
        internal static RegHive CURRENT_CONFIG = Create("HKEY_CURRENT_CONFIG", "HKCC", "\\Registry\\config", new IntPtr(-2147483643), new UIntPtr(0x80000005u), RegistryHive.CurrentConfig);

        private static RegHive[] _hives = new RegHive[] { LOCAL_MACHINE, CURRENT_USER, USERS, CLASSES_ROOT, CURRENT_CONFIG };

        internal static RegHive GetHive(string key) {
            foreach(var hive in _hives) 
                if(hive.Equals(key))
                    return hive;

            return null;
        }

        internal static RegHive Create(string nameFull, string nameShort, string nameRegini, IntPtr handleSafe, UIntPtr handleUnsafe, RegistryHive enumNet) {
            RegHive hive = new RegHive();
            hive.NameFull = nameFull;
            hive.NameShort = nameShort;
            hive.NameRegini = nameRegini;
            hive.HandleSafe = handleSafe;
            hive.HandleUnsafe = handleUnsafe;
            hive.EnumNet = enumNet;
            return hive;
        }

        public override string ToString() => NameFull;
        public override bool Equals(object obj) {
            if(obj == null) return false;
            if(obj is  RegHive)
                return ((RegHive)obj).NameFull.Equals(NameFull);
            if(obj is string) {
                string s = (string)obj;
                return s.StartsWith(NameFull, StringComparison.OrdinalIgnoreCase) || s.StartsWith(NameShort, StringComparison.OrdinalIgnoreCase) || s.StartsWith(NameRegini, StringComparison.OrdinalIgnoreCase);
            }

            if (obj.GetType() == typeof(RegistryHive))
                return ((RegistryHive)obj) == EnumNet;

            return false;
        }
    }
}
