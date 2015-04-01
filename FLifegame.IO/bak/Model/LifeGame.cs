using FLifegame.Common;
using System.ComponentModel;

namespace FLifegame.Model
{
    public abstract class Lifegame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Board board;
        ITimer timer = null;
        //CellDataList cellDataList = null;

        public Board Board
        {
            get { return board; }
        }

        public ITimer Timer
        {
            get { return timer; }
            set  {
                if (value != timer) {
                    timer = value;
                    timer.Tick -= OnTick;
                    timer.Tick += OnTick;
                    PropertyChanged.Raise(this);
                }
            }
        }

        public CellDataList CellDataList { get; protected set; }

        public Lifegame()
        { board = new Board(); }

        public Lifegame(Dimensions dimensions)
        { board = new Board(dimensions); }

        public bool SetFromList(string callDataName)
        {
            CellData cellData;
            if (CellDataList != null && CellDataList.TryGetValue(callDataName, out cellData)) {
                Board.Set(cellData);
                return true;
            }
            return false;
        }

        public void Start()
        {
            if (Timer != null)
                Timer.IsRunning = true;
        }

        public void Stop()
        {
            if (Timer != null)
                Timer.IsRunning = false;
        }

        void OnTick(ITimer timer)
        { Board.Next(); }
    }
}
