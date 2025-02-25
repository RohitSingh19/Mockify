namespace Mockify.API.Helper
{
    public static class ToDataTypeExtention
    {
        public static string ToDataType(this string input)
        {
            
            if (string.IsNullOrEmpty(input)) return input;
            if (input.ToLower() == "int32") return "Integer";            
            if (input.ToLower() == "datetime") return "Date Time";
            if (input.ToLower() == "string[]") return "String Array/Collection";
            return input;
        }
    }
}
