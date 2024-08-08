// PlayerData.cs

using GameAutoChess.Class.Board;
using GameAutoChess.Interface;

public class PlayerData
{
    public int Id { get; }
    public int Coins { get; set; }
    public decimal HealthPlayer { get; set; }
    public int WinStreak { get; set; }
    public int LoseStreak { get; set; }
    public bool ResultMatchWin { get; set; }
    public List<IChessPiece> ChessPieces { get; set; }
    public IPieceStore PieceStore { get; set; }
    public Deck Deck { get; set; }

    public PlayerData(int id, int coins, decimal healthPlayer, int winStreak, int loseStreak, bool resultMatchWin)
    {
        Id = id;
        Coins = coins;
        HealthPlayer = healthPlayer;
        WinStreak = winStreak;
        LoseStreak = loseStreak;
        ResultMatchWin = resultMatchWin;
        ChessPieces = new List<IChessPiece>(); // Initialize the ChessPieces list
    }

    public int GetCoins()
    {
        return Coins;
    }

    public void AddChessPiece(IChessPiece chessPiece)
    {
        ChessPieces.Add(chessPiece);
    }

    public List<IChessPiece> GetChessPieces()
    {
        return ChessPieces;
    }
}