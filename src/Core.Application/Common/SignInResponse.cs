namespace Core.Application.Common
{
    public class SignInResponse
    {
        public bool Succeeded { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool IsLockedOut { get; set; }

        public SignInResponse(bool succeeded, bool requiresTwoFactor, bool isLockedOut)
        {
            Succeeded = succeeded;
            RequiresTwoFactor = requiresTwoFactor;
            IsLockedOut = isLockedOut;
        }
        public static SignInResponse Failure()
        {
            return new SignInResponse(false, false, false);
        }
        public static SignInResponse Success()
        {
            return new SignInResponse(true, false, false);
        }
    }
}