using WebApplicationRedisChache.Db;
using WebApplicationRedisChache.Dtos;

namespace WebApplicationRedisChache.Repositories;

public class ArticlesRepository
{
    private AlexTestJwtDbContext _db;

    public ArticlesRepository(AlexTestJwtDbContext db)
    {
        _db = db;
    }

    public List<ArticleFlatResponseDto> GetAll()
    {
        List<ArticleFlatResponseDto> articles = _db.Articles.Select(
            article => new ArticleFlatResponseDto()
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                AuthorId = article.AuthorId,
                AuthorName = article.Author.Name,
                AuthorAge = article.Author.Age
            }).ToList();

        return articles;
    }

    public ArticleFlatResponseDto GetById(int id)
    {
        ArticleFlatResponseDto article = _db.Articles.Select(
            article => new ArticleFlatResponseDto()
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                AuthorId = article.AuthorId,
                AuthorName = article.Author.Name,
                AuthorAge = article.Author.Age
            }).FirstOrDefault(article => article.Id == id);

        return article;
    }

    public void DeleteById(int id)
    {
        Article article = new Article() { Id = id };
        _db.Articles.Remove(article);
        _db.SaveChanges();
    }
}