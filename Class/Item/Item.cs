using GameAutoChess.Class.ChessPiece;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.Item;

public class Item : IItem
{
    public int IdItem { get; set; }
    public ItemName Name { get; set; }
    public string Description { get; set; }
    public Statistic Stats { get; set; }
    public DamageType Type { get; set; }

    public void ApplyItemEffect(IChessPiece chessPiece)
    {
        if (Name == ItemName.LifeCrystal)
        {
            Stats.HealthPiece = +300;
        }
        else if (Name == ItemName.CattleHideArmor)
        {
            Stats.Armor = +6;
        }
        else if (Name == ItemName.MagickaStaff && Type == DamageType.MagicalDamage)
        {
            Stats.AttackPiece = +(15 / 100);
        }
    }

    public string GetItemDetail()
    {
        string name = Name.ToString();
        string description = Description;
        return name + description;
    }
}