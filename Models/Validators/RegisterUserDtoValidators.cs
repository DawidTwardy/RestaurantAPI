using FluentValidation;
using RestaurantAPI.Entity;

namespace RestaurantAPI.Models.Validators
{
    public class RegisterUserDtoValidators : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidators(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("Passwords do not match");
         
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Email is already in use");
                }
            });
        }
    }
}
