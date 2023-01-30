using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class Bishop : Piece
{
    #region Constructor
    public Bishop(int row, int column, Color color) : base(row, column)
    {
        Color = color;
        Symbol = Symbol.B;
    }
    #endregion

    #region Methods
    public override bool IsPossibleMovement(int destinatedRow, int destinatedColumn, Board b)
    {
        if (destinatedRow == Row && destinatedColumn == Column)
        {
            return false;
        }

        int piecesInTheWay = 0;

        if (Math.Abs(Row - destinatedRow) == Math.Abs(Column - destinatedColumn))
        {
            if (Row > destinatedRow && Column > destinatedColumn)
            {
                for (int i = Row - 1, j = Column - 1; i > destinatedRow; i--, j--)
                {
                    if (Board.BoardTable[i, j].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0)
                    return true;
                else
                    return false;
            }
            else if (Row > destinatedRow && Column < destinatedColumn)
            {
                for (int i = Row - 1, j = Column + 1; i > destinatedRow; i--, j++)
                {
                    if (Board.BoardTable[i, j].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0)
                    return true;
                else
                    return false;
            }
            else if (Row < destinatedRow && Column > destinatedColumn)
            {
                for (int i = Row + 1, j = Column - 1; i < destinatedRow; i++, j--)
                {
                    if (Board.BoardTable[i, j].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                for (int i = Row + 1, j = Column + 1; i < destinatedRow; i++, j++)
                {
                    if (Board.BoardTable[i, j].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0)
                    return true;
                else
                    return false;
            }
        }
        else return false;

    }
    #endregion
}
