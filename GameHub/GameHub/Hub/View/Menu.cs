using GameHub.Battleship.View;
using GameHub.Hub.Controller;
using GameHub.Hub.Exceptions;
using GameHub.Hub.Model;
using GameHub.Hub.Service;
using GameHub.Hub.Util;
using GameHub.TicTacToe.View;

namespace GameHub.Hub.View;

public class Menu
{
    #region Properties and Attributes
    private string _option;
    public string Option 
    {
        get { return _option; }
        set
        {
            _option = "10";
            if (string.IsNullOrEmpty(value) || value.All(c => char.IsWhiteSpace(c)))
                throw new MenuException("\n\tA opção não pode ser nula ou vazia.");
            else if (!value.All(c => char.IsDigit(c)))
                throw new MenuException("\n\tA opção só pode conter números.");
            else if (value.Length != Constraints.OptionLength || int.Parse(value) > Constraints.MenuOptionMaximumLength)
                throw new MenuException("\n\tA opção inserida é invalida.");
            else
                _option = value;
        }
    }
    #endregion

    #region Methods
    public void StartMainMenu()
    {
        Screen.StartHub();

        do
        {
            Screen.ShowMainMenu();

            try
            {
                Option = Console.ReadLine();
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }

            switch (Option)
            {
                case "0":
                    Screen.ExitHub();
                    break;
                case "1":
                    PlayerController.AddPlayer();
                    break;
                case "2":
                    StartAccountMenu();
                    break;
                case "3":
                    Ranking.ShowHubRanking();
                    break;
                case "4":
                    StartGameMenu();
                    break;
            }
                
        } while (Option != Constraints.ZeroOption);
    }

    private void StartAccountMenu()
    {
        string optionDeleteAccount = string.Empty;
        string name = PlayerService.FindPlayerName();
        PlayerService.CheckPass(name);

        do
        {
            Screen.ShowAccountMenu();

            try
            {
                Option = Console.ReadLine();
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }

            switch (Option)
            {
                case "1":
                    PlayerService.DetailPlayer(name);
                    break;
                case "2":
                    PlayerService.UpdatePlayerName(ref name);
                    break;
                case "3":
                    PlayerService.UpdatePlayerPass(name);
                    break;
                case "4":
                    PlayerController.DeletePlayer(name, ref optionDeleteAccount);
                    _option = optionDeleteAccount;
                    break;
            }

        } while (Option != Constraints.ZeroOption);

        _option = Constraints.ComeBackOption;
    }

    private void StartGameMenu()
    {
        do
        {
            Screen.ShowGameMenu();

            try
            {
                Option = Console.ReadLine();
                if (int.Parse(Option) > Constraints.GameMenuOptionMaximumLength)
                    throw new MenuException("\n\tA opção inserida é invalida.");
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }

            switch (Option)
            {
                case "1":
                    TicTacToeMenu.StartTicTacToeMenu();
                    break;
                case "2":
                    BattleshipMenu.StartBattleshipMenu();
                    break;
            }

        } while (Option != Constraints.ZeroOption);

        _option = Constraints.ComeBackOption;
    }
    #endregion
}
