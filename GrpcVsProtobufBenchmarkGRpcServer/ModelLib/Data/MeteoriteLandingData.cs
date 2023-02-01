using ModelLibrary.Data;
using ModelLibrary.GRPC;
using Newtonsoft.Json;


namespace GrpcVsProtobufBenchmark.ModelLib.Data
{
    public static class MeteoriteLandingData
    {
        private static readonly string DataJson;
        
        public static readonly Lazy<List<MeteoriteLandingGrpc>> MeteoriteLandingsGrpc = new(GetMeteoriteLandingsGrpc);
        public static readonly Lazy<MeteoriteLandingList> MeteoriteLandingList = new(GetMeteoriteLandingList);

        static MeteoriteLandingData()
        {
            DataJson = File.ReadAllText("ModelLib/Data/MeteoriteLandings.json");
        }
        
        private static List<MeteoriteLandingGrpc> GetMeteoriteLandingsGrpc() =>
            JsonConvert.DeserializeObject<List<MeteoriteLandingGrpc>>(DataJson, new ProtobufJsonConvertor())!;

        private static MeteoriteLandingList GetMeteoriteLandingList()
        {
            MeteoriteLandingList grpcMeteoriteLandingList = new();
            grpcMeteoriteLandingList.MeteoriteLandings.AddRange(MeteoriteLandingsGrpc.Value);
            return grpcMeteoriteLandingList;
        }
    }
}
