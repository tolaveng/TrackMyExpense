namespace Core.Application.Common
{
    public class SignInResponse
    {
        public bool Succeeded { get; set; }
        public bool TwoFactorRequired { get; set; }
        public bool IsLockedOut { get; set; }
        public bool EmailConfirmationRequired { get; set; }

        public SignInResponse(bool succeeded, bool emailConfirmationRequired, bool twoFactorRequired, bool isLockedOut)
        {
            Succeeded = succeeded;
            EmailConfirmationRequired = emailConfirmationRequired;
            TwoFactorRequired = twoFactorRequired;
            IsLockedOut = isLockedOut;
        }
        public static SignInResponse Failure(bool emailConfirmationRequired, bool requiresTwoFactor, bool isLockedOut)
        {
            return new SignInResponse(false, emailConfirmationRequired, requiresTwoFactor, isLockedOut);
        }
        public static SignInResponse Success()
        {
            return new SignInResponse(true, false, false, false);
        }
    }
}