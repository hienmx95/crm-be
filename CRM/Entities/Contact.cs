using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Contact : DataEntity, IEquatable<Contact>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ProfessionId { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactStatusId { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? CustomerLeadId { get; set; }
        public long? ImageId { get; set; }
        public string Description { get; set; }
        public string EmailOther { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string PhoneHome { get; set; }
        public string FAX { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string ZIPCode { get; set; }
        public long? SexId { get; set; }
        public long? AppUserId { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public long? PositionId { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public AppUser AppUser { get; set; }
        public Company Company { get; set; }
        public ContactStatus ContactStatus { get; set; }
        public AppUser Creator { get; set; }
        public CustomerLead CustomerLead { get; set; }
        public District District { get; set; }
        public Image Image { get; set; }
        public Nation Nation { get; set; }
        public Organization Organization { get; set; }
        public Position Position { get; set; }
        public Profession Profession { get; set; }
        public Province Province { get; set; }
        public Sex Sex { get; set; }
        public List<ContactActivity> ContactActivities { get; set; }
        public List<ContactCallLogMapping> ContactCallLogMappings { get; set; }
        public List<ContactEmail> ContactEmails { get; set; }
        public List<ContactFileGrouping> ContactFileGroupings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Equals(Contact other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ContactFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter ProfessionId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter ContactStatusId { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter ImageId { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter EmailOther { get; set; }
        public DateFilter DateOfBirth { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter PhoneHome { get; set; }
        public StringFilter FAX { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Department { get; set; }
        public StringFilter ZIPCode { get; set; }
        public IdFilter SexId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter PositionId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<ContactFilter> OrFilter { get; set; }
        public ContactOrder OrderBy { get; set; }
        public ContactSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactOrder
    {
        Id = 0,
        Name = 1,
        Profession = 2,
        Company = 3,
        ContactStatus = 4,
        Address = 5,
        Nation = 6,
        Province = 7,
        District = 8,
        CustomerLead = 9,
        Image = 10,
        Description = 11,
        EmailOther = 12,
        DateOfBirth = 13,
        Phone = 14,
        PhoneHome = 15,
        FAX = 16,
        Email = 17,
        Department = 18,
        ZIPCode = 19,
        Sex = 20,
        AppUser = 21,
        RefuseReciveEmail = 22,
        RefuseReciveSMS = 23,
        Position = 24,
        Creator = 25,
        Organization = 26,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum ContactSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Profession = E._2,
        Company = E._3,
        ContactStatus = E._4,
        Address = E._5,
        Nation = E._6,
        Province = E._7,
        District = E._8,
        CustomerLead = E._9,
        Image = E._10,
        Description = E._11,
        EmailOther = E._12,
        DateOfBirth = E._13,
        Phone = E._14,
        PhoneHome = E._15,
        FAX = E._16,
        Email = E._17,
        Department = E._18,
        ZIPCode = E._19,
        Sex = E._20,
        AppUser = E._21,
        RefuseReciveEmail = E._22,
        RefuseReciveSMS = E._23,
        Position = E._24,
        Creator = E._25,
        Organization = E._26,
    }
}
