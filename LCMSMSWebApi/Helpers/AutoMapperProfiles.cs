using AutoMapper;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;

namespace LCMSMSWebApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Orphan, OrphanDto>().ReverseMap();

            CreateMap<OrphanUpdateDto, Orphan>();
        }
    }
}
