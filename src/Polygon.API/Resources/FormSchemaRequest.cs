using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Polygon.API.Resources
{
    public record FormSchemaRequest(JObject Schema);
}