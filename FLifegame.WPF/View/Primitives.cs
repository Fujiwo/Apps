using System.Windows;
using Shapes = System.Windows.Shapes;
using Windows = System.Windows;

namespace FLifegame.View
{
    public static class PrimitiveExtensions
    {
        public static Common.Size ToCommon(this Windows.Size size)
        { return new Common.Size { Width = size.Width, Height = size.Height }; }

        public static Shapes.Rectangle FromCommon(this Common.Rectangle rectangle)
        { return new Shapes.Rectangle { Margin = new Thickness(rectangle.Point.X, rectangle.Point.Y, 0.0, 0.0), Width =  rectangle.Size.Width, Height =  rectangle.Size.Height }; }

        //public static Shapes.Line FromCommon(this Common.Line line)
        //{ return new Shapes.Line { X1 = line.Start.X,  Y1 = line.Start.Y, X2 = line.End.X, Y2 = line.End.Y }; }

        public static void FromCommon(this Common.Line fromLine, Shapes.Line toLine)
        {
            toLine.X1 = fromLine.Start.X;
            toLine.Y1 = fromLine.Start.Y;
            toLine.X2 = fromLine.End.X;
            toLine.Y2 = fromLine.End.Y;
        }
    }
}
