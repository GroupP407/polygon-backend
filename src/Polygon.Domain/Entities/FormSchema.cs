using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Polygon.Domain.Entities
{
    public class FormSchema
    {
        public static FormSchema Create()
        {
            return new()
            {
                CreationTimestamp = DateTimeOffset.UtcNow
            };
        }
        
        private FormSchema()
        {
        }

        public int Id { get; }
        public DateTimeOffset CreationTimestamp { get; set; }
        public JObject Schema { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<FormData> FormData { get; set; } = new List<FormData>();


        public void AddFormData(FormData formData)
        {
            FormData.Add(formData);
        }
    }
}