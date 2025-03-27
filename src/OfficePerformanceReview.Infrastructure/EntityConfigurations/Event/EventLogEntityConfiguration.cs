using OfficeReview.Domain.Events.Root;

namespace OfficePerformanceReview.Infrastructure.EntityConfigurations
{
    internal class EventLogEntityConfiguration : IEntityTypeConfiguration<EventLogs>
    {
        public void Configure(EntityTypeBuilder<EventLogs> builder)
        {
            builder.ToTable("EventLogs");

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(o => o.Id)
                .UseHiLo("eventseq");

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.OwnsOne(e => e.EventType);
                 

            builder.Property(e => e.CreationTime)
                   .IsRequired();
        }
    }
}

