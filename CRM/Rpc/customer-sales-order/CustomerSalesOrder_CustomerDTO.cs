using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_sales_order
{
    public class CustomerSalesOrder_CustomerDTO : DataDTO
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
        
        public Guid RowId { get; set; }
        public CustomerSalesOrder_CompanyDTO Company { get; set; }

        public CustomerSalesOrder_CustomerDTO() {}
        public CustomerSalesOrder_CustomerDTO(Customer Customer)
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
            
            this.RowId = Customer.RowId;
            this.Company = Customer.Company == null ? null : new CustomerSalesOrder_CompanyDTO(Customer.Company);
            
            this.Errors = Customer.Errors;
        }
    }

    public class CustomerSalesOrder_CustomerFilterDTO : FilterDTO
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
        
        public CustomerOrder OrderBy { get; set; }
    }
}