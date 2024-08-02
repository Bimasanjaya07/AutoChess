using GameAutoChess.Class;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IChessPiece
{
    public Position GetPosition();
    public void SetPosition(Position position);
    public Statistic GetStatistic();
    public void SetStatistic(Statistic stats);
    public Detail GetDetail();
    public void SetDetail(Detail detail);
    public void Attack(IChessPiece targetPiece);
    public void Attacked(IChessPiece attackingPiece);
    public void ManaRegen();
    public void CheckAndActivateAbility();
    public void OnAlive();
}