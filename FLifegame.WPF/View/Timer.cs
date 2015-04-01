using FLifegame.Common;
using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace FLifegame.View
{
    public class Timer : ITimer, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<ITimer> Tick;

        readonly long timerTicks = SecondToInterval(0.1);
        readonly DispatcherTimer dispatcherTimer = null;

        public double Interval
        {
            get { return SecondFromInterval(dispatcherTimer.Interval.Ticks); }
            set {
                if (value != Interval && value > 0.0) {
                    dispatcherTimer.Interval = new TimeSpan(SecondToInterval(value));
                    PropertyChanged.Raise(this);
                }
            }
        }

        public bool IsRunning
        {
            get { return dispatcherTimer.IsEnabled; }
            set {
                if (value != IsRunning) {
                    if (value) dispatcherTimer.Start();
                    else dispatcherTimer.Stop();
                    PropertyChanged.Raise(this);
                }
            }
        }

        public Timer()
        {
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal) { Interval = new TimeSpan(timerTicks) };
            dispatcherTimer.Tick += (sender, e) => OnTick();
        }

        static long SecondToInterval(double second)
        { return (long)Math.Round(second * (1000L * 1000L * 10L)); }

        static double SecondFromInterval(long interval)
        { return (double)interval / (1000L * 1000L * 10L); }

        void OnTick()
        {
            if (Tick != null)
                Tick(this);
        }
    }
}
