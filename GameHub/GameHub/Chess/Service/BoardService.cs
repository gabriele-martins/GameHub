using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model;

namespace GameHub.Chess.Service;

public class BoardService : Board
{
    #region Methods
    public static void ShowChessboard()
    {
        Console.Clear();
        Console.Write("\n\t   ");
        for (int i = 0; i < 8; i++)
        {
            Console.Write($" {Columns[i]} ");
        }

        for (int i = 0; i < 8; i++)
        {
            Console.Write($"\n\t {Rows[i]} ");
            for (int j = 0; j < 8; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        if (BoardTable[i, j].Color == Color.White)
                            Console.ForegroundColor = ConsoleColor.White;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;
                        if (BoardTable[i, j].Symbol == Symbol.Empty)
                            Console.Write("   ");
                        else
                            Console.Write($" {BoardTable[i, j]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        if (BoardTable[i, j].Color == Color.White)
                            Console.ForegroundColor = ConsoleColor.White;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;
                        if (BoardTable[i, j].Symbol == Symbol.Empty)
                            Console.Write("   ");
                        else
                            Console.Write($" {BoardTable[i, j]} ");
                        Console.ResetColor();
                    }

                }
                else
                {
                    if (j % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        if (BoardTable[i, j].Color == Color.White)
                            Console.ForegroundColor = ConsoleColor.White;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;
                        if (BoardTable[i, j].Symbol == Symbol.Empty)
                            Console.Write("   ");
                        else
                            Console.Write($" {BoardTable[i, j]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        if (BoardTable[i, j].Color == Color.White)
                            Console.ForegroundColor = ConsoleColor.White;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;
                        if (BoardTable[i, j].Symbol == Symbol.Empty)
                            Console.Write("   ");
                        else
                            Console.Write($" {BoardTable[i, j]} ");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
    #endregion
}
