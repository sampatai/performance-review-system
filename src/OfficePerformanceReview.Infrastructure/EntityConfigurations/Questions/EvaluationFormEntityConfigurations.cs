using OfficeReview.Domain.Questions.Root;

namespace OfficePerformanceReview.Infrastructure.EntityConfigurations.Questions
{
    internal class EvaluationFormEntityConfigurations : IEntityTypeConfiguration<EvaluationFormTemplate>
    {
        public void Configure(EntityTypeBuilder<EvaluationFormTemplate> builder)
        {
            builder.ToTable(nameof(EvaluationFormTemplate).Humanize().Pluralize());


            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id);
            builder.Property(m => m.EvaluationFormGuid)
               .IsRequired();
            builder.HasIndex(m => m.EvaluationFormGuid)
             .IsUnique();


            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.IsActive)
                .IsRequired();

            builder.OwnsOne(m => m.EvaluationType);

            builder.Property(m => m.IsDeleted)
                .IsRequired();
            builder.HasMany(x => x.Questions)
                .WithOne()
                .HasForeignKey("EvaluationFormTemplateId")
                .IsRequired();
        }
    }
}
