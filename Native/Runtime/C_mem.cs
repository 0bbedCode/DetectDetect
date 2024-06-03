using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native.Runtime {
    internal unsafe class C_mem {
        public delegate void* _memcpy_del(void* dst, void* src, int count);     //memcpy
        public delegate void* _memcmp_del(void* srca, void* srcb, int count);   //memcmp
        public delegate void* _memmov_del(void* dest, void* src, int count);    //memmove
        public delegate void* _memset_del(void* dest, uint val, int size);      //memset

        public static readonly _memcpy_del cpy = Win32.Msvcrt.BindDelegate<_memcpy_del>("memcpy");
        public static readonly _memcmp_del cmp = Win32.Msvcrt.BindDelegate<_memcmp_del>("memcmp");
        public static readonly _memmov_del mov = Win32.Msvcrt.BindDelegate<_memmov_del>("memmove");
        public static readonly _memset_del set = Win32.Msvcrt.BindDelegate<_memset_del>("memset");
    }
}
