namespace PresentAte.Common.ApplicationConstants
{
    public static class UserConstants
    {
        public const int UserFirstNameMinLength = 3;
        public const int UserFirstNameMaxLength = 25;

        public const int UserLastNameMinLength = 3;
        public const int UserLastNameMaxLength = 25;

        public const bool PasswordRequireDigit = true;
        public const bool PasswordRequireLowercase = true;
        public const bool PasswordRequireNonAlphanumeric = true;
        public const bool PasswordRequireUppercase = true;
        public const bool SignInRequireConfirmedAccount = false;

        public const int PasswordRequiredLength = 6;
        public const int PasswordRequiredUniqueChars = 1;
    }
}
