using GameHub.Chess.Exceptions;
using GameHub.Chess.Model;
using GameHub.Chess.Model.Enum;
using GameHub.Chess.Model.Pieces;
using GameHub.Chess.Util;
using GameHub.Chess.View;
using GameHub.Hub.Controller;
using GameHub.Hub.Repository;

namespace GameHub.Chess.Service;

public class GameService
{
    #region Methods
    public static void GetMove()
    {
        string playerName = $"{Game.currentPlayer.Name} (Peças Pretas)";
        Color color = Color.Black;

        if (Game.turn)
        {
            playerName = $"{Game.currentPlayer.Name} (Peças Brancas)";
            color = Color.White;
        }

        while (true)
        {
            BoardService.ShowChessboard();

            Console.WriteLine($"\n\n\tVez de {playerName}.");

            if (Game.move == "empate")
            {
                Screen.AskTie();
            }

            Console.Write("\n\tDigite sua jogada: ");

            Game.move = Console.ReadLine().ToLower();

            try
            {
                if (string.IsNullOrEmpty(Game.move))
                {
                    throw new GameException("\n\tValor nulo não é válido.");
                }
                else if (Game.move == "empate" || Game.move == "render")
                {
                    break;
                }
                else if (Game.move == "sim" || Game.move == "não")
                {
                    break;
                }
                else if (Game.move.Length != 2 || !char.IsLetter(Game.move[0]) || !char.IsDigit(Game.move[1]))
                {
                    throw new GameException("\n\tA opção inserida é invalida.");
                }
                else if (!Board.Rows.Contains(Convert.ToString(Game.move[1])) || !Board.Columns.Contains(Convert.ToString(Game.move[0])))
                {
                    throw new GameException("\n\tA posição não existe no tabuleiro.");
                }
                else if (Board.BoardTable[Array.IndexOf(Board.Rows, Convert.ToString(Game.move[1])), Array.IndexOf(Board.Columns, Convert.ToString(Game.move[0]))].Symbol == Symbol.Empty)
                {
                    throw new GameException("\n\tNão há nenhuma peça nessa posição.");
                }
                else if (Board.BoardTable[Array.IndexOf(Board.Rows, Convert.ToString(Game.move[1])), Array.IndexOf(Board.Columns, Convert.ToString(Game.move[0]))].Color != color)
                {
                    throw new GameException("\n\tNão é possível mover uma peça do seu adversário.");
                }
                else
                {
                    Game.currentRow = Array.IndexOf(Board.Rows, Convert.ToString(Game.move[1]));
                    Game.currentColumn = Array.IndexOf(Board.Columns, Convert.ToString(Game.move[0]));
                    break;
                }
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
                continue;
            }
        }
    }

    public static void GetDestinatedPosition()
    {
        string playerNick = $"{Game.currentPlayer.Name} (Peças Pretas)";

        if (Game.turn)
        {
            playerNick = $"{Game.currentPlayer.Name} (Peças Brancas)";
        }

        while (true)
        {
            BoardService.ShowChessboard();

            Console.WriteLine($"\n\n\tVez de {playerNick}.");

            Console.Write("\n\tDigite o destino: ");

            Game.destinatedPosition = Console.ReadLine().ToLower();

            try
            {
                if (string.IsNullOrEmpty(Game.destinatedPosition))
                {
                    throw new GameException("\n\tValor nulo não é válido.");
                }
                else if (Game.destinatedPosition.Length != 2 || !char.IsDigit(Game.destinatedPosition[1]) || !char.IsLetter(Game.destinatedPosition[0]))
                {
                    throw new GameException("\n\tO valor inserido não é válido. O valor deve ser uma letra (a-h) acompanhado de um número (1-8).");
                }
                else if (!Board.Rows.Contains(Convert.ToString(Game.destinatedPosition[1])) || !Board.Columns.Contains(Convert.ToString(Game.destinatedPosition[0])))
                {
                    throw new GameException("\n\tA posição não existe no tabuleiro.");
                }
                else
                {
                    Game.destinatedRow = Array.IndexOf(Board.Rows, Convert.ToString(Game.destinatedPosition[1]));
                    Game.destinatedColumn = Array.IndexOf(Board.Columns, Convert.ToString(Game.destinatedPosition[0]));
                    break;
                }
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
                continue;
            }
        }
    }

