using Grpc.Core;
using Grpc.Net.Client;
using ModelLibrary.GRPC;

namespace GRpcVsRestBenchmark.Clients;

public class GrpcClient
{
    private readonly GrpcChannel _channel;
    private readonly MeteoriteLandingsService.MeteoriteLandingsServiceClient _client;

    public GrpcClient()
    {
        _channel = GrpcChannel.ForAddress("https://localhost:5001");
        _client = new(_channel);
    }

    public async Task<string> GetSmallPayload() => (await _client.GetVersionAsync(new())).ApiVersion;

    public async Task<List<MeteoriteLandingGrpc>> StreamLargePayload()
    {
        List<MeteoriteLandingGrpc> meteoriteLandings = new();

        IAsyncStreamReader<MeteoriteLandingGrpc> response = _client.GetLargePayload(new()).ResponseStream;

        while (await response.MoveNext())
        {
            meteoriteLandings.Add(response.Current);
        }

        return meteoriteLandings;
    }

    public async Task<IList<MeteoriteLandingGrpc>> GetLargePayload() => (await _client.GetLargePayloadAsListAsync(new())).MeteoriteLandings;

    public async Task<string> PostLargePayload(MeteoriteLandingList meteoriteLandings) => (await _client.PostLargePayloadAsync(meteoriteLandings)).Status;
}