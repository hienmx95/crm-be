using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlert
{
    public interface ISLAAlertValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlert SLAAlert);
        Task<bool> Update(SLAAlert SLAAlert);
        Task<bool> Delete(SLAAlert SLAAlert);
        Task<bool> BulkDelete(List<SLAAlert> SLAAlerts);
        Task<bool> Import(List<SLAAlert> SLAAlerts);
    }

    public class SLAAlertValidator : ISLAAlertValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlert SLAAlert)
        {
            SLAAlertFilter SLAAlertFilter = new SLAAlertFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlert.Id },
                Selects = SLAAlertSelect.Id
            };

            int count = await UOW.SLAAlertRepository.Count(SLAAlertFilter);
            if (count == 0)
                SLAAlert.AddError(nameof(SLAAlertValidator), nameof(SLAAlert.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlert SLAAlert)
        {
            return SLAAlert.IsValidated;
        }

        public async Task<bool> Update(SLAAlert SLAAlert)
        {
            if (await ValidateId(SLAAlert))
            {
            }
            return SLAAlert.IsValidated;
        }

        public async Task<bool> Delete(SLAAlert SLAAlert)
        {
            if (await ValidateId(SLAAlert))
            {
            }
            return SLAAlert.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlert> SLAAlerts)
        {
            foreach (SLAAlert SLAAlert in SLAAlerts)
            {
                await Delete(SLAAlert);
            }
            return SLAAlerts.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlert> SLAAlerts)
        {
            return true;
        }
    }
}
