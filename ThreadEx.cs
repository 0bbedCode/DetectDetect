using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetectDetect {
    internal delegate void del_onFinish(ThreadEx thr);
    internal class ThreadEx {
        internal static ThreadEx Create(ThreadStart start) => new ThreadEx(start);
        private ThreadStart _start;
        private event del_onFinish onFinishEvent;

        private Thread _internalThread;

        internal ThreadEx(ThreadStart start) {
            _start = start;
        }

        internal ThreadEx OnFinish(del_onFinish onFinish) {
            onFinishEvent += onFinish;
            return this;
        }

        internal ThreadEx AddToList(List<ThreadEx> list) {
            list.Add(this);
            return this;
        }

        internal ThreadEx Start() {
            new Thread(internal_try_start).Start();
            return this;
        }

        private void internal_try_start() {
            try {
                _start.Invoke();
            }catch(Exception e) { Console.WriteLine(e.Message); }
            onFinishEvent?.Invoke(this);
        }
    }
}
