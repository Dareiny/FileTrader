using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileTrader.AppServices.Specifications;
using FileTrader.AppServices.UserFiles.Repositories;
using FileTrader.Contracts.General;
using FileTrader.Contracts.UserFiles;
using FileTrader.Domain.Files.Entity;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FileTrader.DataAccess.UserFiles.Repository
{
    /// <inheritdoc cref="IUserFilesRepository"/>
    public class UserFilesRepository : IUserFilesRepository
    {

        private readonly IRepository<EFile> _repository;
        private readonly IMapper _mapper;
        public UserFilesRepository(IRepository<EFile> repository, IMapper mapper)
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

        public async Task<ResultWithPagination<FileInfoDTO>> GetAllBySpecification(PaginationRequest request, Specification<EFile> specification, CancellationToken cancellationToken)
        {
            var result = new ResultWithPagination<FileInfoDTO>();

            var query = _repository.GetAll().Where(specification.ToExpression());

            var elementsCount = await query.CountAsync(cancellationToken);
            result.AvailablePages = elementsCount / request.BatchSize;
            if (elementsCount % request.BatchSize > 0) result.AvailablePages++;

            var paginationQuery = await query
               .OrderBy(file => file.CreatedDate)
               .Skip(request.BatchSize * (request.PageNumber - 1))
               .Take(request.BatchSize)
               .ProjectTo<FileInfoDTO>(_mapper.ConfigurationProvider)
               .ToListAsync();

            result.Result = paginationQuery;
            return result;
        }

        public Task<FileInfoDTO> GetInfoByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return _repository.GetAll()
                .Where(s => s.Id == Id)
                .ProjectTo<FileInfoDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAccessAsync(UpdateAccessRequest request, CancellationToken cancellationToken)
        {
            var file = await _repository.GetByIdAsync(request.Id, cancellationToken);
            file.GeneralAccess = request.GeneralAccess;
            await _repository.UpdateAsync(file, cancellationToken);
        }

        public async Task<Guid> UploadAsync(EFile file, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(file, cancellationToken);
            return file.Id;
        }
    }
}
