using FLifegame.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FLifegame.Model
{
    public class Board : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<Board, Position> CellChanged;
        public event Action<Board> CounterChanged;
        public event Action<Board> OnCounterChanged;

        readonly CellData cellData;
        readonly Random random = new Random();
        int counter = 0;

        public Dimensions Dimensions
        {
            get { return cellData.Dimensions; }
        }

        public int Counter
        {
            get { return counter; }
            private set {
                if (value != counter) {
                    counter = value;
                    PropertyChanged.Raise(this);
                    if (CounterChanged != null)
                        CounterChanged(this);
                }
            }
        }

        public int OnCounter
        {
            get { return CellData.OnCounter; }
        }

        public int Count
        {
            get { return cellData.Count; }
        }

        public CellData CellData
        {
            get { return cellData; }
        }

        public Cell this[int index]
        {
            get { return cellData[index]; }
        }

        public Cell this[Position position]
        {
            get { return cellData[position]; }
        }

        public Board()
        {
            cellData = new CellData();
            cellData.CellChanged += OnCellChanged;
            cellData.OnCounterChanged += OnOnCounterChanged;
        }

        public Board(Dimensions dimensions)
        {
            cellData = new CellData(dimensions);
            cellData.CellChanged += OnCellChanged;
            cellData.OnCounterChanged += OnOnCounterChanged;
        }

        public Rectangle GetCellElementPosition(Size panelSize, Position position)
        {
            return new Rectangle { Point = new Point { X = panelSize.Width * position.X / Dimensions.Width, Y = panelSize.Height * position.Y / Dimensions.Height },
                                       Size = new Size { Width = panelSize.Width / Dimensions.Width, Height = panelSize.Height / Dimensions.Height }};
        }

        public Line GetHorizontalLineElementPosition(Size panelSize, int index)
        {
            var y = panelSize.Height * index / Dimensions.Height;
            return new Line { Start = new Point { X = 0.0, Y = y }, End = new Point { X = panelSize.Width, Y = y } };
        }

        public Line GetVerticalLineElementPosition(Size panelSize, int index)
        {
            var x = panelSize.Width * index / Dimensions.Width;
            return new Line { Start = new Point { X = x, Y = 0.0 }, End = new Point { X = x, Y = panelSize.Height } };
        }

        public Position HitTest(Size panelSize, Point point)
        {
           var position = new Position { X = (int)Math.Floor(point.X * Dimensions.Width  / panelSize.Width ),
                                            Y = (int)Math.Floor(point.Y * Dimensions.Height / panelSize.Height) };
           if (!position.IsInRange(Dimensions))
               throw new ArgumentOutOfRangeException();
           return position;
        }

        public void Next()
        {
            var cloneCells = cellData.GetCloneCells();
            Next(cloneCells, cellData.Cells);
            cellData.SetCloneCells(cloneCells);
            Counter++;
        }

        public void Random()
        { RandomizeCells(GetRandomBoolean); }

        public void Clear()
        { cellData.Clear(); }

        public void Reset()
        { Counter = 0; }

        public void Set(CellData cellData)
        { this.cellData.SetToCenter(cellData); }

        void Next(IList<Cell> cloneCells, IList<Cell> cells)
        { Dimensions.ParallelTimes2(position => Next(position, cloneCells, cells)); }

        void Next(Position position, IList<Cell> cloneCells, IList<Cell> cells)
        { cellData.Get(cloneCells, position).On = Next(position, cells); }

        bool Next(Position position, IList<Cell> cells)
        {
            var livingCellCount = GetLivingCellCount(position, cells);
            return cellData.Get(cells, position).On ? livingCellCount == 2 || livingCellCount == 3 : livingCellCount == 3;
        }

        static int ToInteger(bool value)
        { return value ? 1 : 0; }

        bool IsLiving(Position position, IList<Cell> cells)
        { return cellData.IsInRange(position) && cellData.Get(cells, position).On; }

        int GetLivingCount(Position position, IList<Cell> cells)
        { return ToInteger(IsLiving(position, cells)); }

        IEnumerable<Position> GetNeighborCellPositions(Position position)
        {
            for (var x = position.X - 1; x <= position.X + 1; x++) {
                for (var y = position.Y - 1; y <= position.Y + 1; y++) {
                    if (x != position.X || y != position.Y)
                        yield return new Position { X = x, Y = y };
                }
            }
        }

        int GetLivingCellCount(Position position, IList<Cell> cells)
        { return GetNeighborCellPositions(position).ParallelSum<Position, int>(eachPosition => GetLivingCount(eachPosition, cells)); }

        void RandomizeCells(Func<bool> randomizer)
        { cellData.Cells.ParallelForEach((index, item) => cellData.Cells[index].On = randomizer()); }

        bool GetRandomBoolean()
        { return random.Next(2) == 0; }

        public Position IndexToPosition(int index)
        { return cellData.IndexToPosition(index); }

        void OnCellChanged(CellData data, Position position)
        {
            if (CellChanged != null)
                CellChanged(this, position);
        }

        void OnOnCounterChanged(CellData cellData)
        {
            if (OnCounterChanged != null)
                OnCounterChanged(this);
            PropertyChanged.Raise(this, () => OnCounter);
        }
    }
}
