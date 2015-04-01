using FLifegame.Common;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Drawing = System.Drawing;

namespace FLifegame.View
{
    public partial class ButtonsToolStrip : ToolStrip
    {
        public new event Action<ButtonsToolStrip, string> Click;

        public IObservableEnumerable<string> ItemsSource
        {
            set { SetCellDataToolStrips(value); }
        }

        void SetCellDataToolStrips(IObservableEnumerable<string> itemsSource)
        {
            InitializeCellDataToolStrips(itemsSource);
            itemsSource.CollectionChanged -= OnItemsSourceCollectionChanged;
            itemsSource.CollectionChanged += OnItemsSourceCollectionChanged;
        }

        void InitializeCellDataToolStrips(IObservableEnumerable<string> cellDataList)
        {
            SuspendLayout();

            var resources = new ComponentResourceManager(GetType());
            Items.Clear();
            cellDataList.ForEach(text => Items.Add(CreateCellDataButton(text, resources)));

            ResumeLayout(false);
            PerformLayout();
        }

        ToolStripItem CreateCellDataButton(string text, ComponentResourceManager resources)
        {
            var button = new ToolStripButton();

            button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            button.ImageTransparentColor = Color.Magenta;
            button.Name = text;
            button.Size = new Drawing.Size(23, 22);
            button.Text = text;
            button.Click += (sender, e) => OnClick((ToolStripButton)sender, text);

            return button;
        }

        void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { InitializeCellDataToolStrips((IObservableEnumerable<string>)sender); }

        void OnClick(ToolStripButton button, string text)
        {
            Select(button);
            if (Click != null)
                Click(this, text);
        }

        void Select(ToolStripButton button)
        {
            foreach (ToolStripItem toolStripItem in Items)
                toolStripItem.ForeColor = Object.ReferenceEquals(toolStripItem, button) ? Color.Blue : Color.Black;
        }
    }
}
