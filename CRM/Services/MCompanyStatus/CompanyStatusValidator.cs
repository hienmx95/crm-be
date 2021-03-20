using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCompanyStatus
{
    public interface ICompanyStatusValidator : IServiceScoped
    {
        Task<bool> Create(CompanyStatus CompanyStatus);
        Task<bool> Update(CompanyStatus CompanyStatus);
        Task<bool> Delete(CompanyStatus CompanyStatus);
        Task<bool> BulkDelete(List<CompanyStatus> CompanyStatuses);
        Task<bool> Import(List<CompanyStatus> CompanyStatuses);
    }

    public class CompanyStatusValidator : ICompanyStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CompanyStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CompanyStatus CompanyStatus)
        {
            CompanyStatusFilter CompanyStatusFilter = new CompanyStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CompanyStatus.Id },
                Selects = CompanyStatusSelect.Id
            };

            int count = await UOW.CompanyStatusRepository.Count(CompanyStatusFilter);
            if (count == 0)
                CompanyStatus.AddError(nameof(CompanyStatusValidator), nameof(CompanyStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CompanyStatus CompanyStatus)
        {
            return CompanyStatus.IsValidated;
        }

        public async Task<bool> Update(CompanyStatus CompanyStatus)
        {
            if (await ValidateId(CompanyStatus))
            {
            }
            return CompanyStatus.IsValidated;
        }

        public async Task<bool> Delete(CompanyStatus CompanyStatus)
        {
            if (await ValidateId(CompanyStatus))
            {
            }
            return CompanyStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CompanyStatus> CompanyStatuses)
        {
            foreach (CompanyStatus CompanyStatus in CompanyStatuses)
            {
                await Delete(CompanyStatus);
            }
            return CompanyStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CompanyStatus> CompanyStatuses)
        {
            return true;
        }
    }
}
