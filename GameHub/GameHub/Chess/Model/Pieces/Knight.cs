using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class Knight : Piece
{
    #region Constructor
    public Knight(int row, int column, Color color) : base(row, column)
    {
        Color = color;
        Symbol = Symbol.N;
    }
    #endregion

    #region Methods
    public override bool IsPossibleMovement(int destinatedRow, int destinatedColumn, Board b)
    {
        if (destinatedRow == Row + 2 || destinatedRow == Row - 2)
        {
            if (destinatedColumn == Column + 1 || destinatedColumn == Column - 1)
            {
                return true;
            }
            else
                return false;
        }
        else if (destinatedRow == Row + 1 || destinatedRow == Row - 1)
        {
            if (destinatedColumn == Column + 2 || destinatedColumn == Column - 2)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    #endregion
}
