using FLifegame.Common;
using System;
using System.ComponentModel;

namespace FLifegame.View
{
    class Timer : ITimer
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<ITimer> Tick;
        public event Action<ITimer> IntervalChanged;
        public event Action<ITimer> IsRunningChanged;

        System.Windows.Forms.Timer windowsTimer = new System.Windows.Forms.Timer();

        public Timer()
        { windowsTimer.Tick += OnTick; }

        void OnTick(object sender, EventArgs e)
        {
            if (Tick != null)
                Tick(this);
        }

        public double Interval
        {
            get { return windowsTimer.Interval / 1000.0; }
            set {
                if (value != Interval && value > 0.0) {
                    windowsTimer.Interval = (int)Math.Round(value * 1000.0);
                    if (IntervalChanged != null)
                        IntervalChanged(this);
                    PropertyChanged.Raise(this);
                }
            }
        }

        public bool IsRunning
        {
            get { return windowsTimer.Enabled;  }
            set {
                if (value != IsRunning) {
                    windowsTimer.Enabled = value;
                    if (IsRunningChanged != null)
                        IsRunningChanged(this);
                    PropertyChanged.Raise(this);
                }
            }
        }
    }
}
