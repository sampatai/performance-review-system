namespace OfficePerformanceReview.Application.CQRS.Command.User
{
   public static class UserUpdate
    {
        #region Command/Query
        public sealed record Command : EditUserModel, IRequest
        {
            public Command(EditUserModel original) : base(original)
            {
            }
        }
        #endregion

        #region Validation
        public sealed class Validator : ValidatorBase<Command>
        {
            public Validator(IReadonlyStaffRepository readonlyStaffRepository)
            {
                RuleFor(x => x.StaffGuid)
                .MustAsync(readonlyStaffRepository.CheckUserExistsAsync)
                .WithMessage("User not exists.");
            }

        }
        #endregion
    }
}
