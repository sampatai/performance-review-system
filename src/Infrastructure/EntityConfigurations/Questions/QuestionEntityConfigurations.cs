namespace OfficePerformanceReview.Infrastructure.EntityConfigurations;
internal class QuestionEntityConfigurations : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
     
        builder.ToTable(nameof(Question).Pluralize());
  
        builder.HasKey(o => o.Id);

        
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

        builder.Property(e => e.IsRequired)
            .IsRequired();
        builder.Property(e => e.AddRemarks)
            .IsRequired();
        builder.OwnsMany(q => q.Options, optionsBuilder =>
        {
            optionsBuilder.ToJson(); 
        });
        builder.Property(e => e.RatingMin);
        builder.Property(e => e.RatingMax);
    }
}
