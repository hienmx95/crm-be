using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Customer : DataEntity, IEquatable<Customer>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? WardId { get; set; }
        public long CustomerTypeId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public long? ProfessionId { get; set; }
        public long? CustomerResourceId { get; set; }
        public long? SexId { get; set; }
        public long StatusId { get; set; }
        public long? CompanyId { get; set; }
        public long? ParentCompanyId { get; set; }
        public string TaxCode { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public long? NumberOfEmployee { get; set; }
        public long? BusinessTypeId { get; set; }
        public decimal? Investment { get; set; }
        public decimal? RevenueAnnual { get; set; }
        public bool? IsSupplier { get; set; }
        public string Descreption { get; set; }
        public long AppUserId { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }
        public AppUser AppUser { get; set; }
        public BusinessType BusinessType { get; set; }
        public Company Company { get; set; }
        public AppUser Creator { get; set; }
        public CustomerResource CustomerResource { get; set; }
        public CustomerType CustomerType { get; set; }
        public District District { get; set; }
        public Nation Nation { get; set; }
        public Organization Organization { get; set; }
        public Company ParentCompany { get; set; }
        public Profession Profession { get; set; }
        public Province Province { get; set; }
        public Sex Sex { get; set; }
        public Status Status { get; set; }
        public Ward Ward { get; set; }
        public List<CustomerCustomerGroupingMapping> CustomerCustomerGroupingMappings { get; set; }
        public List<CustomerEmail> CustomerEmails { get; set; }
        public List<CustomerPhone> CustomerPhones { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Equals(Customer other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter WardId { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public DateFilter Birthday { get; set; }
        public StringFilter Email { get; set; }
        public IdFilter ProfessionId { get; set; }
        public IdFilter CustomerResourceId { get; set; }
        public IdFilter SexId { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter ParentCompanyId { get; set; }
        public StringFilter TaxCode { get; set; }
        public StringFilter Fax { get; set; }
        public StringFilter Website { get; set; }
        public LongFilter NumberOfEmployee { get; set; }
        public IdFilter BusinessTypeId { get; set; }
        public DecimalFilter Investment { get; set; }
        public DecimalFilter RevenueAnnual { get; set; }
        public StringFilter Descreption { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter CustomerGroupingId { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerFilter> OrFilter { get; set; }
        public CustomerOrder OrderBy { get; set; }
        public CustomerSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Phone = 3,
        Address = 4,
        Nation = 5,
        Province = 6,
        District = 7,
        Ward = 8,
        CustomerType = 9,
        Birthday = 10,
        Email = 11,
        Profession = 12,
        CustomerResource = 13,
        Sex = 14,
        Status = 15,
        Company = 16,
        ParentCompany = 17,
        TaxCode = 18,
        Fax = 19,
        Website = 20,
        NumberOfEmployee = 21,
        BusinessType = 22,
        Investment = 23,
        RevenueAnnual = 24,
        IsSupplier = 25,
        Descreption = 26,
        Creator = 27,
        Used = 31,
        Row = 32,
        AppUser = 33,
        Organization = 34,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Phone = E._3,
        Address = E._4,
        Nation = E._5,
        Province = E._6,
        District = E._7,
        Ward = E._8,
        CustomerType = E._9,
        Birthday = E._10,
        Email = E._11,
        Profession = E._12,
        CustomerResource = E._13,
        Sex = E._14,
        Status = E._15,
        Company = E._16,
        ParentCompany = E._17,
        TaxCode = E._18,
        Fax = E._19,
        Website = E._20,
        NumberOfEmployee = E._21,
        BusinessType = E._22,
        Investment = E._23,
        RevenueAnnual = E._24,
        IsSupplier = E._25,
        Descreption = E._26,
        Creator = E._27,
        Used = E._31,
        Row = E._32,
        AppUser = E._33,
        Organization = E._34,
    }
}
