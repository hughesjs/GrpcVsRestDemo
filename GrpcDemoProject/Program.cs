﻿// See https://aka.ms/new-console-template for more information

using GrpcDemoProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", true);
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

WebApplication app = builder.Build();
app.MapGrpcService<DadJokeService>();
app.MapGrpcService<PhysicsJokeService>();
app.MapGrpcReflectionService();

app.Run();