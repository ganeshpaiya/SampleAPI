using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;

namespace SampleAPI.Cash
{
    public class CashUtilities
    {
        internal static string GenerateKeyCashKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(c => c.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
