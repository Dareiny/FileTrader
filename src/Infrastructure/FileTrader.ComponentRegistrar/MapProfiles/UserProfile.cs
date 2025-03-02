﻿using AutoMapper;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Users.Entity;

namespace FileTrader.ComponentRegistrar.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserRequest, User>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.CreatedDate, map => map.MapFrom(s => DateTime.UtcNow));

            CreateMap<User, UserInfoDTO>();
        }
    }
}