    public static void MovePiece()
    {
        if (Game.piece.Symbol == Symbol.K && Game.piece.Color == Color.White)
        {
            Board.WhiteKingsPosition[0] = Game.destinatedRow;
            Board.WhiteKingsPosition[1] = Game.destinatedColumn;
        }
        else if (Game.piece.Symbol == Symbol.K)
        {
            Board.BlackKingsPosition[0] = Game.destinatedRow;
            Board.BlackKingsPosition[1] = Game.destinatedColumn;
        }

        Game.pieceTaken = Board.BoardTable[Game.destinatedRow, Game.destinatedColumn];

        if (Game.piece.Color == Color.White)
        {
            Game.movesTextPgn = Game.round.ToString() + ". ";
        }

        WriteMovesPiecePgn();
        WriteMovesTextPgn(Game.movesTextPgn);
        Game.textToRemove = Game.movesTextPgn;
        Game.movesTextPgn = string.Empty;

        Game.piece.Row = Game.destinatedRow;
        Game.piece.Column = Game.destinatedColumn;
        Game.piece.Moves++;

        if (Game.piece.Color == Color.White)
        {
            if (Game.piece.IsPossibleMovement(Board.BlackKingsPosition[0], Board.BlackKingsPosition[1], Game.board))
                Game.textPgn = Game.textPgn.Trim() + "+ ";
        }
        else
        {
            if (Game.piece.IsPossibleMovement(Board.WhiteKingsPosition[0], Board.WhiteKingsPosition[1], Game.board))
                Game.textPgn = Game.textPgn.Trim() + "+ ";
        }

        Board.BoardTable[Game.currentRow, Game.currentColumn] = new Piece(Game.currentRow, Game.currentColumn);
        Board.BoardTable[Game.destinatedRow, Game.destinatedColumn] = Game.piece;
        Game.turn = !Game.turn;
    }

    public static void UndoMovePiece()
    {
        try
        {
            if (Game.check == false)
            {
                throw new GameException("\n\tVocê não pode colocar seu Rei em xeque.");
            }
            else
                throw new GameException("\n\tSeu rei está em xeque. Proteja-o.");
        }
        catch (Exception e)
        {
            Screen.WriteRed(e.Message);
            Thread.Sleep(Constraints.TimeToWait);
        }
        finally
        {
            EraseMovesTextPgn(Game.textToRemove);
            Game.textToRemove = string.Empty;

            Game.piece.Row = Game.currentRow;
            Game.piece.Column = Game.currentColumn;
            Game.piece.Moves--;
            Board.BoardTable[Game.currentRow, Game.currentColumn] = Game.piece;
            Board.BoardTable[Game.destinatedRow, Game.destinatedColumn] = Game.pieceTaken;
            Game.turn = !Game.turn;
        }
    }

    public static void UpdateScore()
    {
        if (Game.move == "sim")
        {
            Game.playerWhite.ChessScore.Matches++;
            Game.playerWhite.HubScore.Matches++;
            Game.playerWhite.ChessScore.Ties++;
            Game.playerWhite.HubScore.Ties++;

            Game.playerBlack.ChessScore.Matches++;
            Game.playerBlack.HubScore.Matches++;
            Game.playerBlack.ChessScore.Ties++;
            Game.playerBlack.HubScore.Ties++;
        }
        else if (Game.move == "render")
        {
            Game.winnerPlayer.ChessScore.Matches++;
            Game.winnerPlayer.HubScore.Matches++;
            Game.winnerPlayer.ChessScore.Wins++;
            Game.winnerPlayer.HubScore.Wins++;

            Game.currentPlayer.ChessScore.Matches++;
            Game.currentPlayer.HubScore.Matches++;
            Game.currentPlayer.ChessScore.Defeats++;
            Game.currentPlayer.HubScore.Defeats++;
        }
        else
        {
            Game.winnerPlayer.ChessScore.Matches++;
            Game.winnerPlayer.HubScore.Matches++;
            Game.winnerPlayer.ChessScore.Wins++;
            Game.winnerPlayer.HubScore.Wins++;

            Game.currentPlayer.ChessScore.Matches++;
            Game.currentPlayer.HubScore.Matches++;
            Game.currentPlayer.ChessScore.Defeats++;
            Game.currentPlayer.HubScore.Defeats++;
        }
        PlayerRepository.Serialize(PlayerController.Players);
    }

    public static void WriteMovesTextPgn(string text)
    {
        Game.textPgn += text;
    }

    public static void EraseMovesTextPgn(string text)
    {
        Game.textPgn = Game.textPgn.Replace(text, "");
    }

