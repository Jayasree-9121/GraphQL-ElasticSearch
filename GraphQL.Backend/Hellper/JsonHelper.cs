using System.Text.RegularExpressions;

namespace GraphQLDemo.Hellper
{
    public static class JsonHelper
    {
        public static string CleanJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = input.Trim();

            if (input.StartsWith("```"))
            {
                input = Regex.Replace(input, @"^```[a-zA-Z]*\s*", "");
                input = Regex.Replace(input, @"\s*```$", "");
            }

            return input.Trim();
        }
    }
}
