using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAPolicy
{
    public interface ISLAPolicyValidator : IServiceScoped
    {
        Task<bool> Create(SLAPolicy SLAPolicy);
        Task<bool> Update(SLAPolicy SLAPolicy);
        Task<bool> Delete(SLAPolicy SLAPolicy);
        Task<bool> BulkDelete(List<SLAPolicy> SLAPolicies);
        Task<bool> Import(List<SLAPolicy> SLAPolicies);
    }

    public class SLAPolicyValidator : ISLAPolicyValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAPolicyValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAPolicy SLAPolicy)
        {
            SLAPolicyFilter SLAPolicyFilter = new SLAPolicyFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAPolicy.Id },
                Selects = SLAPolicySelect.Id
            };

            int count = await UOW.SLAPolicyRepository.Count(SLAPolicyFilter);
            if (count == 0)
                SLAPolicy.AddError(nameof(SLAPolicyValidator), nameof(SLAPolicy.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAPolicy SLAPolicy)
        {
            return SLAPolicy.IsValidated;
        }

        public async Task<bool> Update(SLAPolicy SLAPolicy)
        {
            if (await ValidateId(SLAPolicy))
            {
            }
            return SLAPolicy.IsValidated;
        }

        public async Task<bool> Delete(SLAPolicy SLAPolicy)
        {
            if (await ValidateId(SLAPolicy))
            {
            }
            return SLAPolicy.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAPolicy> SLAPolicies)
        {
            foreach (SLAPolicy SLAPolicy in SLAPolicies)
            {
                await Delete(SLAPolicy);
            }
            return SLAPolicies.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAPolicy> SLAPolicies)
        {
            return true;
        }
    }
}
