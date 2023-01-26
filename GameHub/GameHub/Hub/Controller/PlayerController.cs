using GameHub.Hub.Exceptions;
using GameHub.Hub.Model;
using GameHub.Hub.Repository;
using GameHub.Hub.Service;
using GameHub.Hub.Util;
using GameHub.Hub.View;

namespace GameHub.Hub.Controller;

public class PlayerController
{
    #region Properties
    public static List<Player> Players { get; set; } = PlayerRepository.Deserialize();
    #endregion

    #region Methods
    public static void ValidateName(string name)
    {
        if (Players.Count == Constraints.ZeroPlayers || !Players.Exists(player => player.Name == name))
            throw new PlayerException("\n\tO nome digitado não é válido ou não foi encontrado.");
    }

    public static void ValidatePass(string name, string pass)
    {
        if (Players.Find(player => player.Name == name).Pass != pass)
            throw new PlayerException("\n\tSenha incorreta. Tente novamente.");
    }

    public static void AddPlayer()
    {
        Player newPlayer = new Player();

        newPlayer.Name = PlayerService.GetName(newPlayer);
        newPlayer.Pass = PlayerService.GetPass(newPlayer);

        Players.Add(newPlayer);
        PlayerRepository.Serialize(Players);
        Screen.ShowPlayerAdded();
    }

    public static void DeletePlayer(string name, ref string optionDeleteAccount)
    {
        string option = Screen.ConfirmPlayerRemoval();
        if(option == Constraints.Yes)
        {
            Players.RemoveAll(cliente => cliente.Name == name);
            PlayerRepository.Serialize(Players);
            Screen.ShowPlayerDeleted();
            optionDeleteAccount = Constraints.ZeroOption;
        }
    }
    #endregion
}
