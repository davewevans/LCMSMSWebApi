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
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

            CreateMap<Orphan, OrphanDto>().ReverseMap();

            CreateMap<OrphanEditDTO, Orphan>();

            CreateMap<Sponsor, SponsorDto>().ReverseMap();
            CreateMap<SponsorUpdateDTO, Sponsor>();

            CreateMap<Guardian, GuardianDto>().ReverseMap();
            CreateMap<GuardianUpdateDTO, Guardian>();

            CreateMap<Academic, AcademicDto>().ReverseMap();
            CreateMap<AcademicUpdateDTO, Academic>();

            CreateMap<Narration, NarrationDto>().ReverseMap();
            CreateMap<NarrationUpdateDTO, Narration>();

            CreateMap<Picture, PictureDto>().ReverseMap();

            CreateMap<Document, DocumentDTO>().ReverseMap();

            CreateMap<Orphan, OrphanDetailsDTO>();

        }        
    }
}
