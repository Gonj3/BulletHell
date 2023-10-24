namespace Tests;

public class UtilTests
{
    [Theory]
    [InlineData(0, "0s")]
    [InlineData(59, "59s")]
    [InlineData(60, "1m 0s")]
    [InlineData((60 * 59) + 59, "59m 59s")]
    [InlineData((60 * 60), "1h 0s")]
    [InlineData((60 * 60 * 2) + (60 * 32) + 50, "2h 32m 50s")]
    public void Test_FormatSeconds(double seconds, string expected)
    {
        // Arrange / Act
        var result = Util.FormatSeconds(seconds);

        // Assert
        Assert.Equal(result, expected);
    }
}
