namespace GameAutoChess.Class;
using GameAutoChess.Interface;

public class Player : IPlayer
{
    public int Id { get; set; }
    public string NamePlayer { get; set; }
    
    public Player(int id, string namePlayer)
    
    {
        Id = id;
        NamePlayer = namePlayer;
    }

    public Player(int id)
    {
        Id = id;
    }

    public int GetPlayerId()
    {
        return Id;
    }
    public string GetPlayerName()
    {
        return NamePlayer;
    }
}