using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StatusDAO
    {
        public StatusDAO()
        {
            AppUsers = new HashSet<AppUserDAO>();
            Brands = new HashSet<BrandDAO>();
            CallEmotions = new HashSet<CallEmotionDAO>();
            CallTypes = new HashSet<CallTypeDAO>();
            Categories = new HashSet<CategoryDAO>();
            CustomerFeedbacks = new HashSet<CustomerFeedbackDAO>();
            CustomerGroupings = new HashSet<CustomerGroupingDAO>();
            CustomerLevels = new HashSet<CustomerLevelDAO>();
            CustomerResources = new HashSet<CustomerResourceDAO>();
            Customers = new HashSet<CustomerDAO>();
            Districts = new HashSet<DistrictDAO>();
            Items = new HashSet<ItemDAO>();
            KnowledgeArticles = new HashSet<KnowledgeArticleDAO>();
            KnowledgeGroups = new HashSet<KnowledgeGroupDAO>();
            KpiGeneralContents = new HashSet<KpiGeneralContentDAO>();
            KpiGenerals = new HashSet<KpiGeneralDAO>();
            KpiItems = new HashSet<KpiItemDAO>();
            MailTemplates = new HashSet<MailTemplateDAO>();
            Nations = new HashSet<NationDAO>();
            Organizations = new HashSet<OrganizationDAO>();
            Permissions = new HashSet<PermissionDAO>();
            PhoneTypes = new HashSet<PhoneTypeDAO>();
            Positions = new HashSet<PositionDAO>();
            ProductTypes = new HashSet<ProductTypeDAO>();
            Products = new HashSet<ProductDAO>();
            Professions = new HashSet<ProfessionDAO>();
            Provinces = new HashSet<ProvinceDAO>();
            Roles = new HashSet<RoleDAO>();
            ScheduleMasters = new HashSet<ScheduleMasterDAO>();
            SmsTemplates = new HashSet<SmsTemplateDAO>();
            StoreGroupings = new HashSet<StoreGroupingDAO>();
            StoreTypes = new HashSet<StoreTypeDAO>();
            Stores = new HashSet<StoreDAO>();
            Suppliers = new HashSet<SupplierDAO>();
            TaxTypes = new HashSet<TaxTypeDAO>();
            TicketGroups = new HashSet<TicketGroupDAO>();
            TicketIssueLevels = new HashSet<TicketIssueLevelDAO>();
            TicketPriorities = new HashSet<TicketPriorityDAO>();
            TicketSources = new HashSet<TicketSourceDAO>();
            TicketStatuses = new HashSet<TicketStatusDAO>();
            TicketTypes = new HashSet<TicketTypeDAO>();
            Tickets = new HashSet<TicketDAO>();
            UnitOfMeasureGroupings = new HashSet<UnitOfMeasureGroupingDAO>();
            UnitOfMeasures = new HashSet<UnitOfMeasureDAO>();
            Wards = new HashSet<WardDAO>();
            Warehouses = new HashSet<WarehouseDAO>();
            WorkflowDefinitions = new HashSet<WorkflowDefinitionDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AppUserDAO> AppUsers { get; set; }
        public virtual ICollection<BrandDAO> Brands { get; set; }
        public virtual ICollection<CallEmotionDAO> CallEmotions { get; set; }
        public virtual ICollection<CallTypeDAO> CallTypes { get; set; }
        public virtual ICollection<CategoryDAO> Categories { get; set; }
        public virtual ICollection<CustomerFeedbackDAO> CustomerFeedbacks { get; set; }
        public virtual ICollection<CustomerGroupingDAO> CustomerGroupings { get; set; }
        public virtual ICollection<CustomerLevelDAO> CustomerLevels { get; set; }
        public virtual ICollection<CustomerResourceDAO> CustomerResources { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<ItemDAO> Items { get; set; }
        public virtual ICollection<KnowledgeArticleDAO> KnowledgeArticles { get; set; }
        public virtual ICollection<KnowledgeGroupDAO> KnowledgeGroups { get; set; }
        public virtual ICollection<KpiGeneralContentDAO> KpiGeneralContents { get; set; }
        public virtual ICollection<KpiGeneralDAO> KpiGenerals { get; set; }
        public virtual ICollection<KpiItemDAO> KpiItems { get; set; }
        public virtual ICollection<MailTemplateDAO> MailTemplates { get; set; }
        public virtual ICollection<NationDAO> Nations { get; set; }
        public virtual ICollection<OrganizationDAO> Organizations { get; set; }
        public virtual ICollection<PermissionDAO> Permissions { get; set; }
        public virtual ICollection<PhoneTypeDAO> PhoneTypes { get; set; }
        public virtual ICollection<PositionDAO> Positions { get; set; }
        public virtual ICollection<ProductTypeDAO> ProductTypes { get; set; }
        public virtual ICollection<ProductDAO> Products { get; set; }
        public virtual ICollection<ProfessionDAO> Professions { get; set; }
        public virtual ICollection<ProvinceDAO> Provinces { get; set; }
        public virtual ICollection<RoleDAO> Roles { get; set; }
        public virtual ICollection<ScheduleMasterDAO> ScheduleMasters { get; set; }
        public virtual ICollection<SmsTemplateDAO> SmsTemplates { get; set; }
        public virtual ICollection<StoreGroupingDAO> StoreGroupings { get; set; }
        public virtual ICollection<StoreTypeDAO> StoreTypes { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
        public virtual ICollection<SupplierDAO> Suppliers { get; set; }
        public virtual ICollection<TaxTypeDAO> TaxTypes { get; set; }
        public virtual ICollection<TicketGroupDAO> TicketGroups { get; set; }
        public virtual ICollection<TicketIssueLevelDAO> TicketIssueLevels { get; set; }
        public virtual ICollection<TicketPriorityDAO> TicketPriorities { get; set; }
        public virtual ICollection<TicketSourceDAO> TicketSources { get; set; }
        public virtual ICollection<TicketStatusDAO> TicketStatuses { get; set; }
        public virtual ICollection<TicketTypeDAO> TicketTypes { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
        public virtual ICollection<UnitOfMeasureGroupingDAO> UnitOfMeasureGroupings { get; set; }
        public virtual ICollection<UnitOfMeasureDAO> UnitOfMeasures { get; set; }
        public virtual ICollection<WardDAO> Wards { get; set; }
        public virtual ICollection<WarehouseDAO> Warehouses { get; set; }
        public virtual ICollection<WorkflowDefinitionDAO> WorkflowDefinitions { get; set; }
    }
}
