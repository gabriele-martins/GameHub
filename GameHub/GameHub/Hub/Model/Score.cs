using GameHub.Hub.Util;

namespace GameHub.Hub.Model;

public class Score
{
    #region Properties
    public int Points 
    {
        get { return Wins * Constraints.VictoryPoints + Defeats * Constraints.DefeatPoints + Ties; }
    }
    public int Matches { get; set; }
    public int Wins { get; set; }
    public int Defeats { get; set; }
    public int Ties { get; set; }
    #endregion

    #region Constructor
    public Score()
    {
        Matches = Constraints.InitialMatchesNumber;
        Wins = Constraints.InitialWinsNumber;
        Defeats = Constraints.InitialDefeatsNumber;
        Ties = Constraints.InitialTiesNumber;
    }
    #endregion
}
