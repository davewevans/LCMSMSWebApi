using AutoMapper;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace LCMSMSWebApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();

            CreateMap<Orphan, OrphanDto>().ReverseMap();

            CreateMap<OrphanEditDto, Orphan>();

            CreateMap<Sponsor, SponsorDto>().ReverseMap();
            CreateMap<SponsorUpdateDto, Sponsor>();

            CreateMap<Guardian, GuardianDto>().ReverseMap();
            CreateMap<GuardianUpdateDto, Guardian>();

            CreateMap<Academic, AcademicDto>().ReverseMap();
            CreateMap<AcademicUpdateDto, Academic>();

            CreateMap<Narration, NarrationDto>().ReverseMap();
            CreateMap<NarrationUpdateDto, Narration>();

            CreateMap<Picture, PictureDto>().ReverseMap();

            CreateMap<Orphan, OrphanDetailsDto>();

        }        
    }
}
