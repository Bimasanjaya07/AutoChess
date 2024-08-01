using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IPieceStore
{
    public bool RefreshStore();
    public List<IChessPiece> GetPieceFromStore();
}