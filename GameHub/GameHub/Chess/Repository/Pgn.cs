namespace GameHub.Chess.Repository;

public class Pgn
{
    #region Attributes
    private static string _fullPath;
    private static string _path = @"C:..\..\..\Chess\Repository\MatchData\";
    private static string _fileName;
    private static string _event;
    private static string _site;
    private static string _date;
    private static string _round;
    private static string _pgn;
    #endregion

    #region Methods
    public static void CreatePgnFile(string playerWhite, string playerBlack, string result, string movesText)
    {
        _fileName = DateTime.Now.ToString().Replace('/','_').Replace(':','_').Replace(' ','_');
        _fullPath = _path + _fileName + ".pgn";

        _event = "Casual Game";
        _site = "Chess Console Game";
        _date = DateTime.Now.ToString("yyyy.MM.dd");
        _round = "-";

        _pgn = $"[Event \"{_event}\"]\n" +
            $"\n[Site \"{_site}\"]\n" +
            $"\n[Date \"{_date}\"]\n" +
            $"\n[Round \"{_round}\"]\n" +
            $"\n[White \"{playerWhite}\"]\n" +
            $"\n[Black \"{playerBlack}\"]\n" +
            $"\n[Result \"{result}\"]\n\n" +
            movesText;

        if(!File.Exists(_fullPath))
        {
            using (StreamWriter sw = File.CreateText(_fullPath))
            {
                sw.WriteLine(_pgn);
            }
        }
    }
    #endregion
}
