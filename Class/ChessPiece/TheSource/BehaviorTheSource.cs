using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class BehaviorTheSource : IPieceBehavior
{
    public Statistic StatisticPiece { get; set; }
    public List<IChessPiece> OpponentPieces { get; set; }

    public void ApplyBehavior()
    {
        IChessPiece targetWarrior = OpponentPieces.FirstOrDefault(piece => piece.GetDetail().Jobs == JobsPiece.Warrior);
        if (targetWarrior != null)
        {
            targetWarrior.GetStatistic().HealthPiece -= StatisticPiece.AttackPiece;
        }
    }
}