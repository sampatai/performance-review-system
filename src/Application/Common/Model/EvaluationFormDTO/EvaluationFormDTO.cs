public record QuestionDTO(string Question, int QuestionType,
    bool IsRequired, bool AddRemarks, IEnumerable<OptionsDTO>? Options,
            int? RatingMin,
            int? RatingMax);

public record OptionsDTO(string Option);
public record SetQuestionDTO(string Question,
    int QuestionType, bool IsRequired, bool AddRemarks, Guid QuestionGuid,
    IEnumerable<OptionsDTO>? Options,
            int? RatingMin,
            int? RatingMax) : QuestionDTO(Question, QuestionType, IsRequired, AddRemarks, Options, RatingMin, RatingMax);
public record EvaluationFormDTO(string Name, int FormEvaluation);

public record CreateEvaluationFormDTO(
    string Name,
    int FormEvaluation,
    IEnumerable<QuestionDTO> Questions
) : EvaluationFormDTO(Name, FormEvaluation);
public record UpdateEvaluationFormDTO(
    string Name,
    int FormEvaluation,
    IEnumerable<SetQuestionDTO> Questions,
    Guid EvaluationFormGuid
) : SetEvaluationFormDTO(Name, FormEvaluation, Questions, EvaluationFormGuid);

public record SetEvaluationFormDTO(
    string Name,
    int FormEvaluation,
    IEnumerable<SetQuestionDTO> Questions,
    Guid EvaluationFormGuid
) : EvaluationFormDTO(Name, FormEvaluation);

public record GetQuestionDTO(string Question,
    NameValue QuestionType, bool IsRequired, bool AddRemarks, Guid QuestionGuid,
    IEnumerable<OptionsDTO>? Options,
            int? RatingMin,
            int? RatingMax);
public record GetEvaluationFormDTO(
    string Name,
    NameValue FormEvaluation,
    IEnumerable<GetQuestionDTO> Questions,
    Guid EvaluationFormGuid
) : QestionEvaluationFormDTO(Name, FormEvaluation);
public record QestionEvaluationFormDTO(string Name, NameValue FormEvaluation);
public record EvaluationFormListDTO : PageList<GetEvaluationFormDTO>;


