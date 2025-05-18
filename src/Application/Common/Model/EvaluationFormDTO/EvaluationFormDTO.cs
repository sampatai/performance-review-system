

using OfficePerformanceReview.Domain.Questions.ValueObjects;

public record QuestionDTO(string Question, NameValue QuestionType, bool IsRequired, IEnumerable<OptionsDTO>? Options,
            int? RatingMin,
            int? RatingMax);

public record OptionsDTO(string Option);
public record GetQuestionDTO(string Question,
    NameValue QuestionType, bool IsRequired, Guid QuestionGuid,
    IEnumerable<OptionsDTO>? Options,
            int? RatingMin,
            int? RatingMax)
    : QuestionDTO(Question, QuestionType, IsRequired, Options, RatingMin, RatingMax);
public record EvaluationFormDTO(string Name, NameValue FormEvaluation);

public record CreateEvaluationFormDTO(
    string Name,
    NameValue FormEvaluation,
    IEnumerable<QuestionDTO> Questions
) : EvaluationFormDTO(Name, FormEvaluation);

public record GetEvaluationFormDTO(
    string Name,
    NameValue FormEvaluation,
    IEnumerable<GetQuestionDTO> Questions,
    Guid EvaluationFormGuid
) : EvaluationFormDTO(Name, FormEvaluation);

public record EvaluationFormListDTO : PageList<GetEvaluationFormDTO>;


public record UpdateEvaluationFormDTO(
    string Name,
    NameValue FormEvaluation,
    IEnumerable<GetQuestionDTO> Questions,
    Guid EvaluationFormGuid
) : GetEvaluationFormDTO(Name, FormEvaluation, Questions, EvaluationFormGuid);