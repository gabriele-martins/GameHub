using GameHub.Hub.Util;

namespace GameHub.Hub.View;

public static class Screen
{
    #region Opening and Closing
    public static void StartHub()
    {
        Console.Title = "Game Hub";
        Console.WriteLine("\n\tBem vindo(a) ao Game Hub.");
        Console.Write("\n\tPressione qualquer tecla para começar");
        Console.ReadKey();
    }

    public static void ExitHub()
    {
        Console.Clear();
        Console.WriteLine("\n\tEncerrando o Game Hub.");
        Console.WriteLine("\n\tObrigado por jogar.");
    }
    #endregion

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
    public static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\tEscolha uma opção:");
        Console.WriteLine("\n\t1 - Cadastrar novo jogador");
        Console.WriteLine("\t2 - Manipular conta de jogador");
        Console.WriteLine("\t3 - Ver ranking geral");
        Console.WriteLine("\t4 - Jogar");
        Console.WriteLine("\t0 - Sair do Hub");
        Console.Write("\n\tDigite a opção desejada: ");
    }

    public static void ShowAccountMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\tEscolha uma opção:");
        Console.WriteLine("\n\t1 - Detalhar conta");
        Console.WriteLine("\t2 - Alterar nome");
        Console.WriteLine("\t3 - Alterar senha");
        Console.WriteLine("\t4 - Deletar conta");
        Console.WriteLine("\t0 - Voltar para o menu principal");
        Console.Write("\n\tDigite a opção desejada: ");
    }

    public static void ShowGameMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\tEscolha uma opção:");
        Console.WriteLine("\n\t1 - Jogo da Velha");
        Console.WriteLine("\t2 - Batalha Naval");
        Console.WriteLine("\t0 - Voltar para o menu principal");
        Console.Write("\n\tDigite a opção desejada: ");
    }
    #endregion

    #region Requests
    public static string AskName()
    {
        Console.Clear();
        Console.Write("\n\tDigite o nome: ");
        string nick = Console.ReadLine();
        return nick;
    }

    public static string AskPass()
    {
        Console.Clear();
        Console.Write("\n\tDigite a senha: ");
        string pass = Console.ReadLine();
        return pass;
    }

    public static string AskNewPass()
    {
        Console.Clear();
        Console.Write("\n\tDigite a nova senha: ");
        string pass = Console.ReadLine();
        return pass;
    }

    public static string ConfirmPlayerRemoval()
    {
        string option;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n\tVocê tem certeza que deseja excluir sua conta? ");
            Console.Write("\n\tDigite S para sim e N para não: ");

            option = Console.ReadLine().ToUpper();

            if (option == "S" || option == "N")
                return option;
            else
            {
                WriteRed("\n\tA opção inserida é invalida.");
                Thread.Sleep(Constraints.TimeToWait);
                continue;
            }
        }
    }
    #endregion

    #region Success Messages
    public static void ShowPlayerAdded()
    {
        WriteGreen("\n\tJogador adicionado com sucesso.");
        Thread.Sleep(Constraints.TimeToWait);
    }

    public static void ShowPlayerDeleted()
    {
        WriteGreen("\n\tJogador removido com sucesso.");
        Thread.Sleep(Constraints.TimeToWait);
    }

    public static void ShowPlayerNameChanged()
    {
        WriteGreen("\n\tSeu nome foi alterado com sucesso.");
        Thread.Sleep(Constraints.TimeToWait);
    }

    public static void ShowPlayerPassChanged()
    {
        WriteGreen("\n\tSua senha foi alterada com sucesso.");
        Thread.Sleep(Constraints.TimeToWait);
    }
    #endregion

    #region ReadKey Messages
    public static void ShowComeBack()
    {
        Console.Write("\n\tPressione qualquer tecla para voltar");
        Console.ReadKey();
    }
    #endregion
}
