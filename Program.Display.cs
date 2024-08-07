using GameAutoChess.Controller;
using GameAutoChess.Interface;
using GameAutoChess.Class;
using GameAutoChess.Class.ChessPiece;

namespace GameAutoChess;

    public class ProgramDisplay
    {
        private readonly List<IPlayer> _players  = new List<IPlayer>();
        private readonly GameController _gameController;

        public ProgramDisplay(GameController controller)
        {
            _gameController = controller;
        }

        public void InputPlayerCount()
        {
            Console.Clear();
            Console.WriteLine("Welcome to AutoChess.");
            int playerCount = GetPlayerCount();
            for (int i = 1; i <= playerCount; i++)
            {
                SetPlayerDetails(i);
            }
            _gameController.InitializePlayers(_players);
            Console.Clear();
        }

        private int GetPlayerCount()
        {
            while (true)
            {
                Console.WriteLine($"Enter the number of players (between {_gameController.MinPlayer} and {_gameController.MaxPlayer}):");
                if (int.TryParse(Console.ReadLine(), out int playerCount) && playerCount >= _gameController.MinPlayer && playerCount <= _gameController.MaxPlayer)
                {
                    return playerCount;
                }
                else
                {
                    Console.WriteLine($"Number of players must be between {_gameController.MinPlayer} and {_gameController.MaxPlayer}. Please try again.");
                }
            }
        }

        private void SetPlayerDetails(int playerNumber)
        {
            int id = GetPlayerId(playerNumber);
            string name = GetPlayerName(playerNumber);
            _players.Add(new Player(id, name));
        }

        private int GetPlayerId(int playerNumber)
        {
            List<int> playerIds = _players.Select(p => p.GetPlayerId()).ToList();
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
                string name = Console.ReadLine() ?? string.Empty;
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
            foreach (var player in _players)
            {
                Console.WriteLine($"Player ID: {player.GetPlayerId()}, Name: {player.GetPlayerName()}");
            }
        }

        public void DisplayBoard()
        {
            IBoard board = _gameController.GetBoard();
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
                    bool isPlayer1 = player.GetPlayerId() == 1;
                    bool isValidColumn = isPlayer1 ? col < boardMidPoint : col >= boardMidPoint;

                    if (isValidColumn && !board.IsBoardOccupied(new Position(col, row)))
                    {
                        Console.Write($"( Col : {col}, Row : {row}) ");
                    }
                }
            }
            Console.WriteLine();
        }


        public void EnterPrepPhase()
        { 
            foreach (var player in _players)
            {
                Console.WriteLine("");
                Console.WriteLine($"Preparation phase for player {player.GetPlayerName()} (ID: {player.GetPlayerId()})");
                Console.WriteLine("");
                PlayerData playerData = _gameController.GetPlayerData(player);
                Console.WriteLine($"Player Health: {playerData.HealthPlayer}");
                Console.WriteLine($"WinStreak: {playerData.WinStreak}");
                Console.WriteLine($"LoseStreak: {playerData.LoseStreak}");
                Console.WriteLine($"Player Coins: {playerData.GetCoins()}");
                Console.WriteLine("");

                IPieceStore pieceStore = _gameController.GetPieceStore(player);
                IDeck deck = _gameController.GetPlayerDeck(player);
                IBoard board = _gameController.GetBoard();
                
                pieceStore.RandomizePiece();
                
                ProcessBuy(pieceStore, deck, player, board);
                MovePiecePhase(player, board);
                DisplayBoard();
            }
        }

        private void ProcessBuy(IPieceStore pieceStore, IDeck deck, IPlayer player, IBoard board)
        {
            Console.WriteLine("Do you want to buy a piece? (y/n)");
            string response = Console.ReadLine().ToLower();
            if (response == "y")
            {
                Console.WriteLine("Available pieces in the store:");
                List<IChessPiece> pieces = pieceStore.GetAllPiece();
                if (pieces.Count == 0)
                {
                    Console.WriteLine("No pieces available in the store.");
                }
                else
                {
                    bool continueBuying = true;
                    while (continueBuying)
                    {
                        var pieceToBuy = NotifBuy(pieces, pieceStore, deck);

                        SetPosition(player, pieceToBuy, board, deck);

                        continueBuying = ContinueBuying(continueBuying);
                    }
                }
            }
        }

        private static IChessPiece NotifBuy(List<IChessPiece> pieces, IPieceStore pieceStore, IDeck deck)
        {
            var pieceToBuy = PieceToBuy(pieces, pieceStore);

            int price = pieceToBuy.GetDetail().Price;
            if (pieceStore.BuyPiece(pieceToBuy, price, deck))
            {
                Console.WriteLine("Piece bought successfully.");
            }
            else
            {
                Console.WriteLine("Failed to buy piece.");
            }

            return pieceToBuy;
        }

        private static bool ContinueBuying(bool continueBuying)
        {
            string response;
            Console.WriteLine("Do you want to buy another piece? (y/n)");
            response = Console.ReadLine().ToLower();
            if (response != "y")
            {
                continueBuying = false;
            }

            return continueBuying;
        }

        private void SetPosition(IPlayer player, IChessPiece pieceToBuy, IBoard board, IDeck deck)
        {
            bool validPosition = false;
            while (!validPosition)
            {
                Console.WriteLine("Enter the position to place the piece on the board (row and column):");
                Console.Write("Row: ");
                int row = int.Parse(Console.ReadLine());
                Console.Write("Column: ");
                int col = int.Parse(Console.ReadLine());
                var position = new Position(col ,row);

                if (_gameController.MovePieceFromDeckToBoard(player, pieceToBuy, board, position, deck))
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

        private static IChessPiece PieceToBuy(List<IChessPiece> pieces, IPieceStore pieceStore)
        {
            foreach (IChessPiece piece in pieces)
            {
                Detail detail = piece.GetDetail();
                Console.WriteLine($"Piece ID: {detail.IdChessPiece}, Name: {detail.Name}, Price: {detail.Price}");
            }

            IChessPiece? pieceToBuy = null;
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

            return pieceToBuy;
        }

        private void MovePiecePhase(IPlayer player, IBoard board)
        {
            Console.WriteLine("Do you want to move a piece? (y/n)");
            string movePieceResponse = Console.ReadLine().ToLower();
            while (movePieceResponse == "y")
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
                if (pieceToMove != null && _gameController.MovePiece(player, pieceToMove, board, sourcePosition, destPosition))
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

        public void BattlePlayers()
        {
            Console.WriteLine("");
            Console.WriteLine("Battle phase:");
            for (int i = 0; i < _players.Count; i++)
            {
                for (int j = i + 1; j < _players.Count; j++)
                {
                    IPlayer player1 = _players[i];
                    IPlayer player2 = _players[j];
                    Console.WriteLine($"Battle between {player1.GetPlayerName()} (ID: {player1.GetPlayerId()}) and {player2.GetPlayerName()} (ID: {player2.GetPlayerId()})");
                    Console.WriteLine("");
                    
                    Task.Delay(3000).Wait();
                    IPlayer winner = _gameController.PvPBattle(player1, player2);

                    if (winner == player1)
                    {
                        Console.WriteLine($"Player {player1.GetPlayerName()} (ID: {player1.GetPlayerId()}) won the battle.");
                        _gameController.WinRound(player1);
                        _gameController.DefeatRound(player2);
                    }
                    else
                    {
                        Console.WriteLine($"Player {player2.GetPlayerName()} (ID: {player2.GetPlayerId()}) won the battle.");
                        _gameController.WinRound(player2);
                        _gameController.DefeatRound(player1);
                    }
                }
            }

            foreach (var player in _players)
            {
                PlayerData playerData = _gameController.GetPlayerData(player);
                Console.WriteLine($"Player {player.GetPlayerName()} (ID: {player.GetPlayerId()}) health: {playerData.HealthPlayer}");
             

            }
        }
    }

