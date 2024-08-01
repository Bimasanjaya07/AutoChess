using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class AbilityShiningArcer : IAbility
{
    public int IdAbility { get; set; }
    public AbilityName Name { get; set; }
    public string Description { get; set; }
    public DamageType DamageType { get; set; }
    public AbilityType AbilityType { get; set; }
    public AbilityTarget AbilityTarget { get; set; }
    public int Cooldown { get; set; } // Shining Archer
    public bool IsCooldown { get; set; }
    public int MiniDamage { get; set; } 
    public int MaxDamage { get; set; }
    public decimal MiniStunDuration { get; set; }
    public decimal MaxStunDuration { get; set; }
    public IChessPiece chessPiece { get; set; }
    public ShiningArcer shiningArcer { get; set; }

    public AbilityShiningArcer(int idAbility, AbilityName name, string description, DamageType damageType, AbilityType abilityType, AbilityTarget abilityTarget, int cooldown, int miniDamage, int maxDamage, decimal miniStunDuration, decimal maxStunDuration)
    {
        IdAbility = idAbility;
        Name = name;
        Description = description;
        DamageType = damageType;
        AbilityType = abilityType;
        AbilityTarget = abilityTarget;
        Cooldown = cooldown;
        MiniDamage = miniDamage;
        MaxDamage = maxDamage;
        MiniStunDuration = miniStunDuration;
        MaxStunDuration = maxStunDuration;
    }
    public bool SetCooldown()
    {
        if (Cooldown > 0 && Cooldown == Cooldown)
        {
            return IsCooldown = false;
        }

        return IsCooldown = true;
    }
    public void ApplyAbility()
    {
        if (!IsCooldown )
        {
            shiningArcer.StatsPiece.AttackPiece += MiniDamage;
            shiningArcer.DetailPiece.StunDuration += MiniStunDuration;
            Cooldown = 0;
        }
    }
    public void DetailAbility()
    {
        if ( IdAbility == chessPiece.GetDetail().IdChessPiece)
        {
            Name = AbilityName.ShootingStar;
            Description = "Shining Archer shoots Shooting Star to deal damage to the farthest enemy and stun them.";
            DamageType = DamageType.MagicalDamage;
            AbilityType = AbilityType.Active;
            AbilityTarget = AbilityTarget.Single;

            if (chessPiece.GetDetail().Tier == TierPiece.OneStar)
            {
                MiniDamage = 100;
                MaxDamage = 300;
                MiniStunDuration = 1m;
                MaxStunDuration = 5m;
                Cooldown = 8;
            }
            else if (chessPiece.GetDetail().Tier == TierPiece.TwoStar)
            {
                MiniDamage = 165;
                MaxDamage = 500;
                MiniStunDuration = 1.5m;
                MaxStunDuration = 7.5m;
                Cooldown = 7;
            }
            else if (chessPiece.GetDetail().Tier == TierPiece.ThreeStar)
            {
                MiniDamage = 250;
                MaxDamage = 750;
                MiniStunDuration = 2.5m;
                MaxStunDuration = 10m;
                Cooldown = 6;
            }
        }
    }
}