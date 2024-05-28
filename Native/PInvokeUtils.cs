using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    public class PInvokeUtils {
        public static Type[] GetObjectArgTypes(object[] objs) {
            var argTys = new Type[objs.Length];
            for (int iArg = 0; iArg < objs.Length; iArg++) {
                var arg = objs[iArg];

                if (arg == null)
                    argTys[iArg] = typeof(Nullable);
                else if (arg.GetType() != typeof(Type))
                    argTys[iArg] = arg.GetType();
                else
                    argTys[iArg] = typeof(Type);

            }
            return argTys;
        }

        public static bool IsValidPtr(IntPtr ptr)
            => ptr != null && ptr != IntPtr.Zero && ptr != default && ptr != Win32.INVALID_HANDLE_VALUE;

        public static string CleanName(string functionName) {
            if (functionName.Last() == '\0')
                functionName = functionName.TrimEnd('\0');

            return functionName.Replace(" ", string.Empty);
        }

        public static Random rnd = new Random();
        public static string RandomUTF8String(int length) => new string(Enumerable.Repeat(RegularCharSet, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        public static string RegularCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx1234567890";

        public static ModuleBuilder GenRandomModule()
            => AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(RandomUTF8String(rnd.Next(10, 15))),
                AssemblyBuilderAccess.Run).DefineDynamicModule(RandomUTF8String(rnd.Next(10, 15)));

        public static Type[] GetDelegateArgTypes(MethodInfo method) {
            var prms = method.GetParameters();
            var tys = new Type[prms.Length];
            for (int i = 0; i < prms.Length; i++)
                tys[i] = prms[i].ParameterType;

            return tys;
        }

        public static int GenerateHash(CharSet set,
            bool setLastError,
            Type returnType,
            object[] args) => GenerateHash(set, setLastError, returnType, GetObjectArgTypes(args));

        public static int GenerateHash(
            CharSet set,
            bool setLastError,
            Type returnType,
            Type[] paramTypes) {

            return (int)set + (setLastError ? 0 : 1) + returnType.GetHashCode() + paramTypes.GetHashCode();
        }
    }
}
