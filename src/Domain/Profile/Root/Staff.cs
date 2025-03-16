using Microsoft.AspNetCore.Identity;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : IdentityUser<long>
{
    public Guid StaffGuid { get; private set; }
}

