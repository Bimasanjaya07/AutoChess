// Update Program_Display.cs

using GameAutoChess.Controller;
using GameAutoChess.Interface;
using GameAutoChess.Class;
using System.Threading.Tasks;

namespace GameAutoChess.Display
{
    public class Program_Display
    {
        private List<IPlayer> players = new List<IPlayer>();
        private GameController gameController;

        public Program_Display(GameController controller)
        {
            gameController = controller;
        }

        public void InputPlayerCount()
        {
            Console.WriteLine("Enter the number of players:");
            if (int.TryParse(Console.ReadLine(), out int playerCount))
            {
                if (gameController.CheckPlayer(playerCount))
                {
                    InputPlayerNames(playerCount);
                }
                else
                {
                    Console.WriteLine($"Invalid number of players. Please enter a number between {gameController.minPlayer} and {gameController.MaxPlayer}.");
                    InputPlayerCount();
                }
            }
            else
            {
                Console.WriteLine("Invalid number. Please enter a valid number.");
                InputPlayerCount();
            }
        }

        public void InputPlayerNames(int playerCount)
        {
            for (int i = 1; i <= playerCount; i++)
            {
                Console.WriteLine($"Enter ID for player {i}:");
                int playerId = int.Parse(Console.ReadLine());

                Console.WriteLine($"Enter name for player {i}:");
                string playerName = Console.ReadLine();

                players.Add(new Player(playerId, playerName));
            }

            gameController.InitializePlayers(players);
        }

        public void DisplayPlayerNames()
        {
            Console.WriteLine("Player names:");
            foreach (var player in players)
            {
                Console.WriteLine($"ID: {player.GetPlayerId()}, Name: {player.GetPlayerName()}");
            }
        }

        public void DisplayBoard()
        {
            var board = gameController.GetBoard();
            var boardArray = board.GetBoard();
            Console.WriteLine("Game Board:");
            for (int row = 0; row < boardArray.GetLength(0); row++)
            {
                for (int col = 0; col < boardArray.GetLength(1); col++)
                {
                    var piece = boardArray[row, col];
                    if (piece != null)
                    {
                        var player = gameController.GetPlayers().FirstOrDefault(p => gameController.GetPlayerPieces(p).Contains(piece));
                        Console.Write($"[x.{player.GetPlayerId()}.{piece.GetDetail().IdChessPiece}]");
                    }
                    else
                    {
                        Console.Write("[ ]");
                    }
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }

        public async Task PrepPhase()
        {
            foreach (var player in players)
            {
                Console.WriteLine($"Player {player.GetPlayerId()} ({player.GetPlayerName()})'s turn:");
                Console.WriteLine("Available heroes in the piece store:");
                var pieceStore = gameController.GetPieceStore(player);
                if (pieceStore != null)
                {
                    foreach (var piece in pieceStore.GetAllPiece())
                    {
                        Console.WriteLine($"ID: {piece.GetDetail().IdChessPiece}, Name: {piece.GetDetail().Name}, Price: {piece.GetDetail().Price}");
                    }
                }
                else
                {
                    Console.WriteLine("No pieces available in the store.");
                }

                // Simulate player actions (e.g., buying pieces, placing pieces on the board)
                await Task.Delay(30000); // 30 seconds for each player
                DisplayBoard(); // Display the updated board after each player's turn
            }
        }
    }
}