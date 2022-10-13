using System;
namespace snakeGame;

[TestClass]
public class PlayerUnitTests
{
    Player _player;

    public PlayerUnitTests()
    {
        _player = new Player("Player 1");
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    public void NameIsEmptyOrNull_ShouldReturnException(string name)
    {
        Assert.ThrowsException<Exception>(() => new Player(name));
    }

    [TestMethod]
    public void InitialPosition_ShouldBeEqualToZero()
    {
        Assert.AreEqual(_player.Position, 0);
    }

    [TestMethod]
    public void MoveWithNegativeParameter_ShouldReturnException()
    {
        _player.Position = 0;
        Assert.ThrowsException<Exception>(() => _player.Move(-5));
    }

    [TestMethod]
    [DataRow(10,10,20)]
    public void MoveWithPositiveParameter_ShouldMoveToPositionPlusParameter(int initialPosition, int moveValue, int result)
    {
        _player.Position = initialPosition;
        _player.Move(moveValue);
        Assert.AreEqual(_player.Position, result);
    }

    [TestMethod]
    [DataRow]
    [DataRow]
    [DataRow]
    [DataRow]
    [DataRow]
    public void PlayerDiceRoll_ShouldReturnAValueBetweenOneAndSix()
    {
        Assert.IsTrue(_player.DiceRoll() <= 6 && _player.DiceRoll() >= 1);
    }

}

