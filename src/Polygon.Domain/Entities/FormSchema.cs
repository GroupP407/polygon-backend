using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Polygon.Domain.Entities
{
    public class FormSchema
    {
        public FormSchema(DateTimeOffset creationTimestamp)
        {
            CreationTimestamp = creationTimestamp;
        }

        public int Id { get; }
        public DateTimeOffset CreationTimestamp { get; }
        public JObject Schema { get; set; }
        public bool IsDeleted { get; set; }
    }
}