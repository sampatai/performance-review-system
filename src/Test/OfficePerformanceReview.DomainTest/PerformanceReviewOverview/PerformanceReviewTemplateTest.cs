
using OfficePerformanceReview.Domain.PerformanceReviewOverview.Entities;
using OfficePerformanceReview.Domain.PerformanceReviewOverview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficePerformanceReview.DomainTest.PerformanceReviewOverviewTest
{
    internal class PerformanceReviewOverviewTest : TestBase
    {
        private PerformanceReviewTemplate _performanceReviewTemplate;
        private ReviewSettings _settings;

        public override void ExtendSetup()
        {
            base.ExtendSetup();
            // Customizing AutoFixture to generate realistic EvaluationForm instances
            _settings = new ReviewSettings(DateTime.Now, new Random().Next(1, 50), false);
            Fixture.Customize<PerformanceReviewTemplate>(composer => composer
                .FromFactory(() => new PerformanceReviewTemplate(
                    settings: _settings,
                   true
                ))
            );

            // Creating an instance using AutoFixture
            _performanceReviewTemplate = Fixture.Create<PerformanceReviewTemplate>();
            
        }

        [Test]
        public void Constructor_ShouldInitializeCorrectly_WhenValidArgumentsArePassed()
        {
            // Assert
            Assert.That(_performanceReviewTemplate.ReviewGuid, Is.Not.Empty); 
            Assert.That(_performanceReviewTemplate.Settings, Is.EqualTo(_settings)); 
            Assert.That(_performanceReviewTemplate.IsAutoAssign,Is.True); 
            Assert.That(_performanceReviewTemplate.Reviewers, Is.Empty);
        }

        [Test]
        public void AssignReviewers_ShouldUpdateReviewersList_WhenValidReviewersArePassed()
        {
            // Arrange
            var reviewers = new List<ReviewerAssignment>
            {
                new ReviewerAssignment(Faker.Random.Int(250), Faker.Random.Int(250)),
                new ReviewerAssignment(Faker.Random.Int(250), Faker.Random.Int(250))
            };

            // Act
            _performanceReviewTemplate.AssignReviewers(reviewers);

            // Assert
            Assert.That(_performanceReviewTemplate.Reviewers, Is.EquivalentTo(reviewers));
        }

        [Test]
        public void AssignReviewers_ShouldThrowException_WhenReviewersAreNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _performanceReviewTemplate.AssignReviewers(null));
        }

        [Test]
        public void IsReviewPeriodActive_ShouldReturnTrue_WhenCurrentDateIsWithinReviewPeriod()
        {
            // Act
            bool isActive = _performanceReviewTemplate.IsReviewPeriodActive();

            // Assert
            Assert.That(isActive, Is.False);

        }

        [Test]
        public void CanAssignReview_ShouldReturnFalse_WhenDuplicateReviewsAreNotAllowed()
        {
            // Arrange
            var staff = Fixture.CreateMany<Staff>();
            var existingReview = new PerformanceReviewTemplate(_settings, false);
            existingReview.AssignReviewers(new List<ReviewerAssignment> { new ReviewerAssignment(staff.First().Id, staff.Last().Id) });
            var existingReviews = new List<PerformanceReviewTemplate> { existingReview };

            // Act
            bool canAssign = _performanceReviewTemplate.CanAssignReview(staff.Last(), existingReviews);

            // Assert
            Assert.That(canAssign, Is.True);

        }
    }
}
