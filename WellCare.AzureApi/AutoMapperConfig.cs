using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Models;
using WellCare.Repositories.Entities;

namespace WellCare.AzureApi
{
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.CreateMap<User, UserDetails>()
               .ForMember
                (
                   dest => dest.status,
                   opt => opt.MapFrom(src => Status.SUCCESS)
                )
                .ReverseMap();
  
            Mapper.CreateMap<User, UserListItem>().ReverseMap();
            Mapper.CreateMap<HealthScore, HealthScoreDetails>().ReverseMap();
            Mapper.CreateMap<HealthScore, HealthScoreListItem>().ReverseMap();
            Mapper.CreateMap<Content, ContentDetails>().ReverseMap();
            Mapper.CreateMap<Content, ContentListItem>().ReverseMap();
        }
    }
}
