using OfficePerformanceReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Enum;

namespace OfficePerformanceReview.Application.Common.Model.EvaluationForm
{
    public record EvaluationFormDTO(string Name, FormEvaluation FormEvaluation, IEnumerable<QuestionDTO> Questions);

    public record QuestionDTO(string Question, QuestionType QuestionType, bool IsRequired);

    public record CreateEvaluationFormDTO : EvaluationFormDTO
    {
        public CreateEvaluationFormDTO(string name, FormEvaluation formEvaluation,
            IEnumerable<QuestionDTO> questions)
            : base(name, formEvaluation, questions)
        {
        }
    }

    public record UpdateEvaluationFormDTO : EvaluationFormDTO
    {
        public UpdateEvaluationFormDTO(string name, FormEvaluation formEvaluation,
            IEnumerable<QuestionDTO> questions,
            Guid evaluationFormGuid)
            : base(name, formEvaluation, questions)
        {

        }
        public Guid EvaluationFormGuid { get; set; }
    }
}
