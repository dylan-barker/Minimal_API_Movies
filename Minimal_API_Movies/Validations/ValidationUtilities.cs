namespace Minimal_API_Movies.Validations
{
    public static class ValidationUtilities
    {
        public static string NotEmptyMessage = "{PropertyName} is required.";
        public static string MaxLengthMessage = "{PropertyName} cannot exceed 150 characters.";
        public static string FirstLetterUppercaseMessage = "{PropertyName} must start with an uppercase letter.";
        public static string GreaterThanDateMessage(DateTime value) => "{PropertyName} must be after " + value.ToString("yyyy-MM-dd");

        public static bool FirstLetterIsUppercase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;
            var firstLetter = value[0].ToString();
            return firstLetter == firstLetter.ToUpper();
        }


    }
}
