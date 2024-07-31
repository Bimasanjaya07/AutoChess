using GameAutoChess.Class.Board;
using GameAutoChess.Enum;

namespace GameAutoChess.Class.PieceStore;
using GameAutoChess.Class.ChessPiece;
public class PieceStore
{
    public int IdPieceStore { get; set; }
    public int PriceRefreshStore { get; set; }
    public int PricePiece { get; set; }
    public ChessPiece ChessPiece { get; set; }
    public List<ChessPiece> ListChessPiece { get; set; }
    public RarityPiece Rarity { get; set; }
    
    public List<ChessPiece> RefreshStore()
    {
        return ListChessPiece;
    }
    public bool BuyPiece(ChessPiece chessPiece, int pricePiece, Deck playerDeck)
    {
        if (pricePiece >= PricePiece && chessPiece == ChessPiece)
        {
            playerDeck.AddPieceFromStore(chessPiece);
            ListChessPiece.Remove(chessPiece);
            return true;
        }
        return false;
    }
    
}