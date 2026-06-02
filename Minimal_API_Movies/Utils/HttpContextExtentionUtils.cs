namespace Minimal_API_Movies.Utils
{
    public static class HttpContextExtentionUtils
    {
        public static T ExtractValueOrDefault<T>(this HttpContext context, string field, T defaultValue)
            where T : IParsable<T>
        {
            var value = context.Request.Query[field];
            if (!value.Any())
            {
                return defaultValue;
            }
            return T.Parse(value!, null);
        }>
    }
}
