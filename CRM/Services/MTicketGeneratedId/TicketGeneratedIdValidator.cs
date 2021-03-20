using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTicketGeneratedId
{
    public interface ITicketGeneratedIdValidator : IServiceScoped
    {
        Task<bool> Create(TicketGeneratedId TicketGeneratedId);
        Task<bool> Update(TicketGeneratedId TicketGeneratedId);
        Task<bool> Delete(TicketGeneratedId TicketGeneratedId);
        Task<bool> BulkDelete(List<TicketGeneratedId> TicketGeneratedIds);
        Task<bool> Import(List<TicketGeneratedId> TicketGeneratedIds);
    }

    public class TicketGeneratedIdValidator : ITicketGeneratedIdValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketGeneratedIdValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketGeneratedId TicketGeneratedId)
        {
            TicketGeneratedIdFilter TicketGeneratedIdFilter = new TicketGeneratedIdFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketGeneratedId.Id },
                Selects = TicketGeneratedIdSelect.Id
            };

            int count = await UOW.TicketGeneratedIdRepository.Count(TicketGeneratedIdFilter);
            if (count == 0)
                TicketGeneratedId.AddError(nameof(TicketGeneratedIdValidator), nameof(TicketGeneratedId.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(TicketGeneratedId TicketGeneratedId)
        {
            return TicketGeneratedId.IsValidated;
        }

        public async Task<bool> Update(TicketGeneratedId TicketGeneratedId)
        {
            if (await ValidateId(TicketGeneratedId))
            {
            }
            return TicketGeneratedId.IsValidated;
        }

        public async Task<bool> Delete(TicketGeneratedId TicketGeneratedId)
        {
            if (await ValidateId(TicketGeneratedId))
            {
            }
            return TicketGeneratedId.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketGeneratedId> TicketGeneratedIds)
        {
            foreach (TicketGeneratedId TicketGeneratedId in TicketGeneratedIds)
            {
                await Delete(TicketGeneratedId);
            }
            return TicketGeneratedIds.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketGeneratedId> TicketGeneratedIds)
        {
            return true;
        }
    }
}
