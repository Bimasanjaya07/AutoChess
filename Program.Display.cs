using GameAutoChess.Controller;
using GameAutoChess.Interface;
using GameAutoChess.Class;

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
            int minPlayer = gameController.minPlayer;
            int maxPlayer = gameController.MaxPlayer;

            while (true)
            {
                Console.WriteLine($"Enter the number of players (between {minPlayer} and {maxPlayer}):");
                if (int.TryParse(Console.ReadLine(), out int playerCount))
                {
                    if (playerCount >= minPlayer && playerCount <= maxPlayer)
                    {
                        HashSet<int> playerIds = new HashSet<int>();
                        for (int i = 1; i <= playerCount; i++)
                        {
                            int id;
                            while (true)
                            {
                                Console.WriteLine($"Enter ID for player {i}:");
                                id = int.Parse(Console.ReadLine());
                                if (playerIds.Contains(id))
                                {
                                    Console.WriteLine("ID already exists. Please enter a different ID.");
                                }
                                else
                                {
                                    playerIds.Add(id);
                                    break;
                                }
                            }
                            Console.WriteLine($"Enter name for player {i}:");
                            string name = Console.ReadLine();
                            players.Add(new Player(id, name));
                        }
                        gameController.InitializePlayers(players);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Number of players must be between {minPlayer} and {maxPlayer}. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number of players. Please enter a valid number.");
                }
            }
        }

        public void DisplayPlayerNames()
        {
            Console.WriteLine("Player names:");
            foreach (var player in players)
            {
                Console.WriteLine($"Player ID: {player.GetPlayerId()}, Name: {player.GetPlayerName()}");
            }
        }

        public void DisplayBoard()
        {
            IBoard board = gameController.GetBoard();
            IChessPiece[,] boardArray = board.GetBoard();
            Console.WriteLine("Game Board:");
            for (int row = 0; row < boardArray.GetLength(0); row++)
            {
                for (int col = 0; col < boardArray.GetLength(1); col++)
                {
                    Console.Write(boardArray[row, col] != null ? "X " : ". ");
                }
                Console.WriteLine();
            }
        }

        public void EnterPrepPhase()
        {
            foreach (var player in players)
            {
                Console.WriteLine($"Preparation phase for player {player.GetPlayerName()} (ID: {player.GetPlayerId()})");

                IPieceStore pieceStore = gameController.GetPieceStore(player);
                IDeck deck = gameController.GetPlayerDeck(player);
                IBoard board = gameController.GetBoard();
                List<IChessPiece> pieceInStore = pieceStore.GetAllPiece();

                Console.WriteLine("Available pieces in the store:");
                var pieces = pieceStore.GetAllPiece();
                if (pieces.Count == 0)
                {
                    Console.WriteLine("No pieces available in the store.");
                }
                else
                {
                    foreach (var piece in pieces)
                    {
                        var detail = piece.GetDetail();
                        Console.WriteLine($"Piece ID: {detail.IdChessPiece}, Name: {detail.Name}, Price: {detail.Price}");
                    }
                }

                Console.WriteLine("Enter the ID of the piece you want to buy:");
                int pieceId = int.Parse(Console.ReadLine());
                var pieceToBuy = pieceStore.GetPiece(pieceId);

                if (pieceToBuy != null)
                {
                    Console.WriteLine("Enter the price of the piece:");
                    int price = int.Parse(Console.ReadLine());

                    if (gameController.MovePieceFromStoreToDeck(player, pieceToBuy, pieceStore, price, deck))
                    {
                        Console.WriteLine("Piece bought successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to buy piece.");
                    }
                }

                Console.WriteLine("Enter the position to place the piece on the board (row and column):");
                int row = int.Parse(Console.ReadLine());
                int col = int.Parse(Console.ReadLine());
                var position = new Position(row, col);

                if (gameController.MovePieceFromDeckToBoard(player, pieceToBuy, board, position, deck))
                {
                    Console.WriteLine("Piece placed on the board successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to place piece on the board.");
                }

                // Display the board after each player's preparation phase
                DisplayBoard();
            }
        }
    }
}