using Microsoft.Extensions.Primitives;

using MusicApi.Authentication;

namespace MusicApi.Extensions
{
    public static class IHeaderDictionaryExtensions
    {
        public static string GetApiKey(this IHeaderDictionary headers)
        {
            if (!headers.TryGetValue(ApiKeyDefaults.Header, out StringValues value) || 
                value[0] is not string apiKey)
            {
                throw new ArgumentException("Invalid header", nameof(headers));
            }

            return apiKey;
        }
    }
}
