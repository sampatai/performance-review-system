using OfficeReview.Domain.Profile.ValueObjects;
using OfficeReview.Domain.Questions.Entities;

namespace OfficeReview.Domain.Questions.Root
{
    public class FeedbackForm : Entity, IAggregateRoot
    {
        private List<Question> _Questions = new();
        protected FeedbackForm()
        {

        }
        public Guid FormGuid { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<Question> Questions => _Questions.AsReadOnly();

    }
}
