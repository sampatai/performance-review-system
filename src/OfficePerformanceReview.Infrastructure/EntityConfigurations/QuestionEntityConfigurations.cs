

namespace OfficePerformanceReview.Infrastructure.EntityConfigurations
{
    internal class QuestionEntityConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question).Humanize().Pluralize());

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id)
                .UseHiLo("questionseq");
            builder.Property(e => e.QuestionGuid).IsRequired();
            builder.HasIndex(m => m.QuestionGuid)
             .IsUnique();

            builder.Property(e => e.QuestionText)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.OwnsOne(e => e.QuestionType);

            builder.Property(e => e.IsActive)
                   .IsRequired();
            builder.Property(e => e.IsDeleted)
                   .IsRequired();


        }
    }
}
