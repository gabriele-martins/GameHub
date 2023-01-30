using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class Pawn : Piece
{
    #region Constructor
    public Pawn(int row, int column, Color color) : base(row, column)
    {
        Color = color;
        Symbol = Symbol.P;
    }
    #endregion

    #region Methods
    public override bool IsPossibleMovement(int destinatedRow, int destinatedColumn, Board b)
    {
        switch (Color)
        {
            case Color.White:
                if (Moves != 0)
                {
                    if (destinatedRow == Row - 1)
                    {
                        if (destinatedColumn == Column && Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty)
                        {
                            return true;
                        }
                        else if ((destinatedColumn == Column - 1 || destinatedColumn == Column + 1) && Board.BoardTable[destinatedRow, destinatedColumn].Color == Color.Black)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (destinatedRow == Row - 1)
                    {
                        if (destinatedColumn == Column && Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty)
                        {
                            return true;
                        }
                        else if ((destinatedColumn == Column - 1 || destinatedColumn == Column + 1) && Board.BoardTable[destinatedRow, destinatedColumn].Color == Color.Black)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (destinatedRow == Row - 2)
                    {
                        if (destinatedColumn == Column && Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty && Board.BoardTable[Row - 1, destinatedColumn].Symbol == Symbol.Empty)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            case Color.Black:
                if (Moves != 0)
                {
                    if (destinatedRow == Row + 1)
                    {
                        if (destinatedColumn == Column && Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty)
                        {
                            return true;
                        }
                        else if ((destinatedColumn == Column - 1 || destinatedColumn == Column + 1) && Board.BoardTable[destinatedRow, destinatedColumn].Color == Color.White)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (destinatedRow == Row + 1)
                    {
                        if (destinatedColumn == Column && Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty)
                        {
                            return true;
                        }
                        else if ((destinatedColumn == Column - 1 || destinatedColumn == Column + 1) && Board.BoardTable[destinatedRow, destinatedColumn].Color == Color.White)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (destinatedRow == Row + 2)
                    {
                        if (destinatedColumn == Column && Board.BoardTable[destinatedRow, destinatedColumn].Symbol == Symbol.Empty && Board.BoardTable[Row + 1, destinatedColumn].Symbol == Symbol.Empty)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            default:
                return false;
        }
    }
    #endregion
}
