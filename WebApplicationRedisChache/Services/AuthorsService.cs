using Microsoft.Extensions.Caching.Distributed;
using WebApplicationRedisChache.Db;
using WebApplicationRedisChache.Dtos;
using WebApplicationRedisChache.Repositories;
using WebApplicationRedisChache.Util;

namespace WebApplicationRedisChache.Services;

public class AuthorsService
{
    private AuthorsRepository _authorsRepository;
    private RedisUtil _cache;

    public AuthorsService(AuthorsRepository authorsRepository, RedisUtil cache)
    {
        _authorsRepository = authorsRepository;
        _cache = cache;
    }

    public List<AuthorSimpleResponseDto> GetAll()
    {
        string key = "authors:getall";

        if (_cache.ExistData(key))
        {
            return _cache.Get<List<AuthorSimpleResponseDto>>(key);
        }

        List<AuthorSimpleResponseDto> authors = _authorsRepository.GetAll();
        _cache.Save(key, authors);

        return authors;
    }
}