    private static void WriteMovesPiecePgn()
    {
        if (Game.promotion != string.Empty)
        {
            Game.movesTextPgn += Game.destinatedPosition + "=" + Game.promotion + " ";
            return;
        }

        switch (Game.piece.Symbol)
        {
            case Symbol.K:
                if (Game.pieceTaken.Symbol == Symbol.Empty)
                {
                    if (Game.castling == "Minor")
                        Game.movesTextPgn += "O-O ";
                    else if (Game.castling == "Major")
                        Game.movesTextPgn += "O-O-O ";
                    else
                        Game.movesTextPgn += "K" + Game.destinatedPosition + " ";
                }
                else
                    Game.movesTextPgn += "Kx" + Game.destinatedPosition + " ";
                break;
            case Symbol.Q:
                if (Game.pieceTaken.Symbol == Symbol.Empty)
                    Game.movesTextPgn += "Q" + Game.destinatedPosition + " ";
                else
                    Game.movesTextPgn += "Qx" + Game.destinatedPosition + " ";
                break;
            case Symbol.R:
                if (Game.pieceTaken.Symbol == Symbol.Empty)
                    Game.movesTextPgn += "R" + Game.destinatedPosition + " ";
                else
                    Game.movesTextPgn += "Rx" + Game.destinatedPosition + " ";
                break;
            case Symbol.B:
                if (Game.pieceTaken.Symbol == Symbol.Empty)
                    Game.movesTextPgn += "B" + Game.destinatedPosition + " ";
                else
                    Game.movesTextPgn += "Bx" + Game.destinatedPosition + " ";
                break;
            case Symbol.N:
                if (Game.pieceTaken.Symbol == Symbol.Empty)
                    Game.movesTextPgn += "N" + Game.destinatedPosition + " ";
                else
                    Game.movesTextPgn += "Nx" + Game.destinatedPosition + " ";
                break;
            case Symbol.P:
                if (Game.enPassant || Game.pieceTaken.Symbol != Symbol.Empty)
                    Game.movesTextPgn += Board.Columns[Game.piece.Column] + "x" + Game.destinatedPosition + " ";
                else
                    Game.movesTextPgn += Game.destinatedPosition + " ";
                break;
        }
    }

    public static bool CheckValidMove()
    {
        Game.enPassant = false;
        Game.castling = string.Empty;
        Game.promotion = string.Empty;

        if (Game.piece.Symbol == Symbol.P && CheckEnPassant())
        {
            Game.enPassant = true;
            return true;
        }
        else if (Game.piece.Symbol == Symbol.K && CheckCastling())
        {
            return true;
        }

        if (Game.piece.IsPossibleMovement(Game.destinatedRow, Game.destinatedColumn, Game.board))
        {
            if (Game.piece.Symbol == Symbol.P && (Game.destinatedRow == 0 || Game.destinatedRow == 7))
            {
                Game.piece = CheckPawnPromotion();
                Game.promotion = Game.piece.Symbol.ToString();
            }
            return true;
        }
        else return false;
    }

    private static Piece CheckPawnPromotion()
    {
        int promotionRow = 7;
        string symbol;
        Color color = Color.Black;

        if (Game.piece.Color == Color.White)
        {
            promotionRow = 0;
            color = Color.White;
        }

        if (Game.destinatedRow == promotionRow)
        {
            while (true)
            {
                try
                {
                    Screen.ShowPawnPromotionMenu();
                    symbol = Console.ReadLine().ToUpperInvariant();
                    if (symbol == "Q" || symbol == "R" || symbol == "B" || symbol == "N")
                        break;
                    else
                        throw new GameException("\n\tOpção inserida invalida.");
                }
                catch (Exception e)
                {
                    Screen.WriteRed(e.Message);
                    Thread.Sleep(Constraints.TimeToWait);
                }
            }

            if (symbol == "Q")
                Game.piece = new Queen(Game.destinatedRow, Game.destinatedColumn, color);
            else if (symbol == "R")
                Game.piece = new Rook(Game.destinatedRow, Game.destinatedColumn, color);
            else if (symbol == "B")
                Game.piece = new Bishop(Game.destinatedRow, Game.destinatedColumn, color);
            else
                Game.piece = new Knight(Game.destinatedRow, Game.destinatedColumn, color);
        }

        return Game.piece;
    }

