using AutoMapper;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;

namespace FileTrader.ComponentRegistrar.MapProfiles
{
    /// <summary>
    /// Профиль работы с файлами
    /// </summary>
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<EFile, FileDTO>();
            CreateMap<FileDTO, EFile>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length))
                .ForMember(s => s.CreatedDate, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.GeneralAccess, map => map.MapFrom(s => false));
            CreateMap<EFile, FileInfoDTO>();
        }
    }
}
