using GameAutoChess.Interface;

namespace GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;

public class Deck : IDeck
{
    public int IdDeck { get; }
    public int MaxSlotPiece { get; }
    public IChessPiece ChessPiece { get; set; }
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
            DataPlayer.Coins += chessPiece.GetDetail().Price;
            return true;
        }
        return false;
    }

    public bool RemovePieceDeck(IChessPiece chessPiece)
    {
        if (ChessPieces.Contains(chessPiece))
        {
            ChessPieces.Remove(chessPiece);
            return true;
        }
        return false;
    }
    public IChessPiece GetPiece(int idPiece)
    {
        if (idPiece == ChessPiece.GetDetail().IdChessPiece)
        {
            return ChessPiece;
        }
        return null;
    }
    
    public IChessPiece GetPieceFromDeck(int index)
    {
        if (index >= 0 && index < ChessPieces.Count)
        {
            IChessPiece piece = ChessPieces[index];
            ChessPieces.RemoveAt(index);
            return piece;
        }
        return null;
    }
    public bool GetpositionPiece(IChessPiece chessPiece)
    {
        if (ChessPieces.Contains(chessPiece))
        {
           return ChessPieces.Remove(chessPiece);
        }

        return false;
    }
    public int GetDeckId()
    {
        return IdDeck;
    }
    public List<IChessPiece> GetAllPiece()
    {
        return ChessPieces;
    }
   
}