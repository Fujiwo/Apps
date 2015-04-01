using System;
using System.ComponentModel;

namespace FLifegame.Common
{
    public interface ITimer : INotifyPropertyChanged
    {
        event Action<ITimer> Tick;

        double Interval { get; set; }
        bool IsRunning { get; set; }
    }
}
