namespace GameHub.TicTacToe.Exceptions;

public class BoardException : Exception
{
    #region Constructor
    public BoardException(string message) : base(message) { }
    #endregion
}
