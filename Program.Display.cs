// Program_Display.cs
using GameAutoChess.Controller;
using GameAutoChess.Interface;
using GameAutoChess.Class;
using GameAutoChess.Class.ChessPiece;
// Program_Display.cs
using System.Threading.Tasks;

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
            Console.Clear();
            Console.WriteLine("Welcome to AutoChess. Please input the number of players.");
            int playerCount = GetPlayerCount();
            for (int i = 1; i <= playerCount; i++)
            {
                SetPlayerDetails(i);
            }
            gameController.InitializePlayers(players);
            Console.Clear();
        }

        private int GetPlayerCount()
        {
            int minPlayer = gameController.minPlayer;
            int maxPlayer = gameController.MaxPlayer;

            while (true)
            {
                Console.WriteLine($"Enter the number of players (between {minPlayer} and {maxPlayer}):");
                if (int.TryParse(Console.ReadLine(), out int playerCount) && playerCount >= minPlayer && playerCount <= maxPlayer)
                {
                    return playerCount;
                }
                else
                {
                    Console.WriteLine($"Number of players must be between {minPlayer} and {maxPlayer}. Please try again.");
                }
            }
        }

        private void SetPlayerDetails(int playerNumber)
        {
            int id = GetPlayerId(playerNumber);
            string name = GetPlayerName(playerNumber);
            players.Add(new Player(id, name));
        }

        private int GetPlayerId(int playerNumber)
        {
            HashSet<int> playerIds = new HashSet<int>(players.Select(p => p.GetPlayerId()));
            while (true)
            {
                Console.WriteLine($"Enter ID for player {playerNumber}:");
                if (int.TryParse(Console.ReadLine(), out int id) && !playerIds.Contains(id))
                {
                    return id;
                }
                else
                {
                    Console.WriteLine("Invalid or duplicate ID. Please enter a unique ID.");
                }
            }
        }

        private string GetPlayerName(int playerNumber)
        {
            while (true)
            {
                Console.WriteLine($"Enter name for player {playerNumber}:");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("Please input a valid name.");
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
                Console.WriteLine($"Player Health: {playerData.HealthPlayer}");
                Console.WriteLine($"WinStreak: {playerData.WinStreak}");
                Console.WriteLine($"LoseStreak: {playerData.LoseStreak}");
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
                    MovePiecePhase(player, board);
                }
                DisplayBoard();
            }
        }

        private void MovePiecePhase(IPlayer player, IBoard board)
        
        {
            Console.WriteLine("Do you want to move a piece? (yes/no)");
            string movePieceResponse = Console.ReadLine().ToLower();
            while (movePieceResponse == "yes")
            {
                Console.WriteLine("Enter the source position of the piece to move (row and column):");
                Console.Write("Source Row: ");
                int sourceRow = int.Parse(Console.ReadLine());
                Console.Write("Source Column: ");
                int sourceCol = int.Parse(Console.ReadLine());
                var sourcePosition = new Position(sourceRow, sourceCol);

                Console.WriteLine("Enter the destination position to move the piece to (row and column):");
                Console.Write("Destination Row: ");
                int destRow = int.Parse(Console.ReadLine());
                Console.Write("Destination Column: ");
                int destCol = int.Parse(Console.ReadLine());
                var destPosition = new Position(destRow, destCol);

                IChessPiece pieceToMove = board.GetPiece(sourcePosition);
                if (pieceToMove != null && gameController.MovePiece(player, pieceToMove, board, sourcePosition, destPosition))
                {
                    Console.WriteLine("Piece moved successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to move piece. Invalid position.");
                }

                Console.WriteLine("Do you want to move another piece? (yes/no)");
                movePieceResponse = Console.ReadLine().ToLower();
            }
        }

        // Program_Display.cs
        // Program_Display.cs
        public void BattlePlayers()
        {
            Console.WriteLine("Battle phase:");
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = i + 1; j < players.Count; j++)
                {
                    IPlayer player1 = players[i];
                    IPlayer player2 = players[j];
                    Console.WriteLine($"Battle between {player1.GetPlayerName()} (ID: {player1.GetPlayerId()}) and {player2.GetPlayerName()} (ID: {player2.GetPlayerId()})");

                    // Assuming PvPBattle method requires a valid position and chessPiece
                    Position position = new Position(0, 0); // Example position, adjust as needed
                    IChessPiece chessPiece = gameController.GetPlayerPieces(player1).FirstOrDefault(); // Example piece, adjust as needed
                    bool battleResult = gameController.PvPBattle(player1, player2, chessPiece, gameController.GetBoard(), position);

                    if (battleResult)
                    {
                        Console.WriteLine($"Player {player1.GetPlayerName()} (ID: {player1.GetPlayerId()}) won the battle.");
                        gameController.WinRound(player1, chessPiece);
                        gameController.DefeatRound(player2);
                    }
                    else
                    {
                        Console.WriteLine($"Player {player2.GetPlayerName()} (ID: {player2.GetPlayerId()}) won the battle.");
                        gameController.WinRound(player2, chessPiece);
                        gameController.DefeatRound(player1);
                    }

                    // Introduce a delay of 10 seconds between battles
                    Task.Delay(10000).Wait();
                }
            }

            foreach (var player in players)
            {
                PlayerData playerData = gameController.GetPlayerData(player);
                Console.WriteLine($"Player {player.GetPlayerName()} (ID: {player.GetPlayerId()}) health: {playerData.HealthPlayer}");
            }
        }
    }
}