using Microsoft.AspNetCore.Mvc;
using WebApplicationRedisChache.Dtos;
using WebApplicationRedisChache.Services;

namespace WebApplicationRedisChache.Controllers;

[ApiController]
[Route("authors/")]
public class AuthorsController : ControllerBase
{
    private AuthorsService _authorsService;

    public AuthorsController(AuthorsService authorsService)
    {
        _authorsService = authorsService;
    }

    [HttpGet("get-all")]
    public List<AuthorSimpleResponseDto> GetAll()
    {
        return _authorsService.GetAll();
    }
}