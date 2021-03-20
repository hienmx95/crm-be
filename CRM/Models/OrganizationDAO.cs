using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrganizationDAO
    {
        public OrganizationDAO()
        {
            AppUsers = new HashSet<AppUserDAO>();
            Contracts = new HashSet<ContractDAO>();
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
            DirectSalesOrders = new HashSet<DirectSalesOrderDAO>();
            InverseParent = new HashSet<OrganizationDAO>();
            KnowledgeArticleOrganizationMappings = new HashSet<KnowledgeArticleOrganizationMappingDAO>();
            KpiGenerals = new HashSet<KpiGeneralDAO>();
            KpiItems = new HashSet<KpiItemDAO>();
            Notifications = new HashSet<NotificationDAO>();
            Stores = new HashSet<StoreDAO>();
            TicketOfDepartments = new HashSet<TicketOfDepartmentDAO>();
            Tickets = new HashSet<TicketDAO>();
            Warehouses = new HashSet<WarehouseDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public long Level { get; set; }
        public long StatusId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }

        public virtual OrganizationDAO Parent { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<AppUserDAO> AppUsers { get; set; }
        public virtual ICollection<ContractDAO> Contracts { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
        public virtual ICollection<DirectSalesOrderDAO> DirectSalesOrders { get; set; }
        public virtual ICollection<OrganizationDAO> InverseParent { get; set; }
        public virtual ICollection<KnowledgeArticleOrganizationMappingDAO> KnowledgeArticleOrganizationMappings { get; set; }
        public virtual ICollection<KpiGeneralDAO> KpiGenerals { get; set; }
        public virtual ICollection<KpiItemDAO> KpiItems { get; set; }
        public virtual ICollection<NotificationDAO> Notifications { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
        public virtual ICollection<TicketOfDepartmentDAO> TicketOfDepartments { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
    }
}
