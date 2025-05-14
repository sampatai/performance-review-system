

public record QuestionDTO(string Question, NameValue QuestionType, bool IsRequired);


public record GetQuestionDTO(string Question, NameValue QuestionType, bool IsRequired, Guid QuestionGuid)
    : QuestionDTO(Question, QuestionType, IsRequired);
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