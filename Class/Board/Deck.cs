using GameAutoChess.Interface;

namespace GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

public class Deck : IDeck
{
    public int IdDeck { get; }
    public int MaxSlotPiece { get; }
    public List<IChessPiece> ChessPieces { get; set; }
    public PlayerData DataPlayer { get; set; }

    public Deck(int idDeck, int maxSlotPiece, List<IChessPiece> chessPieces)
    {
        IdDeck = idDeck;
        MaxSlotPiece = maxSlotPiece;
        ChessPieces = chessPieces;
    }

    public bool IsDeckFull()
    {
        if(ChessPieces.Count == MaxSlotPiece)
        {
            return true;
        }

        return false;
    }

    public bool AddPieceFromStore(IChessPiece chessPiece)
    {
        if (chessPiece != null)
        {
            ChessPieces.Add(chessPiece);
            return true;
        }
        return false;
    }

    public bool SellPieceFromDeck(IChessPiece chessPiece)
    {
        if (ChessPieces.Contains(chessPiece))
        {
            ChessPieces.Remove(chessPiece);
            return true;
        }
        return false;
    }
}