using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Minimal_API_Movies.DTOs;

namespace Minimal_API_Movies.Utils
{
    public static class SwaggerExtentions
    {
        public static TBuilder AddPaginationParameters<TBuilder>(TBuilder builder)
            where TBuilder : IEndpointConventionBuilder
        {
            return builder.WithOpenApi(options =>
            {
                AddPaginationParameters(options);
                return options;
            });
        }

        public static TBuilder AddMoviesFilterParameters<TBuilder>(TBuilder builder)
            where TBuilder : IEndpointConventionBuilder
        {
            return builder.WithOpenApi(options =>
            {
                AddPaginationParameters(options);
                options.Parameters.Add(new OpenApiParameter
                {
                    Name = "title",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                });
                options.Parameters.Add(new OpenApiParameter
                {
                    Name = "InTheaters",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean"
                    }
                });
                options.Parameters.Add(new OpenApiParameter
                {
                    Name = "GenreId",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer"
                    }
                });
                options.Parameters.Add(new OpenApiParameter
                {
                    Name = "FutureReleases",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean"
                    }
                });
                options.Parameters.Add(new OpenApiParameter
                {
                    Name = "OrderByField",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = new List<IOpenApiAny>
                        {
                            new OpenApiString("Title"),
                            new OpenApiString("ReleaseDate")
                        }
                    }
                });
                options.Parameters.Add(new OpenApiParameter
                {
                    Name = "OrderByAscending",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean"
                    }
                });
                return options;
            });
        }

        private static void AddPaginationParameters(OpenApiOperation openApiOperation)
        {
            openApiOperation.Parameters.Add(new OpenApiParameter
            {
                Name = "page",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Default = new OpenApiInteger(PaginationDTO.pageInitialValue)
                }
            });
            openApiOperation.Parameters.Add(new OpenApiParameter
            {
                Name = "pageSize",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Default = new OpenApiInteger(PaginationDTO.recordsPerPageInitialValue)
                }
            });
        }
    }
}