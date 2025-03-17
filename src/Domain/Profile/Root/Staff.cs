using Microsoft.AspNetCore.Identity;
using OfficePerformanceReview.Domain.PerformanceReviewOverview.Entities;
using OfficePerformanceReview.Domain.Profile.ValueObjects;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : IdentityUser<long>
{
    private readonly List<RefreshToken> _refreshTokens = new();
    
    public Guid StaffGuid { get; private set; }
    public IReadOnlyCollection<RefreshToken> Reviewers => _refreshTokens.AsReadOnly();

    public void SetRefereshToken(string token,DateTime expireDate)
    {
        _refreshTokens.Clear();
        _refreshTokens.Add(new RefreshToken(token, expireDate));
    }

}

