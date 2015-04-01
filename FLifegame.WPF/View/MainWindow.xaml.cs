using FLifegame.IO;
using FLifegame.Model;
using System;
using System.Windows;

namespace FLifegame.View
{
    public partial class MainWindow : Window
    {
        const string userCellDataName = "User";

        SerializableLifegame Lifegame
        {
            get { return controlPanel.DataContext as SerializableLifegame; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Load();
            InitializeCellDataControlPanel();
            SetTimer();
        }

        void Load()
        {
            if (Lifegame != null) {
                try {
                    Lifegame.Load();
                } catch (Exception ex) {

                }
            }
        }

        void InitializeCellDataControlPanel()
        { cellDataControlPanel.Click += (sender, cellDataName) => SetCellData(cellDataName); }

        void SetCellData(string cellDataName)
        { Lifegame.SetFromList(cellDataName); }

        void SetTimer()
        {
            if (Lifegame != null)
                Lifegame.Timer = new Timer();
        }

        void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            if (Lifegame != null)
                Lifegame.Start();
        }

        void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            if (Lifegame != null)
                Lifegame.Stop();
        }

        void OnNextButtonClick(object sender, RoutedEventArgs e)
        { lifegameView.ItemsSource.Next(); }

        void OnRandomButtonClick(object sender, RoutedEventArgs e)
        { lifegameView.ItemsSource.Random(); }

        void OnClearButtonClick(object sender, RoutedEventArgs e)
        { lifegameView.ItemsSource.Clear(); }

        void OnResetButtonClick(object sender, RoutedEventArgs e)
        { lifegameView.ItemsSource.Reset(); }

        void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            if (Lifegame != null) {
                Lifegame.Load();
                Lifegame.SetFromList(userCellDataName);
            }
        }

        //async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        //{
        //    if (Lifegame != null) {
        //        try {
        //            await Lifegame.SaveAsync(userCellDataName);
        //        } catch (Exception ex) {

        //        }
        //    }
        //}

        void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (Lifegame != null) {
                try {
                    Lifegame.Save(userCellDataName);
                } catch (Exception ex) {

                }
            }
        }
    }
}
