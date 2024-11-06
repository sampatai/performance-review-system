using AutoFixture;
using Bogus;
using FluentAssertions;
using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Domain.Profile.Root;
using OfficeReview.Shared.SeedWork;
using System.Net.Mail;


namespace OfficePerformanceReview.DomainTest.ProfileTest
{
    public static class TestHelpers
    {

        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static void AssertValidGuid(Guid guid)
        {
            guid.Should().NotBeEmpty("GUID should not be empty.");
        }

        public static void AssertNotNullOrEmpty(string value, string parameterName)
        {
            value.Should().NotBeNullOrEmpty($"{parameterName} should not be null or empty.");
        }

        public static (string Name, string Position,
            string Email, IEnumerable<Role> Roles, Team Team) GetStaffParam()
        {
            var faker = new Faker();
            var fixture = new Fixture();

            var name = faker.Name.FullName();
            var position = faker.Name.JobTitle();
            var email = faker.Internet.Email();
            var roles = Enumeration.GetRandomEnumValues<Role>();
            var team = Enumeration.GetRandomEnumValue<Team>();

            return (name, position, email, roles, team);
        }

        public static Staff GetStaff()
        {
            var _faker = new Faker();
            var _fixture = new Fixture();

            
            _fixture.Customize<Staff>(composer => composer
                .FromFactory(() => new Staff(
                    name: _faker.Name.FullName(),
                    position: _faker.Name.JobTitle(),
                    email: _faker.Internet.Email(),
                    roles: Enumeration.GetRandomEnumValues<Role>(),
                    team: Enumeration.GetRandomEnumValue<Team>()
                ))
            );

            // Create and return the Staff object
            return _fixture.Create<Staff>();
        }
    }
}
