using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OfficePerformanceReview.Application.Common.Model;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OfficePerformanceReview.WebAPI.DTOs;

public class BaseFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context) => schema.Example = new OpenApiObject
    {
        [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(FilterBase.PageNumber))] = new OpenApiInteger(20),
        [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(FilterBase.PageSize))] = new OpenApiInteger(1),
        [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(FilterBase.SearchTerm))] = new OpenApiString(""),
        [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(FilterBase.SortDirection))] = new OpenApiString("asc"),
        [System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(FilterBase.SortColumn))] = new OpenApiString("")

    };
}

