using GameHub.TicTacToe.View;
using GameHub.TicTacToe.Util;
using GameHub.TicTacToe.Model.Enum;

namespace GameHub.TicTacToe.Model;

public class Tutorial
{
    #region Methods
    public static void ShowTicTacToeTutorial()
    {
        Console.Clear();
        Console.WriteLine("\n\t# BEM VINDO AO TUTORIAL DO JOGO DA VELHA #");
        Thread.Sleep(Constraints.TimeToWait);

        Console.Clear();
        Console.WriteLine("\n\tPrimeiramente os jogadores deverão escolher o tamanho do tabuleiro.");
        Console.WriteLine("\n\tO tamanho é dado pelo número de linhas/colunas da matriz quadrada que forma o tabuleiro.");
        Console.WriteLine("\n\tSe o tamanho for 3, o tabuleiro será como mostrado abaixo:");
        Board board = new Board(3);
        ShowBoardForTutorial(board.Table);
        Console.WriteLine("\n\tO tamanho pode variar de 3 a 15.");
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\tEm seguida, será pedido ao jogador para informar a posição do tabuleiro em que " +
            "\n\tdeseja posicionar sua peça (X ou O).");
        Console.WriteLine("\n\tSe a posição escolhida for 5, por exemplo, o tabuleiro ficará da seguinte forma:");
        board.Table[1, 1] = "X";
        ShowBoardForTutorial(board.Table);
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\tSe o outro jogador escolher a posição 1, o tabuleiro ficará da seguinte forma:");
        board.Table[0, 0] = "O";
        ShowBoardForTutorial(board.Table);
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\tO tabuleiro deve ser completamente preenchido até que algum jogador complete uma " +
            "\n\tlinha, uma coluna ou uma das diagonais.");
        board.Table[0, 0] = "X";
        board.Table[0, 1] = "O";
        board.Table[1, 0] = "O";
        board.Table[2, 2] = "X";
        ShowBoardForTutorial(board.Table);
        Console.WriteLine("\n\tNo exemplo acima, o jogador com a peça X venceu o jogo.");
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\tSe todas as posições forem preenchidas e nenhum jogador conseguir completar uma " +
            "\n\tlinha, coluna ou diagonal com suas peças, então os jogadores empataram.");
        board.Table[0, 0] = "O";
        board.Table[0, 1] = "X";
        board.Table[0, 2] = "O";
        board.Table[1, 0] = "X";
        board.Table[1, 1] = "X";
        board.Table[1, 2] = "O";
        board.Table[2, 0] = "X";
        board.Table[2, 1] = "O";
        board.Table[2, 2] = "X";
        ShowBoardForTutorial(board.Table);
        Console.WriteLine("\n\tNo exemplo acima os jogadores empataram.");
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\t------------------- REGRAS -------------------");
        Console.WriteLine("\n\t1. Não é possível escolher uma posição já tomada pelo adversário.");
        Console.WriteLine("\n\t2. Não é possível escolher uma posição que não existe no tabuleiro.");
        Console.WriteLine("\n\t3. Não é possível jogar contra si mesmo.");
        Console.WriteLine("\n\t4. Não é possível pular sua vez.");
        Console.WriteLine("\n\n\t----------------- APROVEITE! -----------------");
        Console.Write("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }

    public static void ShowBoardForTutorial(string[,] board)
    {
        int size = board.GetLength(0);
        int highestPositionCharactersNumber = Constraints.InvalidInt;
        string gridSplitLine = string.Empty;

        Screen.AdjustSpacesAndGridlines(size, ref highestPositionCharactersNumber, ref gridSplitLine);

        Console.Write("\n");
        for (int i = 0; i < size; i++)
        {
            Console.Write("\t ");
            for (int j = 0; j < size - 1; j++)
            {
                if (board[i, j] == Piece.X.ToString())
                {
                    Screen.WriteRed($" {board[i, j].ToString().PadLeft(highestPositionCharactersNumber)}");
                    Console.Write(" |");
                }
                else if (board[i, j] == Piece.O.ToString())
                {
                    Screen.WriteBlue($" {board[i, j].ToString().PadLeft(highestPositionCharactersNumber)}");
                    Console.Write(" |");
                }
                else
                    Console.Write(" {0} |", board[i, j].ToString().PadLeft(highestPositionCharactersNumber));
            }

            if (board[i, size - 1] == "X")
                Screen.WriteRed($" {board[i, size - 1].ToString().PadLeft(highestPositionCharactersNumber)}\n");
            else if (board[i, size - 1] == "O")
                Screen.WriteBlue($" {board[i, size - 1].ToString().PadLeft(highestPositionCharactersNumber)}\n");
            else
                Console.Write(" {0}\n", board[i, size - 1].ToString().PadLeft(highestPositionCharactersNumber));

            if (i != size - 1)
                Console.WriteLine("\t {0}", gridSplitLine);
        }
    }
    #endregion
}
