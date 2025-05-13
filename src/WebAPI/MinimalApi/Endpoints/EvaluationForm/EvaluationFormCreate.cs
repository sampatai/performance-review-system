using OfficePerformanceReview.Application.Common.Model.EvaluationForm;
using OfficePerformanceReview.Application.CQRS.Command.EvaluationForm;

namespace OfficePerformanceReview.WebAPI.MinimalApi.Endpoints.EvaluationForm
{
    public class EvaluationFormCreate : GroupedEndPoint
    {
        protected override string Group => GroupConstraints._EVALUATION_FROM_GROUP;

        protected override void Configure(RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateEvaluationFormDTO dto, ISender sender) =>
            {
                await sender.Send(new CreateEvaluationForm.Command(dto), CancellationToken.None);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
         .WithName("CreateEvaluationForm")
         .Produces(StatusCodes.Status201Created)
         .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
