using GameHub.Hub.Controller;
using GameHub.Hub.Model;
using GameHub.Hub.Repository;
using GameHub.Hub.Util;
using GameHub.Hub.View;

namespace GameHub.Hub.Service;

public class PlayerService
{
    #region Methods
    public static string GetName(Player player)
    {
        while (true)
        {
            try
            {
                player.Name = Screen.AskName();
                break;
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message); 
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
        return player.Name;
    }

    public static string GetPass(Player player)
    {
        while (true)
        {
            try
            {
                player.Pass = Screen.AskPass();
                break;
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
        return player.Pass;
    }

    public static string GetNewPass(Player player)
    {
        while (true)
        {
            try
            {
                player.Pass = Screen.AskNewPass();
                break;
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
        return player.Pass;
    }

    public static string FindPlayerName()
    {
        string name;
        while (true)
        {
            try
            {
                name = Screen.AskName();
                PlayerController.ValidateName(name);
                break;
            }
            catch(Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
        return name;
    }

    public static string CheckPass(string name)
    {
        string pass;
        while (true)
        {
            try
            {
                pass = Screen.AskPass();
                PlayerController.ValidatePass(name, pass);
                break;
            }
            catch (Exception e)
            {
                Screen.WriteRed(e.Message);
                Thread.Sleep(Constraints.TimeToWait);
            }
        }
        return pass;
    }

    public static void DetailPlayer(string name)
    {
        Console.Clear();
        Console.WriteLine(PlayerController.Players.Find(player => player.Name == name).ToString());
        Screen.ShowComeBack();
    } 

    public static void UpdatePlayerName(ref string name)
    {
        string nameToFind = name;

        Player player = PlayerController.Players.Find(player => player.Name == nameToFind);
        
        name = GetName(player);

        PlayerRepository.Serialize(PlayerController.Players);

        Screen.ShowPlayerNameChanged();
    }

    public static void UpdatePlayerPass(string name)
    {
        Player player = PlayerController.Players.Find(player => player.Name == name);

        CheckPass(name);

        GetNewPass(player);

        PlayerRepository.Serialize(PlayerController.Players);

        Screen.ShowPlayerPassChanged();
    }
    #endregion
}
