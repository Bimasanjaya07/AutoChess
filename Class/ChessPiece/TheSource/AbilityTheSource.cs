using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class AbilityTheSource : IAbility
{
    public int IdAbility { get; set; }
    public AbilityName Name { get; set; }
    public string Description { get; set; }
    public DamageType DamageType { get; set; }
    public AbilityType AbilityType { get; set; }
    public AbilityTarget AbilityTarget { get; set; }
    public int RecoveryInterval { get; set; } // Ability The Source
    public int ManaRecovery { get; set; } 
    public IChessPiece chessPiece { get; set; }
    public TheSource theSource { get; set; }
    public List<IChessPiece> ChessPieces { get; set; }

    public AbilityTheSource(int idAbility, AbilityName name, string description, DamageType damageType, AbilityType abilityType, AbilityTarget abilityTarget, int recoveryInterval, int manaRecovery)
    {
        IdAbility = idAbility;
        Name = name;
        Description = description;
        DamageType = damageType;
        AbilityType = abilityType;
        AbilityTarget = abilityTarget;
        RecoveryInterval = recoveryInterval;
        ManaRecovery = manaRecovery;
    }

    public async void ApplyAbility()
    {
        ManaRecovery = 12;
        RecoveryInterval = 2;
        
        while (true) 
        {
            foreach (IChessPiece chessPiece in ChessPieces)
            {
                if (chessPiece.GetStatistic().HealthPiece > 0)
                {
                    chessPiece.GetDetail().Mana += ManaRecovery;
                }
            }
            await Task.Delay(RecoveryInterval * 1000);
        }
    }

    public void DetailAbility()
    {
        DamageType = DamageType.MagicalDamage;
        if (IdAbility == chessPiece.GetDetail().IdChessPiece)
        {
            Name = AbilityName.Awaken;
            Description = "The Source awakens the power of the chess pieces to recover mana.";
            DamageType = DamageType.MagicalDamage;
            AbilityType = AbilityType.Passive;
            AbilityTarget = AbilityTarget.Range;

            switch (chessPiece.GetDetail().Tier)
            {
                case TierPiece.OneStar:
                    RecoveryInterval = 2;
                    ManaRecovery = 12;
                    break;
                case TierPiece.TwoStar:
                    RecoveryInterval = 2;
                    ManaRecovery = 18;
                    break;
                case TierPiece.ThreeStar:
                    RecoveryInterval = 2;
                    ManaRecovery = 24;
                    break;
            }
        }
    }
}