using GameAutoChess.Class;
using GameAutoChess.Class.Board;
using GameAutoChess.Class.PieceStore;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Controller;

    public class GameController
    {
        public readonly int MaxPlayer = 8;
        private Dictionary<IPlayer, PlayerData> _players;
        private IBoard _board;
        private List<IDeck> _listDeck;
        private List<IPieceStore> _listPieceStore;
        private List<IChessPiece> _chessPieces;

        public GameController()
        {
            _players = new Dictionary<IPlayer, PlayerData>();
            _chessPieces = new List<IChessPiece>();

            // Create player
            IPlayer player1 = new Player(1, "Player 1");
            IPlayer player2 = new Player(2, "Player 2");
            _players.Add(player1, new PlayerData(1, 10, 100, 0, 0, false));
            _players.Add(player2, new PlayerData(2, 10, 100, 0, 0, false));

            // Create board
            IChessPiece[,] initialPieces = new IChessPiece[8, 8];
            _board = new Board(initialPieces, BoardName.JoyfulBeach);

            // Create decks
            _listDeck = new List<IDeck>
            {
                new Deck(1, 8, new List<IChessPiece>()),
                new Deck(2, 8, new List<IChessPiece>())
            };

            // Create piece stores
            _listPieceStore = new List<IPieceStore>
            {
                new PieceStore(1, 2, _chessPieces),
                new PieceStore(2, 2, _chessPieces)
            };
        }

        
        public bool PrepPhase(IPlayer player, IPieceStore pieceStore, IDeck deck, IBoard board, int idPiece, int row, int column)
        {
            if (player == null || pieceStore == null || deck == null || board == null)
            {
                return false;
            }

            if (!_players.ContainsKey(player))
            {
                return false; 
            }
            
            // get piece from store
            IChessPiece pieceToBuy = pieceStore.GetPiece(idPiece);
            if (pieceToBuy != null && pieceStore.BuyPiece(pieceToBuy, pieceToBuy.GetDetail().Price))
            {
                deck.AddPieceFromStore(pieceToBuy);
            }
            
            // sell piece from deck
            IChessPiece pieceToSell = deck.GetPieceFromDeck(idPiece);
            if (pieceToSell != null)
            {
                deck.SellPieceFromDeck(pieceToSell);
            }
            
            // set piece from deck to board
            IChessPiece pieceToSet = deck.GetPieceFromDeck(idPiece);
            Position position = new Position(column, row);
            if (pieceToSet != null && !board.IsBoardOccupied(position))
            {
                board.SetPieceFromDeck(pieceToSet, deck, position);
                deck.RemovePieceDeck(pieceToSet);
            }
            
            // set piece from board to deck
            IChessPiece existingPiece = board.GetPiece(position);
            if (existingPiece != null)
            {
                board.RemovePiece(position);
            }
            return true;
        }
        
    }


