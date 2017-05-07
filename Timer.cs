using System;
using System.ComponentModel;
using System.Threading;

namespace Micro.ThreadTimer {
    public class Timer {
        private BackgroundWorker backWork;
        private bool runn;
        public event Action Tick;

        public bool Running {
            get { return runn; }
            set {
                if (value != runn) {
                    if (value) {
                        backWork = new BackgroundWorker();
                        backWork.WorkerSupportsCancellation = true;
                        backWork.DoWork += new DoWorkEventHandler(background);
                        backWork.RunWorkerAsync();
                    } else {
                        backWork.CancelAsync();
                        backWork.Dispose();
                        backWork = null;
                    }
                    runn = value;
                }
            }
        }
        public int Intervall { get; set; }

        public Timer(int intervall, bool running = false) {
            Intervall = intervall;
            Running = running;
        }
        public void start() {
            Running = true;
        }
        public void stop() {
            Running = false;
        }

        private void background(object sender, DoWorkEventArgs e) {
            while (!e.Cancel) {
                Thread.Sleep(Intervall);
                Tick();
            }
        }
    }
}
