
namespace OfficePerformanceReview.WebAPI.MinimalApi.Abstractions
{
    public abstract class GroupedEndPoint : IEndpoint
    {
        protected abstract string Group { get; }
        protected abstract string Tag { get; }
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            RouteGroupBuilder group;

            // If using versioning, the RouteGroupBuilder will be passed in
            if (app is RouteGroupBuilder versionedGroup)
            {
                group = versionedGroup.MapGroup($"/{Group}")
                                      .WithTags(Tag)
                                      .RequireAuthorization()
                                      .WithOpenApi();
            }
            else
            {
                group = app.MapGroup($"/api/{Group}")
                           .WithTags(Tag)
                           .RequireAuthorization()
                           .WithOpenApi();
            }

            Configure(group);

        }
        protected abstract void Configure(RouteGroupBuilder group);
    }
}
