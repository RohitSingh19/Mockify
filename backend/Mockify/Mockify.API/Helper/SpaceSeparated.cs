using System.Text.RegularExpressions;

namespace Mockify.API.Helper
{
    public static class SpaceSeparated
    {
        public static string ToSpaceSeparated(this string input)
        {
            if(string.IsNullOrEmpty(input)) return input;
            return Regex.Replace(input, @"([a-z])([A-Z])", "$1 $2");
        }
    }
}
