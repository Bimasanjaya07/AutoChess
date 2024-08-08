using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class BehaviorShiningArcer : IPieceBehavior
{
    public Statistic StatisticPiece { get; set; }
    public List<IChessPiece> OpponentPieces { get; set; }

    public void ApplyBehavior()
    {
        IChessPiece targetMage = OpponentPieces.FirstOrDefault(piece => piece.GetDetail().Jobs == JobsPiece.Mage);
        if (targetMage != null)
        {
            targetMage.GetStatistic().HealthPiece -= StatisticPiece.AttackPiece;
        }
    }
}