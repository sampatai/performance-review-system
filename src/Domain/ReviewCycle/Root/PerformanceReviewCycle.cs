using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.ReviewCycle.Enums;

namespace OfficePerformanceReview.Domain.ReviewCycle.Root
{
    public class PerformanceReviewCycle : AuditableEntity, IAggregateRoot
    {
        public Guid PerformanceReviewCycleId { get; private set; }
        public string Name { get; private set; }
        public DateRange DateRange { get; private set; }
        public ReviewCycleFrequency Frequency { get; private set; }
        public bool AutoCreateFormNextCycle { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public ReviewCycleStatus Status { get; private set; }
        

        protected PerformanceReviewCycle()
        {

        }
        public PerformanceReviewCycle(string name, 
            DateRange dateRange, ReviewCycleFrequency frequency,
            bool autoCreateFormNextCycle, ReviewCycleStatus status)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.Null(dateRange, nameof(dateRange));
            Guard.Against.Null(frequency, nameof(frequency));
            Guard.Against.Null(status, nameof(status));

            Name = name;
            DateRange = dateRange;
            Frequency = frequency;
            AutoCreateFormNextCycle = autoCreateFormNextCycle;
            PerformanceReviewCycleId = Guid.NewGuid();
            IsActive = true;
            IsDeleted = false;
            Status = status;
        }
        public void SetPerformanceReviewCycle(string name, 
            DateRange dateRange, ReviewCycleFrequency frequency, 
            bool autoCreateFormNextCycle,
            ReviewCycleStatus cycleStatus)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.Null(dateRange, nameof(dateRange));
            Guard.Against.Null(frequency, nameof(frequency));
            Name = name;
            DateRange = dateRange;
            Frequency = frequency;
            AutoCreateFormNextCycle = autoCreateFormNextCycle;
            Status = cycleStatus;
        }
        public void SetDelete()
        {
            IsDeleted = true;
        }
        public void SetDeActivate()
        {
            IsActive = false;
        }
    }
}
