using GameHub.Battleship.Util;
using GameHub.Battleship.View;

namespace GameHub.Battleship.Model;

public class Tutorial
{
    #region Methods
    public static void ShowBattleshipTutorial()
    {
        Console.Clear();
        Console.WriteLine("\n\t# BEM VINDO AO TUTORIAL DE BATALHA NAVAL #");
        Thread.Sleep(Constraints.TimeToWait);

        Console.Clear();
        Console.WriteLine("\n\tApós o login, cada jogador receberá um tabuleiro diferente com navios posicionados " +
            "\n\taleatóriamente de forma escondida.");
        Board board = new Board();
        board.Table = CreateBoardForTutorial();
        ShowBoardForTutorial(board);
        Console.WriteLine("\n\tO exemplo acima demonstra um dos tabuleiros que será recebido pelos jogadores.");
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\tEm seguida, será pedido ao jogador para escolher uma posição a ser revelada.");
        Console.WriteLine("\n\tA posição deve ser escrita contendo a identificação da coluna (a-j) e a identificação" +
            "\n\tda linha (1-10), respectivamente.");
        Console.WriteLine("\n\tPor exemplo, se o jogador escolher a posição e5, então o tabuleiro ficará da" +
            "\n\tseguinte forma: ");
        board.DiscoveredPositions[4, 4] = true;
        ShowBoardForTutorial(board);
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();
        board.DiscoveredPositions[4, 4] = false;

        Console.Clear();
        Console.WriteLine("\n\tNo caso anterior, o jogador teria acertado o mar, e portanto perderia sua vez.");
        Console.WriteLine("\n\tEntão seria pedido ao outro jogador para revelar uma posição de seu tabuleiro.");
        Console.WriteLine("\n\tSe a posição escolhida fosse e6, o jogador teria acertado um navio.");
        board.DiscoveredPositions[5, 4] = true;
        ShowBoardForTutorial(board);
        Console.WriteLine("\n\tQuando um jogador acerta um navio, ele pode jogar novamente até que acerte o mar.");
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\tO jogo termina quando um dos jogadores acerta todos os navios de seu tabuleiro" +
            "\n\te é declarado vencedor.");
        ChangeDiscoveredPositions(ref board);
        ShowBoardForTutorial(board);
        Console.Write("\n\tPressione qualquer tecla para continuar");
        Console.ReadKey();

        Console.Clear();
        Console.WriteLine("\n\t------------------- REGRAS -------------------");
        Console.WriteLine("\n\t1. Não é possível escolher uma posição já revelada.");
        Console.WriteLine("\n\t2. Não é possível escolher uma posição que não existe no tabuleiro.");
        Console.WriteLine("\n\t3. Não é possível jogar contra si mesmo.");
        Console.WriteLine("\n\t4. Não é possível pular sua vez.");
        Console.WriteLine("\n\n\t----------------- APROVEITE! -----------------");
        Console.Write("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }
    
    public static void ChangeDiscoveredPositions(ref Board board)
    {
        board.DiscoveredPositions[1, 1] = true;
        board.DiscoveredPositions[1, 2] = true;
        board.DiscoveredPositions[1, 3] = true;
        board.DiscoveredPositions[1, 4] = true;
        board.DiscoveredPositions[1, 5] = true;

        board.DiscoveredPositions[1, 8] = true;

        board.DiscoveredPositions[2, 7] = true;
        board.DiscoveredPositions[3, 7] = true;

        board.DiscoveredPositions[4, 1] = true;
        board.DiscoveredPositions[5, 1] = true;
        board.DiscoveredPositions[6, 1] = true;
        board.DiscoveredPositions[7, 1] = true;

        board.DiscoveredPositions[6, 3] = true;
        board.DiscoveredPositions[7, 3] = true;
        board.DiscoveredPositions[8, 3] = true;

        board.DiscoveredPositions[5, 4] = true;
        board.DiscoveredPositions[5, 5] = true;
        board.DiscoveredPositions[5, 6] = true;

        board.DiscoveredPositions[9, 8] = true;

        board.DiscoveredPositions[3, 2] = true;
        board.DiscoveredPositions[4, 3] = true;
        board.DiscoveredPositions[6, 0] = true;
        board.DiscoveredPositions[9, 1] = true;
        board.DiscoveredPositions[7, 4] = true;
        board.DiscoveredPositions[3, 6] = true;
        board.DiscoveredPositions[6, 8] = true;
        board.DiscoveredPositions[8, 9] = true;
    }

    public static Ship[,] CreateBoardForTutorial()
    {
        Ship[,] board = new Ship[10, 10];
        for (int i=0; i<10; i++)
        {
            for(int j=0; j<10; j++)
            {
                board[i, j] = new Ship(0);
            }
        }

        Ship ship1 = new Ship(5);
        Ship ship2 = new Ship(4);
        Ship ship3 = new Ship(3);
        Ship ship4 = new Ship(3);
        Ship ship5 = new Ship(2);
        Ship ship6 = new Ship(1);
        Ship ship7 = new Ship(1);

        board[1, 1] = ship1;
        board[1, 2] = ship1;
        board[1, 3] = ship1;
        board[1, 4] = ship1;
        board[1, 5] = ship1;

        board[1, 8] = ship6;

        board[2, 7] = ship5;
        board[3, 7] = ship6;

        board[4, 1] = ship2;
        board[5, 1] = ship2;
        board[6, 1] = ship2;
        board[7, 1] = ship2;

        board[6, 3] = ship3;
        board[7, 3] = ship3;
        board[8, 3] = ship3;

        board[5, 4] = ship4;
        board[5, 5] = ship4;
        board[5, 6] = ship4;

        board[9, 8] = ship7;

        return board;
    }

    public static void ShowBoardForTutorial(Board board)
    {
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
                    Screen.WriteCoveredWater(" ~ ");
                else if (board.Table[i, j].Size == 0)
                    Screen.WriteDiscoveredWater(" ~ ");
                else
                    Screen.WriteDiscoveredShip("   ");
            }
        }
        Console.WriteLine();
    }
    #endregion
}
