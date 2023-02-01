// See https://aka.ms/new-console-template for more information

using GrpcVsProtobufBenchmark.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", true);
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddControllers();

WebApplication app = builder.Build();
app.MapGrpcService<MeteoriteLandingsService>();
app.MapGrpcReflectionService();
app.MapControllers();

app.Run();