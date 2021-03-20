using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MOpportunityResultType
{
    public interface IOpportunityResultTypeValidator : IServiceScoped
    {
        Task<bool> Create(OpportunityResultType OpportunityResultType);
        Task<bool> Update(OpportunityResultType OpportunityResultType);
        Task<bool> Delete(OpportunityResultType OpportunityResultType);
        Task<bool> BulkDelete(List<OpportunityResultType> OpportunityResultTypes);
        Task<bool> Import(List<OpportunityResultType> OpportunityResultTypes);
    }

    public class OpportunityResultTypeValidator : IOpportunityResultTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OpportunityResultTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OpportunityResultType OpportunityResultType)
        {
            OpportunityResultTypeFilter OpportunityResultTypeFilter = new OpportunityResultTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OpportunityResultType.Id },
                Selects = OpportunityResultTypeSelect.Id
            };

            int count = await UOW.OpportunityResultTypeRepository.Count(OpportunityResultTypeFilter);
            if (count == 0)
                OpportunityResultType.AddError(nameof(OpportunityResultTypeValidator), nameof(OpportunityResultType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(OpportunityResultType OpportunityResultType)
        {
            return OpportunityResultType.IsValidated;
        }

        public async Task<bool> Update(OpportunityResultType OpportunityResultType)
        {
            if (await ValidateId(OpportunityResultType))
            {
            }
            return OpportunityResultType.IsValidated;
        }

        public async Task<bool> Delete(OpportunityResultType OpportunityResultType)
        {
            if (await ValidateId(OpportunityResultType))
            {
            }
            return OpportunityResultType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OpportunityResultType> OpportunityResultTypes)
        {
            foreach (OpportunityResultType OpportunityResultType in OpportunityResultTypes)
            {
                await Delete(OpportunityResultType);
            }
            return OpportunityResultTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OpportunityResultType> OpportunityResultTypes)
        {
            return true;
        }
    }
}
