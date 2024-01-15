using Application.Data;
using Application.Data.Specification;
using Application.UseCases.CreateWorkshop;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.Service.GetServices
{
    public class GetServicesUseCase : IGetServicesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetServicesUseCase> _logger;

        public GetServicesUseCase(IUnitOfWork unitOfWork, ILogger<GetServicesUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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

            var services = await _unitOfWork.ServiceRepository.ListAsync(new GetTodayServicesByWorkshopIdSpecification(workshopId, today));
            return services;
        }
    }
}
