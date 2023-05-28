using WebApplicationRedisChache.Db;
using WebApplicationRedisChache.Dtos;

namespace WebApplicationRedisChache.Repositories;

public class AuthorsRepository
{
    private AlexTestJwtDbContext _db;

    public AuthorsRepository(AlexTestJwtDbContext db)
    {
        _db = db;
    }

    public List<AuthorSimpleResponseDto> GetAll()
    {
        List<AuthorSimpleResponseDto> authors = _db.Authors.Select(
            author => new AuthorSimpleResponseDto()
            {
                Id = author.Id,
                Name = author.Name,
                Age = author.Age,
                Articles = author.Articles.Select(
                    article => new ArticleSimpleResponseDto()
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Content = article.Content
                    }
                ).ToList()
            }).ToList();

        return authors;
    }
}