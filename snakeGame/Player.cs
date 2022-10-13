namespace snakeGame
{
    public class Player
    {
        public string Name;
        public int Position { get; set; }
        private Dice _dice;

        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Player name should not be null or empty");
            }

            Name = name;
            Position = 0;
            _dice = new Dice();
        }

        public void Move(int number)
        {
            if (Position + number < 0) throw new Exception("Can't move to a negative position");

            Position += number;
        }

        public int DiceRoll()
        {
            var diceResult = _dice.Roll();

            Console.WriteLine($"Roll a {diceResult}");

            return diceResult;
        }
    }
}