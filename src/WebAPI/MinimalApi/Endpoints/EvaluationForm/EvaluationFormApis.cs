using OfficePerformanceReview.Application.CQRS.Command.EvaluationForm;
using OfficePerformanceReview.Application.CQRS.Command.SetEvaluationForm;
using OfficePerformanceReview.Application.CQRS.Query.GetEvaluationForm;

namespace OfficePerformanceReview.WebAPI.MinimalApi.Endpoints.EvaluationForm
{
    public class EvaluationFormApis : GroupedEndPoint
    {
        protected override string Group => GroupConstraints._EVALUATION_FROM_GROUP;
        protected override string Tag => GroupConstraints._EVALUATION_TO_TAG;

        protected override void Configure(RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateEvaluationFormDTO dto, ISender sender, CancellationToken cancellationToken) =>
            {
                await sender.Send(new CreateEvaluationForm.Command(dto), cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
             .WithName("CreateEvaluationForm")
             .WithSummary("Create Evaluation Form")
             .Produces(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status500InternalServerError);


            group.MapPost("/list", async (FilterBase filter, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new ListEvaluationFormQuestion.Query(filter);

                var result = await sender.Send(command, cancellationToken);
                return Results.Ok(result);
            })
             .WithName("List of evaluation forms")
             .WithSummary("Fetch list of forms including questions")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/{evaluationGuid:guid}", async (Guid evaluationGuid,
                ISender sender, CancellationToken cancellationToken) =>
            {
                if (evaluationGuid == Guid.Empty)
                {
                    return Results.BadRequest("Mismatched GUID in route and payload.");
                }
                await sender.Send(new GetEvaluationForm.Query(evaluationGuid), cancellationToken);
                return Results.Ok();
            }).WithName("GetEvaluationForm")
            .WithSummary("fetch form template data based on guid")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{evaluationGuid:guid}", async (Guid evaluationGuid, UpdateEvaluationFormDTO dto,
                                                          ISender sender, CancellationToken cancellationToken) =>
            {
                if (evaluationGuid == Guid.Empty)
                {
                    return Results.BadRequest("Mismatched GUID in route and payload.");
                }

                await sender.Send(new UpdateEvaluationForm.Command(dto), cancellationToken);
                return Results.Ok();
            })
            .WithName("UpdateEvaluationForm")
            .WithSummary("Update an existing evaluation form")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        }
    }
}
