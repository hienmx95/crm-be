using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTicketSource
{
    public interface ITicketSourceValidator : IServiceScoped
    {
        Task<bool> Create(TicketSource TicketSource);
        Task<bool> Update(TicketSource TicketSource);
        Task<bool> Delete(TicketSource TicketSource);
        Task<bool> BulkDelete(List<TicketSource> TicketSources);
        Task<bool> Import(List<TicketSource> TicketSources);
    }

    public class TicketSourceValidator : ITicketSourceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketSourceValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketSource TicketSource)
        {
            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketSource.Id },
                Selects = TicketSourceSelect.Id
            };

            int count = await UOW.TicketSourceRepository.Count(TicketSourceFilter);
            if (count == 0)
                TicketSource.AddError(nameof(TicketSourceValidator), nameof(TicketSource.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateName(TicketSource TicketSource)
        {
            if (string.IsNullOrWhiteSpace(TicketSource.Name))
            {
                TicketSource.AddError(nameof(TicketSourceValidator), nameof(TicketSource.Name), ErrorCode.NameEmpty);
            }
            else if (TicketSource.Name.Length > 255)
            {
                TicketSource.AddError(nameof(TicketSourceValidator), nameof(TicketSource.Name), ErrorCode.NameOverLength);
            }
            return TicketSource.IsValidated;
        }
        public async Task<bool>Create(TicketSource TicketSource)
        {
            await ValidateName(TicketSource);
            return TicketSource.IsValidated;
        }

        public async Task<bool> Update(TicketSource TicketSource)
        {
            if (await ValidateId(TicketSource))
            {
                await ValidateName(TicketSource);
            }
            return TicketSource.IsValidated;
        }

        public async Task<bool> Delete(TicketSource TicketSource)
        {
            if (await ValidateId(TicketSource))
            {
            }
            return TicketSource.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketSource> TicketSources)
        {
            foreach (TicketSource TicketSource in TicketSources)
            {
                await Delete(TicketSource);
            }
            return TicketSources.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketSource> TicketSources)
        {
            return true;
        }
    }
}
