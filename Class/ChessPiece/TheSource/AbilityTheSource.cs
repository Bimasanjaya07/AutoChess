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
        
    }
}