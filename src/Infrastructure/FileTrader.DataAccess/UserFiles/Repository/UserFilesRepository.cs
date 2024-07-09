using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.UserFiles.Repositories;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileTrader.DataAccess.UserFiles.Repository
{
    /// <inheritdoc cref="IUserFilesRepository"/>
    public class UserFilesRepository : IUserFilesRepository
    {
 
        private readonly IRepository<UserFile> _repository;
        private readonly IMapper _mapper;
        public UserFilesRepository(IRepository<UserFile> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task DeleteByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(Id, cancellationToken);
        }

        public Task<FileDTO> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll()
                .Where(s => s.Id == id)
                .ProjectTo<FileDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<FileInfoDTO> GetInfoByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return _repository.GetAll()
                .Where(s => s.Id == Id)
                .ProjectTo<FileInfoDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> UploadAsync(UserFile file, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(file, cancellationToken);
            return file.Id;
        }
    }
}
