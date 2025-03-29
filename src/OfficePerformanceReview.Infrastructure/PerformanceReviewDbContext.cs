using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OfficePerformanceReview.Infrastructure.EntityConfigurations;
using OfficeReview.Domain.Events.Root;
using OfficeReview.Domain.Questions.Root;

namespace OfficePerformanceReview.Infrastructure
{

    public class PerformanceReviewDbContext : IdentityDbContext<Staff, IdentityRole<long>, long>, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public PerformanceReviewDbContext(DbContextOptions<PerformanceReviewDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
        }

        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<EvaluationFormTemplate> EvaluationFormTemplates { get; set; } = null!;
        public virtual DbSet<EventLogs> EventLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey }); // Composite primary key
            });

            modelBuilder.Entity<Staff>().OwnsMany(st => st.RefreshTokens, r =>
            {
                r.WithOwner().HasForeignKey("Id");
                r.Property<long>("Id").UseHiLo("reservationseq", "dbo");
                r.HasKey("Id");
                r.Property(res => res.Token).IsRequired().HasMaxLength(500);
                r.Property(res => res.DateCreatedUtc).IsRequired();
                r.Property(res => res.DateExpiresUtc).IsRequired();

                r.ToTable("User_RefreshTokens");
            });

            modelBuilder.Entity<Staff>(s =>
            {
                s.OwnsOne(e => e.Team, q =>
                 {
                     q.Property(pt => pt.Id);
                     q.Property(pt => pt.Name);
                 });
                s.Property(m => m.StaffGuid).IsRequired();
                s.HasIndex(m => m.StaffGuid).IsUnique();
            });

            modelBuilder.Entity<IdentityUserRole<long>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId }); // Composite primary key
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name }); // Composite primary key
            });

            modelBuilder.ApplyConfiguration(new EvaluationFormEntityConfigurations())
                         .ApplyConfiguration(new QuestionEntityConfigurations())
                         .ApplyConfiguration(new EventLogEntityConfiguration())
            ;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (AuditableEntity)entityEntry.Entity;
                var currentUser = "System"; // Replace with actual user context

                if (entityEntry.State == EntityState.Added)
                {
                    entity.SetCreationAudits(currentUser);
                }
                else
                {
                    entity.SetModificationAudits(currentUser);
                }
            }

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }


    }
}
