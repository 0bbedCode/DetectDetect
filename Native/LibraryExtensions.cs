using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic.CompilerServices;

namespace DetectDetect.Native {
    internal static class LibraryExtensions {
        public static T BindDelegate<T>(this Library lib, string methodName, bool setLastError = true, CharSet cSet = CharSet.Auto) {
            var mth = PInvoke.GenerateDefFromDelegate<T>(methodName, lib.Name, cSet, setLastError);
            return Conversions.ToGenericParameter<T>(Delegate.CreateDelegate(typeof(T), mth, false));
        }
    }
}
