using FLifegame.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FLifegame.Model
{
    public static class CellDataConverter
    {
        public static CellData ToCellData(this IEnumerable<string> cellDataTexts, char onCellCharacter)
        {
            var cellDataTextsCount = cellDataTexts.Count();
            if (cellDataTextsCount <= 0)
                return new CellData();

            var dimensions = new Dimensions { Width = cellDataTexts.Max(text => text.Length), Height = cellDataTextsCount };
            var flags = new List<bool>();
            cellDataTexts.ForEach(text => dimensions.Width.Times(index => flags.Add(index < text.Length ? text[index] == onCellCharacter : false)));
            return new CellData(flags, dimensions);
        }

        public static IEnumerable<string> ToTexts(this CellData cellData, char onCellCharacter, char offCellCharacter)
        {
            var mimimumCellData = cellData.ToMinimumData();
            for (var y = 0; y < mimimumCellData.Dimensions.Height; y++) {
                var stringBuilder = new StringBuilder();
                mimimumCellData.Dimensions.Width.Times(x => stringBuilder.Append(mimimumCellData[new Position { X = x, Y = y }].On ? onCellCharacter : offCellCharacter));
                yield return stringBuilder.ToString();
            }
        }
    }
}
