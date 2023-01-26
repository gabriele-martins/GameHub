using GameHub.Hub.Controller;
using GameHub.Hub.Model;
using GameHub.Hub.Repository;
using GameHub.TicTacToe.Model;
using GameHub.TicTacToe.Model.Enum;
using GameHub.TicTacToe.Util;
using GameHub.TicTacToe.View;

namespace GameHub.TicTacToe.Service;

public class GameService
{
    #region Methods
    public static void PutPieceOnTheBoard(string position, string[,] board, string piece)
    {
        int size = board.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == position)
                {
                    board[i, j] = piece;
                    break;
                }
            }
        }
    }

    public static bool? CheckEndGame(string[,] board, string piece)
    {
        int size = board.GetLength(0);

        int mainDiagonalSum = 0;
        int secondaryDiagonalSum = 0;

        int tie = 0;

        for (int i = 0; i < size; i++)
        {
            int rowSum = 0;
            int columnSum = 0;
            for (int j = 0; j < size; j++)
            {
                if (!board[i, j].All(c => char.IsDigit(c)))
                    tie++;

                if (board[i, j] == piece) 
                    rowSum++;

                if (board[j, i] == piece) 
                    columnSum++;

                if (i == j && board[i, i] == piece)
                    mainDiagonalSum++;

                if (i + j == size - 1 && board[i, j] == piece) 
                    secondaryDiagonalSum++;
            }

            if (rowSum == size) 
                return true;

            if (columnSum == size) 
                return true;
        }

        if (mainDiagonalSum == size) 
            return true;

        if (secondaryDiagonalSum == size) 
            return true;

        if (tie == board.Length)
            return null;

        return false;
    }

    public static Board CreateBoard()
    {
        int size;

        while (true)
        {
            try
            {
                Screen.AskBoardSize();
                size = int.Parse(Console.ReadLine());
                Board board = new Board(size);
                return board;
            }
            catch (FormatException)
            {
                Screen.WriteRed("\n\tO tamanho só pode conter valores numéricos.");
                Thread.Sleep(Constraints.TimeToWait);
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
    }

    public static void GetPosition(string playerName, string piece, ref List<string> takenPositions)
    {
        while (true)
        {
            Screen.ShowBoard(Game.Board.Table);

            try
            {
                Screen.AskPosition(playerName, piece);
                Game.Position = int.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Screen.WriteRed("\n\tA posição só pode conter valores numéricos.");
                Thread.Sleep(Constraints.TimeToWait);
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
        
        takenPositions.Add(Game.Position.ToString());
    }

    public static void UpdatePlayersScore(Player xPlayer, Player oPlayer, string result)
    {
        if (result == Piece.X.ToString())
        {
            xPlayer.HubScore.Matches++;
            xPlayer.TicTacToeScore.Matches++;
            xPlayer.HubScore.Wins++;
            xPlayer.TicTacToeScore.Wins++;
            xPlayer.HubScore.Points += 3;
            xPlayer.TicTacToeScore.Points += 3;

            oPlayer.HubScore.Matches++;
            oPlayer.TicTacToeScore.Matches++;
            oPlayer.HubScore.Defeats++;
            oPlayer.TicTacToeScore.Defeats++;
        }
        else if (result == Piece.O.ToString())
        {
            oPlayer.HubScore.Matches++;
            oPlayer.TicTacToeScore.Matches++;
            oPlayer.HubScore.Wins++;
            oPlayer.TicTacToeScore.Wins++;
            oPlayer.HubScore.Points += 3;
            oPlayer.TicTacToeScore.Points += 3;

            xPlayer.HubScore.Matches++;
            xPlayer.TicTacToeScore.Matches++;
            xPlayer.HubScore.Defeats++;
            xPlayer.TicTacToeScore.Defeats++;
        }
        else
        {
            xPlayer.HubScore.Matches++;
            xPlayer.TicTacToeScore.Matches++;
            xPlayer.HubScore.Ties++;
            xPlayer.TicTacToeScore.Ties++;
            xPlayer.HubScore.Points += 1;
            xPlayer.TicTacToeScore.Points += 1;

            oPlayer.HubScore.Matches++;
            oPlayer.TicTacToeScore.Matches++;
            oPlayer.HubScore.Ties++;
            oPlayer.TicTacToeScore.Ties++;
            oPlayer.HubScore.Points += 1;
            oPlayer.TicTacToeScore.Points += 1;
        }
        PlayerRepository.Serialize(PlayerController.Players);
    }
    #endregion
}
