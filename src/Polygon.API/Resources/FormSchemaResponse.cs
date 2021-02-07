using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Polygon.API.Resources
{
    public class FormSchemaResponse
    {
        public int Id { get; set; }
        public JObject Schema { get; set; }
        public DateTimeOffset CreationTimestamp { get; set; }
    }
}