using MT.ReportService.Core.Interfaces.Repositories;
using MT.ReportService.Core.Interfaces.Services;

using System;
using System.Collections.Generic;

using System.Linq.Expressions;

using System.Threading.Tasks;

namespace MT.ReportService.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class, new()
    {

        

        private readonly IGenericRepository<TEntity> _repository;
        public GenericService( IGenericRepository<TEntity> repository)
        {
           
            _repository = repository;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            

            
        }


        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
            
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Find(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

      

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
           
           
        }
    }
}
