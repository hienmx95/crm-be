using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationFRT
{
    public interface ISLAEscalationFRTValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationFRT SLAEscalationFRT);
        Task<bool> Update(SLAEscalationFRT SLAEscalationFRT);
        Task<bool> Delete(SLAEscalationFRT SLAEscalationFRT);
        Task<bool> BulkDelete(List<SLAEscalationFRT> SLAEscalationFRTs);
        Task<bool> Import(List<SLAEscalationFRT> SLAEscalationFRTs);
    }

    public class SLAEscalationFRTValidator : ISLAEscalationFRTValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationFRTValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationFRT SLAEscalationFRT)
        {
            SLAEscalationFRTFilter SLAEscalationFRTFilter = new SLAEscalationFRTFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationFRT.Id },
                Selects = SLAEscalationFRTSelect.Id
            };

            int count = await UOW.SLAEscalationFRTRepository.Count(SLAEscalationFRTFilter);
            if (count == 0)
                SLAEscalationFRT.AddError(nameof(SLAEscalationFRTValidator), nameof(SLAEscalationFRT.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationFRT SLAEscalationFRT)
        {
            return SLAEscalationFRT.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationFRT SLAEscalationFRT)
        {
            if (await ValidateId(SLAEscalationFRT))
            {
            }
            return SLAEscalationFRT.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationFRT SLAEscalationFRT)
        {
            if (await ValidateId(SLAEscalationFRT))
            {
            }
            return SLAEscalationFRT.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationFRT> SLAEscalationFRTs)
        {
            foreach (SLAEscalationFRT SLAEscalationFRT in SLAEscalationFRTs)
            {
                await Delete(SLAEscalationFRT);
            }
            return SLAEscalationFRTs.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationFRT> SLAEscalationFRTs)
        {
            return true;
        }
    }
}
