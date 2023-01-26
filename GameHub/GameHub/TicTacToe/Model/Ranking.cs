using GameHub.Hub.Controller;
using GameHub.Hub.Exceptions;
using GameHub.Hub.Model;
using GameHub.Hub.Util;
using GameHub.Hub.View;

namespace GameHub.TicTacToe.Model;

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
                if (player.TicTacToeScore.Matches != Constraints.InitialMatchesNumber)
                    Rating.Add(player);
            }

            if (Rating.Count == Constraints.ZeroPlayers)
                throw new RankingException("\n\tNenhum jogador jogou Jogo da Velha ainda.");

            Rating = Rating.OrderByDescending(player => player.TicTacToeScore.Points).ThenByDescending(player => player.TicTacToeScore.Wins).ThenBy(player => player.TicTacToeScore.Defeats).ToList();
        }
        else
            throw new RankingException("\n\tNão há jogadores cadastrados no momento.");
    }

    public static void ShowTicTacToeRanking()
    {
        try
        {
            UpdateRanking();

            Console.Clear();
            Console.WriteLine("\n\t------------ RANKING JOGO DA VELHA ------------");
            for (int i = 0, j = Constraints.RankingFirstPlace; i < Rating.Count; i++, j++)
            {
                Console.WriteLine($"\n\t{j}º {Rating[i].Name}");
                Console.WriteLine($"\tPontos: {Rating[i].TicTacToeScore.Points}");
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
