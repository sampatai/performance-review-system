using Microsoft.EntityFrameworkCore;
using OfficePerformanceReview.Domain.Questions.Entities;
using OfficePerformanceReview.Domain.Questions.Enum;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
     
        builder.ToTable(nameof(Question).Humanize().Pluralize());
  
        builder.HasKey(o => o.QuestionGuid);

        
        builder.Ignore(b => b.DomainEvents);
      
        builder.Property(e => e.QuestionGuid)
            .IsRequired();
        builder.HasIndex(m => m.QuestionGuid)
            .IsUnique();

        builder.Property(e => e.QuestionText)
            .IsRequired()
            .HasMaxLength(500);

       
        builder.OwnsOne(e => e.QuestionType, q =>
        {
            q.Property(pt => pt.Id).HasColumnName("QuestionType_Id");
            q.Property(pt => pt.Name).HasColumnName("QuestionType_Name");
        });

      
        builder.Property(e => e.IsActive)
            .IsRequired();
        builder.Property(e => e.IsDeleted)
            .IsRequired();

        builder.Property(e => e.Options)
            .HasColumnType("Options")  
            .HasConversion(
                v => string.Join(",", v),
                v => v.Split(",", StringSplitOptions.None).ToList())
            .IsRequired(false); // Options are not required unless the question type is MultipleChoice or SingleChoice

        builder.Property(e => e.IsRequired)
            .IsRequired();
    }
}
