using GameHub.Hub.Controller;
using GameHub.Hub.Exceptions;
using GameHub.Hub.Model.Entity;
using GameHub.Hub.Service;

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
            else if (value.Length != 1 || int.Parse(value) > 4)
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
                Thread.Sleep(2600);
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
                
        } while (Option != "0");
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
                Thread.Sleep(2600);
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

        } while (Option != "0");

        _option = "10";
    }

    private void StartGameMenu()
    {
        do
        {
            Screen.ShowGameMenu();

            try
            {
                Option = Console.ReadLine();
                if (int.Parse(Option) > 2)
                    throw new MenuException("\n\tA opção inserida é invalida.");
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(2600);
            }

            switch (Option)
            {
                case "1":
                    break;
                case "2":
                    break;
            }

        } while (Option != "0");

        _option = "10";
    }
    #endregion
}
