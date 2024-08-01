using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class ShiningArcer : IChessPiece
{
    public AbilityShiningArcer AbilityPiece { get; set; }
    public Detail DetailPiece { get; set; }
    public Statistic StatsPiece { get; set; }
    public Behavior BehaviorPiece { get; set; }
    

    public ShiningArcer(AbilityShiningArcer abilityPiece, Detail detailPiece, Statistic statsPiece)
    {
        AbilityPiece = abilityPiece;
        DetailPiece = detailPiece;
        StatsPiece = statsPiece;
    }

    public Position GetPosition()
    {
        return DetailPiece.Position;
    }

    public void SetPosition(Position position)
    {
        DetailPiece.Position = position;
    }

    public Detail GetDetail()
    {
        return DetailPiece;
    }

    public void SetDetail(Detail detail)
    {
        DetailPiece = detail;
    }

    public Statistic GetStatistic()
    {
        return StatsPiece;
    }

    public void SetStatistic(Statistic stats)
    {
        StatsPiece = stats;
    }

    public async void Attack(IChessPiece targetPiece)
    {
        while (targetPiece.GetStatistic().HealthPiece > 0 && StatsPiece.HealthPiece > 0)
        {
            targetPiece.GetStatistic().HealthPiece -= StatsPiece.AttackPiece;
            ManaRegen();
            CheckAndActivateAbility();
            BehaviorPiece.ApplyBehavior();
            await Task.Delay(3000);
        }
    }

    public void Attacked(IChessPiece attackingPiece)
    {
        if (StatsPiece.HealthPiece > 0)
        {
            StatsPiece.HealthPiece -= attackingPiece.GetStatistic().AttackPiece;
            ManaRegen();
        }
    }
    public void ManaRegen()
    {
        DetailPiece.Mana += 10;
    }

    public void CheckAndActivateAbility()
    {
        if(AbilityPiece.AbilityType == AbilityType.Active)
        {
            if (DetailPiece.Mana == DetailPiece.MaxMana)
            {
                AbilityPiece.ApplyAbility();
                DetailPiece.Mana = 0; 
            }
        }
        else if (AbilityPiece.AbilityType == AbilityType.Passive)
        {
            AbilityPiece.ApplyAbility();
        }
    }
}