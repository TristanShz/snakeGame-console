namespace snakeGame;

[TestClass]
public class GameUnitTests
{
    private Game _game;

    public GameUnitTests()
    {
        _game = new Game();
    }

    [TestMethod]
    public void InitGamePlayerCountShouldBeZero()
    {
        Assert.AreEqual(_game.PlayerCount, 0);
    }

    [TestMethod]
    public void AddingOnePlayer_PlayerCountShouldReturnOne()
    {
        _game.AddPlayer("Player 1");
        Assert.AreEqual(_game.PlayerCount, 1);
    }

    [TestMethod]
    public void AddingFourPlayers_PlayerCountShouldReturnFour()
    {
        _game.AddPlayer("Player 1");
        _game.AddPlayer("Player 2");
        _game.AddPlayer("Player 3");
        _game.AddPlayer("Player 4");

        Assert.AreEqual(_game.PlayerCount, 4);
    }

    [TestMethod]
    public void AddingTwoPlayersWithTheSameName_ShouldReturnPlayerNameException()
    {
        _game.AddPlayer("Player 1");
        Assert.ThrowsException<PlayerNameException>(() => _game.AddPlayer("Player 1"));
    }

    [TestMethod]
    public void AddingFivePlayers_ShouldReturnHandledNumberOfPlayersException()
    {
        _game.AddPlayer("Player 1");
        _game.AddPlayer("Player 2");
        _game.AddPlayer("Player 3");
        _game.AddPlayer("Player 4");
        Assert.ThrowsException<UnhandledNumberOfPlayersException>(() => _game.AddPlayer("Player 5"));
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void CreateLessThanTwoPlayerWithCreatePlayersMethod_ShouldReturnUnhandledNumberOfPlayersExceptiom(int numberOfPlayers)
    {
        Assert.ThrowsException<UnhandledNumberOfPlayersException>(() => _game.CreatePlayers(numberOfPlayers));
    }

    [TestMethod]
    public void AddingFivePlayersWithCreatePlayersMethod_ShouldReturnUnhandledNumberOfPlayersException()
    {
        Assert.ThrowsException<UnhandledNumberOfPlayersException>(() => _game.CreatePlayers(5));
    }

    [TestMethod]
    public void BeforeGameStart_IsPlayingShouldBeFalse()
    {
        Assert.IsFalse(_game.isPlaying);
    }

    [TestMethod]
    public void WhenGameIsStarted_IsPlayingShouldBeTrue()
    {
        _game.Start(2, 50);
        Assert.IsTrue(_game.isPlaying);
    }

    [TestMethod]
    public void BeforeGameStart_TurnShouldBeZero()
    {
        Assert.AreEqual(_game.Turn, 0);
    }

    [TestMethod]
    public void WhenGameIsStarted_TurnShouldBeOne()
    {
        _game.Start(2, 50);
        Assert.AreEqual(_game.Turn, 1);
    }

    [TestMethod]
    public void BeforeGameStart_PlayersPositionsShouldBeEqualToZero()
    {
        _game.CreatePlayers(4);
        Assert.AreEqual(_game.Players[0].Position, 0);
        Assert.AreEqual(_game.Players[1].Position, 0);
        Assert.AreEqual(_game.Players[2].Position, 0);
        Assert.AreEqual(_game.Players[3].Position, 0);
    }

    [TestMethod]
    public void WhenGameIsStarted_CurrentPlayerIndexShouldBeZero()
    {
        _game.Start(2, 50);
        Assert.AreEqual(_game.CurrentPlayerIndex, 0);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void StartingGameWithLessThanOnePlayer_ShouldReturnGameException(int numberOfPlayers)
    {
        Assert.ThrowsException<UnhandledNumberOfPlayersException>(() => _game.Start(numberOfPlayers, 50));
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void StartingGameWithBoardSizeEqualOrInferiorThanZero_ShouldReturnGameException(int boardSize)
    {
        Assert.ThrowsException<UnhandledBoardSizeException>(() => _game.Start(2, boardSize));
    }

    [TestMethod]
    public void NextTurn_ShouldIncreaseTurnByOne()
    {
        var initialTurn = _game.Turn;
        _game.NextTurn();

        Assert.AreEqual(_game.Turn, initialTurn + 1);
    }

    [TestMethod]
    public void HandleTurn_ShouldMoveCurrentPlayerForward()
    {
        _game.Start(2, 50);
        var initialPosition = _game.CurrentPlayer.Position;
        _game.HandleTurn();
        Assert.IsTrue(_game.CurrentPlayer.Position > initialPosition);
    }

    [TestMethod]
    public void FirstNextTurnCallWhenGameIsStarted_TurnShouldBeTwo()
    {
        _game.Start(2, 50);
        _game.NextTurn();
        Assert.AreEqual(_game.Turn, 2);
    }

    [TestMethod]
    public void FirstNextTurnCallWhenGameIsStarted_CurrentPlayerIndexShouldBeOne()
    {
        _game.Start(2, 50);
        _game.NextTurn();

        Assert.AreEqual(_game.CurrentPlayerIndex, 1);
    }

    [TestMethod]
    public void NextTurnWhenItsTheLastPlayerTurn_CurrentPlayerIndexShouldBeBackToZero()
    {
        _game.Start(2, 50);
        _game.NextTurn();
        _game.NextTurn();

        Assert.AreEqual(_game.CurrentPlayerIndex, 0);
    }

    [TestMethod]
    public void HandlePlayerPositionWhenPlayerIsOnBonusCell_SamePlayerShouldPlayerAnotherTurn()
    {
        _game.Start(2, 50);
        var initialPlayerIndex = _game.CurrentPlayerIndex;
        var bonusCell = _game.Board._bonusCells[0];
        _game.CurrentPlayer.Move(bonusCell);

        _game.HandlePlayerPosition(_game.CurrentPlayer);

        Assert.AreEqual(_game.CurrentPlayerIndex, initialPlayerIndex);

    }

    [TestMethod]
    public void HandlePlayerPositionWhenPlayerIsOverTheBoardSize_ShouldMovePlayerToTheMiddleOfTheBoard()
    {
        _game.Start(2, 50);
        _game.CurrentPlayer.Move(52);
        _game.HandlePlayerPosition(_game.CurrentPlayer);

        Assert.AreEqual(_game.Players[0].Position, 25);

    }

    [TestMethod]
    public void HandlePlayerPositionWhenPlayerIsOverTheBoardSize_ShouldGoToTheNextTurn()
    {
        _game.Start(2, 50);
        var initialTurn = _game.Turn;
        _game.CurrentPlayer.Move(52);
        _game.HandlePlayerPosition(_game.CurrentPlayer);

        Assert.AreEqual(_game.Turn, initialTurn + 1);
    }

    [TestMethod]
    public void HandlePlayerPositionWhenPlayerIsOnTheLastCell_ShouldStopTheGame()
    {
        _game.Start(2, 50);
        _game.CurrentPlayer.Move(50);
        _game.HandlePlayerPosition(_game.CurrentPlayer);

        Assert.IsFalse(_game.isPlaying);
    }

    [TestMethod]
    public void HandlePlayerPositionWhenPlayerIsOnNormalCell_ShouldGoToTheNextTurn()
    {
        _game.Start(2, 50);
        var initialTurn = _game.Turn;
        var random = new Random();

        int generateNormalCell()
        {
            var normalCell = random.Next(1, 50);
            if (_game.Board._bonusCells.Contains(normalCell)) generateNormalCell();
            return normalCell;
        }

        _game.CurrentPlayer.Move(generateNormalCell());
        _game.HandlePlayerPosition(_game.CurrentPlayer);

        Assert.AreEqual(_game.Turn, initialTurn + 1);
    }
}
