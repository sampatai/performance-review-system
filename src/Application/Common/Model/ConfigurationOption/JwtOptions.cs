namespace OfficePerformanceReview.Application.Common.Options
{
    /// <summary>
    /// Represents the JWT configuration section for strongly-typed binding.
    /// </summary>
    public class JwtOptions
    {
        public int ExpiresInMinutes { get; set; }
        public int RefreshTokenExpiresInDays { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string ClientUrl { get; set; } = string.Empty;
    }
}
