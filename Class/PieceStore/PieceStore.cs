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

    public PieceStore(int idPieceStore, int priceRefreshStore, List<IChessPiece> listChessPiece)
    {
        IdPieceStore = idPieceStore;
        PriceRefreshStore = priceRefreshStore;
        ListChessPiece = listChessPiece;
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

    public IChessPiece BuyPiece(IChessPiece chessPiece, int price, Deck deck)
    {
        if (!deck.IsDeckFull())
        {
            if (price == chessPiece.GetDetail().Price && CoinPlayer.Coins >= price)
            {
                CoinPlayer.Coins -= price;
                deck.AddPieceFromStore(chessPiece);
                ListChessPiece.Remove(chessPiece);
                return chessPiece;
            }
        }
        return null;
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

    /*public bool SendPieceToDeck(ChessPiece chessPiece, Deck playerDeck)
    {
        if (chessPiece != null)
        {
            playerDeck.AddPieceFromStore(chessPiece);
            ListChessPiece.Remove(chessPiece);
            return true;
        }
        return false;
    }*/
}