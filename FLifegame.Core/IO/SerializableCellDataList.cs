using FLifegame.Model;
using System;
using System.Threading.Tasks;

namespace FLifegame.IO
{
    public class SerializableCellDataList : CellDataList
    {
        const string applicationName = "FLifegame";

        public void Load()
        { CellDataDictionary.Load(GetFolderPath()); }

        public void Save()
        { CellDataDictionary.Save(GetFolderPath()); }

        public static async Task<CellData> LoadAsync(string cellDataName)
        { return await CellDataSerializer.LoadAsync(GetFolderPath(), cellDataName); }

        //public static async Task SaveAsync(CellData cellData, string cellDataName)
        //{ await CellDataSerializer.SaveAsync(cellData, GetFolderPath(), cellDataName); }

        public static void Save(CellData cellData, string cellDataName)
        { CellDataSerializer.Save(cellData, GetFolderPath(), cellDataName); }

        static string GetFolderPath()
        { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + '\\' + applicationName; }
    }
}
