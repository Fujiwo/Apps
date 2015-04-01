using System;
using System.Windows.Data;
using System.Windows.Media;

namespace FLifegame.ViewModel
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class CellViewModelConverter : IValueConverter
    {
        static readonly Brush onBrush = Brushes.BlueViolet;
        //static readonly Brush offBrush = null;
        static readonly Brush offBrush = Brushes.LightSteelBlue;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { return ((bool?)value == true) ? onBrush : offBrush; }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
            //var brush = value as SolidColorBrush;
            //return brush == null ? false : brush.Color.Equals(onBrush.Color);
        }
    }
}
