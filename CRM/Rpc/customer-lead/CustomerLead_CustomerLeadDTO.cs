using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string TelePhone { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SecondEmail { get; set; }
        public string Website { get; set; }
        public long? CustomerLeadSourceId { get; set; }
        public long? CustomerLeadLevelId { get; set; }
        public long? CampaignId { get; set; }
        public long? ProfessionId { get; set; }
        public decimal? Revenue { get; set; }
        public long? EmployeeQuantity { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? CustomerLeadStatusId { get; set; }
        public string BusinessRegistrationCode { get; set; }
        public long? SexId { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public string Description { get; set; }
        public long? AppUserId { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public string ZipCode { get; set; }
        public long? CurrencyId { get; set; }
        public Guid RowId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerLead_CustomerLeadLevelDTO CustomerLeadLevel { get; set; }
        public CustomerLead_CustomerLeadSourceDTO CustomerLeadSource { get; set; }
        public CustomerLead_CustomerLeadStatusDTO CustomerLeadStatus { get; set; }
        public CustomerLead_AppUserDTO Creator { get; set; }
        public CustomerLead_DistrictDTO District { get; set; }
        public CustomerLead_ProfessionDTO Profession { get; set; }
        public CustomerLead_NationDTO Nation { get; set; }
        public CustomerLead_ProvinceDTO Province { get; set; }
        public CustomerLead_SexDTO Sex { get; set; }
        public CustomerLead_AppUserDTO AppUser { get; set; } 
        public CustomerLead_CurrencyDTO Currency { get; set; } 

        public List<CustomerLead_CustomerLeadEmailDTO> CustomerLeadEmails { get; set; }
        public List<CustomerLead_CustomerLeadFileGroupDTO> CustomerLeadFileGroups { get; set; }
        public List<CustomerLead_CustomerLeadItemMappingDTO> CustomerLeadItemMappings { get; set; }

        public bool IsNewCompany { get; set; }
        public bool IsNewContact { get; set; }
        public bool IsCreateOpportunity { get; set; }
        public bool IsNewOpportunity { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public long? OpportunityId { get; set; }
        public CustomerLead_CompanyDTO Company { get; set; }
        public CustomerLead_ContactDTO Contact { get; set; }
        public CustomerLead_OpportunityDTO Opportunity { get; set; }

        public CustomerLead_CustomerLeadDTO() { }
        public CustomerLead_CustomerLeadDTO(CustomerLead CustomerLead)
        {
            this.Id = CustomerLead.Id;
            this.CompanyName = CustomerLead.CompanyName;
            this.Name = CustomerLead.Name;
            this.TelePhone = CustomerLead.TelePhone;
            this.Phone = CustomerLead.Phone;
            this.Fax = CustomerLead.Fax;
            this.Email = CustomerLead.Email;
            this.SecondEmail = CustomerLead.SecondEmail;
            this.Website = CustomerLead.Website;
            this.CustomerLeadSourceId = CustomerLead.CustomerLeadSourceId;
            this.CustomerLeadLevelId = CustomerLead.CustomerLeadLevelId;
            this.CampaignId = CustomerLead.CampaignId;
            this.ProfessionId = CustomerLead.ProfessionId;
            this.Revenue = CustomerLead.Revenue;
            this.EmployeeQuantity = CustomerLead.EmployeeQuantity;
            this.Address = CustomerLead.Address;
            this.ProvinceId = CustomerLead.ProvinceId;
            this.DistrictId = CustomerLead.DistrictId;
            this.AppUserId = CustomerLead.AppUserId;
            this.CustomerLeadStatusId = CustomerLead.CustomerLeadStatusId;
            this.BusinessRegistrationCode = CustomerLead.BusinessRegistrationCode;
            this.SexId = CustomerLead.SexId;
            this.RefuseReciveSMS = CustomerLead.RefuseReciveSMS;
            this.NationId = CustomerLead.NationId;
            this.RefuseReciveEmail = CustomerLead.RefuseReciveEmail;
            this.Description = CustomerLead.Description;
            this.CurrencyId = CustomerLead.CurrencyId;
            this.CreatorId = CustomerLead.CreatorId;
            this.ZipCode = CustomerLead.ZipCode;
            this.CreatedAt = CustomerLead.CreatedAt;
            this.UpdatedAt = CustomerLead.UpdatedAt;
            this.IsCreateOpportunity = CustomerLead.IsCreateOpportunity;
            this.IsNewCompany = CustomerLead.IsNewCompany;
            this.IsNewContact = CustomerLead.IsNewContact;
            this.IsNewOpportunity = CustomerLead.IsNewOpportunity;
            this.CustomerLeadLevel = CustomerLead.CustomerLeadLevel == null ? null : new CustomerLead_CustomerLeadLevelDTO(CustomerLead.CustomerLeadLevel);
            this.CustomerLeadSource = CustomerLead.CustomerLeadSource == null ? null : new CustomerLead_CustomerLeadSourceDTO(CustomerLead.CustomerLeadSource);
            this.CustomerLeadStatus = CustomerLead.CustomerLeadStatus == null ? null : new CustomerLead_CustomerLeadStatusDTO(CustomerLead.CustomerLeadStatus);
            this.District = CustomerLead.District == null ? null : new CustomerLead_DistrictDTO(CustomerLead.District);
            this.Profession = CustomerLead.Profession == null ? null : new CustomerLead_ProfessionDTO(CustomerLead.Profession);
            this.Province = CustomerLead.Province == null ? null : new CustomerLead_ProvinceDTO(CustomerLead.Province);
            this.AppUser = CustomerLead.AppUser == null ? null : new CustomerLead_AppUserDTO(CustomerLead.AppUser);
            this.Nation = CustomerLead.Nation == null ? null : new CustomerLead_NationDTO(CustomerLead.Nation);
            this.Sex = CustomerLead.Sex == null ? null : new CustomerLead_SexDTO(CustomerLead.Sex);
            this.Currency = CustomerLead.Currency == null ? null : new CustomerLead_CurrencyDTO(CustomerLead.Currency);
            this.Creator = CustomerLead.Creator == null ? null : new CustomerLead_AppUserDTO(CustomerLead.Creator);
            this.Company = CustomerLead.Company == null ? null : new CustomerLead_CompanyDTO(CustomerLead.Company);
            this.Contact = CustomerLead.Contact == null ? null : new CustomerLead_ContactDTO(CustomerLead.Contact);
            this.Opportunity = CustomerLead.Opportunity == null ? null : new CustomerLead_OpportunityDTO(CustomerLead.Opportunity);
            this.CustomerLeadEmails = CustomerLead.CustomerLeadEmails?.Select(x => new CustomerLead_CustomerLeadEmailDTO(x)).ToList();
            this.CustomerLeadItemMappings = CustomerLead.CustomerLeadItemMappings?.Select(x => new CustomerLead_CustomerLeadItemMappingDTO(x)).ToList();
            this.CustomerLeadFileGroups = CustomerLead.CustomerLeadFileGroups?.Select(x => new CustomerLead_CustomerLeadFileGroupDTO(x)).ToList();
            this.Errors = CustomerLead.Errors;
        }

    }

    public class CustomerLead_CustomerLeadFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter CompanyName { get; set; }
        public StringFilter TelePhone { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Fax { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter SecondEmail { get; set; }
        public StringFilter Website { get; set; }
        public IdFilter CustomerLeadSourceId { get; set; }
        public IdFilter CustomerLeadLevelId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter CampaignId { get; set; }
        public IdFilter ProfessionId { get; set; }
        public DecimalFilter Revenue { get; set; }
        public LongFilter EmployeeQuantity { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter CustomerLeadStatusId { get; set; }
        public StringFilter BusinessRegistrationCode { get; set; }
        public IdFilter SexId { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public StringFilter ZipCode { get; set; }
        public IdFilter CurrencyId { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLeadOrder OrderBy { get; set; }
    }
}
