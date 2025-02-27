using System.Text.RegularExpressions;

namespace Mockify.API.Helper
{
    public static class CamelCaseExtention
    {
        public static string ToCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length == 1) return input;
            return char.ToLower(input[0]) + input.Substring(1);
        }
    }
}
