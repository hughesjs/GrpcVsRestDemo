using BenchmarkDotNet.Attributes;
using GRpcVsRestBenchmark.Clients;
using GrpcVsRestBenchmark.ModelLib.Data;
using ModelLibrary.GRPC;

namespace GRpcVsRestBenchmark;

public class GrpcVsRestBenchmark
{
    private readonly RestClient _restClient;
    private readonly GrpcClient _grpcClient;
    private readonly MeteoriteLandingList _meteoriteLandingList;
    
    public GrpcVsRestBenchmark()
    {
        _restClient = new();
        _grpcClient = new();
        _meteoriteLandingList = MeteoriteLandingData.MeteoriteLandingList.Value;
    }

    [Benchmark]
    public async Task RestGetSmallPayload() => await _restClient.GetSmallPayload();

    [Benchmark]
    public async Task GrpcGetSmallPayload() => await _grpcClient.GetSmallPayload();

    [Benchmark]
    public async Task RestGetLargePayload() => await _restClient.GetLargePayload();

    [Benchmark]
    public async Task GrpcGetLargePayload() => await _grpcClient.GetLargePayload();

    [Benchmark]
    public async Task RestPostLargePayload() => await _grpcClient.PostLargePayload(_meteoriteLandingList);

    [Benchmark]
    public async Task GrpcPostLargePayload() => await _grpcClient.PostLargePayload(_meteoriteLandingList);

    [Benchmark]
    public async Task GrpcStreamLargePayload() => await _grpcClient.StreamLargePayload();
}