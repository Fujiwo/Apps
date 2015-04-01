using FLifegame.Common;
using FLifegame.IO;
using FLifegame.Model;
using System;
using System.Windows.Forms;
using Drawing = System.Drawing;

namespace FLifegame.View
{
    public partial class MainForm : Form
    {
        const int defaultSize = 256;
        const string userCellDataName = "User";

        SerializableLifegame lifegame = new SerializableLifegame(new Dimensions { Width = defaultSize, Height = defaultSize });
        Timer timer = new Timer();

        ButtonsToolStrip cellDataToolStrip = new ButtonsToolStrip();

        public MainForm()
        {
            lifegame.Load();
            InitializeComponent();
            InitializeCellDataToolStrips();
            SetBoard();
            SetTimer();
        }

        void InitializeCellDataToolStrips()
        {
            InitializeStyle(cellDataToolStrip);
            toolStripContainer.TopToolStripPanel.Controls.Add(cellDataToolStrip);

            cellDataToolStrip.ItemsSource = lifegame.CellDataList;
            cellDataToolStrip.Click += (sender, cellDataName) => OnLoadCellData(cellDataName);
        }

        static void InitializeStyle(ButtonsToolStrip cellDataToolStrip)
        {
            cellDataToolStrip.Dock = DockStyle.None;
            cellDataToolStrip.Location = new Drawing.Point(3, 25);
            cellDataToolStrip.Name = "cellDataToolStrip";
            cellDataToolStrip.Size = new Drawing.Size(43, 25);
            cellDataToolStrip.TabIndex = 1;
        }

        void SetBoard()
        {
            boardView.Board = lifegame.Board;
            boardView.Board.CounterChanged += OnCounterChanged;
            boardView.Board.OnCounterChanged += OnOnCounterChanged;
            SetCounterText();
            SetLivesText();
        }

        void SetTimer()
        {
            lifegame.Timer = timer;
            timer.IntervalChanged += OnTimerIntervalChanged;
            timer.IsRunningChanged += OnTimerIsRunningChanged;

            SetTimerIntervalText();
            SetTimerIsRunning();
        }

        void SetCounterText()
        { counterLabel.Text = string.Format("Counter: {0}", lifegame.Board.Counter); }

        void SetLivesText()
        { livesLabel.Text = string.Format("Lives: {0}", lifegame.Board.OnCounter); }

        void SetTimerIsRunning()
        {
            startButton.Enabled = !timer.IsRunning;
            stopButton.Enabled  =  timer.IsRunning;
        }

        void SetTimerIntervalText()
        { timerTextBox.Text = string.Format("Timer: {0:F3}s", timer.Interval); }

        void OnCounterChanged(Board board)
        { SetCounterText(); }

        void OnOnCounterChanged(Board board)
        { SetLivesText(); }

        void OnTimerIntervalChanged(ITimer timer)
        { SetTimerIntervalText(); }

        void OnTimerIsRunningChanged(ITimer timer)
        { SetTimerIsRunning(); }

        void OnStart(object sender, EventArgs e)
        { timer.IsRunning = true; }

        void OnStop(object sender, EventArgs e)
        { timer.IsRunning = false; }

        void OnNext(object sender, EventArgs e)
        { lifegame.Board.Next(); }

        void OnRandom(object sender, EventArgs e)
        { lifegame.Board.Random(); }

        void OnClear(object sender, EventArgs e)
        { lifegame.Board.Clear(); }

        void OnReset(object sender, EventArgs e)
        { lifegame.Board.Reset(); }

        void OnLoad(object sender, EventArgs e)
        {
            lifegame.Load();
            lifegame.SetFromList(userCellDataName);
        }

        //async void OnSave(object sender, EventArgs e)
        //{ await lifegame.SaveAsync(userCellDataName); }

        void OnSave(object sender, EventArgs e)
        { lifegame.Save(userCellDataName); }

        void OnTimerTextChanged(object sender, EventArgs e)
        {
            double timerInterval;
            if (double.TryParse(timerTextBox.Text, out timerInterval))
                timer.Interval = timerInterval;
        }

        void OnLoadCellData(string cellDataName)
        { lifegame.SetFromList(cellDataName); }
    }
}
