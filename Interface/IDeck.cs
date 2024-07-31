namespace GameAutoChess.Interface;

public interface IDeck
{
    public bool IsDeckFull();
    public bool AddPieceToDeck(IChessPiece chessPiece);
    public bool SellPieceFromDeck(IChessPiece chessPiece);
}