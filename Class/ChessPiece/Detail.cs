using GameAutoChess.Enum;

namespace GameAutoChess.Class.ChessPiece;

public class Detail
{
    public int IdChessPiece { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Position Position { get; set; }
    public int Price { get; set; }
    public int Sell { get; set; }
    public int MaxSlotItem { get; set; }
    public int ItemCount { get; set; }
    public bool IsAlive { get; set; }
    public bool OnBoard { get; set; }
    public RarityPiece Rarity { get; set; }
    public TierPiece Tier { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public decimal StunDuration { get; set; }
    public bool IsStunned { get; set; }
    public JobsPiece Jobs { get;  }
    public int[] Items { get; set; }

    public Detail(int idChessPiece, string name, string description, Position position, int price, int sell, int maxSlotItem, int itemCount, bool isAlive, bool onBoard, RarityPiece rarity, TierPiece tier, int mana, int maxMana)
    {
        IdChessPiece = idChessPiece;
        Name = name;
        Description = description;
        Position = position;
        Price = price;
        Sell = sell;
        MaxSlotItem = maxSlotItem;
        ItemCount = itemCount;
        IsAlive = isAlive;
        OnBoard = onBoard;
        Rarity = rarity;
        Tier = tier;
        Mana = mana;
        MaxMana = maxMana;
    }

    public Detail(){}
}