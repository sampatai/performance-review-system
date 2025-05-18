using Ardalis.GuardClauses;
using Bogus;
using OfficePerformanceReview.Domain.PerformanceReviewFormAssign.ValueObjects;
using OfficePerformanceReview.Domain.Questions.Enum;


namespace OfficePerformanceReview.DomainTest.PerformanceReviewOverviewTest
{
    internal class PerformanceReviewAssignTests : TestBase
    {
        private Faker _faker;
        private PerformanceReviewAssign _performanceReviewAssign;
        private ReviewCycle _reviewCycle;
        private long _managerId;
        private DateTime _deadline;

        public override void ExtendSetup()
        {
            base.ExtendSetup();

            _faker = new Faker();
            _deadline = _faker.Date.FutureOffset().DateTime;
            _managerId = _faker.Random.Long(100, 999);
            _reviewCycle = new ReviewCycle(_faker.Random.Int(1, 10),_faker.Lorem.Word());

            _performanceReviewAssign = new PerformanceReviewAssign(_deadline, _reviewCycle, _managerId);
        }

        [Test]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            Assert.That(_performanceReviewAssign.ReviewGuid, Is.Not.EqualTo(Guid.Empty));
            Assert.That(_performanceReviewAssign.DeadLine, Is.EqualTo(_deadline));
            Assert.That(_performanceReviewAssign.ManagerId, Is.EqualTo(_managerId));
            Assert.That(_performanceReviewAssign.ReviewCycle, Is.EqualTo(_reviewCycle));
            Assert.That(_performanceReviewAssign.Reviewers, Is.Empty);
        }

        [Test]
        public void AssignReviewers_ShouldAddReviewerAssignment()
        {
            // Arrange
            var staffId = _faker.Random.Long(1000, 2000);
            var reviewerId = _faker.Random.Long(2001, 3000);
            var templateId = _faker.Random.Long(5000, 6000);
            var deadline = _faker.Date.Soon();

            // Act
            _performanceReviewAssign.AssignReviewers(staffId, reviewerId, deadline, templateId);

            // Assert
            var reviewer = _performanceReviewAssign.Reviewers.First();
            Assert.Multiple(() =>
            {
                Assert.That(reviewer.StaffId, Is.EqualTo(staffId));
                Assert.That(reviewer.ReviewerId, Is.EqualTo(reviewerId));
                Assert.That(reviewer.DeadLine.Date, Is.EqualTo(deadline.Date));
                Assert.That(reviewer.PerformanceReviewTemplateId, Is.EqualTo(templateId));
            });
        }

        [Test]
        public void SetAssignTemplate_ShouldAddTemplates()
        {
            // Arrange
            var templates = new Faker<EvaluationFormTemplete>()
                .CustomInstantiator(f => new EvaluationFormTemplete(
                    f.Lorem.Sentence(), f.Random.Number()))
                .Generate(3);

            // Act
            _performanceReviewAssign.SetAssignTemplate(templates);

            // Assert
            Assert.That(_performanceReviewAssign.AssignTemplate.Count, Is.EqualTo(3));
        }

        [Test]
        public void SetAssignReviewers_ShouldUpdateAssignment()
        {
            // Arrange
            var staffId = _faker.Random.Long(1000, 2000);
            var reviewerId = _faker.Random.Long(2001, 3000);
            var templateId = _faker.Random.Long(4000, 5000);
            var deadline = _faker.Date.Soon();

            _performanceReviewAssign.AssignReviewers(staffId, reviewerId, deadline, templateId);
            var existing = _performanceReviewAssign.Reviewers.First();
            var newStaffId = _faker.Random.Long(3000, 4000);

            // Act
            _performanceReviewAssign.SetAssignReviewers(newStaffId, reviewerId, deadline.AddDays(2), templateId, existing.ReviewerAssignmentGuid);

            // Assert
            var updated = _performanceReviewAssign.Reviewers.First();
            Assert.That(updated.StaffId, Is.EqualTo(newStaffId));
            Assert.That(updated.DeadLine.Date, Is.EqualTo(deadline.AddDays(2).Date));
        }

        [Test]
        public void SetAssignReviewers_ShouldThrowNotFoundException_WhenGuidNotFound()
        {
            // Act & Assert
            Assert.Throws<NotFoundException>(() =>
                _performanceReviewAssign.SetAssignReviewers(1, 2, DateTime.Now, 3, Guid.NewGuid()));
        }

        [Test]
        public void SetAssignTemplate_ShouldThrowArgumentNullException_WhenNullPassed()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _performanceReviewAssign.SetAssignTemplate(null));
        }
    }
}
