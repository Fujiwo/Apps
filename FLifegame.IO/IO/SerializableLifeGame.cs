using FLifegame.Common;
using FLifegame.Model;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FLifegame.IO
{
    public class SerializableLifegame : Lifegame
    {
        public SerializableLifegame() : base()
        { CellDataList = new SerializableCellDataList(); }

        public SerializableLifegame(Dimensions dimensions)
            : base(dimensions)
        { CellDataList = new SerializableCellDataList(); }

        public void Load()
        { ((SerializableCellDataList)CellDataList).Load(); }

        public void Save()
        { ((SerializableCellDataList)CellDataList).Save(); }

        //public async Task SaveAsync(string userCellDataName)
        //{ await SerializableCellDataList.SaveAsync(Board.CellData, userCellDataName); }

        public void Save(string userCellDataName)
        { SerializableCellDataList.Save(Board.CellData, userCellDataName); }
    }
}
