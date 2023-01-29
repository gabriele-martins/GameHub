using GameHub.Battleship.Model;
using GameHub.Battleship.Util;

namespace GameHub.Battleship.View;

public class Screen
{
    #region Board
    public static void WriteCoveredWater(string text)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void WriteDiscoveredWater(string text)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void WriteDiscoveredShip(string text)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void ShowBoard(Board board)
    {
        Console.Clear();
        Console.Write("\n\t   ");
        for (int i = 0; i < Constraints.BoardSize; i++)
        {
            Console.Write($" {board.Columns[i]} ");
        }

        for (int i = 0; i < Constraints.BoardSize; i++)
        {
            if (board.Rows[i] != "10")
                Console.Write($"\n\t {board.Rows[i]} ");
            else
                Console.Write($"\n\t{board.Rows[i]} ");

            for (int j = 0; j < Constraints.BoardSize; j++)
            {
                if (board.DiscoveredPositions[i, j] == false)
                    WriteCoveredWater(" ~ ");
                else if (board.Table[i, j].Size == 0)
                    WriteDiscoveredWater(" ~ ");
                else
                    WriteDiscoveredShip("   ");
            }
        }
        Console.WriteLine();
    }

    #endregion

    #region Game
    public static void AskForPlayer(int number)
    {
        Console.Clear();
        Console.WriteLine("\n\t------------ SELEÇÃO DOS JOGADORES ------------");
        Console.WriteLine($"\n\t                   Jogador {number}                   ");
        Thread.Sleep(Constraints.TimeToWait);
    }

    public static void AskPosition(string playerName, int number, Board board)
    {
        Console.WriteLine($"\n\tRestam {board.ShipsQuantity} navios.");
        Console.WriteLine($"\n\n\tVez de {playerName} (Jogador {number})");
        Console.Write("\n\tDigite a posição: ");
    }

    public static void ShowWonGame(string name, Board board)
    {
        Console.Clear();
        ShowBoard(board);
        Console.WriteLine("\n\t----------------- FIM DE JOGO -----------------");
        Console.WriteLine($"\n\tVencedor(a): {name} recebe +3 Pontos.");
        Console.WriteLine("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }
    #endregion

    #region Menu
    public static void ShowBattleshipMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\tEscolha uma opção:");
        Console.WriteLine("\n\t1 - Ver tutorial");
        Console.WriteLine("\t2 - Ver ranking do jogo");
        Console.WriteLine("\t3 - Jogar");
        Console.WriteLine("\t0 - Voltar para o menu anterior");
        Console.Write("\n\tDigite a opção desejada: ");
    }
    #endregion

    #region Color Messages
    public static void WriteRed(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    #endregion

    #region Error Messages
    public static void ShowPlayWithYourself()
    {
        Console.Clear();
        WriteRed("\n\tVocê não pode jogar contra si mesmo.");
        Thread.Sleep(Constraints.TimeToWait);
    }
    #endregion
}
