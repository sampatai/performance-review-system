using OfficePerformanceReview.Domain.Questions.Enum;
using OfficePerformanceReview.Domain.Questions.ValueObjects;

namespace OfficeReview.Domain.Questions.Entities
{
    public class Question : Entity
    {
        protected Question()
        {
        }

        public Guid QuestionGuid { get; private set; }
        public string QuestionText { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsActive { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public bool IsRequired { get; private set; } 
        public IEnumerable<QuestionOption> Options { get; private set; } = [];
        public int? RatingMin { get; private set; }
        public int? RatingMax { get; private set; }
        public bool AddRemarks { get; private set; }
        public Question(
            string question,
            QuestionType questionType,
            bool isRequired,
            bool addRemarks ,
            IEnumerable<QuestionOption>? options = null,
            int? ratingMin = null,
            int? ratingMax = null)
        {
            {
                Guard.Against.NullOrEmpty(question, nameof(question));
                Guard.Against.Null(questionType, nameof(questionType));
                IsActive = true;
                IsDeleted = false;
                AddRemarks = addRemarks;
                QuestionText = question;
                QuestionGuid = Guid.NewGuid();
                QuestionType = questionType;
                IsRequired = isRequired;

                if (questionType == QuestionType.MultipleChoice && questionType == QuestionType.SingleChoice)
                {
                    Guard.Against.NullOrEmpty(options, nameof(options));
                    Options = options!;
                }

                if (questionType == QuestionType.RatingScale)
                {
                    Guard.Against.Null(ratingMin, nameof(ratingMin));
                    Guard.Against.Null(ratingMax, nameof(ratingMax));
                    Guard.Against.OutOfRange(ratingMin.Value, nameof(ratingMin), 1, ratingMax.Value);
                    RatingMin = ratingMin;
                    RatingMax = ratingMax;
                }

            }
        }
        internal void SetQuestion(string question,
            QuestionType questionType,
            bool isRequired,
            bool addRemarks,
            IEnumerable<QuestionOption>? options = null,
            int? ratingMin = null,
            int? ratingMax = null)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));
            Guard.Against.Null(questionType, nameof(questionType));
            QuestionText = question;
            QuestionType = questionType;
            IsRequired = isRequired;
            AddRemarks = addRemarks;
            if (questionType == QuestionType.MultipleChoice || questionType == QuestionType.SingleChoice)
            {
                Guard.Against.NullOrEmpty(options, nameof(options));
                Options = options!;
            }

            if (questionType == QuestionType.RatingScale)
            {
                Guard.Against.Null(ratingMin, nameof(ratingMin));
                Guard.Against.Null(ratingMax, nameof(ratingMax));
                Guard.Against.OutOfRange(ratingMin.Value, nameof(ratingMin), 1, ratingMax.Value);
                RatingMin = ratingMin;
                RatingMax = ratingMax;
            }

        }

        internal void SetDeActivate()
        {
            IsActive = false;
        }
        internal void SetDelete()
        {
            IsDeleted = true;
        }

    }
}