    private static bool CheckEnPassant()
    {
        if (Game.piece.Color == Color.White)
        {
            if (Game.piece.Row == 3 && Game.destinatedRow == 2)
            {
                if (Math.Abs(Game.piece.Column - Game.destinatedColumn) == 1 && Board.BoardTable[3, Game.destinatedColumn].Symbol != Symbol.Empty && Board.BoardTable[3, Game.destinatedColumn].Color != Game.piece.Color)
                {
                    Board.BoardTable[3, Game.destinatedColumn] = new Piece(3, Game.destinatedColumn);
                    return true;
                }
            }
        }
        else
        {
            if (Game.piece.Row == 4 && Game.destinatedRow == 5)
            {
                if (Math.Abs(Game.piece.Column - Game.destinatedColumn) == 1 && Board.BoardTable[4, Game.destinatedColumn].Symbol != Symbol.Empty && Board.BoardTable[4, Game.destinatedColumn].Color != Game.piece.Color)
                {
                    Board.BoardTable[4, Game.destinatedColumn] = new Piece(4, Game.destinatedColumn);
                    return true;
                }
            }
        }
        return false;
    }

    private static bool CheckCastling()
    {
        if (Game.destinatedRow == Game.piece.Row && Game.destinatedColumn != Game.piece.Column)
        {
            if (KingIsInCheck(Game.piece.Color))
                return false;
            else if (KingWillBeInCheck(Game.piece.Color, Game.board, Game.destinatedRow, Game.destinatedColumn))
                return false;

            if (Game.piece.Moves == 0)
            {
                if (Game.destinatedColumn == 6 && Board.BoardTable[Game.destinatedRow, 7].Symbol == Symbol.R && Board.BoardTable[Game.destinatedRow, 7].Moves == 0)
                {
                    if (Board.BoardTable[Game.destinatedRow, 5].Symbol == Symbol.Empty && Board.BoardTable[Game.destinatedRow, 6].Symbol == Symbol.Empty)
                    {
                        Board.BoardTable[Game.destinatedRow, 5] = Board.BoardTable[Game.destinatedRow, 7];
                        Board.BoardTable[Game.destinatedRow, 7] = new Piece(Game.destinatedRow, 7);
                        Board.BoardTable[Game.destinatedRow, 5].Row = Game.destinatedRow;
                        Board.BoardTable[Game.destinatedRow, 5].Column = 5;
                        Board.BoardTable[Game.destinatedRow, 5].Moves++;
                        Game.castling = "Minor";

                        return true;
                    }
                }
                if (Game.destinatedColumn == 2 && Board.BoardTable[Game.destinatedRow, 0].Symbol == Symbol.R && Board.BoardTable[Game.destinatedRow, 0].Moves == 0)
                {
                    if (Board.BoardTable[Game.destinatedRow, 1].Symbol == Symbol.Empty && Board.BoardTable[Game.destinatedRow, 2].Symbol == Symbol.Empty && Board.BoardTable[Game.destinatedRow, 3].Symbol == Symbol.Empty)
                    {
                        Board.BoardTable[Game.destinatedRow, 3] = Board.BoardTable[Game.destinatedRow, 0];
                        Board.BoardTable[Game.destinatedRow, 0] = new Piece(Game.destinatedRow, 0);
                        Board.BoardTable[Game.destinatedRow, 3].Row = Game.destinatedRow;
                        Board.BoardTable[Game.destinatedRow, 3].Column = 3;
                        Board.BoardTable[Game.destinatedRow, 3].Moves++;
                        Game.castling = "Major";

                        return true;
                    }
                }
            }
        }
        return false;
    }

