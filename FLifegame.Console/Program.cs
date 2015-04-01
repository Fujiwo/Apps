using FLifegame.Common;
using FLifegame.IO;
using FLifegame.Model;
using System;

namespace FLifegame
{
    class Program
    {
        const int defaultSize = 32;
        static SerializableLifegame lifegame = new SerializableLifegame(new Dimensions { Width = defaultSize, Height = defaultSize });

        const string defaultCellDataName = "User";

        static void Main(string[] argv)
        {
            //var s1 = new Dimensions { Width = 2, Height = 3 }.Select0();
            //var s2 = new Dimensions { Width = 2, Height = 3 }.Select();

            //s1.ForEach(p => Console.WriteLine("{0}, {1}", p.X, p.Y));
            //Console.WriteLine();
            //s2.ForEach(p => Console.WriteLine("{0}, {1}", p.X, p.Y));
            //Console.WriteLine();
            //new Dimensions { Width = 2, Height = 3 }.Times(p => Console.WriteLine("{0}, {1}", p.X, p.Y));

            //return;

            InitializeCellData(GetCellDataName(argv, defaultCellDataName));
            MainLoop();
        }

        static string GetCellDataName(string[] argv, string defaultCellDataName)
        { return argv.Length > 0 ? argv[0] : defaultCellDataName; }

        static void InitializeCellData(string cellDataName)
        {
            lifegame.Load();
            if (!lifegame.SetFromList(cellDataName)) {
                var acornData = new[] {
                    ".*",
                    "...*",
                    "**..***"
                };
                lifegame.Board.Set(acornData.ToCellData('*'));
                lifegame.Save();
            }
        }

        static void MainLoop()
        {
            const int maximumCount = 100;

            bool cellChanged = true;
            lifegame.Board.CellChanged += (board, position) => cellChanged = true;

            int counter;
            for (counter = 0; cellChanged && counter < maximumCount; counter++) {
                Output(lifegame.Board);
                cellChanged = false;
                lifegame.Board.Next();
            }
        }

        static void Output(Board board)
        {
            board.Dimensions.Height.Times(y => { Output(board, y); Console.WriteLine(); });
            Console.WriteLine();
        }

        static void Output(Board board, int y)
        { board.Dimensions.Width.Times(x => Output(board, new Position { X = x, Y = y })); }

        static void Output(Board board, Position position)
        { Console.Write(GetCellCharacter(board[position].On)); }

        static char GetCellCharacter(bool on)
        { return on ? '■' : '□'; }
    }
}
