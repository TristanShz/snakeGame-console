namespace snakeGame;

[TestClass]
public class GameUnitTests
{
    private Game _game;

    public GameUnitTests()
    {
        _game = new Game(2, 50);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(-1)]
    public void CreateGameWithOneOrLessNumberOfPlayers_ShouldReturnGameException(int numberOfPlayers)
    {
        Assert.ThrowsException<GameException>(() => new Game(numberOfPlayers, 50));
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CreateGameWithBoardSizeEqualOrInferiorThanZero_ShouldReturnGameException(int boardSize)
    {
        Assert.ThrowsException<GameException>(() => new Game(2, boardSize));
    }

    [TestMethod]
    public void CreateGameWithTwoPlayers_ShouldAddTwoPlayers()
    {
        var game = new Game(2, 50);
        Assert.AreEqual(game.Players.Count, 2);
    }

    [TestMethod]
    public void BeforeGameStart_PlayersPositionsShouldBeEqualToZero()
    {
        var game = new Game(2, 50);
        Assert.AreEqual(game.Players[0].Position, 0);
        Assert.AreEqual(game.Players[1].Position, 0);
    }

    [TestMethod]
    public void HandleTurn_ShouldMoveCurrentPlayerForward()
    {
        var game = new Game(2, 50);
        var initialPosition = game.CurrentPlayer.Position;
        game.HandleTurn();
        Assert.IsTrue(game.CurrentPlayer.Position > initialPosition);
    }

    [TestMethod]
    [DataRow(50)]
    public void PlayerOverBoardSize_ShouldReturnToHalfOfBoardSizePosition(int boardSize)
    {
        var game = new Game(2, boardSize);
        game.CurrentPlayer.Move(boardSize);
        game.OverBoardSize(game.CurrentPlayer);

        Assert.AreEqual(game.CurrentPlayer.Position, boardSize / 2);
    }

    [TestMethod]
    public void PlayerIsOnBonusCell_MethodShouldReturnTrue()
    {
        var game = new Game(2, 50);
        Assert.AreEqual(game.IsOnBonusCell(game.BonusCells[0]), true);
    }

    [TestMethod]
    [DataRow(50)]
    public void WhenGameIsOver_CurrentPlayerPositionShoudlBeEqualToBoardSize(int boardSize)
    {
        var game = new Game(2, boardSize);
        game.Start();
        Assert.AreEqual(game.CurrentPlayer.Position, boardSize);
    }
}
