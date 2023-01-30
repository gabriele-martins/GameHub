using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class Rook : Piece
{
    #region Constructor
    public Rook(int row, int column, Color color) : base(row, column)
    {
        Color = color;
        Symbol = Symbol.R;
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

        if (destinatedColumn == Column)
        {
            if (Row > destinatedRow)
            {
                for (int i = Row - 1; i > destinatedRow; i--)
                {
                    if (Board.BoardTable[i, Column].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0) return true;
                else return false;
            }
            else if (Row < destinatedRow)
            {
                for (int i = Row + 1; i < destinatedRow; i++)
                {
                    if (Board.BoardTable[i, Column].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0) return true;
                else return false;
            }
            else
                return false;
        }
        else if (destinatedRow == Row)
        {
            if (Column > destinatedColumn)
            {
                for (int j = Column - 1; j > destinatedColumn; j--)
                {
                    if (Board.BoardTable[Row, j].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0) return true;
                else return false;
            }
            else if (Column < destinatedColumn)
            {
                for (int j = Row + 1; j < destinatedColumn; j++)
                {
                    if (Board.BoardTable[Row, j].Symbol != Symbol.Empty) piecesInTheWay++;
                }
                if (piecesInTheWay == 0) return true;
                else return false;
            }
            else
                return false;
        }
        else
            return false;
    }
    #endregion
}
