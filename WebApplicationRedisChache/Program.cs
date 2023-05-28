using WebApplicationRedisChache.Db;
using WebApplicationRedisChache.Repositories;
using WebApplicationRedisChache.Services;
using WebApplicationRedisChache.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheUrl"];
});

builder.Services.AddSingleton<AlexTestJwtDbContext>();
builder.Services.AddSingleton<RedisUtil>();

builder.Services.AddSingleton<AuthorsRepository>();
builder.Services.AddSingleton<AuthorsService>();

builder.Services.AddSingleton<ArticlesRepository>();
builder.Services.AddSingleton<ArticlesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();