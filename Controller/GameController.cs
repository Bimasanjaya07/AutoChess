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
    private readonly Dictionary<IPlayer, PlayerData> _player;
    private readonly List<IChessPiece> _chessPieces;
    private readonly IBoard _boards;
    public int MaxPlayer = 8;
    public int MinPlayer = 2;

    public GameController()
    {
        _player = new Dictionary<IPlayer, PlayerData>();
        _chessPieces = new List<IChessPiece>();
        _boards = new Board(new IChessPiece[8, 8], BoardName.JoyfulBeach);

        // Create abilities
        AbilityTheSource abilityTheSource = new AbilityTheSource(1, AbilityName.Awaken, "The Source releases a blast of energy, dealing damage to all enemies", DamageType.MagicalDamage, AbilityType.Active, AbilityTarget.Range, 2, 12);
        AbilityTuskChampion abilityTuskChampion = new AbilityTuskChampion(2, AbilityName.ArcticPunch, "Tusk Champion punches the enemy with his walrus punch, dealing critical damage", DamageType.PhysicalDamage, AbilityType.Active, AbilityTarget.Single, 300/100M, 6);
        AbilityShiningArcer abilityShiningArcer = new AbilityShiningArcer(3, AbilityName.ShootingStar, "Shoots a star to the enemy, dealing damage and stunning them", DamageType.MagicalDamage, AbilityType.Active, AbilityTarget.Single, 8, 100, 300, 1, 5);

        // Create details
        Detail detailTheSource = new Detail(1, "The Source", "A mystical being with powerful energy",  1, 1, 8, 0, true, true, RarityPiece.Common, TierPiece.OneStar, 0, 100);
        Detail detailTuskChampion = new Detail(2, "Tusk Champion", "A powerful warrior with a deadly punch",  1, 1, 8, 0, true, true, RarityPiece.Common, TierPiece.OneStar, 0, 100);
        Detail detailShiningArcer = new Detail(3, "Shining Arcer", "An archer with a stunning shot",  1, 1, 8, 0, true, true, RarityPiece.Common, TierPiece.OneStar, 0, 100);

        // Create statistics
        Statistic statsTheSource = new Statistic(500, 45, 1.7m, 469, 1, 5, 0);
        Statistic statsTuskChampion = new Statistic(650, 50, 1.2m, 160, 1, 5, 0);
        Statistic statsShiningArcer = new Statistic(450, 50, 1.0m, 469, 1, 0, 0);
        
        
        // Create behaviors
        IPieceBehavior behaviorTheSource = new BehaviorTheSource();
        IPieceBehavior behaviorTuskChampion = new BehaviorTuskChampion();
        IPieceBehavior behaviorShiningArcer = new BehaviorShiningArcer();
        
        // Create chess pieces
        IChessPiece theSource = new TheSource(detailTheSource, statsTheSource, abilityTheSource, behaviorTheSource);
        IChessPiece tuskChampion = new TuskChampion(detailTuskChampion, statsTuskChampion, abilityTuskChampion, behaviorTuskChampion);
        IChessPiece shiningArcer = new ShiningArcer(abilityShiningArcer, detailShiningArcer, statsShiningArcer, behaviorShiningArcer);

        // Add chess pieces to the list
        _chessPieces.Add(theSource);
        _chessPieces.Add(tuskChampion);
        _chessPieces.Add(shiningArcer);
    }

    public void InitializePlayers(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            PlayerData playerData = new PlayerData(player.GetPlayerId(), 2, 100, 0, 0, false);
            int pieceStoreId = player.GetPlayerId(); 
            int deckId = player.GetPlayerId();
            
            playerData.PieceStore = new PieceStore(pieceStoreId, 2, new List<IChessPiece>(), playerData, _chessPieces);

            playerData.Deck = new Deck(deckId, 5, new List<IChessPiece>());

            _player.Add(player, playerData);
        }
    }
    public bool CheckPlayer(int playerCount)
    {
        return playerCount >=MinPlayer && playerCount <= MaxPlayer;
    }
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

    public bool IsValidPositionForPlayer(IPlayer player, Position position)
    {
        int boardMidPoint = _boards.GetBoard().GetLength(1) / 2;
        if (player.GetPlayerId() == 1)
        {
            return position.Column < boardMidPoint; // Player 1 can place pieces in the up half
        }
        else
        {
            return position.Column >= boardMidPoint; // Other players can place pieces in the down half
        }
    }

    public bool MovePieceFromDeckToBoard(IPlayer player, IChessPiece chessPiece, IBoard board, Position position, IDeck deck)
    {
        if (_player.ContainsKey(player) &&
            !board.IsBoardOccupied(position) &&
            IsValidPositionForPlayer(player, position))
        {
            board.SetPieceFromDeck(chessPiece, deck, position);
            return true;
        }
        return false;
    }


    public bool MovePiece(IPlayer player, IChessPiece chessPiece, IBoard board, Position source, Position destination)
    {
        if (_player.ContainsKey(player) &&
            board.GetPiece(source) == chessPiece &&
            !board.IsBoardOccupied(destination))
        {
            board.MovePiece(chessPiece, source, destination);
            return true;
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
    
    
    private void ValidatePlayers(IPlayer player1, IPlayer player2)
    {
        if (!_player.ContainsKey(player1) && !_player.ContainsKey(player2))
        {
            throw new ArgumentException("Player tidak ditemukan.");
        }
    }
    public void AttackPiece(IChessPiece attackingPiece, IChessPiece targetPiece)
    {
        attackingPiece.Attack(targetPiece);
    }
    public IPlayer PvPBattle(IPlayer player1, IPlayer player2)
    {
        ValidatePlayers(player1, player2);

        List<IChessPiece> player1Pieces = GetPlayerPieces(player1);
        List<IChessPiece> player2Pieces = GetPlayerPieces(player2);
        // reafctor optional
        foreach (var piece1 in player1Pieces.ToList())
        {
            foreach (var piece2 in player2Pieces.ToList())
            {
                AttackPiece(piece1, piece2);
                if (piece2.GetStatistic().HealthPiece <= 0)
                {
                    player2Pieces.Remove(piece2);
                }
                if (piece1.GetStatistic().HealthPiece <= 0)
                {
                    player1Pieces.Remove(piece1);
                    break;
                }
            }
        }

        if (player1Pieces.Count <= 0)
        {
            return player2;
        }
        return player1;
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
    public void WinRound(IPlayer player)
    {
        PlayerData playerData = _player[player];
        playerData.ResultMatchWin = true;
        playerData.Coins += 5;
        playerData.WinStreak++;
    }

    public void DefeatRound(IPlayer player)
    {
        PlayerData playerData = _player[player];
        playerData.ResultMatchWin = false;
        playerData.HealthPlayer -= 10;
        playerData.Coins += 2;
        playerData.LoseStreak++;
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