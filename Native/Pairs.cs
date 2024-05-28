using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    internal class Pairs<T1, T2> {
        internal T1[] High = new T1[1];
        internal T2[] Low = new T2[1];

        internal int Row = 0;
        internal int Count = 0;

        private readonly object lck = new object();

        internal void Append(T1 key, T2 value) {
            lock (lck) {
                if (Row < Count) {
                    var high = new T1[Count + 1];
                    var low = new T2[Count + 1];

                    Array.Copy(High, high, Count);
                    Array.Copy(Low, low, Count);
                    Row++;
                }

                High[Row] = key;
                Low[Row] = value;
                Count++;
            }
        }

        internal bool GetValue(T1 key, out T2 value) {
            lock (lck) {
                for (int i = 0; i < Count; i++) {
                    if (High[i].Equals(key)) {
                        value = Low[i];
                        return true;
                    }
                }

                value = default;
                return false;
            }
        }
    }
}
