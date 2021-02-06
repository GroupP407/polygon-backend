using System;
using System.Collections;
using System.Collections.Generic;
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

        private FormSchema()
        {
        }

        public int Id { get; }
        public DateTimeOffset CreationTimestamp { get; }
        public JObject Schema { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<FormData> FormDatas { get; set; } = new List<FormData>();



    }
}