using GrpcVsRestBenchmark.ModelLib.REST;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace GrpcVsRestBenchmark.ModelLib.Data
{
    public static class MeteoriteLandingData
    {
        private static readonly string DataJson;
        
        public static readonly Lazy<List<MeteoriteLandingRest>> MeteoriteLandingsRest = new(GetMeteoriteLandingsRest);

        static MeteoriteLandingData()
        {
            DataJson = File.ReadAllText("ModelLib/Data/MeteoriteLandings.json");
        }

        private static List<MeteoriteLandingRest> GetMeteoriteLandingsRest() => JsonSerializer.Deserialize<List<MeteoriteLandingRest>>(DataJson)!;
    }
}
