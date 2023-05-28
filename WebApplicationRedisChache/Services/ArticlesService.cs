using WebApplicationRedisChache.Dtos;
using WebApplicationRedisChache.Repositories;
using WebApplicationRedisChache.Util;

namespace WebApplicationRedisChache.Services;

public class ArticlesService
{
    private ArticlesRepository _articlesRepository;
    private RedisUtil _cache;

    public ArticlesService(ArticlesRepository articlesRepository, RedisUtil cache)
    {
        _articlesRepository = articlesRepository;
        _cache = cache;
    }

    public List<ArticleFlatResponseDto> GetAll()
    {
        string key = "articles:getall";

        if (_cache.ExistData(key))
        {
            return _cache.Get<List<ArticleFlatResponseDto>>(key);
        }

        List<ArticleFlatResponseDto> authors = _articlesRepository.GetAll();
        _cache.Save(key, authors);

        return authors;
    }

    public ArticleFlatResponseDto GetById(int id)
    {
        string key = $"articles:getbyid:{id}";

        if (_cache.ExistData(key))
        {
            return _cache.Get<ArticleFlatResponseDto>(key);
        }

        ArticleFlatResponseDto article = _articlesRepository.GetById(id);
        if (article != null)
        {
            _cache.Save(key, article);
        }

        return article;
    }

    public void DeleteById(int id)
    {
        string key = $"articles:getbyid:{id}";

        _cache.Delete(key);
        _articlesRepository.DeleteById(id);
    }
}