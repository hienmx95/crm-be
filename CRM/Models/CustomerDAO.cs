using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerDAO
    {
        public CustomerDAO()
        {
            Contracts = new HashSet<ContractDAO>();
            CustomerCallLogMappings = new HashSet<CustomerCallLogMappingDAO>();
            CustomerCustomerGroupingMappings = new HashSet<CustomerCustomerGroupingMappingDAO>();
            CustomerEmailHistories = new HashSet<CustomerEmailHistoryDAO>();
            CustomerEmails = new HashSet<CustomerEmailDAO>();
            CustomerFeedbacks = new HashSet<CustomerFeedbackDAO>();
            CustomerPhones = new HashSet<CustomerPhoneDAO>();
            CustomerPointHistories = new HashSet<CustomerPointHistoryDAO>();
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
            RepairTickets = new HashSet<RepairTicketDAO>();
            Stores = new HashSet<StoreDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

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
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual BusinessTypeDAO BusinessType { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CustomerResourceDAO CustomerResource { get; set; }
        public virtual CustomerTypeDAO CustomerType { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual NationDAO Nation { get; set; }
        public virtual CompanyDAO ParentCompany { get; set; }
        public virtual ProfessionDAO Profession { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual SexDAO Sex { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual WardDAO Ward { get; set; }
        public virtual ICollection<ContractDAO> Contracts { get; set; }
        public virtual ICollection<CustomerCallLogMappingDAO> CustomerCallLogMappings { get; set; }
        public virtual ICollection<CustomerCustomerGroupingMappingDAO> CustomerCustomerGroupingMappings { get; set; }
        public virtual ICollection<CustomerEmailHistoryDAO> CustomerEmailHistories { get; set; }
        public virtual ICollection<CustomerEmailDAO> CustomerEmails { get; set; }
        public virtual ICollection<CustomerFeedbackDAO> CustomerFeedbacks { get; set; }
        public virtual ICollection<CustomerPhoneDAO> CustomerPhones { get; set; }
        public virtual ICollection<CustomerPointHistoryDAO> CustomerPointHistories { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
        public virtual ICollection<RepairTicketDAO> RepairTickets { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
