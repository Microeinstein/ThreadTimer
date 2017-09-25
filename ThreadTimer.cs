using System;
//using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
//using System.Timers;

namespace Micro.Utils{
    public class Clock {
        public event Action Tick;
        public int Intervall;
        bool _running, highset;
        Timer background;
        
        public bool Running => _running;

        public Clock(int intervall, Action tick) {
            Intervall = intervall;
            Tick += tick;
            //background = new Timer(Intervall);
            //background.Elapsed += (a, b) => work(a);
        }
        public void Start() {
            if (_running)
                return;
            lock (this) {
                _running = true;

                //backWork = new BackgroundWorker();
                //backWork.WorkerSupportsCancellation = true;
                //backWork.DoWork += bgHandler;
                //backWork.RunWorkerAsync();

                highset = false;
                background = new Timer(work, null, Intervall, Intervall);

                //background.Start();

                //bg = job();
            }
        }
        public void Stop() {
            if (!_running)
                return;
            lock (this) {
                _running = false;
                //backWork.DoWork -= bgHandler;
                //backWork.CancelAsync();
                //backWork.Dispose();
                //backWork = null;

                background.Dispose();
                background = null;

                //background.Stop();

                //bg?.Wait();
                //bg = null;
            }
        }

        void work(object a) {
            if (!highset) {
                Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
                highset = true;
            }
            if (_running)
                Tick?.Invoke();
        }
    }
}
