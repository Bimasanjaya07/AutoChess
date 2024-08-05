using GameAutoChess.Class;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IDeck
{
    public bool IsDeckFull();
    public bool AddPieceFromStore(IChessPiece chessPiece);
    public bool SellPieceFromDeck(IChessPiece chessPiece);
    public IChessPiece GetPieceFromDeck(int pieceIndex);
    public IChessPiece GetPiece(int idPiece);
    public bool RemovePieceDeck(IChessPiece chessPiece);
    public int GetDeckId();
    public List<IChessPiece> GetAllPiece();

}