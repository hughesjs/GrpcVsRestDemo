using System.Net.Http.Json;
using GrpcVsRestBenchmark.ModelLib.REST;

namespace GrpcVsRestBenchmark.Clients;

public class RestClient
{
    private readonly HttpClient _client;

    public RestClient()
    {
        // Never do this, inject your clients
        _client = new();
        _client.BaseAddress = new("https://localhost:5000/");
    }

    public async Task<string> GetSmallPayload() => await _client.GetStringAsync("Version");

    public async Task<List<MeteoriteLandingRest>> GetLargePayload() => (await _client.GetFromJsonAsync<List<MeteoriteLandingRest>>("LargePayload"))!;

    public async Task<string> PostLargePayload(List<MeteoriteLandingRest> meteoriteLandings)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("LargePayload", meteoriteLandings);
        return await response.Content.ReadAsStringAsync();
    }
}