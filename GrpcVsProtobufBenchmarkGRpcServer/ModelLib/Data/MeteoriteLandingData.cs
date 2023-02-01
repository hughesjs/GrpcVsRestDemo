using ModelLibrary.GRPC;
using ModelLibrary.REST;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace ModelLibrary.Data
{
    public static class MeteoriteLandingData
    {
        private static readonly string DataJson;
        
        public static Lazy<List<MeteoriteLandingRest>> MeteoriteLandingsRest = new(GetMeteoriteLandingsRest);
        public static Lazy<List<MeteoriteLandingGrpc>> MeteoriteLandingsGrpc = new(GetMeteoriteLandingsGrpc);
        public static Lazy<MeteoriteLandingList> MeteoriteLandingList = new(GetMeteoriteLandingList);

        static MeteoriteLandingData()
        {
            DataJson = File.ReadAllText("MeteoriteLandings.json");
        }

        private static List<MeteoriteLandingRest> GetMeteoriteLandingsRest() =>
            JsonSerializer.Deserialize<List<MeteoriteLandingRest>>(DataJson)!;
        
        // Yes, I know I'm mixing newtonsoft and system.text, but the converter was already in the lib
        private static List<MeteoriteLandingGrpc> GetMeteoriteLandingsGrpc() =>
            JsonConvert.DeserializeObject<List<MeteoriteLandingGrpc>>(DataJson, new ProtobufJsonConvertor());

        private static MeteoriteLandingList GetMeteoriteLandingList()
        {
            MeteoriteLandingList grpcMeteoriteLandingList = new();
            grpcMeteoriteLandingList.MeteoriteLandings.AddRange(MeteoriteLandingsGrpc.Value);
            return grpcMeteoriteLandingList;
        }
    }
}
