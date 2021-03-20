using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalation
{
    public interface ISLAEscalationValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalation SLAEscalation);
        Task<bool> Update(SLAEscalation SLAEscalation);
        Task<bool> Delete(SLAEscalation SLAEscalation);
        Task<bool> BulkDelete(List<SLAEscalation> SLAEscalations);
        Task<bool> Import(List<SLAEscalation> SLAEscalations);
    }

    public class SLAEscalationValidator : ISLAEscalationValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalation SLAEscalation)
        {
            SLAEscalationFilter SLAEscalationFilter = new SLAEscalationFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalation.Id },
                Selects = SLAEscalationSelect.Id
            };

            int count = await UOW.SLAEscalationRepository.Count(SLAEscalationFilter);
            if (count == 0)
                SLAEscalation.AddError(nameof(SLAEscalationValidator), nameof(SLAEscalation.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalation SLAEscalation)
        {
            return SLAEscalation.IsValidated;
        }

        public async Task<bool> Update(SLAEscalation SLAEscalation)
        {
            if (await ValidateId(SLAEscalation))
            {
            }
            return SLAEscalation.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalation SLAEscalation)
        {
            if (await ValidateId(SLAEscalation))
            {
            }
            return SLAEscalation.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalation> SLAEscalations)
        {
            foreach (SLAEscalation SLAEscalation in SLAEscalations)
            {
                await Delete(SLAEscalation);
            }
            return SLAEscalations.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalation> SLAEscalations)
        {
            return true;
        }
    }
}
