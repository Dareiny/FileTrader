using AutoMapper;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
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
            CreateMap<UserFile, FileDTO>();
            CreateMap<FileDTO, UserFile>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length))
                .ForMember(s => s.CreatedDate, map => map.MapFrom(s => DateTime.UtcNow));
            CreateMap<UserFile, FileInfoDTO>();
        }
    }
}
