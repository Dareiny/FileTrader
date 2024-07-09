using AutoMapper;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.UserFiles.Repositories;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<FileInfoDTO> GetInfoByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _userFilesRepository.GetInfoByIdAsync(Id, cancellationToken);
        }

        public async Task<Guid> UploadAsync(FileDTO model, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<UserFile>(model);
            return await _userFilesRepository.UploadAsync(file, cancellationToken);
        }
    }
 }

