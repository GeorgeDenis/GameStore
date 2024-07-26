namespace Application.Common
{
    public static class Constants
    {
        public const string PropertyNameRequired = "{PropertyName} is required.";
        public const string PropertyNameMaxLength = "{PropertyName} must not exceed {MaxLength} characters.";

        public const string EmailInvalid = "Email is not a valid email address.";

        public const string PasswordLength = "Password must be at least 8 characters long.";
        public const string PasswordUppercase = "Password must contain at least one uppercase letter.";
        public const string PasswordSpecial = "Password must contain at least one special character.";


        public const string RatingValue = "Rating must be between 1 and 5.";
        public const string RatingCommentLength = "Comment cannot exceed 500 characters.";

        public const string DateValidation = "{PropertyName} must be in the past or present.";

        public const string GamePrice = "{PropertyName} must be greater than zero.";
        public const string GuidValid = "{PropertyName} must be a valid GUID.";

        public const string ImageExtension = "{PropertyName} must be a valid image file with one of the following extensions: .jpg, .jpeg, .png, .gif, .jfif";


        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public const string PasswordUppercaseCharacterRegex = @"[A-Z]";
        public const string PasswordSpecialCharacterRegex = @"[!@#$%^&*(),.?\:{ }|<>]";
    }
}
