using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    internal class PInvoke {
        private static readonly FieldInfo[] _dllImportFields = new FieldInfo[] {
            typeof(DllImportAttribute).GetField("SetLastError"),
            typeof(DllImportAttribute).GetField("CallingConvention"),
            typeof(DllImportAttribute).GetField("CharSet"),
            typeof(DllImportAttribute).GetField("EntryPoint"),
        };

        private static readonly ConstructorInfo _dllImportConstructor
            = typeof(DllImportAttribute).GetConstructor(new Type[] { typeof(string) });

        public static MethodInfo GenerateDef(
            string funcName,
            string libName,
            Type returnType,
            Type[] paramTypes,
            CharSet set,
            ModuleBuilder module = null) {

            //Generate a PInvoke Method without SetLastError

            if (module is null)
                module = PInvokeUtils.GenRandomModule();

            var method = module.DefinePInvokeMethod(
                funcName,
                libName,
                MethodAttributes.Static | MethodAttributes.Public | MethodAttributes.PinvokeImpl,
                CallingConventions.Standard,
                returnType, paramTypes,
                CallingConvention.Winapi, set);

            method.SetImplementationFlags(method.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
            module.CreateGlobalFunctions();
            return module.GetMethod(funcName);
        }

        public static MethodInfo GenerateDefEx(
            string funcName,
            string libName,
            Type returnType,
            Type[] paramTypes,
            CharSet set,
            ModuleBuilder module = null) {

            //Generate a PInvoke Method with SetLastError applied

            if (module is null)
                module = PInvokeUtils.GenRandomModule();

            var lstErrorAttribute = new CustomAttributeBuilder(
                _dllImportConstructor, new object[] { libName }, _dllImportFields,
                new object[] { true, CallingConvention.StdCall, set, funcName });

            var method = module.DefineGlobalMethod(
                funcName,
                MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
                returnType, paramTypes);

            method.SetCustomAttribute(lstErrorAttribute);
            method.SetImplementationFlags(method.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
            module.CreateGlobalFunctions();
            return module.GetMethod(funcName);
        }


        public static MethodInfo GenerateDefFromDelegate<TDelegate>(
            string funcName,
            string libName,
            CharSet set,
            bool setLastError,
            ModuleBuilder module = null) {

            //Generate a PInvoke Method from a Delegate

            var invoke = typeof(TDelegate).GetMethod("Invoke");

            if (module is null)
                module = PInvokeUtils.GenRandomModule();

            var typeBuilder = module.DefineType("InvokeType");
            var method = typeBuilder.DefinePInvokeMethod(
                  funcName,
                  libName,
                  MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
                  CallingConventions.Standard,
                  invoke.ReturnType,
                  PInvokeUtils.GetDelegateArgTypes(invoke),
                  CallingConvention.Winapi, set);

            if (setLastError) {
                var lstErrorAttribute = new CustomAttributeBuilder(
                    _dllImportConstructor, new object[] { libName }, _dllImportFields,
                    new object[] { true, CallingConvention.StdCall, set, funcName });

                method.SetCustomAttribute(lstErrorAttribute);
            }

            method.SetImplementationFlags(method.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
            return typeBuilder.CreateType().GetMethod(funcName);
        }
    }
}
