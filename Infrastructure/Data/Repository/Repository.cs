using Application.Data.Repository;
using Ardalis.Specification;
using Ardalis.Specification.;
using Domain.Entities;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await DbContext.Set<T>().FindAsync(keyValues, cancellationToken);
        }

        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T enitty, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.Default.Evaluate(DbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
