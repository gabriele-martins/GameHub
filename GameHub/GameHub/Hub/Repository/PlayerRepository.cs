using GameHub.Hub.Model;
using GameHub.Hub.View;
using System.Text.Json;

namespace GameHub.Hub.Repository;

public class PlayerRepository
{
    #region Attributes
    private static readonly JsonSerializerOptions _identation = new JsonSerializerOptions { WriteIndented = true };
    private static readonly string _jsonPath = @"..\..\..\Hub\Repository\Data\Players.json";
    #endregion

    #region Methods
    private static void CheckIfFileExists()
    {
        if (!File.Exists(_jsonPath))
        {
            File.WriteAllText(_jsonPath, "[]");
        }
    }

    public static void Serialize(List<Player> players)
    {
        try
        {
            string playersJson = JsonSerializer.Serialize(players, _identation);
            File.WriteAllText(_jsonPath, playersJson);
        }
        catch (Exception e)
        {
            Screen.WriteRed($"\n\t{e.Message}");
            Thread.Sleep(2600);
        }
    }

    public static List<Player> Deserialize()
    {
        CheckIfFileExists();

        List<Player> playersFromJson = new List<Player>();
        
        try
        {
            string jsonLines = File.ReadAllText(_jsonPath);
            List<Player>? leitura = JsonSerializer.Deserialize<List<Player>?>(jsonLines, _identation);

            if (leitura != null)
            {
                playersFromJson.AddRange(leitura);
            }
        }
        catch (Exception e)
        {
            Screen.WriteRed($"\n\t{e.Message}");
            Thread.Sleep(2600);
        }
        
        return playersFromJson;
    }
    #endregion
}