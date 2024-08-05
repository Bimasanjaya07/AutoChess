// Class/PieceStore/PieceStore.cs
using GameAutoChess.Class.Board;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.PieceStore;
public class PieceStore : IPieceStore
{
    public int IdPieceStore { get; set; }
    public int PriceRefreshStore { get; set; }
    public PlayerData CoinPlayer { get; set; }
    public List<IChessPiece> ListChessPiece { get; set; }
    public RarityPiece Rarity { get; set; }

    public PieceStore(int idPieceStore, int priceRefreshStore, List<IChessPiece> listChessPiece, PlayerData coinPlayer)
    {
        IdPieceStore = idPieceStore;
        PriceRefreshStore = priceRefreshStore;
        ListChessPiece = listChessPiece;
        CoinPlayer = coinPlayer;
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
    public bool BuyPiece(IChessPiece piece, int price, Deck deck)
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

    public bool BuyPiece(IChessPiece pieceToBuy, int price)
    {
        if (CoinPlayer.Coins >= price)
        {
            CoinPlayer.Coins -= price;
            ListChessPiece.Remove(pieceToBuy);
            return true;
        }
        return false;
    }

    public int GetPieceSToreId()
    {
        return IdPieceStore;
    }
}