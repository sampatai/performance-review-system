using OfficeReview.Domain.Questions.Entities;
using OfficeReview.Domain.Questions.Enum;
namespace OfficeReview.Domain.Questions.Root
{
    public class EvaluationForm : AuditableEntity, IAggregateRoot
    {
        private List<Question> _Questions = new();
        protected EvaluationForm()
        {

        }
        public Guid EvaluationFormGuid { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public FormEvaluation EvaluationType { get; private set; }
        public IReadOnlyList<Question> Questions => _Questions.AsReadOnly();

        public EvaluationForm(string name, FormEvaluation formEvaluation)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.Null(formEvaluation, nameof(formEvaluation));

            Name = name;
            EvaluationFormGuid = Guid.NewGuid();
            IsDeleted = false;
            IsActive = false;
            this.EvaluationType = formEvaluation;
        }
        public void SetEvaluationForm(string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Name = name;
        }
        public void SetDelete()
        {
            this.IsDeleted = true;
        }
        public void SetDeActivate()
        {
            this.IsActive = false;
        }
        public void AddQuestion(IEnumerable<Question> questions)
        {
            _Questions.AddRange(questions);
        }
        public void SetQuestion(IEnumerable<Question> questions)
        {
            foreach (Question question in questions
                .Where(x => x.QuestionGuid != Guid.Empty))
            {
                var single = _Questions
                      .Where(x => x.IsActive && x.QuestionGuid == question.QuestionGuid)
                      .SingleOrDefault()!;
                single.SetQuestion(question.QuestionText);
            }
        }
        public void SetDeActivateQuestion(IEnumerable<Question> questions)
        {
            foreach (Question question in questions
                .Where(x => x.QuestionGuid != Guid.Empty))
            {
                var single = _Questions
                      .Where(x => x.IsActive && x.QuestionGuid == question.QuestionGuid)
                      .SingleOrDefault()!;
                single.SetDeActivate();
            }
        }
        public void SetDeleteQuestion(IEnumerable<Question> questions)
        {
            foreach (Question question in questions
                .Where(x => x.QuestionGuid != Guid.Empty))
            {
                var single = _Questions
                      .Where(x => x.IsActive && x.QuestionGuid == question.QuestionGuid)
                      .SingleOrDefault()!;
                single.SetDelete();
            }
        }
    }
}
