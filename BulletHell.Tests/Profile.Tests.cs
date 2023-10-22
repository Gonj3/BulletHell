namespace Tests;

public class ProfileTests
{
    private readonly string name = "playername";
    private readonly uint kills = 2;
    private readonly uint deaths = 5;
    private readonly double timeAlive = 2.2;

    [Fact]
    public void Test_FromFile()
    {
        // Arrange
        var mock = new Mock<IFileAccess>();
        mock.Setup(o => o.GetString()).Returns(name);
        mock.SetupSequence(o => o.Get32()).Returns(deaths).Returns(kills);
        mock.Setup(o => o.GetDouble()).Returns(timeAlive);

        // Act
        var result = Profile.FromFile(mock.Object);

        // Assert
        Assert.Equal(result.Name, name);
        Assert.Equal(result.Deaths, deaths);
        Assert.Equal(result.Kills, kills);
        Assert.Equal(result.TimeAlive, timeAlive);
    }

    [Fact]
    public void Test_WriteToFile()
    {
        // Arrange
        var profile = new Profile()
        {
            Name = name,
            Deaths = deaths,
            Kills = kills,
            TimeAlive = timeAlive
        };

        var mock = new Mock<IFileAccess>(MockBehavior.Strict);

        var seq = new MockSequence();
        // Ensure calls are made in sequence
        mock.InSequence(seq).Setup(o => o.StoreString(name));
        mock.InSequence(seq).Setup(o => o.Store32(deaths));
        mock.InSequence(seq).Setup(o => o.Store32(kills));
        mock.InSequence(seq).Setup(o => o.StoreDouble(timeAlive));

        // Act
        profile.WriteToFile(mock.Object);

        // Assert
        mock.Verify(o => o.StoreString(name));
        mock.Verify(o => o.Store32(deaths));
        mock.Verify(o => o.Store32(kills));
        mock.Verify(o => o.StoreDouble(timeAlive));
    }
}
