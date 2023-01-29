using GameHub.Battleship.Service;
using GameHub.Battleship.Util;
using GameHub.Battleship.View;
using GameHub.Hub.Controller;
using GameHub.Hub.Model;
using GameHub.Hub.Service;

namespace GameHub.Battleship.Model;

public class Game
{
    #region Properties and Attributes
    public static Player Player1 { get; set; }
    public static Player Player2 { get; set; }

    public static bool end;

    public static Board currentBoard = new Board();
    #endregion

    #region Methods
    public static void Play()
    {
        int playerNumber1 = 1;
        int playerNumber2 = 2;
        while (true)
        {
            Player1 = GetPlayer(playerNumber1);
            Player2 = GetPlayer(playerNumber2);
            if (Player1 == Player2)
                Screen.ShowPlayWithYourself();
            else
                break;
        }

        Player currentPlayer = Player2;
        int currentPlayerNumber = playerNumber2;

        Board board1 = new Board();
        Board board2 = new Board();
        currentBoard = board2;

        end = true;

        do
        {
            currentPlayer = (currentPlayer == Player1) ? Player2 : Player1;
            currentPlayerNumber = (currentPlayerNumber == 1) ? 2 : 1;
            currentBoard = (currentBoard == board1) ? board2 : board1;

            GameService.GetPosition(currentPlayer.Name, currentPlayerNumber, ref currentBoard,ref end);
        } while (end);

        Screen.ShowWonGame(currentPlayer.Name, currentBoard);

        GameService.UpdatePlayersScore(currentPlayer, (currentPlayer == Player1) ? Player2 : Player1);
    }

    public static Player GetPlayer(int number)
    {
        Player player;
        while (true)
        {
            try
            {
                Screen.AskForPlayer(number);
                string name = PlayerService.FindPlayerName();
                player = PlayerController.Players.Find(player => player.Name == name);
                break;
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }

        PlayerService.CheckPass(player.Name);

        return player;
    }
    #endregion
}
