using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MConsultingService
{
    public interface IConsultingServiceValidator : IServiceScoped
    {
        Task<bool> Create(ConsultingService ConsultingService);
        Task<bool> Update(ConsultingService ConsultingService);
        Task<bool> Delete(ConsultingService ConsultingService);
        Task<bool> BulkDelete(List<ConsultingService> ConsultingServices);
        Task<bool> Import(List<ConsultingService> ConsultingServices);
    }

    public class ConsultingServiceValidator : IConsultingServiceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ConsultingServiceValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ConsultingService ConsultingService)
        {
            ConsultingServiceFilter ConsultingServiceFilter = new ConsultingServiceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ConsultingService.Id },
                Selects = ConsultingServiceSelect.Id
            };

            int count = await UOW.ConsultingServiceRepository.Count(ConsultingServiceFilter);
            if (count == 0)
                ConsultingService.AddError(nameof(ConsultingServiceValidator), nameof(ConsultingService.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ConsultingService ConsultingService)
        {
            return ConsultingService.IsValidated;
        }

        public async Task<bool> Update(ConsultingService ConsultingService)
        {
            if (await ValidateId(ConsultingService))
            {
            }
            return ConsultingService.IsValidated;
        }

        public async Task<bool> Delete(ConsultingService ConsultingService)
        {
            if (await ValidateId(ConsultingService))
            {
            }
            return ConsultingService.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ConsultingService> ConsultingServices)
        {
            foreach (ConsultingService ConsultingService in ConsultingServices)
            {
                await Delete(ConsultingService);
            }
            return ConsultingServices.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ConsultingService> ConsultingServices)
        {
            return true;
        }
    }
}
