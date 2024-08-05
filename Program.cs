// Update Program.cs

using GameAutoChess.Class;
using GameAutoChess.Display;
using GameAutoChess.Controller;
using GameAutoChess.Interface;
using System.Threading.Tasks;

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
            await display.PrepPhase();
        }
    }
}