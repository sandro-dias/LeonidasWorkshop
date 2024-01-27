using Application.Data;
using Application.Data.Specification;
using Application.UseCases.CreateWorkshop;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Application.UseCases.Service.GetServices
{
    public class GetServicesUseCase : IGetServicesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetServicesUseCase> _logger;
        private readonly IMemoryCache _memoryCache;
        public GetServicesUseCase(IUnitOfWork unitOfWork, ILogger<GetServicesUseCase> logger, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _memoryCache = memoryCache;
        }

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

            //TODO validar em teste unitário e funcional
            var tokenCacheKey = $"{workshop.Name}_{today:yy/MM/dd}";
            if (!_memoryCache.TryGetValue(tokenCacheKey, out IReadOnlyList<Domain.Entities.Service> services))
            {
                services = await _unitOfWork.ServiceRepository.ListAsync(new GetTodayServicesByWorkshopIdSpecification(workshopId, today));
                var expirationDate = DateTime.Now.ToLocalTime().Date.AddDays(1).AddSeconds(-1);
                _memoryCache.Set(tokenCacheKey, services, new DateTimeOffset(expirationDate));
            }

            return services;
        }
    }
}
