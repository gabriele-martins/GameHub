using GameHub.Chess.Util;
using GameHub.Hub.Model;

namespace GameHub.Chess.View;

public class Screen
{
    #region Color Messages
    public static void WriteRed(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void WriteGreen(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    #endregion

    #region Menus
    public static void ShowChessMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\tEscolha uma opção:");
        Console.WriteLine("\n\t1 - Ver ranking do jogo");
        Console.WriteLine("\t2 - Jogar");
        Console.WriteLine("\t0 - Voltar para o menu anterior");
        Console.Write("\n\tDigite a opção desejada: ");
    }

    public static void ShowPawnPromotionMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\tParabéns, seu peão foi promovido.");
        Console.WriteLine("\n\tEscolha qual peça seu peão irá se tornar: ");
        Console.WriteLine("\n\tQ - Rainha");
        Console.WriteLine("\tR - Torre;");
        Console.WriteLine("\tB - Bispo;");
        Console.WriteLine("\tN - Cavalo;");
        Console.Write("\n\tDigite a letra da peça: ");
    }
    #endregion

    #region Requests
    public static void AskForPlayer(string color)
    {
        Console.Clear();
        Console.WriteLine("\n\t------------ SELEÇÃO DOS JOGADORES ------------");
        Console.WriteLine($"\n\t               Jogador ({color})                  ");
        Thread.Sleep(Constraints.TimeToWait);
    }
    public static void AskTie()
    {
        Console.WriteLine("\n\tO outro jogador solicitou empate. Digite \"sim\" para aceitar ou \"não\" para continuar a partida.");
    }
    #endregion

    #region Error Messages
    public static void ShowInvalidMovement()
    {
        Console.Clear();
        WriteRed("\n\tEsse movimento não é válido para essa peça.");
        ShowTryAgainMessage();
    }

    public static void ShowPuttingKingInCheckMessage()
    {
        Console.Clear();
        WriteRed("\n\tVocê não pode colocar seu Rei em xeque.");
        ShowTryAgainMessage();
    }
    #endregion

    #region Success Messages
    public static void ShowSurrenderMessage(Player player)
    {
        WriteGreen("\n\n\t -------- RENDIÇÃO --------");
        WriteGreen($"\n\tVencedor: {player.Name}");
        ShowBackMessage();
    }

    public static void ShowTieMessage()
    {
        WriteGreen("\n\n\t --------- EMPATE ---------");
        ShowBackMessage();
    }

    public static void ShowCheckMateMessage(Player player)
    {
        WriteGreen("\n\n\t ------- XEQUE MATE -------");
        WriteGreen($"\n\tVencedor: {player.Name}");
        ShowBackMessage();
    }
    #endregion

    #region ReadKey Messages
    public static void ShowTryAgainMessage()
    {
        Console.Write("\n\tPressione qualquer tecla para tentar novamente");
        Console.ReadKey();
    }

    public static void ShowBackMessage()
    {
        Console.Write("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }
    #endregion
}
