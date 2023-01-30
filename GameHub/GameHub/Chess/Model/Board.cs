using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Pieces;
using GameHub.Chess.Util;

namespace GameHub.Chess.Model;

public class Board
{
    #region Properties
    public static Piece[,] BoardTable { get; set; }
    public static string[] Rows { get; set; }
    public static string[] Columns { get; set; }
    public static int[] WhiteKingsPosition { get; set; }
    public static int[] BlackKingsPosition { get; set; }
    #endregion

    #region Constructor
    public Board()
    {
        //Creating the rows and columns labels
        Rows = new string[Constraints.BoardSize] { "8", "7", "6", "5", "4", "3", "2", "1" };
        Columns = new string[Constraints.BoardSize] { "a", "b", "c", "d", "e", "f", "g", "h" };

        //Creating the Board with a 2D array
        BoardTable = new Piece[Constraints.BoardSize, Constraints.BoardSize];
        for (int i = 0; i < Constraints.BoardSize; i++)
        {
            for (int j = 0; j < Constraints.BoardSize; j++)
            {
                BoardTable[i, j] = new Piece(i, j);
            }
        }

        //Initial placement of Pawns on the Board
        for (int i = 0; i < Constraints.BoardSize; i++)
        {
            BoardTable[1, i] = new Pawn(1, i, Color.Black);
            BoardTable[6, i] = new Pawn(6, i, Color.White);
        }

        //Initial placement of Rooks on the Board
        BoardTable[7, 0] = new Rook(7, 0, Color.White);
        BoardTable[7, 7] = new Rook(7, 0, Color.White);
        BoardTable[0, 0] = new Rook(0, 0, Color.Black);
        BoardTable[0, 7] = new Rook(0, 7, Color.Black);

        //Initial placement of Knights on the Board
        BoardTable[7, 1] = new Knight(7, 1, Color.White);
        BoardTable[7, 6] = new Knight(7, 6, Color.White);
        BoardTable[0, 1] = new Knight(0, 1, Color.Black);
        BoardTable[0, 6] = new Knight(0, 6, Color.Black);

        //Initial placement of Bishops on the Board
        BoardTable[7, 2] = new Bishop(7, 2, Color.White);
        BoardTable[7, 5] = new Bishop(7, 5, Color.White);
        BoardTable[0, 2] = new Bishop(0, 2, Color.Black);
        BoardTable[0, 5] = new Bishop(0, 5, Color.Black);

        //Initial placement of Queens on the Board
        BoardTable[7, 3] = new Queen(7, 3, Color.White);
        BoardTable[0, 3] = new Queen(0, 3, Color.Black);

        //Initial placement of Kings on the Board
        BoardTable[7, 4] = new King(7, 4, Color.White);
        WhiteKingsPosition = new int[2] { 7, 4 };
        BoardTable[0, 4] = new King(0, 4, Color.Black);
        BlackKingsPosition = new int[2] { 0, 4 };

    }
    #endregion
}