using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Minimal_API_Movies.Endpoints;
using Minimal_API_Movies.Entities;
using Minimal_API_Movies.Repositories;
using Minimal_API_Movies.Services;
using Minimal_API_Movies.Utils;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IActorsRepository, ActorsRepository>();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<IErrorsRepository, ErrorsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

//builder.Services.AddTransient<IFileStorage, AzureFileStorage>();
builder.Services.AddTransient<IFileStorage, LocalFileStorage>();

builder.Services.AddTransient<IUserStore<IdentityUser>, UserStore>();
builder.Services.AddIdentityCore<IdentityUser>();
builder.Services.AddTransient<SignInManager<IdentityUser>>();
builder.Services.AddTransient<IUsersService, UsersService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(x => x.AddProfile<AutoMapperProfiles>());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.MapInboundClaims = false; // to prevent the default mapping of claim types to Microsoft-specific claim types

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // just for testing, in production you should validate the issuer
        ValidateAudience = false, // just for testing, in production you should validate the audience
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKeys = KeysHandler.GetAllKeys(builder.Configuration)
        //IssuerSigningKey = KeysHandler.GetKey(builder.Configuration).First() // just for testing, in production you should use multiple keys and rotate them
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("isadmin", policy => policy.RequireRole("isadmin"));
});

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

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error!;

    var error = new Error();
    error.Date = DateTime.UtcNow;
    error.Message = exception.Message;
    error.StackTrace = exception.StackTrace;

    var repository = context.RequestServices.GetRequiredService<IErrorsRepository>();
    await repository.Create(error);

    await Results.BadRequest(
        new
        {
            type = "error",
            message = "An error occurred while processing your request.",
            status = 500
        }).ExecuteAsync(context);
}));

app.UseStatusCodePages();

app.UseStaticFiles();

app.UseCors();

app.UseOutputCache();

app.UseAuthorization();

app.MapGroup("/genres").MapGenres();
app.MapGroup("/actors").MapActors();
app.MapGroup("/movies").MapMovies();
app.MapGroup("/movie/{movieId:int}/comments").MapComments();
app.MapGroup("/users").MapUsersEndpoints();

app.Run();