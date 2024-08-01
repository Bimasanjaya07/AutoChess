using GameAutoChess.Class.ChessPiece;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.Item;

public class MagickaStaff : IItem
{
    public int IdItem { get; set; }
    public ItemName Name { get; set; }
    public string Description { get; set; }
    public Statistic Stats { get; set; }
    public DamageType Type { get; set; }

    public MagickaStaff(int idItem, ItemName name, string description, Statistic stats, DamageType type)
    {
        IdItem = idItem;
        Name = name;
        Description = description;
        Stats = stats;
        Type = type;
    }
    public void ApplyItemEffect(IChessPiece chessPiece)
    {
        if (Name == ItemName.MagickaStaff && Type == DamageType.MagicalDamage)
        {
            Stats.AttackPiece += (15 / 100);
        }
    }

    public string GetItemDetail()
    {
        string name = Name.ToString();
        string description = Description;
        return name + description;
    }
}