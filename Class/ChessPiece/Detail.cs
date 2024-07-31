namespace GameAutoChess.Class.ChessPiece;

public class Detail
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Sell { get; set; }
    public int MaxSlotItem { get; set; }
    public int ItemCount { get; set; }
    public bool IsAlive { get; set; }
    public bool OnBoard { get; set; }

    public Detail(string name, string description, int price, int sell, int maxSlotItem, int itemCount, bool isAlive, bool onBoard)
    {
        Name = name;
        Description = description;
        Price = price;
        Sell = sell;
        MaxSlotItem = maxSlotItem;
        ItemCount = itemCount;
        IsAlive = isAlive;
        OnBoard = onBoard;
    }
    public Detail(){}
}