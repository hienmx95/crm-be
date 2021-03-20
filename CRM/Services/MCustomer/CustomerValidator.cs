using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MCustomer
{
    public interface ICustomerValidator : IServiceScoped
    {
        Task<bool> Create(Customer Customer);
        Task<bool> Update(Customer Customer);
        Task<bool> Delete(Customer Customer);
        Task<bool> BulkDelete(List<Customer> Customers);
        Task<bool> Import(List<Customer> Customers);
    }

    public class CustomerValidator : ICustomerValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CustomerTypeEmpty,
            CustomerTypeNotExisted,
            NameEmpty,
            NameOverLength,
            PhoneEmpty,
            PhoneOverLength,
            CompanyEmpty,
            CompanyNotExisted,
            StatusNotExisted
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Customer Customer)
        {
            CustomerFilter CustomerFilter = new CustomerFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Customer.Id },
                Selects = CustomerSelect.Id
            };

            int count = await UOW.CustomerRepository.Count(CustomerFilter);
            if (count == 0)
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateCustomerType(Customer Customer)
        {
            if(Customer.CustomerTypeId == 0)
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.CustomerType), ErrorCode.CustomerTypeEmpty);
            }
            else
            {
                var CustomerTypeIds = CustomerTypeEnum.CustomerTypeEnumList.Select(x => x.Id).ToList();
                if (!CustomerTypeIds.Contains(Customer.CustomerTypeId))
                {
                    Customer.AddError(nameof(CustomerValidator), nameof(Customer.CustomerType), ErrorCode.CustomerTypeNotExisted);
                }
            }
            return Customer.IsValidated;
        }

        private async Task<bool> ValidateName(Customer Customer)
        {
            if (string.IsNullOrWhiteSpace(Customer.Name))
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Name), ErrorCode.NameEmpty);
            }
            else if (Customer.Name.Length > 500)
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Name), ErrorCode.NameOverLength);
            }
            return Customer.IsValidated;
        }

        private async Task<bool> ValidatePhone(Customer Customer)
        {
            if (string.IsNullOrWhiteSpace(Customer.Phone))
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Phone), ErrorCode.PhoneEmpty);
            }
            else if (Customer.Phone.Length > 50)
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Phone), ErrorCode.PhoneOverLength);
            }
            return Customer.IsValidated;
        }

        private async Task<bool> ValidateCompany(Customer Customer)
        {
            if(Customer.CompanyId.HasValue == false)
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Company), ErrorCode.CompanyEmpty);
            }
            else
            {
                CompanyFilter CompanyFilter = new CompanyFilter
                {
                    Id = new IdFilter { Equal = Customer.CompanyId.Value }
                };
                var count = await UOW.CompanyRepository.Count(CompanyFilter);
                if(count == 0)
                {
                    Customer.AddError(nameof(CustomerValidator), nameof(Customer.Company), ErrorCode.CompanyNotExisted);
                }
            }
            return Customer.IsValidated;
        }

        private async Task<bool> ValidateStatus(Customer Customer)
        {
            var StatusIds = StatusEnum.StatusEnumList.Select(x => x.Id).ToList();
            if (!StatusIds.Contains(Customer.StatusId))
            {
                Customer.AddError(nameof(CustomerValidator), nameof(Customer.Status), ErrorCode.StatusNotExisted);
            }
            return Customer.IsValidated;
        }

        public async Task<bool>Create(Customer Customer)
        {
            await ValidateCustomerType(Customer);
            if (Customer.IsValidated)
            {
                if(Customer.CustomerTypeId == CustomerTypeEnum.RETAIL.Id)
                {
                    await ValidateName(Customer);
                    await ValidatePhone(Customer);
                    await ValidateStatus(Customer);
                }
                else
                {
                    await ValidateCompany(Customer);
                    await ValidateStatus(Customer);
                }
            }
            return Customer.IsValidated;
        }

        public async Task<bool> Update(Customer Customer)
        {
            if (await ValidateId(Customer))
            {
            }
            return Customer.IsValidated;
        }

        public async Task<bool> Delete(Customer Customer)
        {
            if (await ValidateId(Customer))
            {
            }
            return Customer.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Customer> Customers)
        {
            foreach (Customer Customer in Customers)
            {
                await Delete(Customer);
            }
            return Customers.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Customer> Customers)
        {
            return true;
        }
    }
}
