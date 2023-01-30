using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class King : Piece
{
    #region Constructor
    public King(int row, int column, Color color) : base(row, column)
    {
        Color = color;
        Symbol = Symbol.K;
    }
    #endregion

    #region Methods
    public override bool IsPossibleMovement(int destinatedRow, int destinatedColumn, Board b)
    {
        if (Math.Abs(Row - destinatedRow) == 1 || Math.Abs(Row - destinatedRow) == 0)
        {
            if (Math.Abs(Column - destinatedColumn) == 1 || Math.Abs(Column - destinatedColumn) == 0)
            {
                if (Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty)
                {
                    return true;
                }
                else if (Board.BoardTable[destinatedRow, destinatedColumn].Color != Color)
                {
                    return true;
                }
            }
        }

        return false;
    }
    #endregion
}
