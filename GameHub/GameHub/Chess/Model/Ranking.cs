using GameHub.Hub.Controller;
using GameHub.Hub.Exceptions;
using GameHub.Hub.Model;
using GameHub.Hub.Util;
using GameHub.Hub.View;

namespace GameHub.Chess.Model;

public class Ranking
{
    #region Properties
    public static List<Player> Rating { get; set; }
    #endregion

    #region Methods
    private static void UpdateRanking()
    {
        Rating = new List<Player>();

        if (PlayerController.Players.Count != Constraints.ZeroPlayers)
        {
            foreach (Player player in PlayerController.Players)
            {
                if (player.ChessScore.Matches != Constraints.InitialMatchesNumber)
                    Rating.Add(player);
            }

            if (Rating.Count == Constraints.ZeroPlayers)
                throw new RankingException("\n\tNenhum jogador jogou Xadrez ainda.");

            Rating = Rating.OrderByDescending(player => player.ChessScore.Points).ThenByDescending(player => player.ChessScore.Wins).ThenBy(player => player.ChessScore.Defeats).ToList();
        }
        else
            throw new RankingException("\n\tNão há jogadores cadastrados no momento.");
    }

    public static void ShowChessRanking()
    {
        try
        {
            UpdateRanking();

            Console.Clear();
            Console.WriteLine("\n\t--------------- RANKING XADREZ ----------------");
            for (int i = 0, j = Constraints.RankingFirstPlace; i < Rating.Count; i++, j++)
            {
                Console.WriteLine($"\n\t{j}º {Rating[i].Name}");
                Console.WriteLine($"\tPontos: {Rating[i].ChessScore.Points}");
            }
            Screen.ShowComeBack();

        }
        catch (Exception e)
        {
            Screen.WriteRed(e.Message);
            Thread.Sleep(Constraints.TimeToWait);
        }
    }
    #endregion
}
