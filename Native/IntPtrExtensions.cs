using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    internal static class IntPtrExtensions {
        internal static bool IsValid(this IntPtr handle)
            => (handle != null && handle != IntPtr.Zero && handle != default);
    }
}
