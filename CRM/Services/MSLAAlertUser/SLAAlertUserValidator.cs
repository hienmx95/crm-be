using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertUser
{
    public interface ISLAAlertUserValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertUser SLAAlertUser);
        Task<bool> Update(SLAAlertUser SLAAlertUser);
        Task<bool> Delete(SLAAlertUser SLAAlertUser);
        Task<bool> BulkDelete(List<SLAAlertUser> SLAAlertUsers);
        Task<bool> Import(List<SLAAlertUser> SLAAlertUsers);
    }

    public class SLAAlertUserValidator : ISLAAlertUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertUserValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertUser SLAAlertUser)
        {
            SLAAlertUserFilter SLAAlertUserFilter = new SLAAlertUserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertUser.Id },
                Selects = SLAAlertUserSelect.Id
            };

            int count = await UOW.SLAAlertUserRepository.Count(SLAAlertUserFilter);
            if (count == 0)
                SLAAlertUser.AddError(nameof(SLAAlertUserValidator), nameof(SLAAlertUser.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertUser SLAAlertUser)
        {
            return SLAAlertUser.IsValidated;
        }

        public async Task<bool> Update(SLAAlertUser SLAAlertUser)
        {
            if (await ValidateId(SLAAlertUser))
            {
            }
            return SLAAlertUser.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertUser SLAAlertUser)
        {
            if (await ValidateId(SLAAlertUser))
            {
            }
            return SLAAlertUser.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertUser> SLAAlertUsers)
        {
            foreach (SLAAlertUser SLAAlertUser in SLAAlertUsers)
            {
                await Delete(SLAAlertUser);
            }
            return SLAAlertUsers.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertUser> SLAAlertUsers)
        {
            return true;
        }
    }
}
