using AutoMapper;
using Polygon.API.Resources;
using Polygon.Domain.Entities;

namespace Polygon.API.MappingProfiles
{
    public class FormDataProfiles : Profile
    {
        public FormDataProfiles()
        {
            CreateMap<FormData, FormDataResponse>(MemberList.Destination);
            CreateMap<FormDataRequest, FormData>(MemberList.Source);
            CreateMap<FormData, FormDataRequest>(MemberList.Destination);
        }
    }
}