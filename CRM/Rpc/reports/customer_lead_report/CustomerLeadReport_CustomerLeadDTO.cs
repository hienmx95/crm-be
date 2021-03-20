using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.customer_lead_report
{
    public class CustomerLeadReport_CustomerLeadDTO : DataDTO
    {
        public long STT { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string TelePhone { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SecondEmail { get; set; }
        public long? CompanyId { get; set; }
        public string Website { get; set; }
        public long? CustomerLeadSourceId { get; set; }
        public long? CustomerLeadLevelId { get; set; }
        public long? CampaignId { get; set; }
        public long? ProfessionId { get; set; }
        public decimal? Revenue { get; set; }
        public long? EmployeeQuantity { get; set; }
        public string Address { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? UserId { get; set; }
        public long? CustomerLeadStatusId { get; set; }
        public string BusinessRegistrationCode { get; set; }
        public long? SexId { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public long? NationId { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public string Description { get; set; }
        public bool? IsConvertContact { get; set; }
        public bool? IsConvertCompany { get; set; }
        public bool? IsConvertOpportunity { get; set; }
        public long? CreatorId { get; set; }
        public string ZipCode { get; set; }
        public CustomerLeadReport_CustomerLeadSourceDTO CustomerLeadSource { get; set; }
        public CustomerLeadReport_CustomerLeadStatusDTO CustomerLeadStatus { get; set; }
        public CustomerLeadReport_AppUserDTO User { get; set; }
        public CustomerLeadReport_AppUserDTO CreatorAppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerLeadReport_NationDTO Nation { get; set; }
        public CustomerLeadReport_ProvinceDTO Province { get; set; }
        public CustomerLeadReport_DistrictDTO District { get; set; }
        public CustomerLeadReport_ContactDTO Contact { get; set; }
        public CustomerLeadReport_OpportunityDTO Opportunity { get; set; }
        public CustomerLeadReport_CompanyDTO Company { get; set; }




        //public List<CustomerLeadReport_ProductMappingDTO> CustomerLeadProductMappings { get; set; }
        //public List<CustomerLeadReport_ProductDTO> Products { get; set; }
        //public CustomerLeadReport_CustomerLeadLevelDTO CustomerLeadLevel { get; set; }
        //public CustomerLeadReport_ProfessionDTO Profession { get; set; }
        //public CustomerLeadReport_SexDTO Sex { get; set; }

        public CustomerLeadReport_CustomerLeadDTO() { }
        public CustomerLeadReport_CustomerLeadDTO(CustomerLead CustomerLead)
        {
            this.Id = CustomerLead.Id;
            this.Name = CustomerLead.Name;
            this.TelePhone = CustomerLead.TelePhone;
            this.Phone = CustomerLead.Phone;
            this.Fax = CustomerLead.Fax;
            this.Email = CustomerLead.Email;
            this.SecondEmail = CustomerLead.SecondEmail;
            this.CompanyId = CustomerLead.CompanyId;
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
            this.UserId = CustomerLead.AppUserId;
            this.CustomerLeadStatusId = CustomerLead.CustomerLeadStatusId;
            this.BusinessRegistrationCode = CustomerLead.BusinessRegistrationCode;
            this.SexId = CustomerLead.SexId;
            this.RefuseReciveSMS = CustomerLead.RefuseReciveSMS;
            this.NationId = CustomerLead.NationId;
            this.RefuseReciveEmail = CustomerLead.RefuseReciveEmail;
            this.Description = CustomerLead.Description;
            this.CustomerLeadSource = CustomerLead.CustomerLeadSource == null ? null : new CustomerLeadReport_CustomerLeadSourceDTO(CustomerLead.CustomerLeadSource);
            this.CustomerLeadStatus = CustomerLead.CustomerLeadStatus == null ? null : new CustomerLeadReport_CustomerLeadStatusDTO(CustomerLead.CustomerLeadStatus);
            this.User = CustomerLead.AppUser == null ? null : new CustomerLeadReport_AppUserDTO(CustomerLead.AppUser);
            this.CreatedAt = CustomerLead.CreatedAt;
            this.UpdatedAt = CustomerLead.UpdatedAt;
            this.Errors = CustomerLead.Errors;
            this.Nation = CustomerLead.Nation == null ? null : new CustomerLeadReport_NationDTO(CustomerLead.Nation);
            this.District = CustomerLead.District == null ? null : new CustomerLeadReport_DistrictDTO(CustomerLead.District);
            this.Province = CustomerLead.Province == null ? null : new CustomerLeadReport_ProvinceDTO(CustomerLead.Province);

            this.Contact = CustomerLead.Contact == null ? null : new CustomerLeadReport_ContactDTO(CustomerLead.Contact);
            this.Opportunity = CustomerLead.Opportunity == null ? null : new CustomerLeadReport_OpportunityDTO(CustomerLead.Opportunity);
            this.Company = CustomerLead.Company == null ? null : new CustomerLeadReport_CompanyDTO(CustomerLead.Company);


            this.CreatorId = CustomerLead.CreatorId;
            this.CreatorAppUser = CustomerLead.Creator == null ? null : new CustomerLeadReport_AppUserDTO(CustomerLead.Creator);
            this.ZipCode = CustomerLead.ZipCode;


            //this.CustomerLeadLevel = CustomerLead.CustomerLeadLevel == null ? null : new CustomerLeadReport_CustomerLeadLevelDTO(CustomerLead.CustomerLeadLevel);
            //this.CustomerLeadProductMappings = CustomerLead.CustomerLeadProductMappings?.Select(x => new CustomerLeadReport_ProductMappingDTO(x)).ToList();
            //this.Sex = CustomerLead.Sex == null ? null : new CustomerLeadReport_SexDTO(CustomerLead.Sex);
            //this.Profession = CustomerLead.Profession == null ? null : new CustomerLeadReport_ProfessionDTO(CustomerLead.Profession);
            //this.Products = CustomerLead.CustomerLeadProductMappings?.Select(x => new CustomerLeadReport_ProductDTO(x.Product)).ToList();
        }

    }

    public class CustomerLeadReport_CustomerLeadFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter TelePhone { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Fax { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter SecondEmail { get; set; }
        public IdFilter CompanyId { get; set; }
        public StringFilter Website { get; set; }
        public IdFilter CustomerLeadSourceId { get; set; }
        public IdFilter CustomerLeadLevelId { get; set; }
        public IdFilter CampaignId { get; set; }
        public IdFilter ProfessionId { get; set; }
        public DecimalFilter Revenue { get; set; }
        public LongFilter EmployeeQuantity { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter UserId { get; set; }
        public IdFilter CustomerLeadStatusId { get; set; }
        public StringFilter BusinessRegistrationCode { get; set; }
        public IdFilter SexId { get; set; }
        public IdFilter NationId { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLeadOrder OrderBy { get; set; }
        public IdFilter CreatorId { get; set; }
    }
}
