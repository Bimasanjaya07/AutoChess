using GameAutoChess.Display;
using GameAutoChess.Controller;

namespace GameAutoChess.Program
{
    public class Program
    {
        public static async Task Main()
        {
            GameController gameController = new GameController();
            Program_Display display = new Program_Display(gameController);
            display.InputPlayerCount();
            display.DisplayPlayerNames();
            display.DisplayBoard();
        }
    } 
}