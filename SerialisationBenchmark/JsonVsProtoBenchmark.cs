using System.Text;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using ProtobufDemo.ProtoModels;
using Google.Protobuf;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Benchmark;

public class JsonVsProtoBenchmark
{
    private readonly AddressBook _addressBook;
    private readonly CodedOutputStream _codedOutputStream;
    private readonly Stream _nullStream;

    public JsonVsProtoBenchmark()
    {
        Fixture fixture = new();
        fixture.Customize(new AddressBookCustomisation());
        _addressBook = fixture.Create<AddressBook>();
        _codedOutputStream = new(Stream.Null);
        _nullStream = Stream.Null;
    }

    [Benchmark]
    public void SerialiseToSystemTextJsonOnly() => JsonSerializer.Serialize(_addressBook);

    [Benchmark]
    public void SerialiseToNewtonsoftJsonOnly() => JsonConvert.SerializeObject(_addressBook);

    [Benchmark]
    public void SerialiseToSystemTextJsonEncodeAndWrite() // This is more representative of what the protobuf serialiser is actually doing
    {
        string json = JsonSerializer.Serialize(_addressBook);
        byte[] encodedBytes = Encoding.UTF8.GetBytes(json);
        _nullStream.Write(encodedBytes);
    }
    
    [Benchmark]
    public void SerialiseToNewtonsoftJsonEncodeAndWrite() // This is more representative of what the protobuf serialiser is actually doing
    {
        string json = JsonConvert.SerializeObject(_addressBook);
        byte[] encodedBytes = Encoding.UTF8.GetBytes(json);
        _nullStream.Write(encodedBytes);
    }

    [Benchmark]
    public void SerialiseToProtobufAndWrite() => _addressBook.WriteTo(_codedOutputStream); // Note we have an extra write here that's not necessarily fair
}