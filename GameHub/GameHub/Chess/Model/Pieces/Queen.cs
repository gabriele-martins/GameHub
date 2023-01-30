using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class Queen : Piece
{
    #region Constructor
    public Queen(int row, int column, Color color) : base(row, column)
    {
        Color = color;
        Symbol = Symbol.Q;
    }
    #endregion

    #region Methods
    public override bool IsPossibleMovement(int destinatedRow, int destinatedColumn, Board b)
    {
        Bishop bishop = new Bishop(Row, Column, Color.Colorless);
        Rook rook = new Rook(Row, Column, Color.Colorless);

        if (bishop.IsPossibleMovement(destinatedRow, destinatedColumn, b) || rook.IsPossibleMovement(destinatedRow, destinatedColumn, b))
        {
            return true;
        }
        else return false;
    }
    #endregion  
}
