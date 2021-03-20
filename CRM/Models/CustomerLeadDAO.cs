using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadDAO
    {
        public CustomerLeadDAO()
        {
            Companies = new HashSet<CompanyDAO>();
            Contacts = new HashSet<ContactDAO>();
            CustomerLeadActivities = new HashSet<CustomerLeadActivityDAO>();
            CustomerLeadCallLogMappings = new HashSet<CustomerLeadCallLogMappingDAO>();
            CustomerLeadEmails = new HashSet<CustomerLeadEmailDAO>();
            CustomerLeadFileGroups = new HashSet<CustomerLeadFileGroupDAO>();
            CustomerLeadItemMappings = new HashSet<CustomerLeadItemMappingDAO>();
            Opportunities = new HashSet<OpportunityDAO>();
        }

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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual CustomerLeadLevelDAO CustomerLeadLevel { get; set; }
        public virtual CustomerLeadSourceDAO CustomerLeadSource { get; set; }
        public virtual CustomerLeadStatusDAO CustomerLeadStatus { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual NationDAO Nation { get; set; }
        public virtual ProfessionDAO Profession { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual SexDAO Sex { get; set; }
        public virtual ICollection<CompanyDAO> Companies { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<CustomerLeadActivityDAO> CustomerLeadActivities { get; set; }
        public virtual ICollection<CustomerLeadCallLogMappingDAO> CustomerLeadCallLogMappings { get; set; }
        public virtual ICollection<CustomerLeadEmailDAO> CustomerLeadEmails { get; set; }
        public virtual ICollection<CustomerLeadFileGroupDAO> CustomerLeadFileGroups { get; set; }
        public virtual ICollection<CustomerLeadItemMappingDAO> CustomerLeadItemMappings { get; set; }
        public virtual ICollection<OpportunityDAO> Opportunities { get; set; }
    }
}
