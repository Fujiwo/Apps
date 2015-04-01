using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Drawing = System.Drawing;
using Common = FLifegame.Common;
using System.Drawing;

namespace FLifegame.View
{
    public class Line
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }
    }

    public static class PrimitiveExtensions
    {
        public static Common.Point ToCommon(this Drawing.Point point)
        { return new Common.Point { X = point.X, Y = point.Y }; }

        public static Common.Size ToCommon(this Drawing.Size size)
        { return new Common.Size { Width = size.Width, Height = size.Height }; }

        public static PointF FromCommon(this Common.Point point)
        { return new PointF((float)point.X, (float)point.Y); }

        public static SizeF FromCommon(this Common.Size size)
        { return new SizeF((float)size.Width, (float)size.Height); }

        public static RectangleF FromCommon(this Common.Rectangle rectangle)
        { return new RectangleF(rectangle.Point.FromCommon(), rectangle.Size.FromCommon()); }

        public static Line FromCommon(this Common.Line line)
        { return new Line { Start = line.Start.FromCommon(), End = line.End.FromCommon() }; }
    }
}
