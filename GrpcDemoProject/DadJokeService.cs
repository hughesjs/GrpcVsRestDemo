using Grpc.Core;
using ProtobufDemo.ProtoServices;

namespace GrpcDemoProject;

public class DadJokeService: ProtobufDemo.ProtoServices.DadJokeService.DadJokeServiceBase
{
    public override Task<DadJoke> GetDadJoke(DadJokeRequest request, ServerCallContext context)
    {
        string joke = request.Category switch
        {
            DadJokeRequest.Types.Category.Puns => "Why was the math book sad? Because it had too many problems.",
            DadJokeRequest.Types.Category.OneLiners => "I used to play piano by ear, but now I use my hands.",
            DadJokeRequest.Types.Category.PlayOnWords => "I told my wife she was drawing her eyebrows too high. She looked surprised.",
            DadJokeRequest.Types.Category.Anticlimactic => "I'm reading a book on anti-gravity. It's impossible to put down.",
            DadJokeRequest.Types.Category.Irony => "I used to be a software developer, but now I'm just a debug specialist.",
            _ => throw new ArgumentOutOfRangeException()
        };

        DadJoke response = new()
        {
            Joke = joke
        };

        return Task.FromResult(response);
    }
}