using AutoMapper;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Data.Mapping
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Artists, ArtistsModel>().ReverseMap();
            CreateMap<Users, UserModel>().ReverseMap();
        }
    }
}