    private static bool CheckIfPiecesProtectsKing(Color color, int row, int column)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (Board.BoardTable[i, j].Symbol != Symbol.Empty && Board.BoardTable[i, j].Symbol != Symbol.N && Board.BoardTable[i, j].Color != color && Board.BoardTable[i, j].IsPossibleMovement(row, column, Game.board))
                {
                    List<int[]> positions = new List<int[]>();
                    int incrementRow = Convert.ToInt32(Math.Abs(row - i));
                    int incrementColumn = Convert.ToInt32(Math.Abs(column - j));

                    if (incrementRow == 0 && incrementColumn != 0)
                    {
                        if (column > j)
                        {
                            for (int c = j; c < j + incrementColumn; c++)
                                positions.Add(new int[] { i, c });
                        }
                        else
                        {
                            for (int c = j; c > j - incrementColumn; c--)
                                positions.Add(new int[] { i, c });
                        }
                    }
                    else if (incrementRow != 0 && incrementColumn == 0)
                    {
                        if (row > i)
                        {
                            for (int r = i; r < i + incrementRow; r++)
                                positions.Add(new int[] { r, j });
                        }
                        else
                        {
                            for (int r = i; r > i - incrementRow; r--)
                                positions.Add(new int[] { r, j });
                        }
                    }
                    else
                    {
                        if (row > i && column > j)
                        {
                            for (int r = i, c = j; r < row; r++, c++)
                            {
                                positions.Add(new int[] { r, c });
                            }
                        }
                        else if (row > i && column < j)
                        {
                            for (int r = i, c = j; r < row; r++, c--)
                            {
                                positions.Add(new int[] { r, c });
                            }
                        }
                        else if (row < i && column > j)
                        {
                            for (int r = i, c = j; r > row; r--, c++)
                            {
                                positions.Add(new int[] { r, c });
                            }
                        }
                        else
                        {
                            for (int r = i, c = j; r > row; r--, c--)
                            {
                                positions.Add(new int[] { r, c });
                            }
                        }
                    }

                    if (CheckIfPiecesGetInTheWay(positions, color))
                        return true;
                }
            }
        }
        return false;
    }

    private static bool CheckIfPiecesGetInTheWay(List<int[]> positions, Color color)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (Board.BoardTable[i, j].Symbol != Symbol.Empty && Board.BoardTable[i, j].Color == color)
                {
                    for (int k = 0; k < positions.Count; k++)
                    {
                        if (Board.BoardTable[i, j].IsPossibleMovement(positions[k][0], positions[k][1], Game.board))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public static bool KingIsInCheck(Color color)
    {
        int kingRow = Board.BlackKingsPosition[0], kingColumn = Board.BlackKingsPosition[1];

        if (color == Color.White)
        {
            kingRow = Board.WhiteKingsPosition[0];
            kingColumn = Board.WhiteKingsPosition[1];
        }

        if (KingWillBeInCheck(color, Game.board, kingRow, kingColumn))
        {
            return true;
        }

        return false;
    }

    public static bool KingWillBeInCheck(Color color, Board board, int row, int column)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (Board.BoardTable[i, j].Symbol != Symbol.Empty && Board.BoardTable[i, j].Color != color && Board.BoardTable[i, j].IsPossibleMovement(row, column, board))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool KingIsInCheckMate(Color color, Board b)
    {
        Color adversaryColor = Color.White;
        int kingRow = Board.BlackKingsPosition[0], kingColumn = Board.BlackKingsPosition[1];
        int possiblePositions = 0, possibleCaptures = 0;
        bool check;

        if (color == Color.White)
        {
            adversaryColor = Color.Black;
            kingRow = Board.WhiteKingsPosition[0];
            kingColumn = Board.WhiteKingsPosition[1];
        }

        check = KingIsInCheck(color);

        int[,] kingsSurroudings = new int[8, 2] { { kingRow + 1, kingColumn }, { kingRow - 1, kingColumn }, { kingRow, kingColumn + 1 }, { kingRow, kingColumn - 1 }, { kingRow + 1, kingColumn + 1 }, { kingRow + 1, kingColumn - 1 }, { kingRow - 1, kingColumn + 1 }, { kingRow - 1, kingColumn - 1 } };

        for (int k = 0; k < 8; k++)
        {
            if (kingsSurroudings[k, 0] != 8 && kingsSurroudings[k, 1] != 8 && kingsSurroudings[k, 0] != -1 && kingsSurroudings[k, 1] != -1)
            {
                if (Board.BoardTable[kingsSurroudings[k, 0], kingsSurroudings[k, 1]].Symbol == Symbol.Empty)
                {
                    possiblePositions++;
                    if (KingWillBeInCheck(color, b, kingsSurroudings[k, 0], kingsSurroudings[k, 1]))
                        possibleCaptures++;
                }
                else if (Board.BoardTable[kingsSurroudings[k, 0], kingsSurroudings[k, 1]].Color == adversaryColor)
                {
                    possiblePositions++;
                    if (KingWillBeInCheck(color, b, kingsSurroudings[k, 0], kingsSurroudings[k, 1]))
                        possibleCaptures++;
                }
            }
        }

        if (possibleCaptures == 1 && CheckIfPiecesProtectsKing(color, kingRow, kingColumn))
            return false;

        if (check && possiblePositions == possibleCaptures) return true;
        else return false;
    }
    #endregion
}
