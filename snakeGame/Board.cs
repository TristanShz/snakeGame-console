namespace snakeGame
{
    public class Board
    {
        private int MIN_SIZE = 30;
        private int MAX_SIZE = 150;

        public int Size;
        public List<int> _bonusCells = new List<int>();

        public Board(int size)
        {
            if (invalidBoardSize(size)) throw new UnhandledBoardSizeException($"Board size must be a value betweem {MIN_SIZE} and {MAX_SIZE}");

            Size = size;

            _bonusCells = GenerateBonusCells();
        }

        public int BonusCellsCount
        {
            get
            {
                return _bonusCells.Count;
            }
        }


        private List<int> GenerateBonusCells()
        {
            var random = new Random();

            var bonusCells = new List<int>();

            for (int i = 0; i < Size / 10; i++)
            {
                bonusCells.Add(random.Next(1, Size));
            }

            return bonusCells;
        }

        public Boolean IsOnBonusCell(int cell)
        {
            foreach (var BonusCell in _bonusCells)
            {
                if (BonusCell == cell)
                {
                    Console.WriteLine("It's a bonus cell !! You can play a other turn");
                    return true;
                }
            }

            return false;
        }

        public bool invalidBoardSize(int size) {
            return size < MIN_SIZE || size > MAX_SIZE;
        }
    }

    public class UnhandledBoardSizeException : Exception
    {
        public UnhandledBoardSizeException(string message) : base(message) { }
    }
}