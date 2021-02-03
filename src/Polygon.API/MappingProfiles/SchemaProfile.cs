using AutoMapper;
using Polygon.API.Resources;
using Polygon.Domain.Entities;

namespace Polygon.API.MappingProfiles
{
    public class SchemaProfile : Profile
    {
        public SchemaProfile()
        {
            CreateMap<FormSchema, FormSchemaResponse>(MemberList.Destination);
            CreateMap<FormSchemaRequest, FormSchema>(MemberList.Source);
            CreateMap<FormSchema, FormSchemaRequest>(MemberList.Destination);
        }
    }
}