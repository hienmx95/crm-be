using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MCompanyActivity
{
    public interface ICompanyActivityValidator : IServiceScoped
    {
        Task<bool> Create(CompanyActivity CompanyActivity);
        Task<bool> Update(CompanyActivity CompanyActivity);
        Task<bool> Delete(CompanyActivity CompanyActivity);
        Task<bool> BulkDelete(List<CompanyActivity> CompanyActivities);
        Task<bool> Import(List<CompanyActivity> CompanyActivities);
    }

    public class CompanyActivityValidator : ICompanyActivityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            FromDateEmpty,
            ToDateInvalid
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CompanyActivityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CompanyActivity CompanyActivity)
        {
            CompanyActivityFilter CompanyActivityFilter = new CompanyActivityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CompanyActivity.Id },
                Selects = CompanyActivitySelect.Id
            };

            int count = await UOW.CompanyActivityRepository.Count(CompanyActivityFilter);
            if (count == 0)
                CompanyActivity.AddError(nameof(CompanyActivityValidator), nameof(CompanyActivity.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateDate(CompanyActivity CompanyActivity)
        {
            if (CompanyActivity.FromDate == DateTime.MinValue)
            {
                CompanyActivity.AddError(nameof(CompanyActivityValidator), nameof(CompanyActivity.FromDate), ErrorCode.FromDateEmpty);
            }
            else
            {
                if (CompanyActivity.ToDate.HasValue)
                {
                    if (CompanyActivity.ToDate.Value <= CompanyActivity.FromDate)
                    {
                        CompanyActivity.AddError(nameof(CompanyActivityValidator), nameof(CompanyActivity.ToDate), ErrorCode.ToDateInvalid);
                    }
                }
            }
            return CompanyActivity.IsValidated;
        }

        public async Task<bool>Create(CompanyActivity CompanyActivity)
        {
            await ValidateDate(CompanyActivity);
            return CompanyActivity.IsValidated;
        }

        public async Task<bool> Update(CompanyActivity CompanyActivity)
        {
            if (await ValidateId(CompanyActivity))
            {
                await ValidateDate(CompanyActivity);
            }
            return CompanyActivity.IsValidated;
        }

        public async Task<bool> Delete(CompanyActivity CompanyActivity)
        {
            if (await ValidateId(CompanyActivity))
            {
            }
            return CompanyActivity.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CompanyActivity> CompanyActivities)
        {
            foreach (CompanyActivity CompanyActivity in CompanyActivities)
            {
                await Delete(CompanyActivity);
            }
            return CompanyActivities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CompanyActivity> CompanyActivities)
        {
            return true;
        }
    }
}
