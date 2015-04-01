using FLifegame.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FLifegame.Model
{
    public class CellData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<CellData, Position> CellChanged;
        public event Action<CellData> OnCounterChanged;

        const int DefaultSize = 64;

        Dimensions dimensions = new Dimensions();
        Cell[] cells = null;

        public Dimensions Dimensions
        {
            get { return dimensions; }
            private set {
                if (value != dimensions) {
                    dimensions = value;
                    //InitializeCells();
                    PropertyChanged.Raise(this);
                }
            }
        }

        public IList<Cell> Cells
        {
            get { return cells; }
        }

        public int Count
        {
            get { return dimensions == null ? 0 : dimensions.Width * dimensions.Height; }
        }

        int onCounter = 0;

        public int OnCounter {
            get { return onCounter; }
            private set {
                if (value != onCounter) {
                    onCounter = value;
                    if (OnCounterChanged != null)
                        OnCounterChanged(this);
                    PropertyChanged.Raise(this);
                }
            }
        }

        public Cell this[int index]
        {
            get { return cells[index]; }
        }

        public Cell this[Position position]
        {
            get { return this[PositionToIndex(position)]; }
        }

        public CellData()  : this(new Dimensions { Width = DefaultSize, Height = DefaultSize })
        {}

        public CellData(Dimensions dimensions)
        {
            Dimensions = dimensions;
            InitializeCells();
        }

        public CellData(IList<bool> flags, Dimensions dimensions)
        {
            Dimensions = dimensions;
            InitializeCells(flags);
        }

        public void Clear()
        { Cells.ParallelForAll(cell => cell.On = false); }

        public Cell Get(IList<Cell> cells, Position position)
        { return cells[PositionToIndex(position)]; }

        public IList<Cell> GetCloneCells()
        { return EnumerateCloneCells().ToList(); }

        public void SetCloneCells(IList<Cell> cloneCells)
        { cloneCells.ParallelForEach((index, cell) => cells[index].On = cell.On); }

        public bool SetToCenter(CellData data)
        {
            var offset = (Dimensions - data.Dimensions) / 2;
            if (offset.Width < 0 || offset.Height < 0)
                return false;

            Clear();
            data.Dimensions.ParallelTimes(position => this[position + offset].On = data[position].On);
            return true;
        }

        public CellData ToMinimumData()
        {
            if (Dimensions.IsValid) {
                var onCellPositions = Dimensions.Select().Where(position => this[position].On).ToList();
                var onCellPositionXs = onCellPositions.Select(position => position.X).ToList();
                var onCellPositionYs = onCellPositions.Select(position => position.Y).ToList();
                var minimumPosition = new Position { X = onCellPositionXs.Min(), Y = onCellPositionYs.Min() };
                var maximumPosition = new Position { X = onCellPositionXs.Max(), Y = onCellPositionYs.Max() };

                var dimensions = maximumPosition - minimumPosition;
                dimensions++;
                if (dimensions.IsValid)
                    return new CellData(dimensions.Select(minimumPosition).Select(position => this[position].On).ToList(), dimensions);
            }
            return new CellData();

            //if (!Dimensions.IsValid)
            //    return new CellData();

            //var minimumPosition = new Position { X = Dimensions.Width, Y = Dimensions.Height };
            //var maximumPosition = new Position { X = -1, Y = -1 };

            //Dimensions.Times(position => {
            //    if (this[position].On) {
            //        if (position.X < minimumPosition.X) minimumPosition.X = position.X;
            //        if (position.X > maximumPosition.X) maximumPosition.X = position.X;
            //        if (position.Y < minimumPosition.Y) minimumPosition.Y = position.Y;
            //        if (position.Y > maximumPosition.Y) maximumPosition.Y = position.Y;
            //    }
            //});

            //var dimensions = new Dimensions {
            //    Width = maximumPosition.X - minimumPosition.X + 1,
            //    Height = maximumPosition.Y - minimumPosition.Y + 1
            //};

            //if (!dimensions.IsValid)
            //    return new CellData();

            //var flags = new List<bool>();
            //dimensions.Times(minimumPosition, position => flags.Add(this[position].On));
            //return new CellData(flags, dimensions);
        }

        public bool IsInRange(Position position)
        { return position.IsInRange(Dimensions); }

        public Position IndexToPosition(int index)
        { return new Position { X = index % Dimensions.Width, Y = index / Dimensions.Width }; }

        int PositionToIndex(Position position)
        { return position.Y * Dimensions.Width + position.X; }

        void InitializeCells()
        { InitializeCells(new bool[Count]); }

        void InitializeCells(IList<bool> flags)
        {
            OnCounter = 0;
            var count = flags.Count;
            cells = count <= 0 ? null : new Cell[count];
            flags.ParallelForEach((index, item) => {
                var cell = new Cell { On = item };
                if (item)
                    OnCounter++;
                cells[index] = cell;
                var position = IndexToPosition(index);
                cells[index].PropertyChanged += (sender, e) => OnCellChanged(position);
            });
        }

        void OnCellChanged(Position position)
        {
            if (this[position].On) OnCounter++;
            else                   OnCounter--;
            if (CellChanged != null)
                CellChanged(this, position);
        }

        IEnumerable<Cell> EnumerateCloneCells()
        {
            return cells.Select(cell => (Cell)cell.Clone());
            //foreach (var cell in cells)
            //    yield return (Cell)cell.Clone();
        }
    }
}
