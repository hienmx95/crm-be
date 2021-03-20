using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCompany
{
    public interface ICompanyValidator : IServiceScoped
    {
        Task<bool> Create(Company Company);
        Task<bool> Update(Company Company);
        Task<bool> Delete(Company Company);
        Task<bool> BulkDelete(List<Company> Companys);
        Task<bool> Import(List<Company> Companys);
    }

    public class CompanyValidator : ICompanyValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            AppUserNotExisted,
            AppUserEmpty,
            AddressIsOverLenth,
            PhoneOverLength,
            PhoneExisted
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CompanyValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Company Company)
        {
            CompanyFilter CompanyFilter = new CompanyFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Company.Id },
                Selects = CompanySelect.Id
            };

            int count = await UOW.CompanyRepository.Count(CompanyFilter);
            if (count == 0)
                Company.AddError(nameof(CompanyValidator), nameof(Company.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateAppUser(Company Company)
        {
            if (Company.AppUserId.HasValue)
            {
                AppUserFilter AppUserFilter = new AppUserFilter
                {
                    Id = new IdFilter { Equal = Company.AppUserId }
                };

                var count = await UOW.AppUserRepository.Count(AppUserFilter);
                if (count == 0)
                    Company.AddError(nameof(CompanyValidator), nameof(Company.AppUserId), ErrorCode.AppUserNotExisted);
            }
            else
            {
                Company.AddError(nameof(CompanyValidator), nameof(Company.AppUserId), ErrorCode.AppUserEmpty); 
            }
            return Company.IsValidated;
        }
        public async Task<bool> ValidateAddress(Company Company)
        {
            if (!string.IsNullOrWhiteSpace(Company.Address) && Company.Address.Length > 1000)
            {
                Company.AddError(nameof(CompanyValidator), nameof(Company.Address), ErrorCode.AddressIsOverLenth);
            } 
            return Company.IsValidated;
        }

        public async Task<bool> ValidateName(Company Company)
        {
            if (string.IsNullOrWhiteSpace(Company.Name))
            {
                Company.AddError(nameof(CompanyValidator), nameof(Company.Name), ErrorCode.NameEmpty);
            }
            else if (Company.Name.Length > 500)
            {
                Company.AddError(nameof(CompanyValidator), nameof(Company.Name), ErrorCode.NameOverLength);
            }
            return Company.IsValidated;
        }

        public async Task<bool> ValidatePhone(Company Company)
        {
            if (!string.IsNullOrWhiteSpace(Company.Phone))
            {
                if (Company.Phone.Length > 20)
                {
                    Company.AddError(nameof(CompanyValidator), nameof(Customer.Phone), ErrorCode.PhoneOverLength);
                }
                else
                {
                    CompanyFilter CompanyFilter = new CompanyFilter
                    {
                        Skip = 0,
                        Take = 10,
                        Id = new IdFilter { NotEqual = Company.Id },
                        Phone = new StringFilter { Equal = Company.Phone }
                    };

                    int count = await UOW.CompanyRepository.Count(CompanyFilter);
                    if (count != 0)
                        Company.AddError(nameof(CompanyValidator), nameof(Company.Phone), ErrorCode.PhoneExisted);
                }
            }
            return Company.IsValidated;
        }

        public async Task<bool>Create(Company Company)
        {
            await ValidateName(Company);
            await ValidateAppUser(Company);
            await ValidateAddress(Company); 
            await ValidatePhone(Company); 
            return Company.IsValidated;
        }

        public async Task<bool> Update(Company Company)
        {
            if (await ValidateId(Company))
            {
                await ValidateName(Company);
                await ValidateAppUser(Company);
                await ValidateAddress(Company);
                await ValidatePhone(Company);
            }
            return Company.IsValidated;
        }

        public async Task<bool> Delete(Company Company)
        {
            if (await ValidateId(Company))
            {
            }
            return Company.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Company> Companys)
        {
            foreach (Company Company in Companys)
            {
                await Delete(Company);
            }
            return Companys.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Company> Companys)
        {
            return true;
        }
    }
}
