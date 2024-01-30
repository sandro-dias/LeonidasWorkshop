using Application.Data;
using Application.Data.Specification;
using Application.UseCases.CreateWorkshop;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.Service.GetServices
{
    public class GetServicesUseCase(IUnitOfWork unitOfWork, ILogger<GetServicesUseCase> logger, IMemoryCache memoryCache) : IGetServicesUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<GetServicesUseCase> _logger = logger;
        private readonly IMemoryCache _memoryCache = memoryCache;

        public async Task<IEnumerable<Domain.Entities.Service>> ExecuteAsync(long workshopId)
        {
            var today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Sunday || today.DayOfWeek == DayOfWeek.Saturday)
            {
                return Enumerable.Empty<Domain.Entities.Service>();
            }

            var workshop = await _unitOfWork.WorkshopRepository.GetByIdAsync(workshopId);
            if (workshop == null)
            {
                _logger.LogWarning("[{ClassName}] The workshop does not exist on database", nameof(CreateWorkshopUseCase));
                return Enumerable.Empty<Domain.Entities.Service>();
            }

            var tokenCacheKey = $"{workshop.Name}_SERVICES_{today:yy/MM/dd}";
            if (!_memoryCache.TryGetValue(tokenCacheKey, out IEnumerable<Domain.Entities.Service> services))
            {
                services = await _unitOfWork.ServiceRepository.ListAsync(new GetTodayServicesByWorkshopIdSpecification(workshopId, today));
                var expirationDate = DateTime.Now.ToLocalTime().Date.AddDays(1).AddSeconds(-1);
                _memoryCache.Set(tokenCacheKey, services, new DateTimeOffset(expirationDate));
            }

            return services;
        }
    }
}
