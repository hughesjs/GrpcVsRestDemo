using GrpcVsRestBenchmark.ModelLib.REST;
using ModelLibrary.GRPC;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace GrpcVsRestBenchmark.ModelLib.Data
{
    public static class MeteoriteLandingData
    {
        private static readonly string DataJson;
        
        public static readonly Lazy<List<MeteoriteLandingRest>> MeteoriteLandingsRest = new(GetMeteoriteLandingsRest);
        public static readonly Lazy<List<MeteoriteLandingGrpc>> MeteoriteLandingsGrpc = new(GetMeteoriteLandingsGrpc);
        public static readonly Lazy<MeteoriteLandingList> MeteoriteLandingList = new(GetMeteoriteLandingList);

        static MeteoriteLandingData()
        {
            DataJson = File.ReadAllText("ModelLib/Data/MeteoriteLandings.json");
        }

        private static List<MeteoriteLandingRest> GetMeteoriteLandingsRest() => JsonSerializer.Deserialize<List<MeteoriteLandingRest>>(DataJson)!;
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
