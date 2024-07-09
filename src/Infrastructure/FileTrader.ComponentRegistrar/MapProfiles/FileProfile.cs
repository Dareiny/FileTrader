using AutoMapper;
using FileTrader.Contracts.Users;
using FileTrader.Domain.Files.Entity;
using FileTrader.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrader.ComponentRegistrar.MapProfiles
{
    /// <summary>
    /// Профиль работы с файлами
    /// </summary>
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileDTO, UserFile>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.CreatedDate, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length));
            CreateMap<UserFile, FileInfoDTO>();
        }
    }
}
