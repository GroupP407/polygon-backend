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

        private FormData()
        {
        }

        public int Id { get; }
        public DateTimeOffset ImportedTimestamp { get; }
        public JObject JsonData { get; set; }

        public int FormSchemaId { get; set; }
        public FormSchema FormSchema { get; set; }
        
    }
}