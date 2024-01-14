﻿using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Workshop.GetWorkshopWorkload.Output;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.GetWorkshopWorkload
{
    public class GetWorkshopWorkloadUseCase(IUnitOfWork unitOfWork, ILogger<GetWorkshopWorkloadUseCase> logger) : IGetWorkshopWorkloadUseCase
    {
        public async Task<GetWorkshopWorkloadOutput> ExecuteAsync(int workshopId)
        {   
            var workshop = await unitOfWork.WorkshopRepository.GetByIdAsync(workshopId);
            if (workshop is null)
            {
                logger.LogError("");
                return null;
            }

            var output = await GetWorkloadForTheNextFiveBusinessDays(workshopId);
            return output;
        }

        private async Task<GetWorkshopWorkloadOutput> GetWorkloadForTheNextFiveBusinessDays(long workshopId)
        {
            //TODO: colocar uma validação se a oficina existe!!
            //TODO: validar comportamento caso WorkingDay não exista no banco

            var today = DateTime.Now;
            var output = new GetWorkshopWorkloadOutput();
            for (var count = 1; count <= 5; count++)
            {
                if (today.DayOfWeek == DayOfWeek.Saturday)
                    today = today.AddDays(2);

                if (today.DayOfWeek == DayOfWeek.Sunday)
                    today = today.AddDays(1);

                var workingDay = await unitOfWork.WorkingDayRepository.FirstOrDefaultAsync(new GetWorkingDayByWorkshopSpecification(workshopId, today));
                output.WorkingDayList.Add(workingDay);
                today = today.AddDays(1);
            }

            return output;
        }
    }
}
