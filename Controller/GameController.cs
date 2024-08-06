using System.Text;
using GameAutoChess.Class;
using GameAutoChess.Class.Board;
using GameAutoChess.Class.ChessPiece;
using GameAutoChess.Class.ChessPiece.PieceName;
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

        // Create abilities
        AbilityShiningArcer abilityShiningArcer = new AbilityShiningArcer(3, AbilityName.ShootingStar, "Shoots a star to the enemy, dealing damage and stunning them", DamageType.MagicalDamage, AbilityType.Active, AbilityTarget.Single, 3, 50, 100, 1, 2);
        AbilityTuskChampion abilityTuskChampion = new AbilityTuskChampion(2, AbilityName.ArcticPunch, "Tusk Champion punches the enemy with his walrus punch, dealing critical damage", DamageType.PhysicalDamage, AbilityType.Active, AbilityTarget.Single, 3.0M, 100);
        AbilityTheSource abilityTheSource = new AbilityTheSource(1, AbilityName.Awaken, "The Source releases a blast of energy, dealing damage to all enemies", DamageType.MagicalDamage, AbilityType.Active, AbilityTarget.Range, 3, 12);

        // Create details
        Detail detailTuskChampion = new Detail(1, "Tusk Champion", "A powerful warrior with a deadly punch",  1, 3, 1, 0, true, true, RarityPiece.Common, TierPiece.OneStar, 0, 100);
        Detail detailShiningArcer = new Detail(2, "Shining Arcer", "An archer with a stunning shot",  1, 2, 1, 0, true, true, RarityPiece.Rare, TierPiece.OneStar, 0, 100);
        Detail detailTheSource = new Detail(3, "The Source", "A mystical being with powerful energy",  1, 4, 1, 0, true, true, RarityPiece.Legendary, TierPiece.OneStar, 0, 100);

        // Create statistics
        Statistic statsTuskChampion = new Statistic(100, 20, 1.5m, 1, 30, 5, 10);
        Statistic statsShiningArcer = new Statistic(80, 15, 1.2m, 3, 25, 3, 8);
        Statistic statsTheSource = new Statistic(120, 10, 1.0m, 2, 40, 4, 12);

        // Create chess pieces
        IChessPiece tuskChampion = new TuskChampion(detailTuskChampion, statsTuskChampion, abilityTuskChampion);
        IChessPiece shiningArcer = new ShiningArcer(abilityShiningArcer, detailShiningArcer, statsShiningArcer);
        IChessPiece theSource = new TheSource(detailTheSource, statsTheSource, abilityTheSource);

        // Add chess pieces to the list
        _chessPieces.Add(tuskChampion);
        _chessPieces.Add(shiningArcer);
        _chessPieces.Add(theSource);
    }

    public void InitializePlayers(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            PlayerData playerData = new PlayerData(player.GetPlayerId(), 2, 100, 0, 0, false);
            int pieceStoreId = player.GetPlayerId(); 
            int deckId = player.GetPlayerId();
            
            List<IChessPiece> initialPieces = new List<IChessPiece>(_chessPieces);
            playerData.PieceStore = new PieceStore(pieceStoreId, 2, initialPieces, playerData);

            playerData.Deck = new Deck(deckId, 5, new List<IChessPiece>());

            _player.Add(player, playerData);
        }
    }
    public bool CheckPlayer(int playerCount)
    {
        return playerCount >= minPlayer && playerCount <= MaxPlayer;
    }
    // GameController.cs
    public PlayerData GetPlayerData(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            return _player[player];
        }
        return null;
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

    public bool PrepPhase(IPlayer player, IChessPiece chessPiece, IBoard board, Position position, IPieceStore pieceStore, int piecePrice, IDeck deck)
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

    // GameController.cs
    public bool IsValidPositionForPlayer(IPlayer player, Position position)
    {
        int boardMidPoint = _boards.GetBoard().GetLength(1) / 2;
        if (player.GetPlayerId() == 1)
        {
            return position.Column < boardMidPoint; // Player 1 can place pieces in the left half
        }
        else
        {
            return position.Column >= boardMidPoint; // Other players can place pieces in the right half
        }
    }

    public bool MovePieceFromDeckToBoard(IPlayer player, IChessPiece chessPiece, IBoard board, Position position, IDeck deck)
    {
        if (_player.ContainsKey(player))
        {
            if (!board.IsBoardOccupied(position) && IsValidPositionForPlayer(player, position))
            {
                board.SetPieceFromDeck(chessPiece, deck, position);
                return true;
            }
        }
        return false;
    }

    public bool MovePiece(IPlayer player, IChessPiece chessPiece, IBoard board, Position source, Position destination)
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

    /*
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
    }*/

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
                    if (otherPlayer != player && _player[otherPlayer].HealthPlayer > 0)
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
        return _player[player].PieceStore;
    }
    
    public int GetPlayerCoins(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            return _player[player].Coins;
        }
        return 0;
    }

    public Deck GetPlayerDeck(IPlayer player)
    {
        if (_player.ContainsKey(player))
        {
            return _player[player].Deck;
        }
        return null;
    }
}