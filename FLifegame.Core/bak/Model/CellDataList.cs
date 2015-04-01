using FLifegame.Common;
using NU.OJL.MPRTOS.TLV.Base;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace FLifegame.Model
{
    public class CellDataList : IEnumerable<KeyValuePair<string, CellData>>, IObservableEnumerable<string>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        ObservableDictionary<string, CellData> cellDataDictionary = new ObservableDictionary<string, CellData>();

        protected ObservableDictionary<string, CellData> CellDataDictionary
        {
            get { return cellDataDictionary; }
        }

        public CellData this[string key]
        {
            get { return cellDataDictionary[key.ToLower()]; }
            set { cellDataDictionary[key.ToLower()] = value; }
        }

        public CellDataList()
        { cellDataDictionary.CollectionChanged += OnCellDataListCollectionChanged; }

        public bool TryGetValue(string cellDataName, out CellData cellData)
        { return cellDataDictionary.TryGetValue(cellDataName.ToLower(), out cellData); }

        public IEnumerator<KeyValuePair<string, CellData>> GetEnumerator()
        { return cellDataDictionary.GetEnumerator(); }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            foreach (var cellData in this)
                yield return cellData.Key;
        }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }

        void OnCellDataListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }
    }
}
