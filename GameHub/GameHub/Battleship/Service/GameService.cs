using GameHub.Battleship.Exceptions;
using GameHub.Battleship.Model;
using GameHub.Battleship.Util;
using GameHub.Battleship.View;
using GameHub.Hub.Controller;
using GameHub.Hub.Model;
using GameHub.Hub.Repository;

namespace GameHub.Battleship.Service;

public class GameService
{
    public static void GetPosition(string playerName, int playerNumber, ref Board board, ref bool end)
    {
        string position;
        while (true)
        {
            Screen.ShowBoard(board);

            try
            {
                Screen.AskPosition(playerName, playerNumber, board);
                position = Console.ReadLine();

                if (string.IsNullOrEmpty(position) || position.All(c => char.IsWhiteSpace(c)))
                    throw new GameException("\n\tA posição não pode ser nula ou vazia.");
                else if (position.Length < Constraints.MinimumPositionLength || position.Length > Constraints.MaximumPositionLength || !char.IsLetter(position[0]) || !char.IsDigit(position[1]))
                    throw new GameException("\n\tA posição deve ser uma letra (a-j) e um número (1-10).");
                else
                {
                    string column = position[0].ToString(), row;
                    if (position.Length == Constraints.MinimumPositionLength)
                        row = position[1].ToString();
                    else
                        row = position[1].ToString() + position[2].ToString();

                    if (!board.Rows.Contains(row) || !board.Columns.Contains(column))
                        throw new GameException("\n\tEssa posição não existe no tabuleiro.");

                    int i = Array.IndexOf(board.Rows, row);
                    int j = Array.IndexOf(board.Columns, column);

                    if (board.DiscoveredPositions[i, j])
                        throw new GameException("\n\tPosição já descoberta.");

                    if (UpdateBoard(i, j, ref board))
                    {
                        end = CheckEndGame(board.ShipsQuantity);
                        break;
                    }
                    else
                    {
                        end = CheckEndGame(board.ShipsQuantity);
                        if (end == false)
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
    }

    public static bool UpdateBoard(int row, int column, ref Board board)
    {
        if (board.Table[row, column].Size == 0)
        {
            board.DiscoveredPositions[row, column] = true;
            Screen.ShowBoard(board);
            Thread.Sleep(Constraints.TimeToShowBoard);
            return true;
        }
        else
        {
            board.DiscoveredPositions[row, column] = true;
            UpdateShipsQuantity(ref board);
            return false;
        }
    }

    public static void UpdateShipsQuantity(ref Board board)
    {
        foreach(Ship ship in board.Ships)
        {
            int countDiscoveredPositionsShip = 0;
            for(int r = 0; r < ship.Size; r++)
            {
                if (board.DiscoveredPositions[ship.Positions[r,0], ship.Positions[r,1]])
                    countDiscoveredPositionsShip++;
            }
            
            if(countDiscoveredPositionsShip == ship.Size)
            {
                board.ShipsQuantity--;
                board.Ships.Remove(ship);
                break;
            }
        }
    }

    public static bool CheckEndGame(int shipsQuantity)
    {
        if (shipsQuantity == 0) return false;
        else return true;
    }

    public static void UpdatePlayersScore(Player winner, Player loser)
    {
        winner.HubScore.Matches++;
        winner.BattleshipScore.Matches++;
        winner.HubScore.Wins++;
        winner.BattleshipScore.Wins++;

        loser.HubScore.Matches++;
        loser.BattleshipScore.Matches++;
        loser.HubScore.Defeats++;
        loser.BattleshipScore.Defeats++;

        PlayerRepository.Serialize(PlayerController.Players);
    }
}
