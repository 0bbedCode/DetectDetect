using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    public class Library {
        public readonly string Name;
        private readonly Pairs<string, FuncFactory> Factorys = new Pairs<string, FuncFactory>();

        public Library(string libraryName) {
            if (string.IsNullOrEmpty(libraryName))
                return;

            Name = libraryName;
        }

        public T InvokeEx<T>(string functionName, params object[] args) => InvokeEx<T>(functionName, true, CharSet.Auto, args);
        public T InvokeEx<T>(string functionName, bool setLastError, CharSet set, params object[] args) {
            var mth = setLastError ?
                PInvoke.GenerateDefEx(functionName, Name, typeof(T), PInvokeUtils.GetObjectArgTypes(args), set) :
                PInvoke.GenerateDef(functionName, Name, typeof(T), PInvokeUtils.GetObjectArgTypes(args), set);

            return (T)mth.Invoke(null, args);
        }

        public T Invoke<T>(string functionName, params object[] args) => Invoke<T>(functionName, true, CharSet.Auto, args);
        public T InvokeA<T>(string functionName, params object[] args) => Invoke<T>(functionName, true, CharSet.Ansi, args);
        public T Invoke<T>(string functionName, bool setLastError, CharSet set, params object[] args) {
            if (!Factorys.GetValue(functionName, out FuncFactory fac))
                fac = new FuncFactory(Name, functionName);

            var mth = fac.GetMethod(set, setLastError, typeof(T), args);
            return (T)mth.Invoke(null, args);
        }
    }
}
