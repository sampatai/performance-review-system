
namespace OfficePerformanceReview.WebAPI.MinimalApi.Abstractions
{
    public abstract class GroupedEndPoint : IEndpoint
    {
        protected abstract string Group { get; }
        protected abstract string Tag { get; }
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            Configure(app.MapGroup($"/api/{Group}")
                           .WithTags(Tag)
                           .RequireAuthorization()
                           .WithOpenApi());
        }
        protected abstract void Configure(RouteGroupBuilder group);
    }
}
