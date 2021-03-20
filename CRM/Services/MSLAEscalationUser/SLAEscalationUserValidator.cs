using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationUser
{
    public interface ISLAEscalationUserValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationUser SLAEscalationUser);
        Task<bool> Update(SLAEscalationUser SLAEscalationUser);
        Task<bool> Delete(SLAEscalationUser SLAEscalationUser);
        Task<bool> BulkDelete(List<SLAEscalationUser> SLAEscalationUsers);
        Task<bool> Import(List<SLAEscalationUser> SLAEscalationUsers);
    }

    public class SLAEscalationUserValidator : ISLAEscalationUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationUserValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationUser SLAEscalationUser)
        {
            SLAEscalationUserFilter SLAEscalationUserFilter = new SLAEscalationUserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationUser.Id },
                Selects = SLAEscalationUserSelect.Id
            };

            int count = await UOW.SLAEscalationUserRepository.Count(SLAEscalationUserFilter);
            if (count == 0)
                SLAEscalationUser.AddError(nameof(SLAEscalationUserValidator), nameof(SLAEscalationUser.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationUser SLAEscalationUser)
        {
            return SLAEscalationUser.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationUser SLAEscalationUser)
        {
            if (await ValidateId(SLAEscalationUser))
            {
            }
            return SLAEscalationUser.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationUser SLAEscalationUser)
        {
            if (await ValidateId(SLAEscalationUser))
            {
            }
            return SLAEscalationUser.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationUser> SLAEscalationUsers)
        {
            foreach (SLAEscalationUser SLAEscalationUser in SLAEscalationUsers)
            {
                await Delete(SLAEscalationUser);
            }
            return SLAEscalationUsers.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationUser> SLAEscalationUsers)
        {
            return true;
        }
    }
}
