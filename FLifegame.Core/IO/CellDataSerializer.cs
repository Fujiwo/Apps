using FLifegame.Common;
using FLifegame.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLifegame.IO
{
    //public interface ICellDataSerializer
    //{
    //    void Load(IDictionary<string, CellData> cellDataDictionary, string folderPath);
    //    void Save(IDictionary<string, CellData> cellDataDictionary, string folderPath);
    //    CellData Load(string folderPath, string cellDataName);
    //    void Save(CellData cellData, string folderPath, string cellDataName);
    //}

    //public class CellDataSerializer2 : ICellDataSerializer
    //{
    //    public void Load(IDictionary<string, CellData> cellDataDictionary, string folderPath)
    //    { cellDataDictionary.Load(folderPath); }

    //    public void Save(IDictionary<string, CellData> cellDataDictionary, string folderPath)
    //    { cellDataDictionary.Save(folderPath); }

    //    public CellData Load(string folderPath, string cellDataName)
    //    {
    //        CellData cellData = null;
    //        var task = Task.Factory.StartNew(async () => cellData = await CellDataSerializer.LoadAsync(folderPath, cellDataName)).Unwrap();
    //        task.Wait();
    //        return cellData;
    //    }

    //    public void Save(CellData cellData, string folderPath, string cellDataName)
    //    { cellData.Save(folderPath, cellDataName); }
    //}

    public static class CellDataSerializer
    {
        static readonly string fileExtension = "lif";

        const char onCellCharacter = '*';
        const char offCellCharacter = '.';
        const char commentCharacter = '#';

        public static void Load(this IDictionary<string, CellData> cellDataDictionary, string folderPath)
        {
            cellDataDictionary.Clear();

            if (Directory.Exists(folderPath))
                Directory.GetFiles(folderPath, "*." + fileExtension, SearchOption.AllDirectories)
                          .Select(filePath => new { FilePath = filePath, CellDataName = Path.GetFileNameWithoutExtension(filePath).ToLower() })
                          .ForEach(async item => cellDataDictionary[item.CellDataName] = await LoadAsync(item.FilePath));
        }

        //public static void Save(this IDictionary<string, CellData> cellDataDictionary, string folderPath)
        //{
        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    cellDataDictionary.ForEach(async (KeyValuePair<string, CellData> keyValuePair) =>
        //                                  await keyValuePair.Value.SaveAsync(GetFilePath(folderPath, keyValuePair.Key, fileExtension)));
        //}

        public static void Save(this IDictionary<string, CellData> cellDataDictionary, string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            cellDataDictionary.ForEach((KeyValuePair<string, CellData> keyValuePair) =>
                                         keyValuePair.Value.Save(GetFilePath(folderPath, keyValuePair.Key, fileExtension)));
        }

        public static async Task<CellData> LoadAsync(string folderPath, string cellDataName)
        {
            var filePath = GetFilePath(folderPath, cellDataName, fileExtension);
            return File.Exists(filePath) ? await LoadAsync(filePath) : null;
        }

        //public static async Task SaveAsync(this CellData cellData, string folderPath, string cellDataName)
        //{
        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    await cellData.SaveAsync(GetFilePath(folderPath, cellDataName, fileExtension));
        //}

        public static void Save(this CellData cellData, string folderPath, string cellDataName)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            cellData.Save(GetFilePath(folderPath, cellDataName, fileExtension));
        }

        static async Task<CellData> LoadAsync(string filePath)
        {
            var cellDataTexts = new List<string>();
            using (var reader = new StreamReader(filePath)) {
                while (reader.Peek() >= 0) {
                    var text = await reader.ReadLineAsync();
                    if (RemoveComment(ref text))
                        cellDataTexts.Add(text);
                }
            }
            return cellDataTexts.ToCellData(onCellCharacter);
        }

        //static async Task SaveAsync(this CellData cellData, string filePath)
        //{
        //    using (var writer = new StreamWriter(filePath)) {
        //        await WriteHeaderAsync(writer);
        //        cellData.ToTexts(onCellCharacter, offCellCharacter).ForEach(async text => await writer.WriteAsync(text + '\n'));
        //    }
        //}

        static void Save(this CellData cellData, string filePath)
        {
            using (var writer = new StreamWriter(filePath)) {
                //await WriteHeaderAsync(writer);
                //cellData.ToTexts(onCellCharacter, offCellCharacter).ForEach(async text => await writer.WriteAsync(text + '\n'));
                WriteHeader(writer);
                cellData.ToTexts(onCellCharacter, offCellCharacter).ForEach(text => writer.Write(text + '\n'));
            }
        }

        //static async Task WriteHeaderAsync(StreamWriter writer)
        //{
        //    await writer.WriteAsync(commentCharacter);
        //    await writer.WriteAsync("Life 1.05\n");
        //}

        static void WriteHeader(StreamWriter writer)
        {
            writer.Write(commentCharacter);
            writer.Write("Life 1.05\n");
            writer.Write(commentCharacter);
            writer.Write("P -1 -1\n");
        }

        static string GetFilePath(string folderPath, string fileName, string fileExtension)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(folderPath);
            if (folderPath.Last() != '\\')
                stringBuilder.Append('\\');
            stringBuilder.Append(fileName);
            if (fileName.Last() != '.' && fileExtension.First() != '.')
                stringBuilder.Append('.');
            stringBuilder.Append(fileExtension);
            return stringBuilder.ToString();
        }

        static bool RemoveComment(ref string text)
        {
            text = text.Trim();
            var index = text.IndexOf(commentCharacter);
            if (index < 0)
                return true;
            else if (index == 0)
                return false;
            text = text.Substring(0, index);
            return true;
        }
    }
}
