using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Polygon.Infrastructure
{
    public static class ValueConversionExtensions
    {
        public static PropertyBuilder<JObject> HasJObjectConversion(this PropertyBuilder<JObject> propertyBuilder)
        {           
            var jTokenEqualityComparer = new JTokenEqualityComparer();
            
            var converter = new ValueConverter<JObject, string>(
                v => v.ToString(Formatting.None),
                v => JObject.Parse(v));

            var comparer = new ValueComparer<JObject>(
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => jTokenEqualityComparer.GetHashCode(v),
                v => (JObject)v.DeepClone());

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);            

            return propertyBuilder;
        } 
    }
}