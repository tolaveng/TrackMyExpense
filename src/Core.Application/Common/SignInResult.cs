namespace Core.Application.Common
{
    public class SignInResult
    {
        public bool Succeeded { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool IsLockedOut { get; set; }

        public SignInResult(bool succeeded, bool requiresTwoFactor, bool isLockedOut)
        {
            Succeeded = succeeded;
            RequiresTwoFactor = requiresTwoFactor;
            IsLockedOut = isLockedOut;
        }
        public static SignInResult Failure()
        {
            return new SignInResult(false, false, false);
        }
        public static SignInResult Success()
        {
            return new SignInResult(true, false, false);
        }
    }
}