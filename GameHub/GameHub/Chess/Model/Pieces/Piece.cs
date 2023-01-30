using GameHub.Chess.Model.Enum;

namespace GameHub.Chess.Model.Pieces;

public class Piece
{
    #region Properties
    public int Row { get; set; }
    public int Column { get; set; }
    public int Moves { get; set; }
    public Color Color { get; set; }
    public Symbol Symbol { get; set; }
    #endregion

    #region Constructors
    public Piece(int row, int column)
    {
        Row = row;
        Column = column;
        Color = Color.Colorless;
        Symbol = Symbol.Empty;
        Moves = 0;
    }
    #endregion

    #region Methods
    public virtual bool IsPossibleMovement(int destinatedRow, int destinatedColumn, Board b)
    {
        return false;
    }

    public override string ToString()
    {
        return Symbol.ToString();
    }
    #endregion
}
