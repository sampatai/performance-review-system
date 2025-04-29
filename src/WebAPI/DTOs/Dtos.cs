using OfficePerformanceReview.Application.Common.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace OfficePerformanceReview.WebAPI.DTOs
{
    public class Dtos
    {
    }
    [SwaggerSchemaFilter(typeof(BaseFilter))]
    public record SearchDTO : FilterBase
    {
        public SearchDTO(FilterBase original) : base(original)
        {
        }
    }
}
