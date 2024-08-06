using GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IPieceStore
{
    public List<IChessPiece> RefreshStore();
    public bool BuyPiece(IChessPiece chessPiece, int price, IDeck deck);
    public IChessPiece GetPiece(int i);
    public int GetPieceSToreId();
    public List<IChessPiece> GetAllPiece();
}