namespace FLifegame.Common
{
    public static class PrimitiveExtensions
    {
        public static bool IsInRange(this Position position, Dimensions dimensions)
        { return position.X.XIsInRange(dimensions) && position.Y.YIsInRange(dimensions); }

        public static bool XIsInRange(this int x, Dimensions dimensions)
        { return x.IsInRange(0, dimensions.Width - 1); }

        public static bool YIsInRange(this int y, Dimensions dimensions)
        { return y.IsInRange(0, dimensions.Height - 1); }
    }

    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsValid
        {
            get { return Width > 0 && Height > 0; }
        }

        public static Dimensions operator -(Dimensions dimensions1, Dimensions dimensions2)
        {
            return new Dimensions {
                Width  = dimensions1.Width  - dimensions2.Width ,
                Height = dimensions1.Height - dimensions2.Height
            };
        }

        public static Dimensions operator /(Dimensions dimensions, int number)
        {
            return new Dimensions {
                Width = dimensions.Width / number,
                Height = dimensions.Height / number
            };
        }

        public static Dimensions operator ++(Dimensions dimensions)
        {
            Dimensions result = dimensions;
            dimensions.Width ++;
            dimensions.Height++;
            return result;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Position operator +(Position position, Dimensions dimensions)
        {
            return new Position {
                X = position.X + dimensions.Width ,
                Y = position.Y + dimensions.Height
            };
        }

        public static Dimensions operator -(Position position1, Position position2)
        {
            return new Dimensions {
                Width  = position1.X - position2.X,
                Height = position1.Y - position2.Y
            };
        }

        //public static Dimensions operator +(Position position1, Position position2)
        //{
        //    return new Dimensions {
        //        Width = position1.X - position2.X,
        //        Height = position1.Y - position2.Y
        //    };
        //}
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Size
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class Rectangle
    {
        public Point Point { get; set; }
        public Size Size { get; set; }
    }
}
