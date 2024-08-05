// Program_Display.cs
using GameAutoChess.Controller;
using GameAutoChess.Interface;
using GameAutoChess.Class;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess.Display
{
    public class Program_Display
    {
        private List<IPlayer> players = new List<IPlayer>();
        private List<IChessPiece> chessPieces = new List<IChessPiece>();
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
                                if (int.TryParse(Console.ReadLine(), out id) && !playerIds.Contains(id))
                                {
                                    playerIds.Add(id);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid or duplicate ID. Please enter a unique ID.");
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
                    Console.Write(boardArray[row, col] != null ? "[X]" : "[ ]");
                }
                Console.WriteLine();
            }
        }
        private void DisplayValidPositions(IPlayer player, IBoard board)
        {
            int boardMidPoint = board.GetBoard().GetLength(1) / 2;
            for (int row = 0; row < board.GetBoard().GetLength(0); row++)
            {
                for (int col = 0; col < board.GetBoard().GetLength(1); col++)
                {
                    if ((player.GetPlayerId() == 1 && col < boardMidPoint) || (player.GetPlayerId() != 1 && col >= boardMidPoint))
                    {
                        if (!board.IsBoardOccupied(new Position(row, col)))
                        {
                            Console.Write($"( {col}, {row}) ");
                        }
                    }
                }
            }
            Console.WriteLine();
        }
        public void EnterPrepPhase()
        {
            foreach (var player in players)
            {
                Console.WriteLine($"Preparation phase for player {player.GetPlayerName()} (ID: {player.GetPlayerId()})");
                PlayerData playerData = gameController.GetPlayerData(player);

                Console.WriteLine($"Player Coins: {playerData.GetCoins()}");
                IPieceStore pieceStore = gameController.GetPieceStore(player);
                IDeck deck = gameController.GetPlayerDeck(player);
                IBoard board = gameController.GetBoard();

                Console.WriteLine("Available pieces in the store:");
                List<IChessPiece> pieces = pieceStore.GetAllPiece();
                if (pieces.Count == 0)
                {
                    Console.WriteLine("No pieces available in the store.");
                }
                else
                {
                    foreach (IChessPiece piece in pieces)
                    {
                        Detail detail = piece.GetDetail();
                        Console.WriteLine($"Piece ID: {detail.IdChessPiece}, Name: {detail.Name}, Price: {detail.Price}");
                    }

                    IChessPiece pieceToBuy = null;
                    while (pieceToBuy is null)
                    {
                        Console.WriteLine("Enter the ID of the piece you want to buy:");
                        int pieceId = int.Parse(Console.ReadLine());
                        pieceToBuy = pieceStore.GetPiece(pieceId);

                        if (pieceToBuy is null)
                        {
                            Console.WriteLine("Invalid piece ID. Please enter a valid piece ID.");
                        }
                    }

                    int price = pieceToBuy.GetDetail().Price;
                    if (pieceStore.BuyPiece(pieceToBuy, price, deck))
                    {
                        Console.WriteLine("Piece bought successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to buy piece.");
                    }

                    bool validPosition = false;
                    while (!validPosition)
                    {
                        Console.WriteLine("Enter the position to place the piece on the board (row and column):");
                        Console.Write("Row: ");
                        int row = int.Parse(Console.ReadLine());
                        Console.Write("Column: ");
                        int col = int.Parse(Console.ReadLine());
                        var position = new Position(row, col);

                        if (gameController.MovePieceFromDeckToBoard(player, pieceToBuy, board, position, deck))
                        {
                            Console.WriteLine("Piece placed on the board successfully.");
                            validPosition = true;
                        }
                        else
                        {
                            Console.WriteLine("Failed to place piece on the board. Invalid position.");
                            Console.WriteLine("Valid positions for you are:");
                            DisplayValidPositions(player, board);
                        }
                    }
                }

                // Display the board after each player's preparation phase
                DisplayBoard();
            }
        }
    }
}