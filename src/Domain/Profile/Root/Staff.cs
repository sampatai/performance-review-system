using Microsoft.AspNetCore.Identity;
using OfficePerformanceReview.Domain.Profile.ValueObjects;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : IdentityUser<long>

{
    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public void SetRefereshToken(string token,DateTime expireDate)
    {
        
        _refreshTokens.Add(new RefreshToken(token, expireDate));
    }

}

