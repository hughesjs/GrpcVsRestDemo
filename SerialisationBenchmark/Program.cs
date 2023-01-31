// See https://aka.ms/new-console-template for more information


using System.Text;
using AutoFixture;
using Benchmark;
using Google.Protobuf;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Google.Protobuf.Collections;
using ProtobufDemo.ProtoModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

// Run serialisation benchmark
// Summary summary = BenchmarkRunner.Run<JsonVsProtoBenchmark>();

namespace Benchmark;

public class Benchmark
{
    public static void Main()
    {
        //Get size difference
        Fixture fixture = new();
        fixture.Customize(new AddressBookCustomisation());
        // fixture.Customize(new PersonCustomisation());
        // fixture.Customize(new PhoneNumberCustomisation());


        AddressBook book = fixture.Create<AddressBook>();

        string json = JsonSerializer.Serialize(book);
        byte[] encodedBytes = Encoding.UTF8.GetBytes(json);

        Console.WriteLine($"JSON Payload Size: {encodedBytes.Length}B");


        MemoryStream memStream = new();
        CodedOutputStream codedOutputStream = new(memStream);

        book.WriteTo(codedOutputStream);


        Console.WriteLine($"Protobuf Payload Size: {memStream.ToArray().Length}B");
    }
}

