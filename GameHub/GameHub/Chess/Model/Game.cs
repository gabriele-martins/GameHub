using GameHub.Chess.View;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Pieces;
using GameHub.Chess.Repository;
using GameHub.Chess.Service;
using GameHub.Chess.Util;
using GameHub.Chess.Exceptions;
using GameHub.Hub.Controller;
using GameHub.Hub.Model;
using GameHub.Hub.Service;

namespace GameHub.Chess.Model;

public class Game
{
    #region Attributes
    public static Player playerWhite;
    public static Player playerBlack;
    public static Player currentPlayer;
    public static Player winnerPlayer;
    public static Board board;
    public static Piece piece;
    public static Piece pieceTaken;
    public static Color currentColor;
    public static int currentRow;
    public static int currentColumn;
    public static int destinatedRow;
    public static int destinatedColumn;
    public static int round;
    public static string destinatedPosition;
    public static string move;
    public static string movesTextPgn;
    public static string textToRemove;
    public static string textPgn;
    public static string castling;
    public static string promotion;
    public static bool enPassant;
    public static bool check;
    public static bool turn;
    #endregion

    #region Methods
    public static void Play()
    {
        GetPlayers();

        winnerPlayer = new Player();

        board = new Board();

        round = 0;

        move = string.Empty;

        movesTextPgn = string.Empty;

        check = true;

        turn = true;

        textPgn = string.Empty;

        do
        {
            ChangePlayer();

            if (move == "empate")
            {
                GameService.GetMove();
                if (move == "sim")
                {
                    BoardService.ShowChessboard();
                    Screen.ShowTieMessage();
                    movesTextPgn = "1/2-1/2";
                    GameService.WriteMovesTextPgn(movesTextPgn);
                    break;
                }
                else
                {
                    turn = !turn;
                    round--;
                    GameService.EraseMovesTextPgn("1/2-1/2");
                    continue;
                }
            }

            if (GameService.KingIsInCheckMate(currentColor, board))
            {
                if (currentColor == Color.White)
                {
                    winnerPlayer = playerBlack;
                    textPgn = textPgn.Trim();
                    movesTextPgn += "# 0-1";
                }
                else
                {
                    winnerPlayer = playerWhite;
                    textPgn = textPgn.Trim();
                    movesTextPgn += "# 1-0";
                }

                BoardService.ShowChessboard();
                Screen.ShowCheckMateMessage(winnerPlayer);
                GameService.WriteMovesTextPgn(movesTextPgn);
                break;
            }

            GameService.GetMove();

            if (move == "empate")
            {
                turn = !turn;
                continue;
            }
            else if (move == "render")
            {
                if (currentColor == Color.White)
                {
                    winnerPlayer = playerBlack;
                    movesTextPgn += "0-1";

                }
                else
                {
                    winnerPlayer = playerWhite;
                    movesTextPgn += "1-0";
                }

                BoardService.ShowChessboard();
                Screen.ShowSurrenderMessage(winnerPlayer);
                GameService.WriteMovesTextPgn(movesTextPgn);
                break;
            }

            GameService.GetDestinatedPosition();

            piece = Board.BoardTable[currentRow, currentColumn];

            if (piece.Symbol == Symbol.K)
            {
                if (GameService.KingWillBeInCheck(currentColor, board, destinatedRow, destinatedColumn))
                {
                    Screen.ShowPuttingKingInCheckMessage();
                    continue;
                }
            }
            else if (!GameService.KingIsInCheck(currentColor))
            {
                check = false;
            }

            if (GameService.CheckValidMove())
            {
                GameService.MovePiece();
            }
            else
            {
                Screen.ShowInvalidMovement();
                continue;
            }

            if (GameService.KingIsInCheck(currentColor))
            {
                GameService.UndoMovePiece();
                continue;
            }

            check = true;

        } while (true);

        GameService.UpdateScore();
        if (movesTextPgn.Contains("1-0"))
            Pgn.CreatePgnFile(playerWhite.Name, playerBlack.Name, "1-0", textPgn);
        else if (movesTextPgn.Contains("0-1"))
            Pgn.CreatePgnFile(playerWhite.Name, playerBlack.Name, "0-1", textPgn);
        else
            Pgn.CreatePgnFile(playerWhite.Name, playerBlack.Name, "1/2-1/2", textPgn);
    }

    public static Player AskForPlayer(string color)
    {
        Player player;
        while (true)
        {
            try
            {
                Screen.AskForPlayer(color);
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

    private static void GetPlayers()
    {
        do
        {
            try
            {
                playerWhite = AskForPlayer("Brancas");

                playerBlack = AskForPlayer("Pretas");

                if (playerWhite.Name == playerBlack.Name)
                    throw new GameException("\n\tVocê não pode jogar contra você mesmo.");
                else
                    break;
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        } while (true);
    }

    private static void ChangePlayer()
    {
        if (turn)
        {
            currentPlayer = playerWhite;
            currentColor = Color.White;
            round++;
        }
        else
        {
            currentPlayer = playerBlack;
            currentColor = Color.Black;
        }
    }
    #endregion
}
