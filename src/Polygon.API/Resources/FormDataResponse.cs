using System;
using Newtonsoft.Json.Linq;

namespace Polygon.API.Resources
{
    public record FormDataResponse(int Id, JObject Schema, DateTimeOffset CreationTimestamp);
}