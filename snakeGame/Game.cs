using System;
namespace snakeGame
{
    public class Game
    {
        public List<int> BonusCells = new List<int> { 10, 20, 30, 40, 42 };
        private int _numberOfPlayers;
        private int _boardSize;
        private int _currentPlayerIndex;
        public List<Player> Players;

        public Game(int numberOfPlayers, int boardSize)
        {
            if (numberOfPlayers <= 1) throw new GameException("Number of player must be greater than 1");
            if (boardSize <= 0) throw new GameException("Board size must be greater than 0");
            _numberOfPlayers = numberOfPlayers;
            _boardSize = boardSize;
            _currentPlayerIndex = 0;
            Players = new List<Player>();

            CreatePlayers();
        }

        public Player CurrentPlayer
        {
            get
            {
                return Players[_currentPlayerIndex];
            }
        }

        public void Start()
        { 
            HandleTurn();
        }

        private void CreatePlayers()
        {
            for (int i = 0; i < _numberOfPlayers; i++)
            {
                Players.Add(new Player($"Player {i + 1}"));
            }
        }

        public void HandleTurn()
        {
            Console.WriteLine($"\n----- {CurrentPlayer.Name} turn -----");

            CurrentPlayer.Move(CurrentPlayer.DiceRoll());

            CheckCell(CurrentPlayer.Position);
        }

        public void PlayNextTurn()
        {
            if (_currentPlayerIndex == Players.Count - 1) _currentPlayerIndex = 0;
            else _currentPlayerIndex += 1;

            HandleTurn();
        }

        private void Win(Player player)
        {
            Console.WriteLine($"\n\n {player.Name} win the game, CONGRATULATION !!!!\n");
        }

        public void CheckCell(int cell)
        {
            if (cell > _boardSize)
            {
                OverBoardSize(CurrentPlayer);
                PlayNextTurn();
            }
            else if (cell == _boardSize) Win(CurrentPlayer);
            else
            {
                if (IsOnBonusCell(cell)) HandleTurn();
                else PlayNextTurn();
            }
        }

        public Boolean IsOnBonusCell(int cell)
        {
            foreach (var BonusCell in BonusCells)
            {
                if (BonusCell == cell)
                {
                    Console.WriteLine("It's a bonus cell !! You can play a other turn");
                    return true;
                }
            }

            return false;
        }

        public void OverBoardSize(Player player)
        {
            player.Position = _boardSize / 2;
            Console.WriteLine($"{player.Name} pass the case {_boardSize} he fell to the case {_boardSize/2}");
        }
    }

    public class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }
}

