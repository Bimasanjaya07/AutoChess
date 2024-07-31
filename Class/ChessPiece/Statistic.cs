namespace GameAutoChess.Class.ChessPiece;

public class Statistic
{
    public int HealthPiece { get; set; }
    public int AttackPiece { get; set; }
    public decimal AttackRate { get; set; }
    public int AttackRange { get; set; }
    public int DamageToEnemy { get; set; }
    public int Armor { get; set; }
    public int MagicResistance { get; set; } // Convert to % using /100
    
    public Statistic(int healthPiece, int attackPiece, decimal attackRate, int attackRange, int damageToEnemy, int armor, int magicResistance)
    {
        HealthPiece = healthPiece;
        AttackPiece = attackPiece;
        AttackRate = attackRate;
        AttackRange = attackRange;
        DamageToEnemy = damageToEnemy;
        Armor = armor;
        MagicResistance = magicResistance;
    }

    public Statistic() { }
}