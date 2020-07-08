using FluentValidation;

namespace CMMS.Application.Configuration.Validation
{
    public static class FluentValidationExtentions
    {
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("(?<!\\w)(\\(?(\\+|00)?48\\)?)?[ -]?\\d{3}[ -]?\\d{3}[ -]?\\d{3}(?!\\w)")
                .WithMessage("Phone number format is invalid");
        }

        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$")
                .WithMessage("Password must have at least one uppercase and lowercase character, one digit, one special character and have minimum 8 characters");
        }
    }
}
