using GameHub.Chess.Exceptions;
using GameHub.Chess.Model;
using GameHub.Chess.Util;

namespace GameHub.Chess.View;

public class ChessMenu
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
    public static void StartChessMenu()
    {
        do
        {
            Screen.ShowChessMenu();

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
                    Ranking.ShowChessRanking();
                    break;
                case "2":
                    Game.Play();
                    break;
            }

        } while (Option != Constraints.ZeroOption);
    }
    #endregion
}