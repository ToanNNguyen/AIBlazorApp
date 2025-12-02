using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApiAiBlazorLab.Services;
using Xunit;

public class CatFactServiceTests
{
    //  valid JSON returns correct fact
    [Fact]
    public async Task GetRandomFact_ReturnsFact()
    {
        // Arrange
        var json = "{\"fact\":\"Cats sleep 16 hours a day.\",\"length\":32}";
        var client = new HttpClient(new FakeHandler(json));
        var service = new CatFactService(client);

        // Act
        var result = await service.GetRandomFactAsync();

        // Assert
        Assert.Equal("Cats sleep 16 hours a day.", result);
    }

    //  missing `fact` property → fallback
   [Fact]
public async Task GetRandomFact_MissingFact_UsesFallback()
{
    // Arrange
    var json = "{\"length\":32}";
    var client = new HttpClient(new FakeHandler(json));
    var service = new CatFactService(client);

    // Act
    var result = await service.GetRandomFactAsync();

    // Assert
    Assert.Equal("No fact available.", result);
}

    //  `null` JSON → fallback
    [Fact]
public async Task GetRandomFact_NullJson_UsesFallback()
{
    // Arrange
    var json = "null"; // deserializes to null CatFact
    var client = new HttpClient(new FakeHandler(json));
    var service = new CatFactService(client);

    // Act
    var result = await service.GetRandomFactAsync();

    // Assert
    Assert.Equal("No fact available.", result);
}


    //  invalid JSON → throws (or change to fallback if you handle it)
    [Fact]
    public async Task GetRandomFact_InvalidJson_ThrowsJsonException()
    {
        // Arrange
        var json = "this is not valid json";
        var client = new HttpClient(new FakeHandler(json));
        var service = new CatFactService(client);

        // Act + Assert
        await Assert.ThrowsAsync<JsonException>(() => service.GetRandomFactAsync());
    }
}
