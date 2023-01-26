namespace GameHub.Hub.Model;

public class Score
{
    #region Properties
    public int Points { get; set; }
    public int Matches { get; set; }
    public int Wins { get; set; }
    public int Defeats { get; set; }
    public int Ties { get; set; }
    #endregion

    #region Constructor
    public Score()
    {
        Points = 0;
        Matches = 0;
        Wins = 0;
        Defeats = 0;
        Ties = 0;
    }
    #endregion
}
