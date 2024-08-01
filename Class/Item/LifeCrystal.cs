using GameAutoChess.Class.ChessPiece;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Class.Item;

public class LifeCrystal : IItem
{
    public int IdItem { get; set; }
    public ItemName Name { get; set; }
    public string Description { get; set; }
    public Statistic Stats { get; set; }

    public LifeCrystal(int idItem, ItemName name, string description, Statistic stats)
    {
        IdItem = idItem;
        Name = name;
        Description = description;
        Stats = stats;
    }

    public void ApplyItemEffect(IChessPiece chessPiece)
    {
        if (Name == ItemName.LifeCrystal)
        {
            Stats.HealthPiece += 300;
        }
    }

    public string GetItemDetail()
    {
        string name = Name.ToString();
        string description = Description;
        return name + description;
    }
}