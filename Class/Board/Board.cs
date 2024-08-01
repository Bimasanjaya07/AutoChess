using GameAutoChess.Enum;
using GameAutoChess.Interface;
namespace GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

public class Board : IBoard
{
    public IChessPiece[,] BoardPieces { get; set; }
    public BoardName NameBoard { get; set; }

    public Board(IChessPiece[,] boardPieces, BoardName nameBoard)
    {
        BoardPieces = new IChessPiece[8, 8];
        NameBoard = nameBoard;
    }

    public bool IsBoardOccupied(Position position)
    {
        if (position.Column < 0 || position.Column >= BoardPieces.GetLength(0) ||
            position.Row < 0 || position.Row >= BoardPieces.GetLength(1))
        {
            return false;
        }

        IChessPiece pieceAtPosition = BoardPieces[position.Column, position.Row];
        return pieceAtPosition != null;
    }

    

    public bool IsFullBoard()
    {
        throw new NotImplementedException();
    }

    public IChessPiece[,] GetBoard()
    {
        throw new NotImplementedException();
    }

    public IChessPiece GetPiece(Position position)
    {
        throw new NotImplementedException();
    }

    public void SetPieceFromDeck(IChessPiece piece, Deck deckPosition, Position destination)
    {
        throw new NotImplementedException();
    }

    public void MovePiece(IChessPiece piece, Position source, Position destination)
    {
        throw new NotImplementedException();
    }
}