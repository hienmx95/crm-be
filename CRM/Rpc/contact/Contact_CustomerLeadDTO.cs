using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_CustomerLeadDTO : DataDTO
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

        public long? CompanyId { get; set; }

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

        public string ZipCode { get; set; }

        public long? CurrencyId { get; set; }

        public Guid RowId { get; set; }


        public Contact_CustomerLeadDTO() { }
        public Contact_CustomerLeadDTO(CustomerLead CustomerLead)
        {

            this.Id = CustomerLead.Id;

            this.Name = CustomerLead.Name;

            this.CompanyName = CustomerLead.CompanyName;

            this.TelePhone = CustomerLead.TelePhone;

            this.Phone = CustomerLead.Phone;

            this.Fax = CustomerLead.Fax;

            this.Email = CustomerLead.Email;

            this.SecondEmail = CustomerLead.SecondEmail;

            this.Website = CustomerLead.Website;

            this.CustomerLeadSourceId = CustomerLead.CustomerLeadSourceId;

            this.CustomerLeadLevelId = CustomerLead.CustomerLeadLevelId;

            this.CompanyId = CustomerLead.CompanyId;

            this.CampaignId = CustomerLead.CampaignId;

            this.ProfessionId = CustomerLead.ProfessionId;

            this.Revenue = CustomerLead.Revenue;

            this.EmployeeQuantity = CustomerLead.EmployeeQuantity;

            this.Address = CustomerLead.Address;

            this.NationId = CustomerLead.NationId;

            this.ProvinceId = CustomerLead.ProvinceId;

            this.DistrictId = CustomerLead.DistrictId;

            this.CustomerLeadStatusId = CustomerLead.CustomerLeadStatusId;

            this.BusinessRegistrationCode = CustomerLead.BusinessRegistrationCode;

            this.SexId = CustomerLead.SexId;

            this.RefuseReciveSMS = CustomerLead.RefuseReciveSMS;

            this.RefuseReciveEmail = CustomerLead.RefuseReciveEmail;

            this.Description = CustomerLead.Description;

            this.AppUserId = CustomerLead.AppUserId;

            this.CreatorId = CustomerLead.CreatorId;

            this.ZipCode = CustomerLead.ZipCode;

            this.CurrencyId = CustomerLead.CurrencyId;

            this.RowId = CustomerLead.RowId;

            this.Errors = CustomerLead.Errors;
        }
    }

    public class Contact_CustomerLeadFilterDTO : FilterDTO
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

        public StringFilter ZipCode { get; set; }

        public IdFilter CurrencyId { get; set; }

        public GuidFilter RowId { get; set; }

        public CustomerLeadOrder OrderBy { get; set; }
    }
}