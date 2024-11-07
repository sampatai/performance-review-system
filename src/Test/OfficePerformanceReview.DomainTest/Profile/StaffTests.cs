using AutoFixture;
using Bogus;
using FluentAssertions;
using OfficeReview.Domain.Profile.Enums;
namespace OfficePerformanceReview.DomainTest.ProfileTest
{
    public class StaffTests
    {
        private Fixture _fixture;
       private Staff _staff;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _staff= TestHelpers.GetStaff();
        }

        [Test]
        public void init_should_be_succeed()
        {
            // Arrange
            var staffFake = TestHelpers.GetStaffParam();

            // Act
            var staff = new Staff(staffFake.Name, staffFake.Position, staffFake.Email, staffFake.Roles, staffFake.Team);

            // Assert
            TestHelpers.AssertValidGuid(staff.StaffGuid);
            TestHelpers.AssertNotNullOrEmpty(staff.Name, nameof(staff.Name));
            TestHelpers.AssertNotNullOrEmpty(staff.PositionTitle, nameof(staff.PositionTitle));
            TestHelpers.IsValidEmail(staff.Email).Should().BeTrue();
            staff.Roles.Should().Equal(staffFake.Roles);
            staff.Team.Should().Be(staffFake.Team);
            staff.IsActive.Should().BeTrue();
            staff.IsDeleted.Should().BeFalse();
        }

        [Test]
        public void init_should_throw_argument_exception_when_email_isinvalid()
        {
            // Arrange
            var staffFake = TestHelpers.GetStaffParam();

            staffFake.Email = "invalid_email";

            // Act
            Action act = () => new Staff(staffFake.Name, staffFake.Position, staffFake.Email, staffFake.Roles, staffFake.Team);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("*Invalid email*");
        }

        [Test]
        public void setstaff_should_be_succeed()
        {
            // Arrange
           

            var newStaff = TestHelpers.GetStaffParam();
            // Act
            _staff.SetStaff(newStaff.Name, newStaff.Position, newStaff.Roles, newStaff.Team);

            // Assert
            TestHelpers.AssertNotNullOrEmpty(_staff.Name, nameof(_staff.Name));
            TestHelpers.AssertNotNullOrEmpty(_staff.PositionTitle, nameof(_staff.PositionTitle));
            _staff.Roles.Should().Equal(newStaff.Roles);
            _staff.Team.Should().Be(newStaff.Team);
        }

        [Test]
        public void setdelete_should_be_succeed()
        {
            // Arrange
           
            // Act
            _staff.SetDelete();

            // Assert
            _staff.IsDeleted.Should().BeTrue();
        }

        [Test]
        public void SetDeActivate_ShouldSetIsActiveToFalse()
        {
            // Arrange
           

            // Act
            _staff.SetDeActivate();

            // Assert
            _staff.IsActive.Should().BeFalse();
        }

        [Test]
        public void ChangeTeam_ShouldUpdateTeamProperty()
        {
            // Arrange
            
            var newTeam = _fixture.Create<Team>();

            // Act
            _staff.ChangeTeam(newTeam);

            // Assert
            _staff.Team.Should().Be(newTeam);
        }

        [Test]
        public void SetChangeRole_ShouldAddNewRoleToRolesList()
        {
            // Arrange
            
            var newRole = _fixture.Create<Role>();

            // Act
            _staff.SetChangeRole(newRole);

            // Assert
            _staff.Roles.Should().Contain(newRole);
        }

       
    }
}



