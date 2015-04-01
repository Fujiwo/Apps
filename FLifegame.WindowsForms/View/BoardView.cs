using FLifegame.Common;
using FLifegame.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FLifegame.View
{
    public partial class BoardView : UserControl
    {
        static readonly Color offColor = Color.LightSteelBlue;
        static readonly Brush onBrush = Brushes.BlueViolet;
        static readonly Brush offBrush = Brushes.LightSteelBlue;
        static readonly Pen linePen = Pens.Gray;

        Board board = null;

        public Board Board {
            get { return board; }
            set {
                if (value != board) {
                    board = value;
                    board.CellChanged -= OnCellChanged;
                    board.CellChanged += OnCellChanged;
                }
            }
        }

        public BoardView()
        {
            InitializeComponent();
            SetStyle();
        }

        void OnCellChanged(Board board, Position position)
        {
            using (var graphics = CreateGraphics())
                RerawCell(graphics, position);
        }

        void OnPaint(object sender, PaintEventArgs e)
        {
            DrawBackground(e.Graphics);
            DrawCells(e);
            DrawLines(e.Graphics);
        }

        void SetStyle()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        void DrawBackground(Graphics graphics)
        { graphics.Clear(offColor); }

        void DrawCells(PaintEventArgs e)
        {
            if (Board != null)
                Board.Dimensions.Times(position => DrawCell(e.Graphics, position));
        }

        void DrawCell(Graphics graphics, Position position)
        {
            if (Board != null && Board[position].On)
                RerawCell(graphics, position);
        }

        void RerawCell(Graphics graphics, Position position)
        {
            if (Board != null)
                DrawCell(graphics, Board.GetCellElementPosition(Size.ToCommon(), position).FromCommon(), Board[position].On);
        }

        RectangleF DeflateRectangle(RectangleF rectangle)
        {
            var temporaryRectangle = rectangle;
            temporaryRectangle.Inflate(new SizeF(-1.0f, -1.0f));
            return temporaryRectangle;
        }

        void DrawCell(Graphics graphics, RectangleF position, bool on = true)
        { graphics.FillRectangle(on ? onBrush : offBrush, DeflateRectangle(position)); }

        void DrawLines(Graphics graphics)
        {
            if (Board == null)
                return;
            (Board.Dimensions.Height + 1).Times(index => DrawHorizontalLine(graphics, index));
            (Board.Dimensions.Width + 1).Times(index => DrawVerticalLine(graphics, index));
        }

        void DrawHorizontalLine(Graphics graphics, int index)
        {
            if (Board != null)
                DrawLine(graphics, Board.GetHorizontalLineElementPosition(Size.ToCommon(), index).FromCommon());
        }

        void DrawVerticalLine(Graphics graphics, int index)
        {
            if (Board != null)
                DrawLine(graphics, Board.GetVerticalLineElementPosition(Size.ToCommon(), index).FromCommon());
        }

        //void DrawLine(Graphics graphics, double x1, double y1, double x2, double y2)
        //{ DrawLine(graphics, new Line { Start = new PointF(x: (float)x1, y: (float)y1), End = new PointF(x: (float)x2, y: (float)y2) }); }

        void DrawLine(Graphics graphics, Line line)
        { graphics.DrawLine(linePen, line.Start, line.End); }

        void OnSizeChanged(object sender, EventArgs e)
        { Invalidate(); }

        void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (Board == null)
                return;

            var position = Board.HitTest(Size.ToCommon(), e.Location.ToCommon());
            Board[position].Invert();
        }
    }
}
