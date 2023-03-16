using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.Repository;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Infra.AntCorruptionLayer.ViaCep;

public class CepRepositoryCacheDecoratorUnitTest : BaseTest
{
    [Fact(DisplayName = "If Has Cep On Cache, Should Only Return")]
    public async Task HasCepOnCache()
    {
        //Arrange
        var cep = new Cep("70070130");
        var addressCache = new ViaCepResponse
        {
            Cep = cep.ToString(),
            Localidade = Faker.Address.City(),
            Logradouro = Faker.Address.StreetName(),
            Bairro = Faker.Address.OrdinalDirection(),
            Uf = Faker.Address.State()
        }.ToDomainAddress();

        var options = Options.Create(new MemoryDistributedCacheOptions());
        var cache = new MemoryDistributedCache(options);
        await cache.SetStringAsync($"CustomerProfile:{cep.Number}:Cep", JsonConvert.SerializeObject(addressCache));

        var repository = new Mock<ICepRepository>();
        var repositoryWithCacheDecorator = new CepRepositoryCacheDecorator(repository.Object, cache);

        //Act
        var address = await repositoryWithCacheDecorator.GetAddress(cep);

        //Assert
        repository.Verify(x => x.GetAddress(It.IsAny<Cep>()), Times.Never);
        address.Should().BeEquivalentTo(addressCache);
    }

    [Fact(DisplayName = "If Has No Cache Data, Should Set And Return The Value")]
    public async Task HasNoCache()
    {
        //Arrange
        var cep = new Cep("70070130");
        var viaCepResponse = new ViaCepResponse
        {
            Cep = cep.ToString(),
            Localidade = Faker.Address.City(),
            Logradouro = Faker.Address.StreetName(),
            Bairro = Faker.Address.OrdinalDirection(),
            Uf = Faker.Address.State()
        };

        var options = Options.Create(new MemoryDistributedCacheOptions());
        var cache = new MemoryDistributedCache(options);

        var repository = new Mock<ICepRepository>();
        repository.Setup(x => x.GetAddress(cep))
            .ReturnsAsync(viaCepResponse.ToDomainAddress());
        var repositoryWithCacheDecorator = new CepRepositoryCacheDecorator(repository.Object, cache);

        //Act
        var address = await repositoryWithCacheDecorator.GetAddress(cep);

        //Assert
        address.Should().BeEquivalentTo(viaCepResponse.ToDomainAddress());

        var addressOnCacheJson = await cache.GetStringAsync($"CustomerProfile:{cep.Number}:Cep");
        var addressOnCache = JsonConvert.DeserializeObject<Address>(addressOnCacheJson);
        addressOnCache.Should().BeEquivalentTo(address);
    }
}