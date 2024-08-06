namespace GameAutoChess.Class;
using GameAutoChess.Interface;

public class Player : IPlayer
{
    public int IdPlayer { get; set; }
    public string NamePlayer { get; set; }
    
    public Player(int idPlayer, string namePlayer)
    
    {
        IdPlayer = idPlayer;
        NamePlayer = namePlayer;
    }

    public Player(int idPlayer)
    {
        IdPlayer = idPlayer;
    }

    public int GetPlayerId()
    {
        return IdPlayer;
    }
    public string GetPlayerName()
    {
        return NamePlayer;
    }
}