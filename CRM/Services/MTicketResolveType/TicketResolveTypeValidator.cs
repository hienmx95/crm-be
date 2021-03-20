using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTicketResolveType
{
    public interface ITicketResolveTypeValidator : IServiceScoped
    {
        Task<bool> Create(TicketResolveType TicketResolveType);
        Task<bool> Update(TicketResolveType TicketResolveType);
        Task<bool> Delete(TicketResolveType TicketResolveType);
        Task<bool> BulkDelete(List<TicketResolveType> TicketResolveTypes);
        Task<bool> Import(List<TicketResolveType> TicketResolveTypes);
    }

    public class TicketResolveTypeValidator : ITicketResolveTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketResolveTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketResolveType TicketResolveType)
        {
            TicketResolveTypeFilter TicketResolveTypeFilter = new TicketResolveTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketResolveType.Id },
                Selects = TicketResolveTypeSelect.Id
            };

            int count = await UOW.TicketResolveTypeRepository.Count(TicketResolveTypeFilter);
            if (count == 0)
                TicketResolveType.AddError(nameof(TicketResolveTypeValidator), nameof(TicketResolveType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(TicketResolveType TicketResolveType)
        {
            return TicketResolveType.IsValidated;
        }

        public async Task<bool> Update(TicketResolveType TicketResolveType)
        {
            if (await ValidateId(TicketResolveType))
            {
            }
            return TicketResolveType.IsValidated;
        }

        public async Task<bool> Delete(TicketResolveType TicketResolveType)
        {
            if (await ValidateId(TicketResolveType))
            {
            }
            return TicketResolveType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketResolveType> TicketResolveTypes)
        {
            foreach (TicketResolveType TicketResolveType in TicketResolveTypes)
            {
                await Delete(TicketResolveType);
            }
            return TicketResolveTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketResolveType> TicketResolveTypes)
        {
            return true;
        }
    }
}
