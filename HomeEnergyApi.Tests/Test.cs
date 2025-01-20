using System.Text;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class Test
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private string testHome = JsonSerializer.Serialize(new Home(1, "Test", "123 Test St.", "Test City", 123));
    // private string testHomeNoId = JsonSerializer.Serialize(new Home(2, "Testy", "456 Assert St.", "Unitville", 456)).Replace("\"Id\":2", "\"Id\":null");
    // private string testHomeNoOwnerLastName = JsonSerializer.Serialize(new Home(3, "Test", "123 Test St.", "Test City", 123)).Replace("\"OwnerLastName\":\"Test\"", "\"OwnerLastName\":null");
    // private string testHomeInvalidStreetAddress = JsonSerializer.Serialize(new Home(3, "Tester", "123456789123456789123456789123456789 Avenue.", "Integration Town", 789));
    // private string testHomeInvalidMonthlyElectricUsage = JsonSerializer.Serialize(new Home(3, "Tester", "789 Theory St.", "Integration Town", 75000));

    public Test(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory, TestPriority(1)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseCodeOnGETHomes(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(2)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturns201CreatedHTTPResponseOnAddingValidHomeThroughPOST(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 201, $"HomeEnergyApi did not return \"201: Created\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent.ToLower() == testHome.ToLower(), $"HomeEnergyApi did not return the home being added as a response from the POST request at {url}; \n Expected : {testHome.ToLower()} \n Received : {responseContent.ToLower()} \n");
    }

    [Theory, TestPriority(3)]
    [InlineData("/Homes/1")]
    public async Task HomeEnergyApiReturnsAddedHomeFromGETUsingRouteParamter(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);
        string responseContent = await response.Content.ReadAsStringAsync();

        Assert.True(responseContent.ToLower() == testHome.ToLower(), $"HomeEnergyApi did not return successful the added Home through a GET request using router parameters at {url}; \n Expected : {testHome} \n Received : {responseContent}");
    }

    [Theory, TestPriority(4)]
    [InlineData("/Homes/99")]
    public async Task HomeEnergyApiReturns404NotFoundFromGETUsingRouteParamterForNonExistentHome(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True((int)response.StatusCode == 404, $"HomeEnergyApi did not return \"404: Not Found\" HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }



}
