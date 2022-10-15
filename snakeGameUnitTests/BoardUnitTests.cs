using System;
using snakeGame;

namespace snakeGameUnitTests
{
    [TestClass]
    public class BoardUnitTests
    {
        private Board _board;
        int BoardSize = 50;

        [TestInitialize]
        public void InitTests()
        {
            _board = new Board(BoardSize);
        }

        [TestMethod]
        [DataRow(0)]
        public void InitBoardWithValueLowerThanMinValue_ShouldReturnBoardException(int size)
        {
            Assert.ThrowsException<UnhandledBoardSizeException>(() => new Board(size));
        }

        [TestMethod]
        [DataRow(999)]
        public void InitBoardWithValueGreaterThanMaxValue_ShoudReturnBoardException(int size)
        {
            Assert.ThrowsException<UnhandledBoardSizeException>(() => new Board(size));
        }

        [TestMethod]
        public void InitBoard_ShouldGenerateOneTenthBoardSizeValueOfBonusCells()
        {
            Assert.AreEqual(_board.BonusCellsCount, BoardSize / 10);
        }

        [TestMethod]
        public void BonusCellsShouldBeAllBetweenOneAndBoardSize()
        {
            var validValues = true;
            foreach (var bonusCell in _board._bonusCells)
            {
                if (bonusCell > BoardSize || bonusCell < 1) validValues = false;
            }

            Assert.IsTrue(validValues);
        }

        [TestMethod]
        public void CheckABonusCell_ShouldReturnTrue()
        {
            Assert.IsTrue(_board.IsOnBonusCell(_board._bonusCells[0]));
        }

        [TestMethod]
        public void CheckANonBonusCell_ShouldReturnFalse()
        {
            var random = new Random();

            int generateBonusCell()
            {
                var randomCell = random.Next(1, BoardSize);
                while (_board._bonusCells.Contains(randomCell))
                {
                    randomCell = random.Next(1, BoardSize);
                }
                return randomCell;
            }

            Assert.IsFalse(_board.IsOnBonusCell(generateBonusCell()));
        }
    }
}

