namespace GameAutoChess.Class;

public class Position
{
    public int Column { get; set; }
    public int Row { get; set; }
    
    public Position(int column, int row)
    {
        Column = column;
        Row = row;
    }
}