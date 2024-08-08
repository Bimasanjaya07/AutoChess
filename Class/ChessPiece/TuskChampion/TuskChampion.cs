using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class TuskChampion : IChessPiece
{
    public Detail DetailPiece { get; set; }
    public Statistic StatsPiece { get; set; }
    public AbilityTuskChampion AbilityPiece { get; set; }
    public IPieceBehavior BehaviorPiece { get; set; }
    

    public TuskChampion(Detail detailPiece, Statistic statsPiece, AbilityTuskChampion abilityPiece, IPieceBehavior behaviorPiece)
    {
        DetailPiece = detailPiece;
        StatsPiece = statsPiece;
        AbilityPiece = abilityPiece;
        BehaviorPiece = behaviorPiece;
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
    public void Attack(IChessPiece targetPiece)
    {
        targetPiece.GetStatistic().HealthPiece -= StatsPiece.AttackPiece;
        ManaRegen();
        CheckAndActivateAbility();
        BehaviorPiece.ApplyBehavior();
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
    public void OnAlive()
    {
        if (StatsPiece.HealthPiece <= 0)
        {
            DetailPiece.IsAlive = false;
        } else if (StatsPiece.HealthPiece > 0)
        {
            DetailPiece.IsAlive = true;
        }
    }

    public IAbility getAbility()
    {
        return AbilityPiece;
    }
}