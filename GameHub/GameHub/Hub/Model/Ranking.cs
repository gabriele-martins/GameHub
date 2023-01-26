using GameHub.Hub.Controller;
using GameHub.Hub.Exceptions;
using GameHub.Hub.View;

namespace GameHub.Hub.Model;

public class Ranking
{
    #region Properties
    public static List<Player> Rating { get; set; } = new List<Player>();
    #endregion

    #region Methods
    private static void UpdateRanking()
    {
        if (PlayerController.Players.Count != 0)
        {
            foreach (Player player in PlayerController.Players)
            {
                if (player.HubScore.Matches != 0)
                    Rating.Add(player);
            }

            if (Rating.Count == 0)
                throw new RankingException("\n\tNenhum jogador jogou ainda.");

            Rating = Rating.OrderByDescending(player => player.HubScore.Points).ThenByDescending(player => player.HubScore.Wins).ThenBy(player => player.HubScore.Defeats).ToList();
        }
        else
            throw new RankingException("\n\tNão há jogadores cadastrados no momento.");
    }

    public static void ShowHubRanking()
    {
        try
        {
            UpdateRanking();

            Console.Clear();
            Console.WriteLine("\n\t---------------- RANKING GERAL ----------------");
            for (int i = 0, j = 1; i < Rating.Count; i++, j++)
            {
                Console.WriteLine($"\n\t{j}º {Rating[i].Name}");
                Console.WriteLine($"\tPontos: {Rating[i].HubScore.Points}");
            }
            Screen.ShowComeBack();

        }
        catch (Exception e)
        {
            Screen.WriteRed(e.Message);
            Thread.Sleep(2600);
        }
    }
    #endregion
}
