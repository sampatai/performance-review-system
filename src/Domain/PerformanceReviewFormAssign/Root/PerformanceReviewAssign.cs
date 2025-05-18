using OfficePerformanceReview.Domain.PerformanceReviewFormAssign.Entities;
using OfficePerformanceReview.Domain.PerformanceReviewFormAssign.ValueObjects;


public class PerformanceReviewAssign : AuditableEntity, IAggregateRoot
{
    private readonly List<ReviewerAssignment> _reviewers = new();
    private readonly List<EvaluationFormTemplete> _evaluationFormTemplete = new();

    protected PerformanceReviewAssign() { }
    public DateTime DeadLine { get; private set; }
    public Guid ReviewGuid { get; private set; }
    public ReviewCycle ReviewCycle { get; private set; }
    public IReadOnlyCollection<EvaluationFormTemplete> AssignTemplate => _evaluationFormTemplete.AsReadOnly();
    public long ManagerId { get; private set; }
    public IReadOnlyCollection<ReviewerAssignment> Reviewers => _reviewers.AsReadOnly();

    public PerformanceReviewAssign(DateTime deadLine, ReviewCycle reviewCycle, long managerId)
    {
        Guard.Against.OutOfRange(deadLine, nameof(deadLine), DateTime.MinValue, DateTime.MaxValue);

        Guard.Against.Null(reviewCycle, nameof(reviewCycle));
        DeadLine = deadLine;
        ReviewGuid = Guid.NewGuid();
        ReviewCycle = reviewCycle;
        ManagerId = managerId;
    }

    public void AssignReviewers(long staffId,
            long reviewerId,
            DateTime deadLine,
            long performanceReviewTemplateId)
    {
        _reviewers.Add(new(staffId, reviewerId, deadLine, performanceReviewTemplateId));
    }
    public void SetAssignTemplate(IEnumerable<EvaluationFormTemplete> evaluationFormTemplete)
    {
        Guard.Against.Null(evaluationFormTemplete, nameof(evaluationFormTemplete));
        _evaluationFormTemplete.Clear();
        _evaluationFormTemplete.AddRange(evaluationFormTemplete);
    }
    public void SetAssignReviewers(long staffId,
            long reviewerId,
            DateTime deadLine,
            long performanceReviewTemplateId,
            Guid reviewerAssignmentGuid
        )
    {
        var assignReview = _reviewers.SingleOrDefault(x => x.ReviewerAssignmentGuid == reviewerAssignmentGuid);
        if (assignReview is null)
            throw new NotFoundException("assignReview", "Reviewer Assignment not found");
        assignReview.SetReviewerAssignment(staffId, reviewerId, deadLine, performanceReviewTemplateId);
    }

}

