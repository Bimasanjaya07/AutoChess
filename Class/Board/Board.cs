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
        for (int row = 0; row < BoardPieces.GetLength(0); row++)
        {
            for (int col = 0; col < BoardPieces.GetLength(1); col++)
            {
                if (BoardPieces[row, col] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public IChessPiece[,] GetBoard()
    {
        return BoardPieces;
    }

    public IChessPiece GetPiece(Position position)
    {
        return BoardPieces[position.Column, position.Row];
    }

    public void SetPieceFromDeck(IChessPiece piece, Deck deckPosition, Position destination)
    {
        if (IsBoardOccupied(destination))
        {
            return;
        }

        BoardPieces[destination.Column, destination.Row] = piece;
        deckPosition.SellPieceFromDeck(piece);
    }

    public void MovePiece(IChessPiece piece, Position source, Position destination)
    {
        if (IsBoardOccupied(destination))
        {
            return;
        }

        BoardPieces[destination.Column, destination.Row] = piece;
        BoardPieces[source.Column, source.Row] = null;
    }
}