using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Company : DataEntity,  IEquatable<Company>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string FAX { get; set; }
        public string PhoneOther { get; set; }
        public string Email { get; set; }
        public string EmailOther { get; set; }
        public string ZIPCode { get; set; }
        public decimal? Revenue { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? NumberOfEmployee { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public long? CustomerLeadId { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public long? Level { get; set; }
        public long? ProfessionId { get; set; }
        public long? AppUserId { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public long? CurrencyId { get; set; }
        public long? CompanyStatusId { get; set; }
        public string Description { get; set; }
        public Guid RowId { get; set; }
        public AppUser AppUser { get; set; }
        public CompanyStatus CompanyStatus { get; set; }
        public AppUser Creator { get; set; }
        public Currency Currency { get; set; }
        public CustomerLead CustomerLead { get; set; }
        public District District { get; set; }
        public Nation Nation { get; set; }
        public Organization Organization { get; set; }
        public Company Parent { get; set; }
        public Profession Profession { get; set; }
        public Province Province { get; set; }
        public List<CompanyActivity> CompanyActivities { get; set; }
        public List<CompanyCallLogMapping> CompanyCallLogMappings { get; set; }
        public List<CompanyEmail> CompanyEmails { get; set; }
        public List<CompanyFileGrouping> CompanyFileGroupings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(Company other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CompanyFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter FAX { get; set; }
        public StringFilter PhoneOther { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter EmailOther { get; set; }
        public StringFilter ZIPCode { get; set; }
        public DecimalFilter Revenue { get; set; }
        public StringFilter Website { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public LongFilter NumberOfEmployee { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter ParentId { get; set; }
        public StringFilter Path { get; set; }
        public LongFilter Level { get; set; }
        public IdFilter ProfessionId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter CurrencyId { get; set; }
        public IdFilter CompanyStatusId { get; set; }
        public StringFilter Description { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CompanyFilter> OrFilter { get; set; }
        public CompanyOrder OrderBy {get; set;}
        public CompanySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyOrder
    {
        Id = 0,
        Name = 1,
        Phone = 2,
        FAX = 3,
        PhoneOther = 4,
        Email = 5,
        EmailOther = 6,
        ZIPCode = 7,
        Revenue = 8,
        Website = 9,
        Address = 10,
        Nation = 11,
        Province = 12,
        District = 13,
        NumberOfEmployee = 14,
        RefuseReciveEmail = 15,
        RefuseReciveSMS = 16,
        CustomerLead = 17,
        Parent = 18,
        Path = 19,
        Level = 20,
        Profession = 21,
        AppUser = 22,
        Creator = 23,
        Currency = 24,
        CompanyStatus = 25,
        Description = 26,
        Organization = 27,
        Row = 30,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CompanySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Phone = E._2,
        FAX = E._3,
        PhoneOther = E._4,
        Email = E._5,
        EmailOther = E._6,
        ZIPCode = E._7,
        Revenue = E._8,
        Website = E._9,
        Address = E._10,
        Nation = E._11,
        Province = E._12,
        District = E._13,
        NumberOfEmployee = E._14,
        RefuseReciveEmail = E._15,
        RefuseReciveSMS = E._16,
        CustomerLead = E._17,
        Parent = E._18,
        Path = E._19,
        Level = E._20,
        Profession = E._21,
        AppUser = E._22,
        Creator = E._23,
        Currency = E._24,
        CompanyStatus = E._25,
        Description = E._26,
        Organization = E._27,
        Row = E._30,
    }
}
