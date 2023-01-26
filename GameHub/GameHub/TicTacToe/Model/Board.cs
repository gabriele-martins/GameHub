using GameHub.TicTacToe.Exceptions;
using GameHub.TicTacToe.Util;

namespace GameHub.TicTacToe.Model;

public class Board
{
    #region Properties and Attributes
    private int _size;
    public int Size 
    {
        get { return _size; } 
        set
        {
            if (value < Constraints.MinimumBoardSize || value > Constraints.MaximumBoardSize)
                throw new BoardException("\n\tO tamanho inserido não é válido.");
            else
                _size = value;
        }
    }
    public string[,] Table { get; set; }
    #endregion

    #region Constructor
    public Board(int size)
    {
        Size = size;
        Table = new string[Size,Size];

        int position = Constraints.InitialPosition;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Table[i, j] = position.ToString();
                position++;
            }
        }
    }
    #endregion
}
