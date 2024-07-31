using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.ChessPiece;

public class Ability : IAbility
{
    public int IdAbility { get; set; }
    public AbilityName Name { get; set; }
    public string Description { get; set; }
    public DamageType DamageType { get; set; }
    public AbilityType AbilityType { get; set; }
    public AbilityTarget AbilityTarget { get; set; }
    public int RecoveryInterval { get; set; } // Ability The Source
    public int ManaRecovery { get; set; } 
    public decimal LethalBlowDamage { get; set; } // Tusk Camphion
    public int KnockUpTime { get; set; }
    public int Cooldown { get; set; } // Tusk Champion & Shining Archer
    public int MiniDamage { get; set; } // Shining Archer
    public int MaxDamage { get; set; }
    public decimal MiniStunDuration { get; set; }
    public decimal MaxStunDuration { get; set; }
    
    
    
    public void ActivatedAbility(IChessPiece chessPiece)
    {
        
    }

    public void PassiveAbility(IChessPiece chessPiece)
    {
        
    }
}