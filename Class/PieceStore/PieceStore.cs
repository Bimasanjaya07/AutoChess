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

    public bool BuyPiece(IChessPiece chessPiece, int price, Deck deck)
    {
        if (!deck.IsDeckFull())
        {
            if (price == chessPiece.GetDetail().Price && CoinPlayer.Coins >= price)
            {
                CoinPlayer.Coins -= price;
                deck.AddPieceFromStore(chessPiece);
                ListChessPiece.Remove(chessPiece);
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