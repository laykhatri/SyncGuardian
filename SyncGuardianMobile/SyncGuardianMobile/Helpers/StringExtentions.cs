using System.Text.Json;

namespace SyncGuardianMobile.Helpers
{
    public static class StringExtentions
    {
        public static bool IsJson(this string str)
        {
            if (str == null)
            {
                return false;
            }

            try
            {
                JsonDocument.Parse(str);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

#nullable enable
        public static T? ToClass<T>(this string str) where T : class
        {
            if (str.IsJson())
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(str) as T;
                }
                catch (JsonException)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
