using GameAutoChess.Enum;

namespace GameAutoChess.Class.ChessPiece;

public class Detail
{
    public int IdChessPiece { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Sell { get; set; }
    public int MaxSlotItem { get; set; }
    public int ItemCount { get; set; }
    public bool IsAlive { get; set; }
    public bool OnBoard { get; set; }
    public RarityPiece Rarity { get; set; }

    public Detail(int idChessPiece, string name, string description, int price, int sell, int maxSlotItem, int itemCount, bool isAlive, bool onBoard, RarityPiece rarity)
    {
        IdChessPiece = idChessPiece;
        Name = name;
        Description = description;
        Price = price;
        Sell = sell;
        MaxSlotItem = maxSlotItem;
        ItemCount = itemCount;
        IsAlive = isAlive;
        OnBoard = onBoard;
        Rarity = rarity;
    }

    public Detail(){}
}