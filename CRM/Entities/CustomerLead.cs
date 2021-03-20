using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerLead : DataEntity,  IEquatable<CustomerLead>
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
        public AppUser AppUser { get; set; }
        public AppUser Creator { get; set; }
        public Currency Currency { get; set; }
        public CustomerLeadLevel CustomerLeadLevel { get; set; }
        public CustomerLeadSource CustomerLeadSource { get; set; }
        public CustomerLeadStatus CustomerLeadStatus { get; set; }
        public District District { get; set; }
        public Nation Nation { get; set; }
        public Organization Organization { get; set; }
        public Profession Profession { get; set; }
        public Province Province { get; set; }
        public Sex Sex { get; set; }
        public List<CustomerLeadActivity> CustomerLeadActivities { get; set; }
        public List<CustomerLeadCallLogMapping> CustomerLeadCallLogMappings { get; set; }
        public List<CustomerLeadEmail> CustomerLeadEmails { get; set; }
        public List<CustomerLeadFileGroup> CustomerLeadFileGroups { get; set; }
        public List<CustomerLeadItemMapping> CustomerLeadItemMappings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool IsNewCompany { get; set; }
        public bool IsNewContact { get; set; }
        public bool IsCreateOpportunity { get; set; }
        public bool IsNewOpportunity { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public long? OpportunityId { get; set; }
        public Company Company { get; set; }
        public Contact Contact { get; set; }
        public Opportunity Opportunity { get; set; }
        public bool Equals(CustomerLead other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    } 
    public class CustomerLeadFilter : FilterEntity
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
        public List<CustomerLeadFilter> OrFilter { get; set; }
        public CustomerLeadOrder OrderBy { get; set; }
        public CustomerLeadSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadOrder
    {
        Id = 0,
        Name = 1,
        CompanyName = 2,
        TelePhone = 3,
        Phone = 4,
        Fax = 5,
        Email = 6,
        SecondEmail = 7,
        Website = 8,
        CustomerLeadSource = 9,
        CustomerLeadLevel = 10,
        Company = 11,
        Campaign = 12,
        Profession = 13,
        Revenue = 14,
        EmployeeQuantity = 15,
        Address = 16,
        Nation = 17,
        Province = 18,
        District = 19,
        CustomerLeadStatus = 20,
        BusinessRegistrationCode = 21,
        Sex = 22,
        RefuseReciveSMS = 23,
        RefuseReciveEmail = 24,
        Description = 25,
        AppUser = 26,
        Creator = 27,
        ZipCode = 28,
        Currency = 29,
        Organization = 30,
        Row = 33,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerLeadSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        CompanyName = E._2,
        TelePhone = E._3,
        Phone = E._4,
        Fax = E._5,
        Email = E._6,
        SecondEmail = E._7,
        Website = E._8,
        CustomerLeadSource = E._9,
        CustomerLeadLevel = E._10,
        Company = E._11,
        Campaign = E._12,
        Profession = E._13,
        Revenue = E._14,
        EmployeeQuantity = E._15,
        Address = E._16,
        Nation = E._17,
        Province = E._18,
        District = E._19,
        CustomerLeadStatus = E._20,
        BusinessRegistrationCode = E._21,
        Sex = E._22,
        RefuseReciveSMS = E._23,
        RefuseReciveEmail = E._24,
        Description = E._25,
        AppUser = E._26,
        Creator = E._27,
        ZipCode = E._28,
        Currency = E._29,
        Organization = E._30,
        Row = E._33,
    }
}