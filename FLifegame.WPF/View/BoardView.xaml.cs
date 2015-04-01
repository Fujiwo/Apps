using FLifegame.Common;
using FLifegame.Model;
using FLifegame.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Shapes = System.Windows.Shapes;
using Windows = System.Windows;

namespace FLifegame.View
{
    public partial class BoardView : UserControl
    {
        static readonly Brush lineElementStrokeBrush = Brushes.Gray;

        public Board ItemsSource
        {
            get { return (Board)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
          DependencyProperty.Register("ItemsSource", typeof(Board), typeof(BoardView),
                                           new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        List<Shape> cellElements = new List<Shape>();
        List<Shapes.Line> horizontalLineElements = new List<Shapes.Line>();
        List<Shapes.Line> verticalLineElements = new List<Shapes.Line>();

        public BoardView()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }

        IList<Shape> CellElements
        { get { return cellElements; } }

        IList<Shapes.Line> HorizontalLineElements
        { get { return horizontalLineElements; } }

        IList<Shapes.Line> VerticalLineElements
        { get { return verticalLineElements; } }

        void AddCellElement(Shape cellElement)
        {
            canvas.Children.Add(cellElement);
            cellElements.Add(cellElement);
        }

        void AddHorizontalLineElement(Shapes.Line lineElement)
        {
            canvas.Children.Add(lineElement);
            horizontalLineElements.Add(lineElement);
        }

        void AddVerticalLineElement(Shapes.Line lineElement)
        {
            canvas.Children.Add(lineElement);
            verticalLineElements.Add(lineElement);
        }

         void ClearElements()
        {
            canvas.Children.Clear();
            CellElements.Clear();
            ClearLineElements();
        }

         void ClearLineElements()
         {
             HorizontalLineElements.Clear();
             VerticalLineElements.Clear();
         }

        static void OnItemsSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisInstance = dependencyObject as BoardView;
            var itemsSource = dependencyPropertyChangedEventArgs.NewValue as Board;

            if (thisInstance == null)
                throw new ArgumentNullException();
            if (itemsSource == null)
                return;
            thisInstance.InitializeCanvas();
        }

        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        { SetElementPositions(); }

        void InitializeCanvas()
        {
            CreateElements();
            SetDataContexts();
        }

        void CreateElements()
        {
            ClearElements();
            ItemsSource.Dimensions.Times(CreateCellElement);
            CreateLineElements();
        }

        void CreateLineElements()
        {
            (ItemsSource.Dimensions.Height + 1).Times(CreateHorizontalLineElement);
            (ItemsSource.Dimensions.Width + 1).Times(CreateVerticalLineElement);
        }

        void CreateCellElement(Position position)
        {
            var cellElement = new Shapes.Rectangle();
            AddCellElement(cellElement);
            SetBinding(cellElement);
            SetHandler(cellElement);
        }

        void CreateHorizontalLineElement()
        { AddHorizontalLineElement(CreateLineElement()); }

        void CreateVerticalLineElement()
        { AddVerticalLineElement(CreateLineElement()); }

        Shapes.Line CreateLineElement()
        { return new Shapes.Line { Stroke = lineElementStrokeBrush }; }

        void SetElementPositions()
        {
            var panelSize = new Windows.Size(canvas.ActualWidth, canvas.ActualHeight);
            SetCellElementPositions(panelSize);
            SetLineElementPositions(panelSize);
        }

        void SetCellElementPositions(Windows.Size panelSize)
        { CellElements.ForEach((index, cellElement) => SetCellElementPosition(panelSize, cellElement, index)); }

        void SetCellElementPosition(Windows.Size panelSize, FrameworkElement element, int index)
        { SetCellElementPosition(panelSize, element, ItemsSource.IndexToPosition(index)); }

        void SetCellElementPosition(Windows.Size panelSize, FrameworkElement element, Position position)
        {
            var cellElementPosition = ItemsSource.GetCellElementPosition(panelSize.ToCommon(), position).FromCommon();
            element.Margin = cellElementPosition.Margin;
            element.Width = cellElementPosition.Width;
            element.Height = cellElementPosition.Height;

            //element.Margin = new Thickness(
            //    panelSize.Width  * position.X / ItemsSource.Dimensions.Width ,
            //    panelSize.Height * position.Y / ItemsSource.Dimensions.Height,
            //    0.0,
            //    0.0
            //);

            //element.Width = panelSize.Width / ItemsSource.Dimensions.Width;
            //element.Height = panelSize.Height / ItemsSource.Dimensions.Height;
        }

        void SetLineElementPositions(Windows.Size panelSize)
        {
            SetHorizontalLineElementPositions(panelSize);
            SetVerticalLineElementPositions(panelSize);
        }

        void SetHorizontalLineElementPositions(Windows.Size panelSize)
        { HorizontalLineElements.ForEach((index, lineElement) => SetHorizontalLineElementPosition(panelSize, lineElement, index)); }

        void SetVerticalLineElementPositions(Windows.Size panelSize)
        { VerticalLineElements.ForEach((index, lineElement) => SetVerticalLineElementPosition(panelSize, lineElement, index)); }

        void SetHorizontalLineElementPosition(Windows.Size panelSize,  Shapes.Line line, int index)
        {
            ItemsSource.GetHorizontalLineElementPosition(panelSize.ToCommon(), index).FromCommon(line);

            //lineElement.X1 =
            //lineElement.X2 = panelSize.Width * index / ItemsSource.Dimensions.Width;
            //lineElement.Y1 = 0.0;
            //lineElement.Y2 = panelSize.Height; 
        }

        void SetVerticalLineElementPosition(Windows.Size panelSize, Shapes.Line line, int index)
        {
            ItemsSource.GetVerticalLineElementPosition(panelSize.ToCommon(), index).FromCommon(line);

            //lineElement.X1 = 0.0;
            //lineElement.X2 = panelSize.Width;
            //lineElement.Y1 =
            //lineElement.Y2 = panelSize.Height * index / ItemsSource.Dimensions.Height;
        }

        static void SetBinding(Shape cellElement)
        { cellElement.SetBinding(Shape.FillProperty, new Binding() { Path = new PropertyPath("On"), Converter = new CellViewModelConverter() }); }

        static void SetHandler(FrameworkElement cellElement)
        {
            cellElement.MouseUp -= OnCellElementClick;
            cellElement.MouseUp += OnCellElementClick;
        }

        static void OnCellElementClick(object sender, MouseButtonEventArgs e)
        {
            var cellElement = sender as Shape;
            if (cellElement != null)
                OnCellElementClick(cellElement);
        }

        static void OnCellElementClick(Shape cellElement)
        {
            var cell = cellElement.DataContext as Cell;
            if (cell != null)
                cell.Invert();
        }

        void SetDataContexts()
        {
            if (ItemsSource == null || canvas.Children.Count < ItemsSource.Count)
                return;

            ItemsSource.Count.Times(
                index => {
                    var cellElement = canvas.Children[index] as FrameworkElement;
                    if (cellElement != null)
                        cellElement.DataContext = ItemsSource[index];
                }
            );
        }
    }
}
