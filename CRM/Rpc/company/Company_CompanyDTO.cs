using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyDTO : DataDTO
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
        public long? CurrencyId { get; set; }
        public long? CompanyStatusId { get; set; }
        public string Description { get; set; }
        public Guid RowId { get; set; }
        public Company_AppUserDTO AppUser { get; set; }
        public Company_CompanyStatusDTO CompanyStatus { get; set; }
        public Company_AppUserDTO Creator { get; set; }
        public Company_CurrencyDTO Currency { get; set; }
        public Company_CustomerLeadDTO CustomerLead { get; set; }
        public Company_DistrictDTO District { get; set; }
        public Company_NationDTO Nation { get; set; }
        public Company_CompanyDTO Parent { get; set; }
        public Company_ProfessionDTO Profession { get; set; }
        public Company_ProvinceDTO Province { get; set; }
        public List<Company_CompanyActivityDTO> CompanyActivities { get; set; }
        public List<Company_CompanyCallLogMappingDTO> CompanyCallLogMappings { get; set; }
        public List<Company_CompanyEmailDTO> CompanyEmails { get; set; }
        public List<Company_CompanyFileGroupingDTO> CompanyFileGroupings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Company_CompanyDTO() {}
        public Company_CompanyDTO(Company Company)
        {
            this.Id = Company.Id;
            this.Name = Company.Name;
            this.Phone = Company.Phone;
            this.FAX = Company.FAX;
            this.PhoneOther = Company.PhoneOther;
            this.Email = Company.Email;
            this.EmailOther = Company.EmailOther;
            this.ZIPCode = Company.ZIPCode;
            this.Revenue = Company.Revenue;
            this.Website = Company.Website;
            this.Address = Company.Address;
            this.NationId = Company.NationId;
            this.ProvinceId = Company.ProvinceId;
            this.DistrictId = Company.DistrictId;
            this.NumberOfEmployee = Company.NumberOfEmployee;
            this.RefuseReciveEmail = Company.RefuseReciveEmail;
            this.RefuseReciveSMS = Company.RefuseReciveSMS;
            this.CustomerLeadId = Company.CustomerLeadId;
            this.ParentId = Company.ParentId;
            this.Path = Company.Path;
            this.Level = Company.Level;
            this.ProfessionId = Company.ProfessionId;
            this.AppUserId = Company.AppUserId;
            this.CreatorId = Company.CreatorId;
            this.CurrencyId = Company.CurrencyId;
            this.CompanyStatusId = Company.CompanyStatusId;
            this.Description = Company.Description;
            this.RowId = Company.RowId;
            this.AppUser = Company.AppUser == null ? null : new Company_AppUserDTO(Company.AppUser);
            this.CompanyStatus = Company.CompanyStatus == null ? null : new Company_CompanyStatusDTO(Company.CompanyStatus);
            this.Creator = Company.Creator == null ? null : new Company_AppUserDTO(Company.Creator);
            this.Currency = Company.Currency == null ? null : new Company_CurrencyDTO(Company.Currency);
            this.CustomerLead = Company.CustomerLead == null ? null : new Company_CustomerLeadDTO(Company.CustomerLead);
            this.District = Company.District == null ? null : new Company_DistrictDTO(Company.District);
            this.Nation = Company.Nation == null ? null : new Company_NationDTO(Company.Nation);
            this.Parent = Company.Parent == null ? null : new Company_CompanyDTO(Company.Parent);
            this.Profession = Company.Profession == null ? null : new Company_ProfessionDTO(Company.Profession);
            this.Province = Company.Province == null ? null : new Company_ProvinceDTO(Company.Province);
            this.CompanyActivities = Company.CompanyActivities?.Select(x => new Company_CompanyActivityDTO(x)).ToList();
            this.CompanyCallLogMappings = Company.CompanyCallLogMappings?.Select(x => new Company_CompanyCallLogMappingDTO(x)).ToList();
            this.CompanyEmails = Company.CompanyEmails?.Select(x => new Company_CompanyEmailDTO(x)).ToList();
            this.CompanyFileGroupings = Company.CompanyFileGroupings?.Select(x => new Company_CompanyFileGroupingDTO(x)).ToList();
            this.CreatedAt = Company.CreatedAt;
            this.UpdatedAt = Company.UpdatedAt;
            this.Errors = Company.Errors;
        }
    }

    public class Company_CompanyFilterDTO : FilterDTO
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
        public IdFilter CurrencyId { get; set; }
        public IdFilter CompanyStatusId { get; set; }
        public StringFilter Description { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CompanyOrder OrderBy { get; set; }
    }
}
