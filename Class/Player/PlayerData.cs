using GameAutoChess.Class.Board;
using GameAutoChess.Interface;

namespace GameAutoChess.Class;

public class PlayerData
{
    public int Id { get; }
    public int Coins { get; set; }
    public decimal HealthPlayer { get; set; }
    public int WinStreak { get; set; }
    public int LoseStreak { get; set; }
    public bool ResultMatchWin { get; set; }
    public int[] InventoryItem { get; set; }
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
    }

    public void RecordMatchResult(bool result)
    {
        ResultMatchWin = result;
        if (result)
        {
            WinStreak++;
            LoseStreak = 0;
        }
        else
        {
            LoseStreak++;
            WinStreak = 0;
        }
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