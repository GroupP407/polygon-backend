using System;
using Newtonsoft.Json.Linq;

namespace Polygon.Domain.Entities
{
    public class FormData
    {
        public FormData(DateTimeOffset creationTimestamp)
        {
            ImportedTimestamp = creationTimestamp;
        }

        public int Id { get; }
        public DateTimeOffset ImportedTimestamp { get; }
        public JObject Data { get; set; }

        public int FormSchemaId { get; set; }
        public FormSchema FormSchema { get; set; }
        
    }
}