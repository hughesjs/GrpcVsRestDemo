using System.Runtime.InteropServices.JavaScript;
using AutoFixture;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using ProtobufDemo.ProtoModels;

namespace Benchmark;

public class AddressBookCustomisation: ICustomization
{
    // This is lazy... Don't write your customisations like this... Please
    public void Customize(IFixture fixture)
    {
        fixture.Customize<AddressBook>(c => c
            .FromFactory(() =>
            {
                
                
                
                AddressBook addressBook = new();
                addressBook.People.Add(MakePerson());
                addressBook.People.Add(MakePerson());
                addressBook.People.Add(MakePerson());
                addressBook.People.Add(MakePerson());
                addressBook.People.Add(MakePerson());
                return addressBook;

                Person MakePerson()
                {
                    Person person = new()
                    {
                        Email = Faker.Internet.Email(),
                        Id = Faker.RandomNumber.Next(),
                        Name = Faker.Name.FullName(),
                        LastUpdated = Timestamp.FromDateTime(Faker.Finance.Maturity().ToUniversalTime())
                    };
                    person.Phones.Add(MakePhoneNumber());
                    person.Phones.Add(MakePhoneNumber());
                    person.Phones.Add(MakePhoneNumber());
                    person.Phones.Add(MakePhoneNumber());
                    person.Phones.Add(MakePhoneNumber());
                    return person;
                }

                Person.Types.PhoneNumber MakePhoneNumber()
                {
                    Person.Types.PhoneNumber number = new()
                    {
                        Number = Faker.Phone.Number(),
                        Type = Person.Types.PhoneType.Mobile
                    };
                    return number;
                }
            }));
    }
}