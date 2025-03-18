using OfficePerformanceReview.Domain.PerformanceReview.Root;

public class PerformanceReviewEntityConfiguration : IEntityTypeConfiguration<PerformanceReview>
{
    public void Configure(EntityTypeBuilder<PerformanceReview> builder)
    {
        builder.ToTable("PerformanceReviews");
        builder.Ignore(b => b.DomainEvents);

        builder.Property(o => o.Id);
        builder.Property(m => m.PerformanceReviewGuid).IsRequired();
        builder.HasIndex(m => m.PerformanceReviewGuid).IsUnique();

        builder.OwnsOne(e => e.AppraisedName, q =>
        {
            q.Property(pt => pt.Id);
            q.Property(pt => pt.Name);
        });

        builder.OwnsOne(e => e.CompletedBy, q =>
        {
            q.Property(pt => pt.Id);
            q.Property(pt => pt.Name);
        });

        builder.OwnsOne(e => e.EvaluationType, q =>
        {
            q.Property(pt => pt.Id);
            q.Property(pt => pt.Name);
        });

        builder.Property(p => p.ReviewDate);

        builder.OwnsOne(e => e.FeedbackStatus, q =>
        {
            q.Property(pt => pt.Id);
            q.Property(pt => pt.Name);
        });

        builder.OwnsMany(m => m.Evaluators, a =>
        {
            a.WithOwner().HasForeignKey("PerformanceReviewId");
            a.Property(o => o.Id).UseHiLo("evaluatorseseq");
            a.HasKey(o => o.Id);
            a.Ignore(b => b.DomainEvents);
            a.Property(st => st.PeerEvaluationGuid).IsRequired();
            a.HasIndex(st => st.PeerEvaluationGuid).IsUnique();
            a.Property(st => st.ReviewDate).IsRequired();
            a.Property(st => st.DeadLine).IsRequired();
            a.Property(st => st.IsActive).IsRequired();

            a.OwnsOne(e => e.EvaluationType, q =>
            {
                q.Property(pt => pt.Id);
                q.Property(pt => pt.Name);
            });

            a.OwnsOne(e => e.FeedbackStatus, q =>
            {
                q.Property(pt => pt.Id);
                q.Property(pt => pt.Name);
            });

            a.OwnsMany(st => st.Feedbacks, r =>
            {
                r.WithOwner().HasForeignKey("EvaluatorId");
                r.Property<long>("Id").UseHiLo("reservationseq", "dbo");
                r.HasKey("Id");
                r.Property(res => res.QuestionText).IsRequired().HasMaxLength(500);
                r.Property(res => res.Text).IsRequired().HasMaxLength(3000);

                r.OwnsOne(r1 => r1.QuestionType, q =>
                {
                    q.Property(pt => pt.Id);
                    q.Property(pt => pt.Name);
                });

                r.OwnsOne(r1 => r1.RatingScale, q =>
                {
                    q.Property(pt => pt.Id);
                    q.Property(pt => pt.Name);
                });

                r.ToTable("PeerEvaluation_Feedback");
            });
            a.ToTable("PeerEvaluation");
        });

        // Configure the single Feedback entity
        builder.OwnsOne(f => f.Feedbacks, fb =>
        {
            fb.ToTable("PerformanceReviewFeedback");
            fb.Ignore(b => b.DomainEvents);
            fb.Property(o => o.Id);
            fb.Property(m => m.FeedbackGuid).IsRequired();
            fb.HasIndex(m => m.FeedbackGuid).IsUnique();

            fb.OwnsMany(st => st.BehaviorMetrics, bm =>
            {
                bm.WithOwner().HasForeignKey("PerformanceReviewFeedbackId");
                bm.Property(b => b.Id).UseHiLo("behaviormetricssq", "dbo");
                bm.HasKey(b => b.Id);

                bm.OwnsOne(st => st.Metric, r =>
                {
                    r.Property(res => res.QuestionText).IsRequired().HasMaxLength(500);
                    r.Property(res => res.Text).IsRequired().HasMaxLength(3000);

                    r.OwnsOne(r1 => r1.QuestionType, q =>
                    {
                        q.Property(pt => pt.Id);
                        q.Property(pt => pt.Name);
                    });

                    r.OwnsOne(r1 => r1.RatingScale, q =>
                    {
                        q.Property(pt => pt.Id);
                        q.Property(pt => pt.Name);
                    });
                });

                bm.OwnsOne(r1 => r1.RevieweeRating, q =>
                {
                    q.Property(pt => pt.Id);
                    q.Property(pt => pt.Name);
                });
            });
        });
    }
}