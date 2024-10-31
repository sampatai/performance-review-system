namespace OfficeReview.Domain.Profile.Enums
{
    public class Team : Enumeration
    {
        public static Team Executive = new(1, nameof(Executive));
        public static Team HumanResources = new(2, nameof(HumanResources));
        public static Team Development = new(3, nameof(Development));
        public static Team IT = new(4, nameof(IT));
        public static Team Finance = new(5, nameof(Finance));
        public static Team AdministrativeSupport = new(6, nameof(AdministrativeSupport));
        public static Team Other = new(6, nameof(Other));

        public Team(int id, string name) : base(id, name)
        {
        }
    }
}
