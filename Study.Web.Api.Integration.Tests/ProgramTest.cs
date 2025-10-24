namespace Study.Web.Api.Integration.Tests;

public class ProgramTest(WebApiFixture fixture)
{
    [Fact]
    public async Task Should_weatherforecast_returns_200()
    {
        // Arrange
        var client = fixture.Factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(200, (int)response.StatusCode);
    }
}