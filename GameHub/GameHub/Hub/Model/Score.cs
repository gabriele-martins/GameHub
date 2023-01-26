using GameHub.Hub.Util;

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
        Points = Constraints.InitialPointsNumber;
        Matches = Constraints.InitialMatchesNumber;
        Wins = Constraints.InitialWinsNumber;
        Defeats = Constraints.InitialDefeatsNumber;
        Ties = Constraints.InitialTiesNumber;
    }
    #endregion
}
