using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Repositories;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddScoped<IGenresRepository, GenresRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddOutputCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseOutputCache();

app.MapGet("/", () => "Hello World!");

app.MapGet("/genres", async (IGenresRepository genresRepository) =>
{
    var genres = await genresRepository.GetAll();
    return genres;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(30)).Tag("genres-get")); // Cache the response for 30 seconds

app.MapGet("/genres/{id:int}", async (int id, IGenresRepository genresRepository) =>
{
    var genre = await genresRepository.GetById(id);

    if (genre is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(genre);
});

app.MapPost("/genres", async (Genre genre, IGenresRepository genresRepository,
    IOutputCacheStore outputCacheStore) =>
{
    await genresRepository.Create(genre);
    await outputCacheStore.EvictByTagAsync("genres-get", default);
    return TypedResults.Created($"/genres/{genre.Id}", genre);
});

app.Run();
