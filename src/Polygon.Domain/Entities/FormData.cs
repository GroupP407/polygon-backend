using System;
using Newtonsoft.Json.Linq;

namespace Polygon.Domain.Entities
{
    public class FormData
    {
        public static FormData Create()
        {
            return new()
            {
                ImportedTimestamp = DateTimeOffset.UtcNow
            };
        }

        private FormData()
        {
        }

        public int Id { get; }
        public DateTimeOffset ImportedTimestamp { get; set; }
        public JObject JsonData { get; set; }

        public int FormSchemaId { get; set; }
        public FormSchema FormSchema { get; set; }
        
    }
}