using System.Net.Http.Json;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.Repository;

public class CepRepositoryCacheDecorator : ICepRepository
{
    private readonly ICepRepository _repository;
    private readonly IDistributedCache _distributedCache;

    public CepRepositoryCacheDecorator(ICepRepository repository, IDistributedCache distributedCache)
    {
        _repository = repository;
        _distributedCache = distributedCache;
    }

    public async Task<Address> GetAddress(Cep cep)
    {
        var cacheKey = GenerateCacheKey(cep);
        var (address, hasCache) = await GetFromCache(cacheKey);

        if (hasCache)
            return address;

        address = await _repository.GetAddress(cep);
        await SaveOnCache(address, cacheKey);
        return address;
    }

    private async Task<(Address? address, bool hasCache)> GetFromCache(string cacheKey)
    {
        var cacheJson = await _distributedCache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheJson))
            return (null, false);

        var viaCepResponseCache = JsonConvert.DeserializeObject<Address>(cacheJson);
        return (viaCepResponseCache, true);
    }

    private Task SaveOnCache(Address address, string cacheKey)
    {
        var cacheOptions = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
        };
        
        return _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(address), cacheOptions);
    }

    private static string GenerateCacheKey(Cep cep)
    {
        return $"CustomerProfile:{cep.Number}:Cep";
    }
}