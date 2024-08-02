using GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IPieceStore
{
    public List<IChessPiece> RefreshStore();
    public IChessPiece BuyPiece(IChessPiece chessPiece, int price, Deck deck);
    public IChessPiece GetPiece(int i);
    public bool BuyPiece(IChessPiece pieceToBuy, int price);
}