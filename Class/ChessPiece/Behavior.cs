using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece;

public class Behavior : IPieceBehavior
{
    public JobsPiece JobsPiece { get; set; }
    public Statistic StatisticPiece { get; set; }
    public List<IChessPiece> OpponentPieces { get; set; }

    public void ApplyBehavior()
    {
        switch (JobsPiece)
        {
            case JobsPiece.Hunter:
                IChessPiece targetMage = OpponentPieces.FirstOrDefault(piece => piece.GetDetail().Jobs == JobsPiece.Mage);
                if (targetMage != null)
                {
                    targetMage.GetStatistic().HealthPiece -= StatisticPiece.AttackPiece;
                }
                break;
            case JobsPiece.Warrior:
                IChessPiece targetMageForWarrior = OpponentPieces.FirstOrDefault(piece => piece.GetDetail().Jobs == JobsPiece.Mage);
                if (targetMageForWarrior != null)
                {
                    targetMageForWarrior.GetStatistic().HealthPiece -= StatisticPiece.AttackPiece;
                }
                break;
            case JobsPiece.Mage:
                IChessPiece targetWarrior = OpponentPieces.FirstOrDefault(piece => piece.GetDetail().Jobs == JobsPiece.Warrior);
                if (targetWarrior != null)
                {
                    targetWarrior.GetStatistic().HealthPiece -= StatisticPiece.AttackPiece;
                }
                break;
        }
    }
}