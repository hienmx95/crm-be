using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            Companies = new HashSet<CompanyDAO>();
            Contacts = new HashSet<ContactDAO>();
            ContractInvoiceProvinces = new HashSet<ContractDAO>();
            ContractReceiveProvinces = new HashSet<ContractDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            CustomerSalesOrderDeliveryProvinces = new HashSet<CustomerSalesOrderDAO>();
            CustomerSalesOrderInvoiceProvinces = new HashSet<CustomerSalesOrderDAO>();
            Customers = new HashSet<CustomerDAO>();
            Districts = new HashSet<DistrictDAO>();
            OrderQuoteInvoiceProvinces = new HashSet<OrderQuoteDAO>();
            OrderQuoteProvinces = new HashSet<OrderQuoteDAO>();
            Stores = new HashSet<StoreDAO>();
            Suppliers = new HashSet<SupplierDAO>();
            Warehouses = new HashSet<WarehouseDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? Priority { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CompanyDAO> Companies { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<ContractDAO> ContractInvoiceProvinces { get; set; }
        public virtual ICollection<ContractDAO> ContractReceiveProvinces { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderDeliveryProvinces { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderInvoiceProvinces { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteInvoiceProvinces { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteProvinces { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
    }
}
