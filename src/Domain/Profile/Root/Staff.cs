using Microsoft.AspNetCore.Identity;
using OfficePerformanceReview.Domain.Profile.ValueObjects;
using OfficeReview.Domain.Profile.Enums;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : IdentityUser<long>, IAggregateRoot

{
    private Staff() { } 
    public Staff(Team team, string firstName, string lastName, string email) : base()
    {
        Team = team;
        FirstName = Guard.Against.NullOrEmpty(firstName);
        LastName = Guard.Against.NullOrEmpty(lastName);
        Email = Guard.Against.NullOrEmpty(email);
        UserName = email;
        StaffGuid=Guid.NewGuid();
    }
    private readonly List<RefreshToken> _refreshTokens = new();
    public IEnumerable<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
    public Team Team { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid StaffGuid { get; set; }

    public void SetRefereshToken(string token, DateTime expireDate)
    {
        _refreshTokens.Clear();
        _refreshTokens.Add(new RefreshToken(token, expireDate));
    }
    
}

