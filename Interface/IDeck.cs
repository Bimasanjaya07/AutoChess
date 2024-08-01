using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IDeck
{
    public bool IsDeckFull();
    public bool AddPieceFromStore(IChessPiece chessPiece);
    public bool SellPieceFromDeck(IChessPiece chessPiece);
}