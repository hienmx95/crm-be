using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MCustomerLead
{
    public interface ICustomerLeadValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLead CustomerLead);
        Task<bool> Convert(CustomerLead CustomerLead);
        Task<bool> Update(CustomerLead CustomerLead);
        Task<bool> Delete(CustomerLead CustomerLead);
        Task<bool> BulkDelete(List<CustomerLead> CustomerLeads);
        Task<bool> Import(List<CustomerLead> CustomerLeads);
    }

    public class CustomerLeadValidator : ICustomerLeadValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            CompanyIsNull,
            CompanyNameEmpty,
            CompanyEmpty,
            CompanyNotExisted,
            PhoneEmpty,
            PhoneOverLength,
            PhoneExisted,
            ContactIsNull,
            ContactEmpty,
            ContactNotExisted,
            OpportunityEmpty,
            OpportunityNotExisted,
            ClosingDateEmpty,
            ClosingDateWrong,
            ProbabilityEmpty,
            FaxOverLength
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLeadValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLead CustomerLead)
        {
            CustomerLeadFilter CustomerLeadFilter = new CustomerLeadFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLead.Id },
                Selects = CustomerLeadSelect.Id
            };

            int count = await UOW.CustomerLeadRepository.Count(CustomerLeadFilter);
            if (count == 0)
                CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateName(CustomerLead CustomerLead)
        {
            if (string.IsNullOrWhiteSpace(CustomerLead.Name))
            {
                CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Name), ErrorCode.NameEmpty);
            }
            else if (CustomerLead.Name.Length > 255)
            {
                CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Name), ErrorCode.NameOverLength);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateCompanyName(CustomerLead CustomerLead)
        {
            if (string.IsNullOrWhiteSpace(CustomerLead.Company.Name))
            {
                CustomerLead.Company.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Company.Name), ErrorCode.CompanyNameEmpty);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateCompany(CustomerLead CustomerLead)
        {
            if (CustomerLead.CompanyId.HasValue == false)
            {
                CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Company), ErrorCode.CompanyEmpty);
            }
            else
            {
                CompanyFilter CompanyFilter = new CompanyFilter
                {
                    Id = new IdFilter { Equal = CustomerLead.CompanyId }
                };

                var count = await UOW.CompanyRepository.Count(CompanyFilter);
                if (count == 0)
                    CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Company), ErrorCode.CompanyNotExisted);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidatePhone(CustomerLead CustomerLead)
        {
            if (string.IsNullOrWhiteSpace(CustomerLead.Contact.Phone))
            {
                CustomerLead.Contact.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact.Phone), ErrorCode.PhoneEmpty);
            }
            else if (CustomerLead.Contact.Phone.Length > 20)
            {
                CustomerLead.Contact.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact.Phone), ErrorCode.PhoneOverLength);
            }
            else
            {
                ContactFilter ContactFilter = new ContactFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = CustomerLead.Contact.Id },
                    Phone = new StringFilter { Equal = CustomerLead.Contact.Phone }
                };

                int count = await UOW.ContactRepository.Count(ContactFilter);
                if (count != 0)
                    CustomerLead.Contact.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact.Phone), ErrorCode.PhoneExisted);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateContactName(CustomerLead CustomerLead)
        {
            if (string.IsNullOrWhiteSpace(CustomerLead.Contact.Name))
            {
                CustomerLead.Contact.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact.Name), ErrorCode.NameEmpty);
            }
            else if (CustomerLead.Contact.Name.Length > 500)
            {
                CustomerLead.Contact.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact.Name), ErrorCode.NameOverLength);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateContact(CustomerLead CustomerLead)
        {
            if (CustomerLead.ContactId.HasValue == false)
            {
                CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact), ErrorCode.ContactEmpty);
            }
            else
            {
                ContactFilter ContactFilter = new ContactFilter
                {
                    Id = new IdFilter { Equal = CustomerLead.ContactId }
                };

                var count = await UOW.ContactRepository.Count(ContactFilter);
                if (count == 0)
                    CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact), ErrorCode.ContactNotExisted);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateOpportunityName(CustomerLead CustomerLead)
        {
            if (string.IsNullOrWhiteSpace(CustomerLead.Opportunity.Name))
            {
                CustomerLead.Opportunity.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Opportunity.Name), ErrorCode.NameEmpty);
            }
            else if (CustomerLead.Opportunity.Name.Length > 500)
            {
                CustomerLead.Opportunity.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Opportunity.Name), ErrorCode.NameOverLength);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateOpportunity(CustomerLead CustomerLead)
        {
            if (CustomerLead.OpportunityId.HasValue == false)
            {
                CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Opportunity), ErrorCode.OpportunityEmpty);
            }
            else
            {
                OpportunityFilter OpportunityFilter = new OpportunityFilter
                {
                    Id = new IdFilter { Equal = CustomerLead.OpportunityId }
                };

                var count = await UOW.OpportunityRepository.Count(OpportunityFilter);
                if (count == 0)
                    CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Opportunity), ErrorCode.OpportunityNotExisted);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> ValidateFax(CustomerLead CustomerLead)
        {
            if (!string.IsNullOrWhiteSpace(CustomerLead.Fax))
            {
                if (CustomerLead.Fax.Length > 10)
                {
                    CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Fax), ErrorCode.FaxOverLength);
                }
            }
            return CustomerLead.IsValidated;
        }
        public async Task<bool> Create(CustomerLead CustomerLead)
        {
            await ValidateName(CustomerLead);
            await ValidateFax(CustomerLead);
            return CustomerLead.IsValidated;
        }

        public async Task<bool> Convert(CustomerLead CustomerLead)
        {
            if (await ValidateId(CustomerLead))
            {
                if (CustomerLead.Company == null)
                {
                    CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Company), ErrorCode.CompanyIsNull);
                }
                else
                {
                    if (CustomerLead.IsNewCompany)
                    {
                        await ValidateCompanyName(CustomerLead);
                    }
                    else
                    {
                        await ValidateCompany(CustomerLead);
                    }
                }

                if (CustomerLead.Contact == null)
                {
                    CustomerLead.AddError(nameof(CustomerLeadValidator), nameof(CustomerLead.Contact), ErrorCode.ContactIsNull);
                }
                else
                {
                    if (CustomerLead.IsNewContact)
                    {
                        await ValidateContactName(CustomerLead);
                        await ValidatePhone(CustomerLead);
                    }
                    else
                    {
                        await ValidateContact(CustomerLead);
                    }
                }
                if (CustomerLead.IsCreateOpportunity)
                {
                    if (CustomerLead.IsNewOpportunity)
                    {
                        await ValidateOpportunityName(CustomerLead);
                    }
                    else
                    {
                        await ValidateOpportunity(CustomerLead);
                    }
                }
            }
                
            return CustomerLead.IsValidated;
        }

        public async Task<bool> Update(CustomerLead CustomerLead)
        {
            if (await ValidateId(CustomerLead))
            {
                await ValidateName(CustomerLead);
                await ValidateFax(CustomerLead);
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> Delete(CustomerLead CustomerLead)
        {
            if (await ValidateId(CustomerLead))
            {
            }
            return CustomerLead.IsValidated;
        }

        public async Task<bool> BulkDelete(List<CustomerLead> CustomerLeads)
        {
            foreach (CustomerLead CustomerLead in CustomerLeads)
            {
                await Delete(CustomerLead);
            }
            return CustomerLeads.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<CustomerLead> CustomerLeads)
        {
            return true;
        }
    }
}
