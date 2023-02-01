using Grpc.Core;
using ProtobufDemo.ProtoServices;

namespace GrpcDemoProject;

public class PhysicsJokeService: ProtobufDemo.ProtoServices.PhysicsJokeService.PhysicsJokeServiceBase
{
    private int _jokeIndex;
    
    private readonly List<string> _jokes = new()
    {
            "Why did the physicist cross the road? To get to the other side of the equation!",
            "Why did the particle go to the therapist? It was feeling a little wave-particle duality!",
            "Why did the physics major wear glasses? He couldn't C#!",
            "Why don't physicists trust atoms? Because they make up everything!",
            "Why was the physics book sad? Because it had so many problems!",
            "Why did the electron go to church? To get a little charged up!",
            "Why did the physicist run around his lab? He wanted to generate a little centrifugal force!",
            "Why did the particle go to the party? It wanted to see some excited states!",
            "Why did the physicist put his money in the freezer? He wanted cold hard cash!",
            "Why did the physicist put his keys in the fridge? He wanted to keep his potential energy low!",
            "Why was the physicist always calm? He had a lot of mass!",
            "Why did the physicist put his calculator in the oven? He wanted to bake some pi!",
            "Why did the particle go to the amusement park? It wanted to experience some exciting states!",
            "Why did the physicist wear a coat to work? He wanted to keep his kinetic energy constant!",
            "Why did the physicist turn off the lights? He wanted to see the dark side of the spectrum!",
            "Why was the physics book so smart? It had a lot of mass!",
            "Why did the particle go to the doctor? It was feeling a little wave-particle duality!",
            "Why was the physicist always so tired? Because he was constantly calculating!",
            "Why did the physicist cross the playground? To get to the other side of the equation!",
            "Why did the physicist put his work in the fridge? He wanted to keep it cool!",
            "Why did the physicist put his calculator in the blender? He wanted to make some smoothies!",
            "Why did the particle go to the party? It wanted to have a little fun!",
            "Why did the physicist go to the bar? To get a little charged up!",
            "Why did the physicist turn off the lights? He wanted to conserve energy!",
            "Why did the physicist put his car in the freezer? He wanted to get some coolant!",
            "Why did the physicist wear a coat to work? He wanted to conserve energy!",
            "Why did the physicist put his work in the microwave? He wanted to cook up some solutions!",
            "Why did the particle go to the amusement park? It wanted to have a little fun!",
            "Why did the physicist go to the bar? To get a little charged up!",
            "Why did the physicist turn off the lights? He wanted to conserve energy!",
            "Why did the physicist put his calculator in the blender? He wanted to make some smoothies!",
            "Why did the particle go to the doctor? It was feeling a little wave-particle duality!",
    };

    public override async Task SubscribeToJokes(IAsyncStreamReader<Reset> requestStream, IServerStreamWriter<PhysicsJoke> responseStream, ServerCallContext context)
    {
        // There's almost certainly a nice tidy way to do this with a loop of some sort... But it's late and I'm tired
        Task resetWaiter = Task.Run(() => ResetTask(requestStream, context), context.CancellationToken);
        Task jokeSender = Task.Run(() => JokeTask(responseStream, context), context.CancellationToken);

        await Task.WhenAll(resetWaiter, jokeSender);
    }

    private async Task JokeTask(IAsyncStreamWriter<PhysicsJoke> responseStream, ServerCallContext context)
    {
        while (!context.CancellationToken.IsCancellationRequested)
        {
            await responseStream.WriteAsync(new PhysicsJoke
            {
                Joke = _jokes[_jokeIndex++%_jokes.Count]
            });
            
            await Task.Delay(5000, context.CancellationToken);
        }
    }

    private async Task ResetTask(IAsyncStreamReader<Reset> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext(context.CancellationToken))
        {
            _jokeIndex = 0;
        }
    }
}