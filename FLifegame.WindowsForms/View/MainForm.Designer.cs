namespace FLifegame.View
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.boardView = new FLifegame.View.BoardView();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            //this.cellDataToolStrip = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.nextButton = new System.Windows.Forms.ToolStripButton();
            this.randomButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.resetButton = new System.Windows.Forms.ToolStripButton();
            this.loadButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.counterLabel = new System.Windows.Forms.ToolStripLabel();
            this.livesLabel = new System.Windows.Forms.ToolStripLabel();
            this.timerTextBox = new System.Windows.Forms.ToolStripTextBox();

            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.boardView);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(784, 611);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(784, 661);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            //this.toolStripContainer.TopToolStripPanel.Controls.Add(this.cellDataToolStrip);
            // 
            // boardView
            // 
            this.boardView.Board = null;
            this.boardView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardView.Location = new System.Drawing.Point(0, 0);
            this.boardView.Name = "boardView";
            this.boardView.Size = new System.Drawing.Size(784, 611);
            this.boardView.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton,
            this.nextButton,
            this.randomButton,
            this.clearButton,
            this.resetButton,
            this.loadButton,
            this.saveButton,
            this.counterLabel,
            this.livesLabel,
            this.timerTextBox});
            this.mainToolStrip.Location = new System.Drawing.Point(3, 0);
            this.mainToolStrip.Name = "toolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(478, 25);
            this.mainToolStrip.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(41, 22);
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.OnStart);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(38, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.OnStop);
            // 
            // nextButton
            // 
            this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(39, 22);
            this.nextButton.Text = "Next";
            this.nextButton.Click += new System.EventHandler(this.OnNext);
            // 
            // randomButton
            // 
            this.randomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.randomButton.Image = ((System.Drawing.Image)(resources.GetObject("randomButton.Image")));
            this.randomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(59, 22);
            this.randomButton.Text = "Random";
            this.randomButton.Click += new System.EventHandler(this.OnRandom);
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearButton.Image = ((System.Drawing.Image)(resources.GetObject("clearButton.Image")));
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(41, 22);
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.OnClear);
            // 
            // resetButton
            // 
            this.resetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetButton.Image = ((System.Drawing.Image)(resources.GetObject("resetButton.Image")));
            this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(44, 22);
            this.resetButton.Text = "Reset";
            this.resetButton.Click += new System.EventHandler(this.OnReset);
            // 
            // loadButton
            // 
            this.loadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(39, 22);
            this.loadButton.Text = "Load";
            this.loadButton.Click += new System.EventHandler(this.OnLoad);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(40, 22);
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.OnSave);
            // 
            // counterLabel
            // 
            this.counterLabel.Name = "counterLabel";
            this.counterLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // livesLabel
            // 
            this.livesLabel.Name = "livesLabel";
            this.livesLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // timerTextBox
            // 
            this.timerTextBox.Name = "timerTextBox";
            this.timerTextBox.Size = new System.Drawing.Size(100, 25);
            this.timerTextBox.TextChanged += new System.EventHandler(this.OnTimerTextChanged);
            // 
            // cellDataToolStrip
            // 
            //this.cellDataToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            //this.cellDataToolStrip.Location = new System.Drawing.Point(3, 25);
            //this.cellDataToolStrip.Name = "cellDataToolStrip";
            //this.cellDataToolStrip.Size = new System.Drawing.Size(43, 25);
            //this.cellDataToolStrip.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.toolStripContainer);
            this.Name = "MainForm";
            this.Text = "FLG";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        //private System.Windows.Forms.ToolStrip cellDataToolStrip;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripButton loadButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton nextButton;
        private System.Windows.Forms.ToolStripButton randomButton;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripButton resetButton;
        private System.Windows.Forms.ToolStripLabel counterLabel;
        private System.Windows.Forms.ToolStripLabel livesLabel;
        private System.Windows.Forms.ToolStripTextBox timerTextBox;
        private BoardView boardView;
    }
}

