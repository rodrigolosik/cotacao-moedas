using Domain.Dtos;
using Bogus;

namespace CotacaoMoedas.UnitTests.Utils;

public static class Fakedata
{
    public static IEnumerable<ResponseEconomiaDto> ResponseDtos(int quantidade = 10)
    {
        return new Faker<ResponseEconomiaDto>("pt_BR")
            .RuleFor(r => r.High, Faker => Faker.Finance.Random.Float().ToString())
            .RuleFor(r => r.Low, Faker => Faker.Random.Float().ToString())
            .RuleFor(r => r.VarBid, Faker => Faker.Random.Float().ToString())
            .RuleFor(r => r.PctChange, Faker => Faker.Random.Float().ToString())
            .RuleFor(r => r.Bid, Faker => Faker.Random.Float().ToString())
            .RuleFor(r => r.Ask, Faker => Faker.Random.Float().ToString())
            .RuleFor(r => r.Timestamp, Faker => Faker.Random.Long(-62135596800, 253402300799).ToString())
            .RuleFor(r => r.Name, Faker => Faker.Name.LastName())
            .RuleFor(r => r.CreateDate, Faker => Faker.Date.Recent().ToString())
            .Generate(quantidade);
    }
}
