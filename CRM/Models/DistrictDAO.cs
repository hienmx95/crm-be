using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class DistrictDAO
    {
        public DistrictDAO()
        {
            Companies = new HashSet<CompanyDAO>();
            Contacts = new HashSet<ContactDAO>();
            ContractInvoiceDistricts = new HashSet<ContractDAO>();
            ContractReceiveDistricts = new HashSet<ContractDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            CustomerSalesOrderDeliveryDistricts = new HashSet<CustomerSalesOrderDAO>();
            CustomerSalesOrderInvoiceDistricts = new HashSet<CustomerSalesOrderDAO>();
            Customers = new HashSet<CustomerDAO>();
            OrderQuoteDistricts = new HashSet<OrderQuoteDAO>();
            OrderQuoteInvoiceDistricts = new HashSet<OrderQuoteDAO>();
            Stores = new HashSet<StoreDAO>();
            Suppliers = new HashSet<SupplierDAO>();
            Wards = new HashSet<WardDAO>();
            Warehouses = new HashSet<WarehouseDAO>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Tên quận huyện
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Thứ tự ưu tiên
        /// </summary>
        public long? Priority { get; set; }
        /// <summary>
        /// Tỉnh phụ thuộc
        /// </summary>
        public long ProvinceId { get; set; }
        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public long StatusId { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Trường để đồng bộ
        /// </summary>
        public Guid RowId { get; set; }
        public bool Used { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CompanyDAO> Companies { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<ContractDAO> ContractInvoiceDistricts { get; set; }
        public virtual ICollection<ContractDAO> ContractReceiveDistricts { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderDeliveryDistricts { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderInvoiceDistricts { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteDistricts { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteInvoiceDistricts { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
        public virtual ICollection<WardDAO> Wards { get; set; }
        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
    }
}
