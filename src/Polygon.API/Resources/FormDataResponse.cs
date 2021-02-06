using System;
using Newtonsoft.Json.Linq;

namespace Polygon.API.Resources
{
    /*public class FormDataResponse
    {
        public int Id { get; set; }
        public JObject JsonData { get; set; }
        public DateTimeOffset ImportedTimestamp { get; set; }
    }*/
    
    public record FormDataResponse(int Id, JObject JsonData, DateTimeOffset ImportedTimestamp);
}