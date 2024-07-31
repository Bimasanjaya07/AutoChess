namespace GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

public class Deck
{
    public List<ChessPiece> ChessPieces { get; set; }

    public Deck()
    {
        ChessPieces = new List<ChessPiece>();
    }

    public void AddPieceFromStore(ChessPiece chessPiece)
    {
        ChessPieces.Add(chessPiece);
    }

    public void SellChessPiece(ChessPiece chessPiece)
    {
        ChessPieces.Remove(chessPiece);
    }

    public List<ChessPiece> GetChessPieces()
    {
        return ChessPieces;
    }
}