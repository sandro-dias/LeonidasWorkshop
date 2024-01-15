using Application.Data.Repository;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class Repository<T> : IAsyncRepository<T> where T : Entity
    {
        protected readonly WorkshopContext DbContext;

        public Repository(WorkshopContext dbContext)
        {
            DbContext = dbContext;
        }

        public Repository() { }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await DbContext.Set<T>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.Default.GetQuery(DbContext.Set<T>().AsQueryable(), spec);
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }
    }
}
