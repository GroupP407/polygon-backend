using System;
using Newtonsoft.Json.Linq;

namespace Polygon.API.Resources
{
    public record FormSchemaResponse(int Id, JObject Schema, DateTimeOffset CreationTimestamp);
}