﻿namespace OfficePerformanceReview.Domain.Profile.ValueObjects
{
    public class RefreshToken : ValueObject
    {
        protected RefreshToken()
        {
            
        }
        public RefreshToken(string token,DateTime dateExpiresUtc)
        {
           Token= Guard.Against.NullOrEmpty(token);
            dateExpiresUtc = Guard.Against.NullOrOutOfSQLDateRange(dateExpiresUtc);
        }
        public string Token { get; private set; }
        public DateTime DateCreatedUtc { get; private set; } = DateTime.UtcNow;
        public DateTime DateExpiresUtc { get; private set; }
        public bool IsExpired => DateTime.UtcNow >= DateExpiresUtc;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return DateCreatedUtc;
            yield return DateExpiresUtc;
            yield return IsExpired;
        }
    }
}
