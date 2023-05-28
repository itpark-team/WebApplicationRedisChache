using Microsoft.AspNetCore.Mvc;
using WebApplicationRedisChache.Dtos;
using WebApplicationRedisChache.Services;

namespace WebApplicationRedisChache.Controllers;

[ApiController]
[Route("articles/")]
public class ArticlesController : ControllerBase
{
    private ArticlesService _articlesService;

    public ArticlesController(ArticlesService articlesService)
    {
        _articlesService = articlesService;
    }

    [HttpGet("get-all")]
    public List<ArticleFlatResponseDto> GetAll()
    {
        return _articlesService.GetAll();
    }

    [HttpGet("get-by-id/{id}")]
    public ArticleFlatResponseDto GetById(int id)
    {
        return _articlesService.GetById(id);
    }

    [HttpDelete("delete-by-id/{id}")]
    public void DeleteById(int id)
    {
        _articlesService.DeleteById(id);
    }
}