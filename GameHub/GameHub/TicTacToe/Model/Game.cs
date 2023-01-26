using GameHub.Hub.Controller;
using GameHub.Hub.Model;
using GameHub.Hub.Service;
using GameHub.TicTacToe.Exceptions;
using GameHub.TicTacToe.Model.Enum;
using GameHub.TicTacToe.Service;
using GameHub.TicTacToe.Util;
using GameHub.TicTacToe.View;

namespace GameHub.TicTacToe.Model;

public class Game
{
    #region Properties and Attributes
    public static Player XPlayer { get; set; }
    public static Player OPlayer { get; set; }
    public static Board Board { get; set; }

    private static int _position;
    public static int Position 
    { 
        get { return _position; }
        set
        {
            if (value < Constraints.InitialPosition || value > Board.Table.Length)
                throw new GameException("\n\tA posição digitada não existe no tabuleiro.");
            else if (TakenPositions.Contains(value.ToString()))
                throw new GameException("\n\tA posição já foi tomada pelo adversário.");
            else
                _position = value;
        }
    }

    public static List<string> TakenPositions;
    #endregion

    #region Methods
    public static void Play()
    {
        while (true)
        {
            XPlayer = GetPlayer(Piece.X.ToString());
            OPlayer = GetPlayer(Piece.O.ToString());
            if (XPlayer == OPlayer)
                Screen.ShowPlayWithYourself();
            else
                break;
        }

        Player currentPlayer;

        Board = GameService.CreateBoard();

        string piece = Piece.O.ToString();

        TakenPositions = new List<string>();

        while (true)
        {
            if (piece == Piece.O.ToString())
            {
                piece = Piece.X.ToString();
                currentPlayer = XPlayer;
            }
            else
            {
                piece = Piece.O.ToString();
                currentPlayer = OPlayer;
            }

            GameService.GetPosition(currentPlayer.Name, piece, ref TakenPositions);

            GameService.PutPieceOnTheBoard(Position.ToString(), Board.Table, piece);

            bool? check = GameService.CheckEndGame(Board.Table, piece);

            if (check == true)
            {
                Screen.ShowBoard(Board.Table);
                Screen.ShowWonGame(currentPlayer.Name, piece);
                GameService.UpdatePlayersScore(XPlayer, OPlayer, piece);
                break;
            }
            else if (check == null)
            {
                Screen.ShowBoard(Board.Table);
                Screen.ShowTiedGame(XPlayer.Name, OPlayer.Name);
                piece = string.Empty;
                GameService.UpdatePlayersScore(XPlayer, OPlayer, piece);
                break;
            }
        }
    }

    public static Player GetPlayer(string piece)
    {
        Player player;
        while (true)
        {
            try
            {
                Screen.AskForPlayer(piece);
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
