using OfficePerformanceReview.Domain.PerformanceReviewOverview.Entities;
using OfficePerformanceReview.Domain.PerformanceReviewOverview.ValueObjects;
using OfficeReview.Domain.Profile.Root;

public class PerformanceReviewTemplate : AuditableEntity, IAggregateRoot
{
    private readonly List<ReviewerAssignment> _reviewers = new();

    protected PerformanceReviewTemplate() { }

    public Guid ReviewGuid { get; private set; }
    public ReviewSettings Settings { get; private set; }
    public bool IsAutoAssign { get; private set; }
    public IReadOnlyCollection<ReviewerAssignment> Reviewers => _reviewers.AsReadOnly();

    public PerformanceReviewTemplate(ReviewSettings settings, bool isAutoAssign)
    {
        Guard.Against.Null(settings, nameof(settings));
        ReviewGuid = Guid.NewGuid();
        Settings = settings;
        IsAutoAssign = isAutoAssign;
    }

    public void AssignReviewers(IEnumerable<ReviewerAssignment> reviewers)
    {
        Guard.Against.Null(reviewers, nameof(reviewers));

        _reviewers.Clear();
        _reviewers.AddRange(reviewers);
    }
    public bool IsReviewPeriodActive()
    {
        var endDate = Settings.GetEndDate();
        return DateTime.UtcNow >= Settings.StartDate && DateTime.UtcNow <= endDate;
    }
    public bool CanAssignReview(Staff employee, List<PerformanceReviewTemplate> existingReviews)
    {
        if (!Settings.AllowDuplicateReviews)
        {
            return !existingReviews.Any(r => r.Reviewers.Any(m => m.StaffId == employee.Id));
        }
        return true;
    }
}
