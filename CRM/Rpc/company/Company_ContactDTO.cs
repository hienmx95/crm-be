using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_ContactDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ProfessionId { get; set; }
        public long? CompanyId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? NationId { get; set; }
        public long? CustomerLeadId { get; set; }
        public long? ImageId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string EmailOther { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneOther { get; set; }
        public string FAX { get; set; }
        public string Email { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public string ZIPCode { get; set; }
        public long? SexId { get; set; }
        public long? AppUserId { get; set; }
        public long? PositionId { get; set; }
        public string Department { get; set; }
        public long? ContactStatusId { get; set; }
        public Company_AppUserDTO AppUser { get; set; }
        public Company_CompanyDTO Company { get; set; }
        public Company_ContactStatusDTO ContactStatus { get; set; }
        public Company_CustomerLeadDTO CustomerLead { get; set; }
        public Company_DistrictDTO District { get; set; }
        public Company_ImageDTO Image { get; set; }
        public Company_NationDTO Nation { get; set; }
        public Company_PositionDTO Position { get; set; }
        public Company_ProfessionDTO Profession { get; set; }
        public Company_ProvinceDTO Province { get; set; }
        public Company_SexDTO Sex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Company_ContactDTO() {}
        public Company_ContactDTO(Contact Contact)
        {
            this.Id = Contact.Id;
            this.Name = Contact.Name;
            this.ProfessionId = Contact.ProfessionId;
            this.CompanyId = Contact.CompanyId;
            this.ProvinceId = Contact.ProvinceId;
            this.DistrictId = Contact.DistrictId;
            this.NationId = Contact.NationId;
            this.CustomerLeadId = Contact.CustomerLeadId;
            this.ImageId = Contact.ImageId;
            this.Description = Contact.Description;
            this.Address = Contact.Address;
            this.EmailOther = Contact.EmailOther;
            this.DateOfBirth = Contact.DateOfBirth;
            this.Phone = Contact.Phone;
            this.PhoneHome = Contact.PhoneHome;
            this.FAX = Contact.FAX;
            this.Email = Contact.Email;
            this.RefuseReciveEmail = Contact.RefuseReciveEmail;
            this.RefuseReciveSMS = Contact.RefuseReciveSMS;
            this.ZIPCode = Contact.ZIPCode;
            this.SexId = Contact.SexId;
            this.AppUserId = Contact.AppUserId;
            this.PositionId = Contact.PositionId;
            this.Department = Contact.Department;
            this.ContactStatusId = Contact.ContactStatusId;
            this.Company = Contact.Company == null ? null : new Company_CompanyDTO(Contact.Company);
            this.AppUser = Contact.AppUser == null ? null : new Company_AppUserDTO(Contact.AppUser);
            this.District = Contact.District == null ? null : new Company_DistrictDTO(Contact.District);
            this.Image = Contact.Image == null ? null : new Company_ImageDTO(Contact.Image);
            this.Profession = Contact.Profession == null ? null : new Company_ProfessionDTO(Contact.Profession);
            this.Nation = Contact.Nation == null ? null : new Company_NationDTO(Contact.Nation);
            this.Province = Contact.Province == null ? null : new Company_ProvinceDTO(Contact.Province);
            this.Sex = Contact.Sex == null ? null : new Company_SexDTO(Contact.Sex);
            this.ContactStatus = Contact.ContactStatus == null ? null : new Company_ContactStatusDTO(Contact.ContactStatus);
            this.Position = Contact.Position == null ? null : new Company_PositionDTO(Contact.Position);
            this.CustomerLead = Contact.CustomerLead == null ? null : new Company_CustomerLeadDTO(Contact.CustomerLead);
            this.CreatedAt = Contact.CreatedAt;
            this.UpdatedAt = Contact.UpdatedAt;
            this.Errors = Contact.Errors;
        }
    }

    public class Company_ContactFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter ProfessionId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter ImageId { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter EmailOther { get; set; }
        public DateFilter DateOfBirth { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter PhoneHome { get; set; }
        public StringFilter PhoneOther { get; set; }
        public StringFilter FAX { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter ZIPCode { get; set; }
        public IdFilter SexId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter PositionId { get; set; }
        public StringFilter Department { get; set; }
        public IdFilter ContactStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContactOrder OrderBy { get; set; }
    }
}