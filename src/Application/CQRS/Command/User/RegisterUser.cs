namespace OfficePerformanceReview.Application.CQRS.Command.User
{
    public static class RegisterUser
    {
        #region Command/Query
        public sealed record Command : RegisterUserModel, IRequest
        {
            public Command(RegisterUserModel original) : base(original)
            {
            }
        }
        #endregion

        #region Validation
        public sealed class Validator : ValidatorBase<Command>
        {
            public Validator(IReadonlyStaffRepository readonlyStaffRepository)
            {

                RuleFor(x => x.Email)
                .MustAsync(readonlyStaffRepository.CheckEmailExistsAsync)
                .WithMessage("Email already exists.");
            }

        }
        #endregion

    }
}
