using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece.PieceName;

public class AbilityTuskChampion : IAbility
{
    public int IdAbility { get; set; }
    public AbilityName Name { get; set; }
    public string Description { get; set; }
    public DamageType DamageType { get; set; }
    public AbilityType AbilityType { get; set; }
    public AbilityTarget AbilityTarget { get; set; }
    public decimal LethalBlowDamage { get; set; } // Tusk Camphion
    public int Cooldown { get; set; }
    public bool IsCooldown { get; set; }
    public TuskChampion tuskChampion { get; set; }
    public IChessPiece chessPiece { get; set; }

    public AbilityTuskChampion(int idAbility, AbilityName name, string description, DamageType damageType, AbilityType abilityType, AbilityTarget abilityTarget, decimal lethalBlowDamage, int cooldown)
    {
        IdAbility = idAbility;
        Name = name;
        Description = description;
        DamageType = damageType;
        AbilityType = abilityType;
        AbilityTarget = abilityTarget;
        LethalBlowDamage = lethalBlowDamage;
        Cooldown = cooldown;
    }

    public void ApplyAbility()
    {
        LethalBlowDamage = 300/100;
        if (!IsCooldown )
        {
            tuskChampion.StatsPiece.AttackPiece *= LethalBlowDamage;
            Cooldown = 0;
        }
    }
    public void DetailAbility()
    {
        
    }
}