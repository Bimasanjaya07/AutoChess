namespace GameAutoChess.Interface;

public interface IAbility
{
    public void ActivatedAbility(IChessPiece chessPiece);
    public void PassiveAbility(IChessPiece chessPiece);
}