using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationFRTUser
{
    public interface ISLAEscalationFRTUserValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<bool> Update(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<bool> Delete(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<bool> BulkDelete(List<SLAEscalationFRTUser> SLAEscalationFRTUsers);
        Task<bool> Import(List<SLAEscalationFRTUser> SLAEscalationFRTUsers);
    }

    public class SLAEscalationFRTUserValidator : ISLAEscalationFRTUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationFRTUserValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter = new SLAEscalationFRTUserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationFRTUser.Id },
                Selects = SLAEscalationFRTUserSelect.Id
            };

            int count = await UOW.SLAEscalationFRTUserRepository.Count(SLAEscalationFRTUserFilter);
            if (count == 0)
                SLAEscalationFRTUser.AddError(nameof(SLAEscalationFRTUserValidator), nameof(SLAEscalationFRTUser.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            return SLAEscalationFRTUser.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            if (await ValidateId(SLAEscalationFRTUser))
            {
            }
            return SLAEscalationFRTUser.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            if (await ValidateId(SLAEscalationFRTUser))
            {
            }
            return SLAEscalationFRTUser.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationFRTUser> SLAEscalationFRTUsers)
        {
            foreach (SLAEscalationFRTUser SLAEscalationFRTUser in SLAEscalationFRTUsers)
            {
                await Delete(SLAEscalationFRTUser);
            }
            return SLAEscalationFRTUsers.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationFRTUser> SLAEscalationFRTUsers)
        {
            return true;
        }
    }
}
