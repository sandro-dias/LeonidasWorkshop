using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Workshop.GetWorkshopWorkload.Output;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.GetWorkshopWorkload
{
    public class GetWorkshopWorkloadUseCase(IUnitOfWork unitOfWork, ILogger<GetWorkshopWorkloadUseCase> logger) : IGetWorkshopWorkloadUseCase
    {
        public async Task<GetWorkshopWorkloadOutput> ExecuteAsync(long workshopId)
        {
            var workshop = await unitOfWork.WorkshopRepository.GetByIdAsync(workshopId);
            if (workshop is null)
            {
                logger.LogError("[{ClassName}] The workshop does not exist on database, workshopId: {workshopId}", nameof(GetWorkshopWorkloadUseCase), workshopId);
                return null;
            }

            var output = await GetWorkloadForTheNextFiveBusinessDays(workshop);
            return output;
        }

        private async Task<GetWorkshopWorkloadOutput> GetWorkloadForTheNextFiveBusinessDays(Domain.Entities.Workshop workshop)
        {
            var initialDate = DateTime.Now;
            var endDate = GetEndDate(initialDate);

            var workingDayList = await unitOfWork.WorkingDayRepository.ListAsync(new GetWorkingDayByDateRangeSpecification(workshop.WorkShopId, initialDate, endDate));
            var output = new GetWorkshopWorkloadOutput
            {
                WorkingDayList = [.. workingDayList]
            };

            return output;
        }

        public DateTime GetEndDate(DateTime initialDate)
        {
            var endDate = initialDate;
            for (var count = 1; count <= 5; count++)
            {
                if (endDate.DayOfWeek == DayOfWeek.Saturday)
                    endDate = endDate.AddDays(1);

                if (endDate.DayOfWeek == DayOfWeek.Friday)
                    endDate = endDate.AddDays(2);

                endDate = endDate.AddDays(1);
            }

            return endDate;
        }
    }
}
