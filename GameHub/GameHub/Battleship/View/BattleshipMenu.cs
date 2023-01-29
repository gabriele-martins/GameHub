using GameHub.Battleship.Exceptions;
using GameHub.Battleship.Model;
using GameHub.Battleship.Util;

namespace GameHub.Battleship.View;

public class BattleshipMenu
{
    #region Properties and Attributes
    private static string _option;
    public static string Option
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
    public static void StartBattleshipMenu()
    {
        do
        {
            Screen.ShowBattleshipMenu();

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
                    Tutorial.ShowBattleshipTutorial();
                    break;
                case "2":
                    Ranking.ShowBattleshipRanking();
                    break;
                case "3":
                    Game.Play();
                    break;
            }

        } while (Option != Constraints.ZeroOption);
    }
    #endregion
}