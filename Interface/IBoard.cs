using GameAutoChess.Class;
using GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IBoard
{
    public bool IsBoardOccupied(Position position);
    public bool IsFullBoard();
    public IChessPiece[,] GetBoard();
    public IChessPiece GetPiece(Position position);
    public void SetPieceFromDeck(IChessPiece piece, Deck deckPosition, Position destination);
    public void MovePiece(IChessPiece piece, Position source, Position destination);
    
}