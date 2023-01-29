using GameHub.TicTacToe.Model.Enum;
using GameHub.TicTacToe.Util;

namespace GameHub.TicTacToe.View;

public class Screen
{
    #region Board
    public static void AdjustSpacesAndGridlines(int size, ref int highestPositionCharactersNumber, ref string gridSplitLine)
    {
        highestPositionCharactersNumber = (size*size).ToString().Length;

        string gridLines = new String('-', highestPositionCharactersNumber + 2) + "+";

        gridSplitLine = string.Concat(Enumerable.Repeat(gridLines, size));
        gridSplitLine = gridSplitLine.Remove(gridSplitLine.Length - 1);
    }

    public static void WriteRed(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void WriteBlue(string text)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void ShowBoard(string[,] board)
    {
        int size = board.GetLength(0);
        int highestPositionCharactersNumber = Constraints.InvalidInt;
        string gridSplitLine = string.Empty;

        AdjustSpacesAndGridlines(size, ref highestPositionCharactersNumber, ref gridSplitLine);

        Console.Clear();
        Console.WriteLine("\n");
        for (int i = 0; i < size; i++)
        {
            Console.Write("\t ");
            for (int j = 0; j < size - 1; j++)
            {
                if (board[i, j] == Piece.X.ToString())
                {
                    WriteRed($" {board[i, j].ToString().PadLeft(highestPositionCharactersNumber)}");
                    Console.Write(" |");
                }
                else if (board[i, j] == Piece.O.ToString())
                {
                    WriteBlue($" {board[i, j].ToString().PadLeft(highestPositionCharactersNumber)}");
                    Console.Write(" |");
                }
                else
                    Console.Write(" {0} |", board[i, j].ToString().PadLeft(highestPositionCharactersNumber));
            }

            if (board[i, size - 1] == "X")
                WriteRed($" {board[i, size - 1].ToString().PadLeft(highestPositionCharactersNumber)}\n");
            else if (board[i, size - 1] == "O")
                WriteBlue($" {board[i, size - 1].ToString().PadLeft(highestPositionCharactersNumber)}\n");
            else 
                Console.Write(" {0}\n", board[i, size - 1].ToString().PadLeft(highestPositionCharactersNumber));

            if (i != size - 1) 
                Console.WriteLine("\t {0}", gridSplitLine);
        }
    }
    #endregion

    #region Game
    public static void AskBoardSize()
    {
        Console.Clear();
        Console.Write("\n\tDigite o tamanho do tabuleiro (de 3 à 15): ");
    }

    public static void AskPosition(string playerName, string piece)
    {
        Console.WriteLine($"\n\tVez de {playerName} ({piece})");
        Console.Write("\n\tDigite a posição: ");
    }

    public static void AskForPlayer(string piece)
    {
        Console.Clear();
        Console.WriteLine("\n\t------------ SELEÇÃO DOS JOGADORES ------------");
        Console.WriteLine($"\n\t                  Jogador ({piece})                  ");
        Thread.Sleep(Constraints.TimeToWait);
    }

    public static void ShowWonGame(string name, string piece)
    {
        Console.WriteLine("\n\t----------------- FIM DE JOGO -----------------");
        Console.WriteLine($"\n\tVencedor(a): {name} ({piece}) recebe +3 Pontos.");
        Console.WriteLine("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }

    public static void ShowTiedGame(string xName, string oName)
    {
        Console.WriteLine("\n\t------------------- EMPATE --------------------");
        Console.WriteLine($"\n\t{xName} (X) recebe +1 Ponto.");
        Console.WriteLine($"\n\t{oName} (O) recebe +1 Ponto.");
        Console.WriteLine("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }
    #endregion

    #region Menu
    public static void ShowTicTacToeMenu()
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

    #region Error Message
    public static void ShowPlayWithYourself()
    {
        Console.Clear();
        WriteRed("\n\tVocê não pode jogar contra si mesmo.");
        Thread.Sleep(Constraints.TimeToWait);
    }
    #endregion
}
