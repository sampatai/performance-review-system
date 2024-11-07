using AutoFixture;
using Bogus;


namespace OfficePerformanceReview.DomainTest.Common
{
    [TestFixture]
    public abstract class TestBase
    {
        protected Fixture Fixture;
        protected Faker Faker;

        [SetUp]
        public void Setup()
        {
            Fixture = new Fixture();
            Faker = new Faker();
            ExtendSetup();
        }
        public virtual void ExtendSetup() { }
    }
}
