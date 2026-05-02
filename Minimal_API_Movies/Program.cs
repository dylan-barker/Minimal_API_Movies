using Minimal_API_Movies.Endpoints;
using Minimal_API_Movies.Repositories;
using Minimal_API_Movies.Utils;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IActorsRepository, ActorsRepository>();

builder.Services.AddAutoMapper(x => x.AddProfile<AutoMapperProfiles>());

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

app.MapGroup("/genres").MapGenres();

app.Run();