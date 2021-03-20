using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class NationDAO
    {
        public NationDAO()
        {
            Companies = new HashSet<CompanyDAO>();
            Contacts = new HashSet<ContactDAO>();
            ContractInvoiceNations = new HashSet<ContractDAO>();
            ContractReceiveNations = new HashSet<ContractDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            CustomerSalesOrderDeliveryNations = new HashSet<CustomerSalesOrderDAO>();
            CustomerSalesOrderInvoiceNations = new HashSet<CustomerSalesOrderDAO>();
            Customers = new HashSet<CustomerDAO>();
            OrderQuoteInvoiceNations = new HashSet<OrderQuoteDAO>();
            OrderQuoteNations = new HashSet<OrderQuoteDAO>();
            Suppliers = new HashSet<SupplierDAO>();
        }

        public long Id { get; set; }
        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public string Code { get; set; }
        public string Name { get; set; }
        public long? Priority { get; set; }
        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public long StatusId { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CompanyDAO> Companies { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<ContractDAO> ContractInvoiceNations { get; set; }
        public virtual ICollection<ContractDAO> ContractReceiveNations { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderDeliveryNations { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderInvoiceNations { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteInvoiceNations { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteNations { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
    }
}
