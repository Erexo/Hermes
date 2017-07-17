namespace Hermes.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool Empty(this string s)
            => string.IsNullOrWhiteSpace(s);
    }
}
