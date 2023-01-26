using GameHub.Hub.Controller;
using GameHub.Hub.Exceptions;

namespace GameHub.Hub.Model;

public class Player
{
    #region Properties and Attributes
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value) || value.All(c => char.IsWhiteSpace(c)))
                throw new PlayerException("\n\tO nome não pode ser nulo ou vazio.");
            else if (value.Length < 3)
                throw new PlayerException("\n\tO nome deve conter no mínimo 3 caractéres.");
            else if (value.All(c => char.IsDigit(c)) || value.All(c => char.IsPunctuation(c)) || value.All(c => char.IsSymbol(c)))
                throw new PlayerException("\n\tO nome não pode conter apenas números, pontuações ou símbolos.");
            else if (PlayerController.Players != null && PlayerController.Players.Exists(player => player.Name == value))
                throw new PlayerException("\n\tJogador com mesmo nome já cadastrado.");
            else
                _name = value;
        }
    }

    private string _pass;
    public string Pass
    {
        get { return _pass; }
        set
        {
            if (string.IsNullOrEmpty(value) || value.All(c => char.IsWhiteSpace(c)))
                throw new PlayerException("\n\tA senha não pode ser nula ou vazia.");
            else if (value.Length < 5)
                throw new PlayerException("\n\tA senha deve conter no mínimo 5 caractéres.");
            else
                _pass = value;
        }
    }

    public Score HubScore { get; set; }
    public Score BattleshipScore { get; set; }
    public Score TicTacToeScore { get; set; }

    #endregion

    #region Constructor
    public Player()
    {
        HubScore = new Score();
        BattleshipScore = new Score();
        TicTacToeScore = new Score();
    }
    #endregion

    #region Methods
    public override string ToString()
    {
        return $"\n\t------------- DETALHES DE JOGADOR -------------" +
            $"\n\tNome: {Name}" +
            $"\n\tSenha: {Pass}" +
            $"\n\t-------------------- Total --------------------" +
            $"\n\tPontos: {HubScore.Points}" +
            $"\n\tPartidas Jogadas: {HubScore.Matches}" +
            $"\n\tVitórias: {HubScore.Wins}" +
            $"\n\tDerrotas: {HubScore.Defeats}" +
            $"\n\tEmpates: {HubScore.Ties}" +
            $"\n\t---------------- Batalha Naval ----------------" +
            $"\n\tPontos: {BattleshipScore.Points}" +
            $"\n\tPartidas Jogadas: {BattleshipScore.Matches}" +
            $"\n\tVitórias: {BattleshipScore.Wins}" +
            $"\n\tDerrotas: {BattleshipScore.Defeats}" +
            $"\n\tEmpates: {BattleshipScore.Ties}" +
            $"\n\t---------------- Jogo da Velha ----------------" +
            $"\n\tPontos: {TicTacToeScore.Points}" +
            $"\n\tPartidas Jogadas: {TicTacToeScore.Matches}" +
            $"\n\tVitórias: {TicTacToeScore.Wins}" +
            $"\n\tDerrotas: {TicTacToeScore.Defeats}" +
            $"\n\tEmpates: {TicTacToeScore.Ties}";
    }
    #endregion
}
