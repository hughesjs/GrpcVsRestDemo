using Grpc.Core;
using GrpcVsRestBenchmark.ModelLib.Data;
using ModelLibrary.GRPC;
using Version = ModelLibrary.GRPC.Version;

namespace GrpcVsRestBenchmark.Grpc;

public class MeteoriteLandingsService: ModelLibrary.GRPC.MeteoriteLandingsService.MeteoriteLandingsServiceBase
{
    public override Task<Version> GetVersion(EmptyRequest request, ServerCallContext context)
    {
        return Task.FromResult(new Version
        {
            ApiVersion = "API Version 1.0"
        });
    }

    public override async Task GetLargePayload(EmptyRequest request, IServerStreamWriter<MeteoriteLandingGrpc> responseStream, ServerCallContext context)
    {
        foreach (MeteoriteLandingGrpc meteoriteLanding in MeteoriteLandingData.MeteoriteLandingsGrpc.Value)
        {
            await responseStream.WriteAsync(meteoriteLanding);
        }
    }

    public override Task<MeteoriteLandingList> GetLargePayloadAsList(EmptyRequest request, ServerCallContext context)
    {
        return Task.FromResult(MeteoriteLandingData.MeteoriteLandingList.Value);
    }

    public override Task<StatusResponse> PostLargePayload(MeteoriteLandingList request, ServerCallContext context)
    {
        return Task.FromResult(new StatusResponse { Status = "SUCCESS" });
    }
}