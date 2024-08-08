// Class/PieceStore/PieceStore.cs
using GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;
using GameAutoChess.Class.ChessPiece.PieceName;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.PieceStore;
public class PieceStore : IPieceStore
{
    public int Id { get; set; }
    public int PriceRefreshStore { get; set; }
    public PlayerData CoinPlayer { get; set; }
    public List<IChessPiece> ListChessPiece { get; set; }
    public RarityPiece Rarity { get; set; }
    private List<IChessPiece> _allPieces;

    public PieceStore(int id, int priceRefreshStore, List<IChessPiece> listChessPiece, PlayerData coinPlayer, List<IChessPiece> allPieces)
    {
        Id = id;
        PriceRefreshStore = priceRefreshStore;
        ListChessPiece = listChessPiece;
        CoinPlayer = coinPlayer;
        _allPieces = allPieces;
    }
    public List<IChessPiece> GetAllPiece()
    {
        return ListChessPiece;
    }

    public List<IChessPiece> RefreshStore()
    {
        if(CoinPlayer.Coins >= PriceRefreshStore)
        {
            CoinPlayer.Coins -= PriceRefreshStore;
            ListChessPiece.Clear();
            ListChessPiece = new List<IChessPiece>();
            return ListChessPiece;
        }

        return null;
    }

    // PieceStore.cs
    public bool BuyPiece(IChessPiece piece, int price, IDeck deck)
    {
        if (piece != null && deck != null && !deck.IsDeckFull())
        {
            if (CoinPlayer.Coins >= price)
            {
                CoinPlayer.Coins -= price;
                deck.AddPieceFromStore(piece);
                ListChessPiece.Remove(piece); // Remove piece from store after buying
                return true;
            }
        }
        return false;
    }
    public IChessPiece GetPiece(int idPiece)
    {
        foreach (var piece in ListChessPiece)
        {
            if (piece.GetDetail().IdChessPiece == idPiece)
            {
                return piece;
            }
        }
        return null;
    }

    public int GetPieceSToreId()
    {
        return Id;
    }
    
    public List<IChessPiece> RandomizePiece()
    {
        ListChessPiece.Clear();
        ListChessPiece = _allPieces.OrderBy(x => Guid.NewGuid()).Take(3).ToList(); // Randomly select 3 pieces
        return ListChessPiece;
    }
}