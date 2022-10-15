using System;
namespace snakeGame
{
    public class Game
    {
        public int MAX_PLAYERS = 4;
        public int CurrentPlayerIndex;
        public int Turn;
        public Board Board;
        public List<Player> Players;
        public bool isPlaying;

        public Game()
        {
            CurrentPlayerIndex = 0;
            Turn = 0;
            Players = new List<Player>();
            isPlaying = false;
        }

        public int PlayerCount
        {
            get
            {
                return Players.Count;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return Players[CurrentPlayerIndex];
            }
        }

        public void Start(int numberOfPlayers, int boardSize)
        {
            CreateBoard(boardSize);
            CreatePlayers(numberOfPlayers);

            isPlaying = true;
            Turn += 1;
        }

        public void GameLoop()
        {
            while (isPlaying)
            {
                HandleTurn();
                HandlePlayerPosition(CurrentPlayer);
            }
        }

        public void AddPlayer(string name)
        {
            if (InvalidPlayerName(name)) throw new PlayerNameException($"Player with the name {name} is already created");
            if (CheckPlayerCount()) Players.Add(new Player(name));
            else throw new UnhandledNumberOfPlayersException($"Player Limit is {MAX_PLAYERS}");
        }

        public void CreatePlayers(int numberOfPlayers)
        {
            if (numberOfPlayers <= 1) throw new UnhandledNumberOfPlayersException("Number of player must be greater than 1");
            for (int i = 0; i < numberOfPlayers; i++)
            {
                AddPlayer($"Player {i + 1}");
            }
        }

        private void CreateBoard(int boardSize)
        { 
            Board = new Board(boardSize);
        }

        public void NextTurn()
        {
            Turn += 1;
            if (CurrentPlayerIndex == Players.Count - 1) CurrentPlayerIndex = 0;
            else CurrentPlayerIndex += 1;
        }

        public void HandlePlayerPosition(Player player)
        {
            if (Board.IsOnBonusCell(player.Position)) return;
            else if (player.Position == Board.Size) Win(player);
            else if (player.Position > Board.Size) OverBoardSize(player);
            NextTurn();
        }

        public void HandleTurn()
        {
                Console.WriteLine($"\n----- {CurrentPlayer.Name} turn -----");

                CurrentPlayer.Move(CurrentPlayer.DiceRoll());
        }

        private void OverBoardSize(Player player)
        {
            player.Position = Board.Size / 2;
            Console.WriteLine($"{player.Name} pass the case {Board.Size} he fell to the case {Board.Size / 2}");
        }

        private void Win(Player player)
        {
            Console.WriteLine($"\n\n {player.Name} win the game, CONGRATULATION !!!!\n");
            isPlaying = false;
        }


        private bool CheckPlayerCount()
        {
            if (PlayerCount < MAX_PLAYERS) return true;

            return false;
        }

        private bool InvalidPlayerName(string name)
        {
            foreach (var player in Players)
            {
                if (player.Name == name) return true;
            }

            return false;
        }
    }

    public class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }

    public class PlayerNameException : Exception
    {
        public PlayerNameException(string message) : base(message) { }
    }

    public class UnhandledNumberOfPlayersException : Exception
    {
        public UnhandledNumberOfPlayersException(string message) : base(message) { }
    }
}

