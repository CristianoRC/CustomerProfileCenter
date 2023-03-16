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
        };

        var opts = Options.Create(new MemoryDistributedCacheOptions());
        var cache = new MemoryDistributedCache(opts);
        await cache.SetStringAsync($"CustomerProfile:{cep.Number}:Cep", JsonConvert.SerializeObject(addressCache));

        var repository = new Mock<ICepRepository>();
        var repositoryWithCacheDecorator = new CepRepositoryCacheDecorator(repository.Object, cache);

        //Act
        var address = await repositoryWithCacheDecorator.GetAddress(cep);

        //Assert
        repository.Verify(x => x.GetAddress(It.IsAny<Cep>()), Times.Never);
        var addressCacheConverted = addressCache.ToDomainAddress();
        address.Should().BeEquivalentTo(addressCacheConverted);
    }

    [Fact(DisplayName = "If Has No Cache Data, Should Set And Return The Value")]
    public async Task HasNoCache()
    {
        throw new NotImplementedException();
    }
}