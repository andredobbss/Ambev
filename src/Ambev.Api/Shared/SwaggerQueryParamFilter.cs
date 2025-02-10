using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.Api.Shared;

public class SwaggerQueryParamFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.Name == "GetCartsByEspecialFilersAsync")
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "category",
                In = ParameterLocation.Query,
                Description = "Filtra por categoria exata (ex: 'Produto ABC').",
                Schema = new OpenApiSchema { Type = "string" }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "title",
                In = ParameterLocation.Query,
                Description = "Filtra por título do produto (ex: 'Produto ABC*' ou *Produto ABC para busca parcial).",
                Schema = new OpenApiSchema { Type = "string" }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "_minPrice",
                In = ParameterLocation.Query,
                Description = "Filtra por preço mínimo (ex: '_minPrice=50').",
                Schema = new OpenApiSchema { Type = "number", Format = "decimal" }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "_maxPrice",
                In = ParameterLocation.Query,
                Description = "Filtra por preço máximo (ex: '_maxPrice=200').",
                Schema = new OpenApiSchema { Type = "number", Format = "decimal" }
            });
        }

        if (context.MethodInfo.Name == "GetProductsEspecialFilterAsync")
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "category",
                In = ParameterLocation.Query,
                Description = "Filtra por categoria exata (ex: 'Produto ABC').",
                Schema = new OpenApiSchema { Type = "string" }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "title",
                In = ParameterLocation.Query,
                Description = "Filtra por título do produto (ex: 'Produto ABC*' ou *Produto ABC para busca parcial).",
                Schema = new OpenApiSchema { Type = "string" }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "_minPrice",
                In = ParameterLocation.Query,
                Description = "Filtra por preço mínimo (ex: '_minPrice=50').",
                Schema = new OpenApiSchema { Type = "number", Format = "decimal" }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "_maxPrice",
                In = ParameterLocation.Query,
                Description = "Filtra por preço máximo (ex: '_maxPrice=200').",
                Schema = new OpenApiSchema { Type = "number", Format = "decimal" }
            });
        }
    }
}

