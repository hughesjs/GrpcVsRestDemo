using GrpcVsProtobufBenchmark.ModelLib.Data;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.REST;

namespace GrpcVsProtobufBenchmark.Rest;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class MeteoriteLandingsController : ControllerBase
{
    [HttpGet]
    [Route("Version")]
    public ActionResult<string> Version() => "API Version 1.0";
    
    [HttpGet]
    [Route("LargePayload")]
    public ActionResult<IEnumerable<MeteoriteLandingRest>> GetLargePayloadAsync() => new ObjectResult(MeteoriteLandingData.MeteoriteLandingsRest.Value);

    [HttpPost]
    [Route("LargePayload")]
    public IActionResult PostLargePayload([FromBody] IEnumerable<MeteoriteLandingRest> meteoriteLandings) => Ok();
}