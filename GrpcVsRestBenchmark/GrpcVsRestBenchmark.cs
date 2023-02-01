using BenchmarkDotNet.Attributes;
using GrpcVsRestBenchmark.Clients;
using GrpcVsRestBenchmark.ModelLib.Data;
using ModelLibrary.GRPC;

namespace GrpcVsRestBenchmark;

public class GrpcVsRestBenchmark
{
    private RestClient _restClient;
    private GrpcClient _grpcClient;
    private MeteoriteLandingList _meteoriteLandingList;
    
    [GlobalSetup]
    public void Setup()
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