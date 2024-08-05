using GameAutoChess.Class;
using GameAutoChess.Class.Board;
using GameAutoChess.Class.PieceStore;
using GameAutoChess.Enum;
using GameAutoChess.Interface;

namespace GameAutoChess.Controller;

public class GameController
{
    private Dictionary<IPlayer, PlayerData> _player;
    private List<IChessPiece> _chessPieces;
    private IBoard _boards;
    public int MaxPlayer = 8;
    public int minPlayer = 2;

    public GameController()
    {
        _player = new Dictionary<IPlayer, PlayerData>();
        _chessPieces = new List<IChessPiece>();
        _boards = new Board(new IChessPiece[8, 8], BoardName.JoyfulBeach);
    }
    public bool CheckPlayer(int playerCount)
    {
        return playerCount >= minPlayer && playerCount <= MaxPlayer;
    }
    public void InitializePlayers(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            _player.Add(player, new PlayerData());
        }
    }
    
    public IBoard GetBoard()
    {
        return _boards;
    }


    public List<IPlayer> GetPlayers()
    {
        return _player.Keys.ToList();
    }
    public List<IChessPiece> GetChessPieces()
    {
        return _chessPieces;
    }
    public bool PrepPhase(IPlayer player, IChessPiece chessPiece, IBoard board, Position position, IPieceStore pieceStore, int piecePrice, Deck deck)
    {
        if (_player.ContainsKey(player))
        {
            bool buyPiece = pieceStore.BuyPiece(chessPiece, piecePrice, deck);
            if (buyPiece)
            {
                _chessPieces.Add(chessPiece);
            }
             
            
            if (_chessPieces.Contains(chessPiece) && !board.IsBoardOccupied(position))
            {
                _player[player].AddChessPiece(chessPiece);
                _chessPieces.Remove(chessPiece);
                board.SetPieceFromDeck(chessPiece, deck, position);
                return true;
            }
        }
        return false;
    }
    

    /*public bool CreepBattlePhase(IPlayer player, IChessPiece chessPiece, IBoard board)
    {
        if (_player.ContainsKey(player))
        {
            if (_player[player].GetChessPieces().Contains(chessPiece) && board.IsBoardOccupied(chessPiece.GetPosition()))
            {
                board.RemovePiece(chessPiece.GetPosition());
                return true;
            }
        }
        return false;
    }*/
    public bool MovePieceFromStoreToDeck(IPlayer player, IChessPiece chessPiece, IPieceStore pieceStore, int piecePrice, Deck deck)
    {
        if (_player.ContainsKey(player))
        {
            bool buyPiece = pieceStore.BuyPiece(chessPiece, piecePrice, deck);
            if (buyPiece)
            {
                _player[player].AddChessPiece(chessPiece);
                return true;
            }
        }
        return false;
    }
    public bool MovePieceFromDeckToBoard(IPlayer player, IChessPiece chessPiece, IBoard board, Position position, Deck deck)
    {
        if (_player.ContainsKey(player))
        {
            if (!board.IsBoardOccupied(position))
            {
                board.SetPieceFromDeck(chessPiece, deck, position);
                return true;
            }
        }
        return false;
    }
    public bool MovePiece (IPlayer player, IChessPiece chessPiece, IBoard board, Position source, Position destination)
    {
        if (_player.ContainsKey(player))
        {
            if (board.GetPiece(source) == chessPiece && !board.IsBoardOccupied(destination))
            {
                board.MovePiece(chessPiece, source, destination);
                return true;
            }
        }
        return false;
    }
    public List<IChessPiece> GetAllPieceDeck(Deck deck)
    {
        return deck.GetAllPiece();
    }
    public List<IChessPiece> GetAllPieceBoard(IBoard board)
    {
        List<IChessPiece> pieces = new List<IChessPiece>();
        for (int row = 0; row < board.GetBoard().GetLength(0); row++)
        {
            for (int col = 0; col < board.GetBoard().GetLength(1); col++)
            {
                if (board.GetBoard()[row, col] != null)
                {
                    pieces.Add(board.GetBoard()[row, col]);
                }
            }
        }
        return pieces;
    }

    /*public bool GetItemPlayer(IPlayer player, IItem item)
    {
        
    }
    public bool SetItemPiece(IPlayer player, IChessPiece chessPiece, IItem item)
    {
        
    }*/

    
    public bool InisiatePieceStore(IPlayer player, IPieceStore pieceStore)
    {
        if (_player.ContainsKey(player))
        {
            List<IChessPiece> initialChessPieces = new List<IChessPiece>(); 
            int initialPriceRefreshStore = 2; 
            PlayerData playerData = _player[player];

            pieceStore = new PieceStore(pieceStore.GetPieceSToreId(), initialPriceRefreshStore, initialChessPieces, playerData);
            playerData.PieceStore = pieceStore;
            return true;
        }
        return false;
    }
    public bool WinRound(IPlayer player, IChessPiece chessPiece)
    {
        if (_player.ContainsKey(player))
        {
            PlayerData playerData = _player[player];
            playerData.ResultMatchWin = true;
            playerData.Coins += 10;
            return true;
        }
        return false;
    }
    public bool DefeatRound(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            PlayerData playerData = _player[player];
            playerData.ResultMatchWin = false;
            playerData.HealthPlayer -= 10;
            playerData.Coins += 5;
            return true;
        }
        return false;
    }

    public async Task<bool> NextRound(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            await Task.Delay(30 * 60 * 1000);
            return true;
        }
        return false;
    }
    public bool PlayerLose(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            PlayerData playerData = _player[player];
            if (playerData.HealthPlayer <= 0)
            {
                return true;
            }
        }
        return false;
    }
    public bool PlayerWin(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            PlayerData playerData = _player[player];
            if (playerData.HealthPlayer > 0)
            {
                foreach (var otherPlayer in _player.Keys)
                {
                    if (!otherPlayer.Equals(player) && _player[otherPlayer].HealthPlayer > 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }
    public bool GameEnd()
    {
        foreach (var player in _player.Keys)
        {
            if (PlayerWin(player) || PlayerLose(player))
            {
                return true;
            }
        }
        return false;
    }

    public List<IChessPiece> GetPlayerPieces(IPlayer player) 
    {
        if (_player.ContainsKey(player))
        {
            return _player[player].GetChessPieces(); 
        }
        return new List<IChessPiece>();
    }
    public IPieceStore GetPieceStore(IPlayer player)
    {
        // Assuming each player has a piece store associated with them
        return _player[player].PieceStore;
    }
}

