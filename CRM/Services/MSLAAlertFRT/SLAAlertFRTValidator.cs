using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertFRT
{
    public interface ISLAAlertFRTValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertFRT SLAAlertFRT);
        Task<bool> Update(SLAAlertFRT SLAAlertFRT);
        Task<bool> Delete(SLAAlertFRT SLAAlertFRT);
        Task<bool> BulkDelete(List<SLAAlertFRT> SLAAlertFRTs);
        Task<bool> Import(List<SLAAlertFRT> SLAAlertFRTs);
    }

    public class SLAAlertFRTValidator : ISLAAlertFRTValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertFRTValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertFRT SLAAlertFRT)
        {
            SLAAlertFRTFilter SLAAlertFRTFilter = new SLAAlertFRTFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertFRT.Id },
                Selects = SLAAlertFRTSelect.Id
            };

            int count = await UOW.SLAAlertFRTRepository.Count(SLAAlertFRTFilter);
            if (count == 0)
                SLAAlertFRT.AddError(nameof(SLAAlertFRTValidator), nameof(SLAAlertFRT.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertFRT SLAAlertFRT)
        {
            return SLAAlertFRT.IsValidated;
        }

        public async Task<bool> Update(SLAAlertFRT SLAAlertFRT)
        {
            if (await ValidateId(SLAAlertFRT))
            {
            }
            return SLAAlertFRT.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertFRT SLAAlertFRT)
        {
            if (await ValidateId(SLAAlertFRT))
            {
            }
            return SLAAlertFRT.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertFRT> SLAAlertFRTs)
        {
            foreach (SLAAlertFRT SLAAlertFRT in SLAAlertFRTs)
            {
                await Delete(SLAAlertFRT);
            }
            return SLAAlertFRTs.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertFRT> SLAAlertFRTs)
        {
            return true;
        }
    }
}
