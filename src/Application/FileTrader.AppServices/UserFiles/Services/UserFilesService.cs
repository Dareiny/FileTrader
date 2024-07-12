using AutoMapper;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.UserFiles.Repositories;
using FileTrader.Contracts.General;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;

namespace FileTrader.AppServices.UserFiles.Services
{
    /// <inheridoc cref="IUserFilesService"/>
    public class UserFilesService : IUserFilesService
    {
        private readonly IUserFilesRepository _userFilesRepository;
        private readonly IMapper _mapper;


        /// <summary>
        /// Инициализирует экземпляр <see cref="UserFilesService"/>.
        /// </summary>
        /// <param name="userFilesRepository"></param>
        public UserFilesService(IUserFilesRepository userFilesRepository, IMapper mapper)
        {
            _userFilesRepository = userFilesRepository;
            _mapper = mapper;
        }

        public async Task DeleteByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            await _userFilesRepository.DeleteByIdAsync(Id, cancellationToken);
        }

        public Task<FileDTO> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userFilesRepository.DownloadAsync(id, cancellationToken);
        }

        public async Task<ResultWithPagination<FileInfoDTO>> GetFilesAsync(PaginationRequest request, Specification<EFile> specification, CancellationToken cancellationToken)
        {
            return await _userFilesRepository.GetAllBySpecification(request, specification, cancellationToken);
        }

        public async Task<FileInfoDTO> GetInfoByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _userFilesRepository.GetInfoByIdAsync(Id, cancellationToken);
        }

        public async Task UpdateAccessAsync(UpdateAccessRequest request, CancellationToken cancellationToken)
        {
            await _userFilesRepository.UpdateAccessAsync(request, cancellationToken);
        }

        public async Task<Guid> UploadAsync(FileDTO model, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<EFile>(model);
            return await _userFilesRepository.UploadAsync(file, cancellationToken);
        }
    }
}

