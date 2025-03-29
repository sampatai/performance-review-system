
namespace OfficePerformanceReview.Application.Common.Model
{
    public record LoginModel(string UserName, string Password);
    public record LoginResponse
    {
        public LoginResponse(string message,
            bool unauthorized, string firstName,
            string lastName, string jwt,
            DateTime expireDate)

        {
            FirstName = firstName;
            LastName = lastName;
            JWT = jwt;
            DateExpiresUtc = expireDate;
            Message = message;
            Unauthorized = unauthorized;
        }
        public LoginResponse(string message,
            bool unauthorized)
        {
            Message = message;
            Unauthorized = unauthorized;
            DateExpiresUtc = null;
        }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JWT { get; set; } = string.Empty;
        public DateTime? DateExpiresUtc { get; set; } = null;
        public string Message { get; set; }
        public bool Unauthorized { get; set; }
    }


    public record LoginUserModel(string FirstName, string LastName, string JWT);

    

}
