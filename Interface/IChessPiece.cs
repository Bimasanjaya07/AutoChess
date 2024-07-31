using GameAutoChess.Class;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Interface;

public interface IChessPiece
{
    public Position GetPosition();
    public void SetPosition(Position position);
    public Statistic GetStatistic();
    public Statistic SetStatistic(Statistic stats);
    public Detail GetDetail();
    public Detail SetDetail(Detail detail);
}