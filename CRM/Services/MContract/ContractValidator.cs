using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MContract
{
    public interface IContractValidator : IServiceScoped
    {
        Task<bool> Create(Contract Contract);
        Task<bool> Update(Contract Contract);
        Task<bool> Delete(Contract Contract);
        Task<bool> BulkDelete(List<Contract> Contracts);
        Task<bool> Import(List<Contract> Contracts);
    }

    public class ContractValidator : IContractValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CodeEmpty,
            ContractTypeEmpty,
            TotalValueEmpty,
            ValidityDateError,
            ExpirationDateError,
            ExpirationDateRangeError,
            CustomerEmpty,
            AppUserEmpty,
            InvoiceAddressError,
            ReceiveAddressError,
            CurrencyEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ContractValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Contract Contract)
        {
            ContractFilter ContractFilter = new ContractFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Contract.Id },
                Selects = ContractSelect.Id
            };

            int count = await UOW.ContractRepository.Count(ContractFilter);
            if (count == 0)
                Contract.AddError(nameof(ContractValidator), nameof(Contract.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateCode(Contract Contract)
        {
            if (string.IsNullOrWhiteSpace(Contract.Code))
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.Code), ErrorCode.CodeEmpty);
            } 
            return Contract.IsValidated;
        } 
        public async Task<bool> ValidateType(Contract Contract)
        {
            if (Contract.ContractTypeId == 0)
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.ContractType), ErrorCode.ContractTypeEmpty);
            }
            return Contract.IsValidated;
        }
        public async Task<bool> ValidateTotalValue(Contract Contract)
        {
            if (Contract.TotalValue == 0)
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.TotalValue), ErrorCode.TotalValueEmpty);
            }
            return Contract.IsValidated;
        }
        private async Task<bool> ValidateStartDateAndEndDate(Contract Contract)
        { 
            if (Contract.ValidityDate == null ||  Contract.ValidityDate == default(DateTime))
                Contract.AddError(nameof(ContractValidator), nameof(Contract.ValidityDate), ErrorCode.ValidityDateError);
            if (Contract.ExpirationDate == null || Contract.ExpirationDate == default(DateTime))
                Contract.AddError(nameof(ContractValidator), nameof(Contract.ExpirationDate), ErrorCode.ExpirationDateError);
             
            if (Contract.ExpirationDate != null)
            {
                if (Contract.ExpirationDate < Contract.ValidityDate)
                {
                    Contract.AddError(nameof(ContractValidator), nameof(Contract.ExpirationDate), ErrorCode.ExpirationDateRangeError);
                }
            } 
            return Contract.IsValidated;
        }
        public async Task<bool> ValidateCustomer(Contract Contract)
        {
            if (Contract.CustomerId == 0)
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.Customer), ErrorCode.CustomerEmpty);
            }
            return Contract.IsValidated;
        }
        public async Task<bool> ValidateAppUser(Contract Contract)
        {
            if (Contract.AppUserId == 0)
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.AppUser), ErrorCode.AppUserEmpty);
            }
            return Contract.IsValidated;
        }
        public async Task<bool> ValidateInvoiceAddress(Contract Contract)
        {
            if (string.IsNullOrWhiteSpace(Contract.InvoiceAddress))
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.InvoiceAddress), ErrorCode.InvoiceAddressError);
            }
            return Contract.IsValidated;
        }
        public async Task<bool> ValidateReceiveAddress(Contract Contract)
        {
            if (string.IsNullOrWhiteSpace(Contract.ReceiveAddress))
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.ReceiveAddress), ErrorCode.ReceiveAddressError);
            }
            return Contract.IsValidated;
        }
        public async Task<bool> ValidateCurrency(Contract Contract)
        {
            if (Contract.CurrencyId == 0)
            {
                Contract.AddError(nameof(ContractValidator), nameof(Contract.Currency), ErrorCode.CurrencyEmpty);
            }
            return Contract.IsValidated;
        }

        public async Task<bool>Create(Contract Contract)
        {
            await ValidateCode(Contract);
            await ValidateType(Contract);
            await ValidateTotalValue(Contract);
            await ValidateStartDateAndEndDate(Contract);
            await ValidateCustomer(Contract);
            await ValidateAppUser(Contract);
            await ValidateInvoiceAddress(Contract);
            await ValidateReceiveAddress(Contract);
            await ValidateCurrency(Contract);
            return Contract.IsValidated;
        }

        public async Task<bool> Update(Contract Contract)
        {
            if (await ValidateId(Contract))
            {
                await ValidateCode(Contract);
                await ValidateType(Contract);
                await ValidateTotalValue(Contract);
                await ValidateStartDateAndEndDate(Contract);
                await ValidateCustomer(Contract);
                await ValidateAppUser(Contract);
                await ValidateInvoiceAddress(Contract);
                await ValidateReceiveAddress(Contract);
                await ValidateCurrency(Contract);
            }
            return Contract.IsValidated;
        }

        public async Task<bool> Delete(Contract Contract)
        {
            if (await ValidateId(Contract))
            {
               
            }
            return Contract.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Contract> Contracts)
        {
            foreach (Contract Contract in Contracts)
            {
                await Delete(Contract);
            }
            return Contracts.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Contract> Contracts)
        {
            return true;
        }
    }
}
