using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece;

public class ChessPiece : IChessPiece
{
    public Position Position { get; set; }
    public Detail Detail { get; set; }
    public Statistic Stats { get; set; }

    public ChessPiece(Position position, Detail detail, Statistic stats)
    {
        Position = position;
        Detail = detail;
        Stats = stats;
    }
    public ChessPiece(){}


    public Position GetPosition()
    {
        return GetPosition();
    }
    public void SetPosition(Position position)
    {
        Position = position;
    }
    public Statistic GetStatistic()
    {
        return Stats;
    }
    public Statistic SetStatistic(Statistic stats)
    {
        Stats = stats;
        return Stats;
    }
    public Detail GetDetail()
    {
        return Detail;
    }
    public Detail SetDetail(Detail detail)
    {
        Detail = detail;
        return Detail;
    }
}