using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerDTO : DataDTO
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
        public bool Used { get; set; }
        public long AppUserId { get; set; }
        public long CreatorId { get; set; }
        public Guid RowId { get; set; }
        public Customer_AppUserDTO AppUser { get; set; }
        public Customer_BusinessTypeDTO BusinessType { get; set; }
        public Customer_CompanyDTO Company { get; set; }
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_CustomerResourceDTO CustomerResource { get; set; }
        public Customer_CustomerTypeDTO CustomerType { get; set; }
        public Customer_DistrictDTO District { get; set; }
        public Customer_NationDTO Nation { get; set; }
        public Customer_CompanyDTO ParentCompany { get; set; }
        public Customer_ProfessionDTO Profession { get; set; }
        public Customer_ProvinceDTO Province { get; set; }
        public Customer_SexDTO Sex { get; set; }
        public Customer_StatusDTO Status { get; set; }
        public Customer_WardDTO Ward { get; set; }
        public List<Customer_CustomerCustomerGroupingMappingDTO> CustomerCustomerGroupingMappings { get; set; }
        public List<Customer_CustomerEmailDTO> CustomerEmails { get; set; }
        public List<Customer_CustomerPhoneDTO> CustomerPhones { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_CustomerDTO() {}
        public Customer_CustomerDTO(Customer Customer)
        {
            this.Id = Customer.Id;
            this.Code = Customer.Code;
            this.Name = Customer.Name;
            this.Phone = Customer.Phone;
            this.Address = Customer.Address;
            this.NationId = Customer.NationId;
            this.ProvinceId = Customer.ProvinceId;
            this.DistrictId = Customer.DistrictId;
            this.WardId = Customer.WardId;
            this.CustomerTypeId = Customer.CustomerTypeId;
            this.Birthday = Customer.Birthday;
            this.Email = Customer.Email;
            this.ProfessionId = Customer.ProfessionId;
            this.CustomerResourceId = Customer.CustomerResourceId;
            this.SexId = Customer.SexId;
            this.StatusId = Customer.StatusId;
            this.CompanyId = Customer.CompanyId;
            this.ParentCompanyId = Customer.ParentCompanyId;
            this.TaxCode = Customer.TaxCode;
            this.Fax = Customer.Fax;
            this.Website = Customer.Website;
            this.NumberOfEmployee = Customer.NumberOfEmployee;
            this.BusinessTypeId = Customer.BusinessTypeId;
            this.Investment = Customer.Investment;
            this.RevenueAnnual = Customer.RevenueAnnual;
            this.IsSupplier = Customer.IsSupplier;
            this.Descreption = Customer.Descreption;
            this.Used = Customer.Used;
            this.AppUserId = Customer.AppUserId;
            this.CreatorId = Customer.CreatorId;
            this.RowId = Customer.RowId;
            this.BusinessType = Customer.BusinessType == null ? null : new Customer_BusinessTypeDTO(Customer.BusinessType);
            this.Company = Customer.Company == null ? null : new Customer_CompanyDTO(Customer.Company);
            this.AppUser = Customer.AppUser == null ? null : new Customer_AppUserDTO(Customer.AppUser);
            this.Creator = Customer.Creator == null ? null : new Customer_AppUserDTO(Customer.Creator);
            this.CustomerResource = Customer.CustomerResource == null ? null : new Customer_CustomerResourceDTO(Customer.CustomerResource);
            this.CustomerType = Customer.CustomerType == null ? null : new Customer_CustomerTypeDTO(Customer.CustomerType);
            this.District = Customer.District == null ? null : new Customer_DistrictDTO(Customer.District);
            this.Nation = Customer.Nation == null ? null : new Customer_NationDTO(Customer.Nation);
            this.ParentCompany = Customer.ParentCompany == null ? null : new Customer_CompanyDTO(Customer.ParentCompany);
            this.Profession = Customer.Profession == null ? null : new Customer_ProfessionDTO(Customer.Profession);
            this.Province = Customer.Province == null ? null : new Customer_ProvinceDTO(Customer.Province);
            this.Sex = Customer.Sex == null ? null : new Customer_SexDTO(Customer.Sex);
            this.Status = Customer.Status == null ? null : new Customer_StatusDTO(Customer.Status);
            this.Ward = Customer.Ward == null ? null : new Customer_WardDTO(Customer.Ward);
            this.CustomerCustomerGroupingMappings = Customer.CustomerCustomerGroupingMappings?.Select(x => new Customer_CustomerCustomerGroupingMappingDTO(x)).ToList();
            this.CustomerEmails = Customer.CustomerEmails?.Select(x => new Customer_CustomerEmailDTO(x)).ToList();
            this.CustomerPhones = Customer.CustomerPhones?.Select(x => new Customer_CustomerPhoneDTO(x)).ToList();
            this.CreatedAt = Customer.CreatedAt;
            this.UpdatedAt = Customer.UpdatedAt;
            this.Errors = Customer.Errors;
        }
    }

    public class Customer_CustomerFilterDTO : FilterDTO
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
        public IdFilter RowId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter CustomerGroupingId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerOrder OrderBy { get; set; }
    }
}
