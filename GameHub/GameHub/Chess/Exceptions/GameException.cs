namespace GameHub.Chess.Exceptions;

public class GameException : Exception
{
    #region Constructor
    public GameException(string message) : base(message) { }
    #endregion
}

