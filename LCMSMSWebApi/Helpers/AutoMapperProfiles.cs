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

            CreateMap<Orphan, OrphanDTO>().ReverseMap();

            CreateMap<OrphanEditDTO, Orphan>();

            CreateMap<Sponsor, SponsorDTO>().ReverseMap();
            CreateMap<SponsorUpdateDTO, Sponsor>();

            CreateMap<Guardian, GuardianDTO>().ReverseMap();
            CreateMap<GuardianUpdateDTO, Guardian>();

            CreateMap<Academic, AcademicDTO>().ReverseMap();
            CreateMap<AcademicUpdateDTO, Academic>();

            CreateMap<Narration, NarrationDTO>().ReverseMap();
            CreateMap<NarrationUpdateDTO, Narration>();

            CreateMap<Picture, PictureDTO>().ReverseMap();

            CreateMap<Document, DocumentDTO>().ReverseMap();

            CreateMap<Orphan, OrphanDetailsDTO>();

        }        
    }
}
