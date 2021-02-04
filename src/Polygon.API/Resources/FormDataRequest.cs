using Newtonsoft.Json.Linq;

namespace Polygon.API.Resources
{
    public record FormDataRequest(JObject Schema);
}