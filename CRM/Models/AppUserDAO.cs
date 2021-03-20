using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class AppUserDAO
    {
        public AppUserDAO()
        {
            AppUserRoleMappings = new HashSet<AppUserRoleMappingDAO>();
            AuditLogProperties = new HashSet<AuditLogPropertyDAO>();
            CallLogAppUsers = new HashSet<CallLogDAO>();
            CallLogCreators = new HashSet<CallLogDAO>();
            CompanyActivities = new HashSet<CompanyActivityDAO>();
            CompanyAppUsers = new HashSet<CompanyDAO>();
            CompanyCreators = new HashSet<CompanyDAO>();
            CompanyEmailCCMappings = new HashSet<CompanyEmailCCMappingDAO>();
            CompanyEmails = new HashSet<CompanyEmailDAO>();
            CompanyFileGroupings = new HashSet<CompanyFileGroupingDAO>();
            ContactActivities = new HashSet<ContactActivityDAO>();
            ContactAppUsers = new HashSet<ContactDAO>();
            ContactCreators = new HashSet<ContactDAO>();
            ContactEmailCCMappings = new HashSet<ContactEmailCCMappingDAO>();
            ContactEmails = new HashSet<ContactEmailDAO>();
            ContactFileGroupings = new HashSet<ContactFileGroupingDAO>();
            ContractAppUsers = new HashSet<ContractDAO>();
            ContractCreators = new HashSet<ContractDAO>();
            ContractFileGroupings = new HashSet<ContractFileGroupingDAO>();
            CustomerAppUsers = new HashSet<CustomerDAO>();
            CustomerCreators = new HashSet<CustomerDAO>();
            CustomerEmailHistories = new HashSet<CustomerEmailHistoryDAO>();
            CustomerLeadActivities = new HashSet<CustomerLeadActivityDAO>();
            CustomerLeadAppUsers = new HashSet<CustomerLeadDAO>();
            CustomerLeadCreators = new HashSet<CustomerLeadDAO>();
            CustomerLeadEmailCCMappings = new HashSet<CustomerLeadEmailCCMappingDAO>();
            CustomerLeadEmails = new HashSet<CustomerLeadEmailDAO>();
            CustomerLeadFileGroups = new HashSet<CustomerLeadFileGroupDAO>();
            CustomerSalesOrderCreators = new HashSet<CustomerSalesOrderDAO>();
            CustomerSalesOrderSalesEmployees = new HashSet<CustomerSalesOrderDAO>();
            DirectSalesOrderCreators = new HashSet<DirectSalesOrderDAO>();
            DirectSalesOrderSaleEmployees = new HashSet<DirectSalesOrderDAO>();
            Files = new HashSet<FileDAO>();
            InventoryHistories = new HashSet<InventoryHistoryDAO>();
            KnowledgeArticles = new HashSet<KnowledgeArticleDAO>();
            KpiGeneralCreators = new HashSet<KpiGeneralDAO>();
            KpiGeneralEmployees = new HashSet<KpiGeneralDAO>();
            KpiItemCreators = new HashSet<KpiItemDAO>();
            KpiItemEmployees = new HashSet<KpiItemDAO>();
            OpportunityActivities = new HashSet<OpportunityActivityDAO>();
            OpportunityAppUsers = new HashSet<OpportunityDAO>();
            OpportunityCreators = new HashSet<OpportunityDAO>();
            OpportunityEmailCCMappings = new HashSet<OpportunityEmailCCMappingDAO>();
            OpportunityEmails = new HashSet<OpportunityEmailDAO>();
            OpportunityFileGroupings = new HashSet<OpportunityFileGroupingDAO>();
            OrderQuoteAppUsers = new HashSet<OrderQuoteDAO>();
            OrderQuoteCreators = new HashSet<OrderQuoteDAO>();
            RepairTickets = new HashSet<RepairTicketDAO>();
            RequestWorkflowStepMappings = new HashSet<RequestWorkflowStepMappingDAO>();
            SLAAlertFRTUsers = new HashSet<SLAAlertFRTUserDAO>();
            SLAAlertUsers = new HashSet<SLAAlertUserDAO>();
            SLAEscalationFRTUsers = new HashSet<SLAEscalationFRTUserDAO>();
            SLAEscalationUsers = new HashSet<SLAEscalationUserDAO>();
            ScheduleMasterManagers = new HashSet<ScheduleMasterDAO>();
            ScheduleMasterSalers = new HashSet<ScheduleMasterDAO>();
            SmsQueues = new HashSet<SmsQueueDAO>();
            Stores = new HashSet<StoreDAO>();
            Suppliers = new HashSet<SupplierDAO>();
            TicketAppUserCloseds = new HashSet<TicketDAO>();
            TicketAppUserResolveds = new HashSet<TicketDAO>();
            TicketCreators = new HashSet<TicketDAO>();
            TicketOfUsers = new HashSet<TicketOfUserDAO>();
            TicketUsers = new HashSet<TicketDAO>();
            WorkflowDefinitionCreators = new HashSet<WorkflowDefinitionDAO>();
            WorkflowDefinitionModifiers = new HashSet<WorkflowDefinitionDAO>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Tên hiển thị
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Địa chỉ nhà
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string Phone { get; set; }
        public long SexId { get; set; }
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// Phòng ban
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Đơn vị công tác
        /// </summary>
        public long OrganizationId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        /// <summary>
        /// Trạng thái
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

        public virtual OrganizationDAO Organization { get; set; }
        public virtual SexDAO Sex { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<AppUserRoleMappingDAO> AppUserRoleMappings { get; set; }
        public virtual ICollection<AuditLogPropertyDAO> AuditLogProperties { get; set; }
        public virtual ICollection<CallLogDAO> CallLogAppUsers { get; set; }
        public virtual ICollection<CallLogDAO> CallLogCreators { get; set; }
        public virtual ICollection<CompanyActivityDAO> CompanyActivities { get; set; }
        public virtual ICollection<CompanyDAO> CompanyAppUsers { get; set; }
        public virtual ICollection<CompanyDAO> CompanyCreators { get; set; }
        public virtual ICollection<CompanyEmailCCMappingDAO> CompanyEmailCCMappings { get; set; }
        public virtual ICollection<CompanyEmailDAO> CompanyEmails { get; set; }
        public virtual ICollection<CompanyFileGroupingDAO> CompanyFileGroupings { get; set; }
        public virtual ICollection<ContactActivityDAO> ContactActivities { get; set; }
        public virtual ICollection<ContactDAO> ContactAppUsers { get; set; }
        public virtual ICollection<ContactDAO> ContactCreators { get; set; }
        public virtual ICollection<ContactEmailCCMappingDAO> ContactEmailCCMappings { get; set; }
        public virtual ICollection<ContactEmailDAO> ContactEmails { get; set; }
        public virtual ICollection<ContactFileGroupingDAO> ContactFileGroupings { get; set; }
        public virtual ICollection<ContractDAO> ContractAppUsers { get; set; }
        public virtual ICollection<ContractDAO> ContractCreators { get; set; }
        public virtual ICollection<ContractFileGroupingDAO> ContractFileGroupings { get; set; }
        public virtual ICollection<CustomerDAO> CustomerAppUsers { get; set; }
        public virtual ICollection<CustomerDAO> CustomerCreators { get; set; }
        public virtual ICollection<CustomerEmailHistoryDAO> CustomerEmailHistories { get; set; }
        public virtual ICollection<CustomerLeadActivityDAO> CustomerLeadActivities { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeadAppUsers { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeadCreators { get; set; }
        public virtual ICollection<CustomerLeadEmailCCMappingDAO> CustomerLeadEmailCCMappings { get; set; }
        public virtual ICollection<CustomerLeadEmailDAO> CustomerLeadEmails { get; set; }
        public virtual ICollection<CustomerLeadFileGroupDAO> CustomerLeadFileGroups { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderCreators { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrderSalesEmployees { get; set; }
        public virtual ICollection<DirectSalesOrderDAO> DirectSalesOrderCreators { get; set; }
        public virtual ICollection<DirectSalesOrderDAO> DirectSalesOrderSaleEmployees { get; set; }
        public virtual ICollection<FileDAO> Files { get; set; }
        public virtual ICollection<InventoryHistoryDAO> InventoryHistories { get; set; }
        public virtual ICollection<KnowledgeArticleDAO> KnowledgeArticles { get; set; }
        public virtual ICollection<KpiGeneralDAO> KpiGeneralCreators { get; set; }
        public virtual ICollection<KpiGeneralDAO> KpiGeneralEmployees { get; set; }
        public virtual ICollection<KpiItemDAO> KpiItemCreators { get; set; }
        public virtual ICollection<KpiItemDAO> KpiItemEmployees { get; set; }
        public virtual ICollection<OpportunityActivityDAO> OpportunityActivities { get; set; }
        public virtual ICollection<OpportunityDAO> OpportunityAppUsers { get; set; }
        public virtual ICollection<OpportunityDAO> OpportunityCreators { get; set; }
        public virtual ICollection<OpportunityEmailCCMappingDAO> OpportunityEmailCCMappings { get; set; }
        public virtual ICollection<OpportunityEmailDAO> OpportunityEmails { get; set; }
        public virtual ICollection<OpportunityFileGroupingDAO> OpportunityFileGroupings { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteAppUsers { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuoteCreators { get; set; }
        public virtual ICollection<RepairTicketDAO> RepairTickets { get; set; }
        public virtual ICollection<RequestWorkflowStepMappingDAO> RequestWorkflowStepMappings { get; set; }
        public virtual ICollection<SLAAlertFRTUserDAO> SLAAlertFRTUsers { get; set; }
        public virtual ICollection<SLAAlertUserDAO> SLAAlertUsers { get; set; }
        public virtual ICollection<SLAEscalationFRTUserDAO> SLAEscalationFRTUsers { get; set; }
        public virtual ICollection<SLAEscalationUserDAO> SLAEscalationUsers { get; set; }
        public virtual ICollection<ScheduleMasterDAO> ScheduleMasterManagers { get; set; }
        public virtual ICollection<ScheduleMasterDAO> ScheduleMasterSalers { get; set; }
        public virtual ICollection<SmsQueueDAO> SmsQueues { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
        public virtual ICollection<TicketDAO> TicketAppUserCloseds { get; set; }
        public virtual ICollection<TicketDAO> TicketAppUserResolveds { get; set; }
        public virtual ICollection<TicketDAO> TicketCreators { get; set; }
        public virtual ICollection<TicketOfUserDAO> TicketOfUsers { get; set; }
        public virtual ICollection<TicketDAO> TicketUsers { get; set; }
        public virtual ICollection<WorkflowDefinitionDAO> WorkflowDefinitionCreators { get; set; }
        public virtual ICollection<WorkflowDefinitionDAO> WorkflowDefinitionModifiers { get; set; }
    }
}
