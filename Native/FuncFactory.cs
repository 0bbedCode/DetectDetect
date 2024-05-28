using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    internal class FuncFactory {
        internal string FunctionName;
        internal string LibraryName;

        internal Pairs<int, MethodInfo> Functions = new Pairs<int, MethodInfo>();

        internal FuncFactory(string libraryName, string functionName) {
            FunctionName = functionName;
            LibraryName = libraryName;
        }

        internal MethodInfo GetMethod(
            CharSet set,
            bool setLastError,
            Type returnType,
            params object[] args) {

            var pTys = PInvokeUtils.GetObjectArgTypes(args);
            var hsh = PInvokeUtils.GenerateHash(set, setLastError, returnType, pTys);

            if (Functions.GetValue(hsh, out var val)) 
                return val;
            
            MethodInfo mth = !setLastError ?
                PInvoke.GenerateDef(FunctionName, LibraryName, returnType, pTys, set) :
                PInvoke.GenerateDefEx(FunctionName, LibraryName, returnType, pTys, set);

            if (mth is null)
                return null;

            Functions.Append(hsh, mth);
            return mth;
        }
    }
}
