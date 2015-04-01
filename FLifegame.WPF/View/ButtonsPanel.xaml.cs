using FLifegame.Common;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FLifegame.View
{
    public partial class ButtonsPanel : UserControl
    {
        public event Action<ButtonsPanel, string> Click;

        public static readonly DependencyProperty ItemsSourceProperty =
          DependencyProperty.Register("ItemsSource", typeof(IObservableEnumerable<string>), typeof(ButtonsPanel),
                                           new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        public static readonly DependencyProperty OrientationProperty =
          DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ButtonsPanel),
                                           new PropertyMetadata(Orientation.Vertical, new PropertyChangedCallback(OnOrientationChanged)));

        public IObservableEnumerable<string> ItemsSource
        {
            get { return (IObservableEnumerable<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public ButtonsPanel()
        { InitializeComponent(); }

        void InitializePanel()
        {
            panel.Children.Clear();
            ItemsSource.ForEach(text => panel.Children.Add(CreateCellDataButton(text)));
        }

        UIElement CreateCellDataButton(string text)
        {
            var button = new Button { Content = text };
            button.Click += (sender, e) => OnClick((Button)sender, text);
            return button;
        }

        static void OnOrientationChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisInstance = dependencyObject as ButtonsPanel;
            if (thisInstance == null)
                throw new ArgumentNullException();

            thisInstance.panel.Orientation = (Orientation)dependencyPropertyChangedEventArgs.NewValue;
        }

        static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisInstance = dependencyObject as ButtonsPanel;
            var itemsSource = dependencyPropertyChangedEventArgs.NewValue as IObservableEnumerable<string>;
            
            if (thisInstance == null)
                throw new ArgumentNullException();
            if (itemsSource == null)
                return;

            itemsSource.CollectionChanged -= thisInstance.OnItemsSourceCollectionChanged;
            itemsSource.CollectionChanged += thisInstance.OnItemsSourceCollectionChanged;
            thisInstance.InitializePanel();
        }

        void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { InitializePanel(); }

        void OnClick(Button button, string text)
        {
            Select(button);

            if (Click != null)
                Click(this, text);
        }

        void Select(Button button)
        {
            foreach (Control control in panel.Children)
                control.Foreground = Object.ReferenceEquals(control, button) ? Brushes.Blue : Brushes.Black;
        }
    }
}
