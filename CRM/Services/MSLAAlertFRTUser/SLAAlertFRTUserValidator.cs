using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertFRTUser
{
    public interface ISLAAlertFRTUserValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertFRTUser SLAAlertFRTUser);
        Task<bool> Update(SLAAlertFRTUser SLAAlertFRTUser);
        Task<bool> Delete(SLAAlertFRTUser SLAAlertFRTUser);
        Task<bool> BulkDelete(List<SLAAlertFRTUser> SLAAlertFRTUsers);
        Task<bool> Import(List<SLAAlertFRTUser> SLAAlertFRTUsers);
    }

    public class SLAAlertFRTUserValidator : ISLAAlertFRTUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertFRTUserValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertFRTUser SLAAlertFRTUser)
        {
            SLAAlertFRTUserFilter SLAAlertFRTUserFilter = new SLAAlertFRTUserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertFRTUser.Id },
                Selects = SLAAlertFRTUserSelect.Id
            };

            int count = await UOW.SLAAlertFRTUserRepository.Count(SLAAlertFRTUserFilter);
            if (count == 0)
                SLAAlertFRTUser.AddError(nameof(SLAAlertFRTUserValidator), nameof(SLAAlertFRTUser.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertFRTUser SLAAlertFRTUser)
        {
            return SLAAlertFRTUser.IsValidated;
        }

        public async Task<bool> Update(SLAAlertFRTUser SLAAlertFRTUser)
        {
            if (await ValidateId(SLAAlertFRTUser))
            {
            }
            return SLAAlertFRTUser.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertFRTUser SLAAlertFRTUser)
        {
            if (await ValidateId(SLAAlertFRTUser))
            {
            }
            return SLAAlertFRTUser.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertFRTUser> SLAAlertFRTUsers)
        {
            foreach (SLAAlertFRTUser SLAAlertFRTUser in SLAAlertFRTUsers)
            {
                await Delete(SLAAlertFRTUser);
            }
            return SLAAlertFRTUsers.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertFRTUser> SLAAlertFRTUsers)
        {
            return true;
        }
    }
}
