namespace GameAutoChess.Interface;

public interface IItem
{
    public void ApplyItemEffect(IChessPiece chessPiece);
    public string GetItemDetail();
}