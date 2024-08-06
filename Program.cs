using GameAutoChess.Controller;

namespace GameAutoChess;

    public static class Program
    {
        public static async Task Main()
        {
            GameController gameController = new GameController();
            ProgramDisplay display = new ProgramDisplay(gameController);
            display.InputPlayerCount();
            display.DisplayPlayerNames();
            display.DisplayBoard();
            
            bool gameEnded = false;
            while (!gameEnded)
            {
                display.EnterPrepPhase();
                display.BattlePlayers();

                foreach (var player in gameController.GetPlayers())
                {
                    if (gameController.PlayerLose(player))
                    {
                        gameEnded = true;
                        break;
                    }
                }
            }

            Console.WriteLine("Game over. A player has been defeated.");
        }
    }
