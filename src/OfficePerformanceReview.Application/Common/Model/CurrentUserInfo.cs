using OfficeReview.Domain.Profile.Enums;


namespace OfficePerformanceReview.Application.Common.Model
{
   public record CurrentUserInfo(long UserId,string UserName,string FullName,Role Role);
    
}
