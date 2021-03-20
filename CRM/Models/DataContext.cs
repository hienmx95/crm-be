using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRM.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<ActionDAO> Action { get; set; }
        public virtual DbSet<ActionPageMappingDAO> ActionPageMapping { get; set; }
        public virtual DbSet<ActivityPriorityDAO> ActivityPriority { get; set; }
        public virtual DbSet<ActivityStatusDAO> ActivityStatus { get; set; }
        public virtual DbSet<ActivityTypeDAO> ActivityType { get; set; }
        public virtual DbSet<AppUserDAO> AppUser { get; set; }
        public virtual DbSet<AppUserPermissionDAO> AppUserPermission { get; set; }
        public virtual DbSet<AppUserRoleMappingDAO> AppUserRoleMapping { get; set; }
        public virtual DbSet<AuditLogPropertyDAO> AuditLogProperty { get; set; }
        public virtual DbSet<BrandDAO> Brand { get; set; }
        public virtual DbSet<BusinessConcentrationLevelDAO> BusinessConcentrationLevel { get; set; }
        public virtual DbSet<BusinessTypeDAO> BusinessType { get; set; }
        public virtual DbSet<CallCategoryDAO> CallCategory { get; set; }
        public virtual DbSet<CallEmotionDAO> CallEmotion { get; set; }
        public virtual DbSet<CallLogDAO> CallLog { get; set; }
        public virtual DbSet<CallStatusDAO> CallStatus { get; set; }
        public virtual DbSet<CallTypeDAO> CallType { get; set; }
        public virtual DbSet<CategoryDAO> Category { get; set; }
        public virtual DbSet<ColorDAO> Color { get; set; }
        public virtual DbSet<CompanyDAO> Company { get; set; }
        public virtual DbSet<CompanyActivityDAO> CompanyActivity { get; set; }
        public virtual DbSet<CompanyCallLogMappingDAO> CompanyCallLogMapping { get; set; }
        public virtual DbSet<CompanyEmailDAO> CompanyEmail { get; set; }
        public virtual DbSet<CompanyEmailCCMappingDAO> CompanyEmailCCMapping { get; set; }
        public virtual DbSet<CompanyFileGroupingDAO> CompanyFileGrouping { get; set; }
        public virtual DbSet<CompanyFileMappingDAO> CompanyFileMapping { get; set; }
        public virtual DbSet<CompanyStatusDAO> CompanyStatus { get; set; }
        public virtual DbSet<ConsultingServiceDAO> ConsultingService { get; set; }
        public virtual DbSet<ContactDAO> Contact { get; set; }
        public virtual DbSet<ContactActivityDAO> ContactActivity { get; set; }
        public virtual DbSet<ContactCallLogMappingDAO> ContactCallLogMapping { get; set; }
        public virtual DbSet<ContactEmailDAO> ContactEmail { get; set; }
        public virtual DbSet<ContactEmailCCMappingDAO> ContactEmailCCMapping { get; set; }
        public virtual DbSet<ContactFileGroupingDAO> ContactFileGrouping { get; set; }
        public virtual DbSet<ContactFileMappingDAO> ContactFileMapping { get; set; }
        public virtual DbSet<ContactStatusDAO> ContactStatus { get; set; }
        public virtual DbSet<ContractDAO> Contract { get; set; }
        public virtual DbSet<ContractContactMappingDAO> ContractContactMapping { get; set; }
        public virtual DbSet<ContractFileGroupingDAO> ContractFileGrouping { get; set; }
        public virtual DbSet<ContractFileMappingDAO> ContractFileMapping { get; set; }
        public virtual DbSet<ContractItemDetailDAO> ContractItemDetail { get; set; }
        public virtual DbSet<ContractPaymentHistoryDAO> ContractPaymentHistory { get; set; }
        public virtual DbSet<ContractStatusDAO> ContractStatus { get; set; }
        public virtual DbSet<ContractTypeDAO> ContractType { get; set; }
        public virtual DbSet<CooperativeAttitudeDAO> CooperativeAttitude { get; set; }
        public virtual DbSet<CurrencyDAO> Currency { get; set; }
        public virtual DbSet<CustomerDAO> Customer { get; set; }
        public virtual DbSet<CustomerCCEmailHistoryDAO> CustomerCCEmailHistory { get; set; }
        public virtual DbSet<CustomerCallLogMappingDAO> CustomerCallLogMapping { get; set; }
        public virtual DbSet<CustomerCustomerGroupingMappingDAO> CustomerCustomerGroupingMapping { get; set; }
        public virtual DbSet<CustomerEmailDAO> CustomerEmail { get; set; }
        public virtual DbSet<CustomerEmailHistoryDAO> CustomerEmailHistory { get; set; }
        public virtual DbSet<CustomerFeedbackDAO> CustomerFeedback { get; set; }
        public virtual DbSet<CustomerFeedbackTypeDAO> CustomerFeedbackType { get; set; }
        public virtual DbSet<CustomerGroupingDAO> CustomerGrouping { get; set; }
        public virtual DbSet<CustomerLeadDAO> CustomerLead { get; set; }
        public virtual DbSet<CustomerLeadActivityDAO> CustomerLeadActivity { get; set; }
        public virtual DbSet<CustomerLeadCallLogMappingDAO> CustomerLeadCallLogMapping { get; set; }
        public virtual DbSet<CustomerLeadEmailDAO> CustomerLeadEmail { get; set; }
        public virtual DbSet<CustomerLeadEmailCCMappingDAO> CustomerLeadEmailCCMapping { get; set; }
        public virtual DbSet<CustomerLeadFileGroupDAO> CustomerLeadFileGroup { get; set; }
        public virtual DbSet<CustomerLeadFileMappingDAO> CustomerLeadFileMapping { get; set; }
        public virtual DbSet<CustomerLeadItemMappingDAO> CustomerLeadItemMapping { get; set; }
        public virtual DbSet<CustomerLeadLevelDAO> CustomerLeadLevel { get; set; }
        public virtual DbSet<CustomerLeadSourceDAO> CustomerLeadSource { get; set; }
        public virtual DbSet<CustomerLeadStatusDAO> CustomerLeadStatus { get; set; }
        public virtual DbSet<CustomerLevelDAO> CustomerLevel { get; set; }
        public virtual DbSet<CustomerPhoneDAO> CustomerPhone { get; set; }
        public virtual DbSet<CustomerPointHistoryDAO> CustomerPointHistory { get; set; }
        public virtual DbSet<CustomerResourceDAO> CustomerResource { get; set; }
        public virtual DbSet<CustomerSalesOrderDAO> CustomerSalesOrder { get; set; }
        public virtual DbSet<CustomerSalesOrderContentDAO> CustomerSalesOrderContent { get; set; }
        public virtual DbSet<CustomerSalesOrderPaymentHistoryDAO> CustomerSalesOrderPaymentHistory { get; set; }
        public virtual DbSet<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotion { get; set; }
        public virtual DbSet<CustomerTypeDAO> CustomerType { get; set; }
        public virtual DbSet<DirectSalesOrderDAO> DirectSalesOrder { get; set; }
        public virtual DbSet<DirectSalesOrderContentDAO> DirectSalesOrderContent { get; set; }
        public virtual DbSet<DirectSalesOrderPromotionDAO> DirectSalesOrderPromotion { get; set; }
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<EditedPriceStatusDAO> EditedPriceStatus { get; set; }
        public virtual DbSet<EmailStatusDAO> EmailStatus { get; set; }
        public virtual DbSet<EmailTypeDAO> EmailType { get; set; }
        public virtual DbSet<EntityReferenceDAO> EntityReference { get; set; }
        public virtual DbSet<EventMessageDAO> EventMessage { get; set; }
        public virtual DbSet<F1_ResourceActionPageMappingDAO> F1_ResourceActionPageMapping { get; set; }
        public virtual DbSet<FieldDAO> Field { get; set; }
        public virtual DbSet<FieldTypeDAO> FieldType { get; set; }
        public virtual DbSet<FileDAO> File { get; set; }
        public virtual DbSet<FileTypeDAO> FileType { get; set; }
        public virtual DbSet<ImageDAO> Image { get; set; }
        public virtual DbSet<ImproveQualityServingDAO> ImproveQualityServing { get; set; }
        public virtual DbSet<InfulenceLevelMarketDAO> InfulenceLevelMarket { get; set; }
        public virtual DbSet<InventoryDAO> Inventory { get; set; }
        public virtual DbSet<InventoryHistoryDAO> InventoryHistory { get; set; }
        public virtual DbSet<ItemDAO> Item { get; set; }
        public virtual DbSet<ItemImageMappingDAO> ItemImageMapping { get; set; }
        public virtual DbSet<KMSStatusDAO> KMSStatus { get; set; }
        public virtual DbSet<KnowledgeArticleDAO> KnowledgeArticle { get; set; }
        public virtual DbSet<KnowledgeArticleKeywordDAO> KnowledgeArticleKeyword { get; set; }
        public virtual DbSet<KnowledgeArticleOrganizationMappingDAO> KnowledgeArticleOrganizationMapping { get; set; }
        public virtual DbSet<KnowledgeGroupDAO> KnowledgeGroup { get; set; }
        public virtual DbSet<KpiCriteriaGeneralDAO> KpiCriteriaGeneral { get; set; }
        public virtual DbSet<KpiCriteriaItemDAO> KpiCriteriaItem { get; set; }
        public virtual DbSet<KpiGeneralDAO> KpiGeneral { get; set; }
        public virtual DbSet<KpiGeneralContentDAO> KpiGeneralContent { get; set; }
        public virtual DbSet<KpiGeneralContentKpiPeriodMappingDAO> KpiGeneralContentKpiPeriodMapping { get; set; }
        public virtual DbSet<KpiItemDAO> KpiItem { get; set; }
        public virtual DbSet<KpiItemContentDAO> KpiItemContent { get; set; }
        public virtual DbSet<KpiItemContentKpiCriteriaItemMappingDAO> KpiItemContentKpiCriteriaItemMapping { get; set; }
        public virtual DbSet<KpiPeriodDAO> KpiPeriod { get; set; }
        public virtual DbSet<KpiYearDAO> KpiYear { get; set; }
        public virtual DbSet<LastestEventMessageDAO> LastestEventMessage { get; set; }
        public virtual DbSet<MailTemplateDAO> MailTemplate { get; set; }
        public virtual DbSet<MarketPriceDAO> MarketPrice { get; set; }
        public virtual DbSet<MenuDAO> Menu { get; set; }
        public virtual DbSet<NationDAO> Nation { get; set; }
        public virtual DbSet<NotificationDAO> Notification { get; set; }
        public virtual DbSet<NotificationStatusDAO> NotificationStatus { get; set; }
        public virtual DbSet<OpportunityDAO> Opportunity { get; set; }
        public virtual DbSet<OpportunityActivityDAO> OpportunityActivity { get; set; }
        public virtual DbSet<OpportunityCallLogMappingDAO> OpportunityCallLogMapping { get; set; }
        public virtual DbSet<OpportunityContactMappingDAO> OpportunityContactMapping { get; set; }
        public virtual DbSet<OpportunityEmailDAO> OpportunityEmail { get; set; }
        public virtual DbSet<OpportunityEmailCCMappingDAO> OpportunityEmailCCMapping { get; set; }
        public virtual DbSet<OpportunityFileGroupingDAO> OpportunityFileGrouping { get; set; }
        public virtual DbSet<OpportunityFileMappingDAO> OpportunityFileMapping { get; set; }
        public virtual DbSet<OpportunityItemMappingDAO> OpportunityItemMapping { get; set; }
        public virtual DbSet<OpportunityResultTypeDAO> OpportunityResultType { get; set; }
        public virtual DbSet<OrderCategoryDAO> OrderCategory { get; set; }
        public virtual DbSet<OrderPaymentStatusDAO> OrderPaymentStatus { get; set; }
        public virtual DbSet<OrderQuoteDAO> OrderQuote { get; set; }
        public virtual DbSet<OrderQuoteContentDAO> OrderQuoteContent { get; set; }
        public virtual DbSet<OrderQuoteStatusDAO> OrderQuoteStatus { get; set; }
        public virtual DbSet<OrganizationDAO> Organization { get; set; }
        public virtual DbSet<PageDAO> Page { get; set; }
        public virtual DbSet<PaymentStatusDAO> PaymentStatus { get; set; }
        public virtual DbSet<PermissionDAO> Permission { get; set; }
        public virtual DbSet<PermissionActionMappingDAO> PermissionActionMapping { get; set; }
        public virtual DbSet<PermissionContentDAO> PermissionContent { get; set; }
        public virtual DbSet<PermissionOperatorDAO> PermissionOperator { get; set; }
        public virtual DbSet<PhoneTypeDAO> PhoneType { get; set; }
        public virtual DbSet<PositionDAO> Position { get; set; }
        public virtual DbSet<PotentialResultDAO> PotentialResult { get; set; }
        public virtual DbSet<ProbabilityDAO> Probability { get; set; }
        public virtual DbSet<ProductDAO> Product { get; set; }
        public virtual DbSet<ProductGroupingDAO> ProductGrouping { get; set; }
        public virtual DbSet<ProductImageMappingDAO> ProductImageMapping { get; set; }
        public virtual DbSet<ProductProductGroupingMappingDAO> ProductProductGroupingMapping { get; set; }
        public virtual DbSet<ProductTypeDAO> ProductType { get; set; }
        public virtual DbSet<ProfessionDAO> Profession { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<RatingStatusDAO> RatingStatus { get; set; }
        public virtual DbSet<RelationshipCustomerTypeDAO> RelationshipCustomerType { get; set; }
        public virtual DbSet<RepairStatusDAO> RepairStatus { get; set; }
        public virtual DbSet<RepairTicketDAO> RepairTicket { get; set; }
        public virtual DbSet<RequestStateDAO> RequestState { get; set; }
        public virtual DbSet<RequestWorkflowDefinitionMappingDAO> RequestWorkflowDefinitionMapping { get; set; }
        public virtual DbSet<RequestWorkflowParameterMappingDAO> RequestWorkflowParameterMapping { get; set; }
        public virtual DbSet<RequestWorkflowStepMappingDAO> RequestWorkflowStepMapping { get; set; }
        public virtual DbSet<RoleDAO> Role { get; set; }
        public virtual DbSet<SLAAlertDAO> SLAAlert { get; set; }
        public virtual DbSet<SLAAlertFRTDAO> SLAAlertFRT { get; set; }
        public virtual DbSet<SLAAlertFRTMailDAO> SLAAlertFRTMail { get; set; }
        public virtual DbSet<SLAAlertFRTPhoneDAO> SLAAlertFRTPhone { get; set; }
        public virtual DbSet<SLAAlertFRTUserDAO> SLAAlertFRTUser { get; set; }
        public virtual DbSet<SLAAlertMailDAO> SLAAlertMail { get; set; }
        public virtual DbSet<SLAAlertPhoneDAO> SLAAlertPhone { get; set; }
        public virtual DbSet<SLAAlertUserDAO> SLAAlertUser { get; set; }
        public virtual DbSet<SLAEscalationDAO> SLAEscalation { get; set; }
        public virtual DbSet<SLAEscalationFRTDAO> SLAEscalationFRT { get; set; }
        public virtual DbSet<SLAEscalationFRTMailDAO> SLAEscalationFRTMail { get; set; }
        public virtual DbSet<SLAEscalationFRTPhoneDAO> SLAEscalationFRTPhone { get; set; }
        public virtual DbSet<SLAEscalationFRTUserDAO> SLAEscalationFRTUser { get; set; }
        public virtual DbSet<SLAEscalationMailDAO> SLAEscalationMail { get; set; }
        public virtual DbSet<SLAEscalationPhoneDAO> SLAEscalationPhone { get; set; }
        public virtual DbSet<SLAEscalationUserDAO> SLAEscalationUser { get; set; }
        public virtual DbSet<SLAPolicyDAO> SLAPolicy { get; set; }
        public virtual DbSet<SLAStatusDAO> SLAStatus { get; set; }
        public virtual DbSet<SLATimeUnitDAO> SLATimeUnit { get; set; }
        public virtual DbSet<SaleStageDAO> SaleStage { get; set; }
        public virtual DbSet<ScheduleMasterDAO> ScheduleMaster { get; set; }
        public virtual DbSet<SexDAO> Sex { get; set; }
        public virtual DbSet<SmsQueueDAO> SmsQueue { get; set; }
        public virtual DbSet<SmsQueueStatusDAO> SmsQueueStatus { get; set; }
        public virtual DbSet<SmsTemplateDAO> SmsTemplate { get; set; }
        public virtual DbSet<SocialChannelTypeDAO> SocialChannelType { get; set; }
        public virtual DbSet<StatusDAO> Status { get; set; }
        public virtual DbSet<StoreDAO> Store { get; set; }
        public virtual DbSet<StoreAssetsDAO> StoreAssets { get; set; }
        public virtual DbSet<StoreConsultingServiceMappingDAO> StoreConsultingServiceMapping { get; set; }
        public virtual DbSet<StoreCooperativeAttitudeMappingDAO> StoreCooperativeAttitudeMapping { get; set; }
        public virtual DbSet<StoreCoverageCapacityDAO> StoreCoverageCapacity { get; set; }
        public virtual DbSet<StoreDeliveryTimeDAO> StoreDeliveryTime { get; set; }
        public virtual DbSet<StoreDeliveryTimeMappingDAO> StoreDeliveryTimeMapping { get; set; }
        public virtual DbSet<StoreExtendDAO> StoreExtend { get; set; }
        public virtual DbSet<StoreGroupingDAO> StoreGrouping { get; set; }
        public virtual DbSet<StoreImageMappingDAO> StoreImageMapping { get; set; }
        public virtual DbSet<StoreInfulenceLevelMarketMappingDAO> StoreInfulenceLevelMarketMapping { get; set; }
        public virtual DbSet<StoreMarketPriceMappingDAO> StoreMarketPriceMapping { get; set; }
        public virtual DbSet<StoreMeansOfDeliveryDAO> StoreMeansOfDelivery { get; set; }
        public virtual DbSet<StorePersonnelDAO> StorePersonnel { get; set; }
        public virtual DbSet<StoreRelationshipCustomerMappingDAO> StoreRelationshipCustomerMapping { get; set; }
        public virtual DbSet<StoreRepresentDAO> StoreRepresent { get; set; }
        public virtual DbSet<StoreStatusDAO> StoreStatus { get; set; }
        public virtual DbSet<StoreTypeDAO> StoreType { get; set; }
        public virtual DbSet<StoreWarrantyServiceDAO> StoreWarrantyService { get; set; }
        public virtual DbSet<SupplierDAO> Supplier { get; set; }
        public virtual DbSet<TaxTypeDAO> TaxType { get; set; }
        public virtual DbSet<TicketDAO> Ticket { get; set; }
        public virtual DbSet<TicketGeneratedIdDAO> TicketGeneratedId { get; set; }
        public virtual DbSet<TicketGroupDAO> TicketGroup { get; set; }
        public virtual DbSet<TicketIssueLevelDAO> TicketIssueLevel { get; set; }
        public virtual DbSet<TicketOfDepartmentDAO> TicketOfDepartment { get; set; }
        public virtual DbSet<TicketOfUserDAO> TicketOfUser { get; set; }
        public virtual DbSet<TicketPriorityDAO> TicketPriority { get; set; }
        public virtual DbSet<TicketResolveTypeDAO> TicketResolveType { get; set; }
        public virtual DbSet<TicketSourceDAO> TicketSource { get; set; }
        public virtual DbSet<TicketStatusDAO> TicketStatus { get; set; }
        public virtual DbSet<TicketTypeDAO> TicketType { get; set; }
        public virtual DbSet<UnitOfMeasureDAO> UnitOfMeasure { get; set; }
        public virtual DbSet<UnitOfMeasureGroupingDAO> UnitOfMeasureGrouping { get; set; }
        public virtual DbSet<UnitOfMeasureGroupingContentDAO> UnitOfMeasureGroupingContent { get; set; }
        public virtual DbSet<UsedVariationDAO> UsedVariation { get; set; }
        public virtual DbSet<VariationDAO> Variation { get; set; }
        public virtual DbSet<VariationGroupingDAO> VariationGrouping { get; set; }
        public virtual DbSet<WardDAO> Ward { get; set; }
        public virtual DbSet<WarehouseDAO> Warehouse { get; set; }
        public virtual DbSet<WorkflowDefinitionDAO> WorkflowDefinition { get; set; }
        public virtual DbSet<WorkflowDirectionDAO> WorkflowDirection { get; set; }
        public virtual DbSet<WorkflowParameterDAO> WorkflowParameter { get; set; }
        public virtual DbSet<WorkflowStateDAO> WorkflowState { get; set; }
        public virtual DbSet<WorkflowStepDAO> WorkflowStep { get; set; }
        public virtual DbSet<WorkflowTypeDAO> WorkflowType { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySql("server=127.0.0.1;uid=root;pwd=123@123a;database=crmf2;Allow User Variables=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionDAO>(entity =>
            {
                //entity.ToTable("Action", "PER");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Action_Menu");
            });

            modelBuilder.Entity<ActionPageMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ActionId, e.PageId });

                //entity.ToTable("ActionPageMapping", "PER");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ActionPageMappings)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionPageMapping_Action");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.ActionPageMappings)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionPageMapping_Page");
            });

            modelBuilder.Entity<ActivityPriorityDAO>(entity =>
            {
                //entity.ToTable("ActivityPriority", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ActivityStatusDAO>(entity =>
            {
                //entity.ToTable("ActivityStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ActivityTypeDAO>(entity =>
            {
                //entity.ToTable("ActivityType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<AppUserDAO>(entity =>
            {
                //entity.ToTable("AppUser", "MDM");

                entity.Property(e => e.Id)
                    .HasComment("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasComment("Địa chỉ nhà");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(4000)
                    .HasComment("Ảnh đại diện");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Department)
                    .HasMaxLength(500)
                    .HasComment("Phòng ban");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(500)
                    .HasComment("Tên hiển thị");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .HasComment("Địa chỉ email");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.OrganizationId).HasComment("Đơn vị công tác");

                entity.Property(e => e.Phone)
                    .HasMaxLength(500)
                    .HasComment("Số điện thoại liên hệ");

                entity.Property(e => e.RowId).HasComment("Trường để đồng bộ");

                entity.Property(e => e.StatusId).HasComment("Trạng thái");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("Tên đăng nhập");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUser_Organization");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.SexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUser_Sex");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUser_UserStatus");
            });

            modelBuilder.Entity<AppUserPermissionDAO>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AppUserPermission");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<AppUserRoleMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.AppUserId, e.RoleId })
                    .HasName("PK_UserRoleMapping");

                //entity.ToTable("AppUserRoleMapping", "MDM");

                entity.Property(e => e.AppUserId).HasComment("Id nhân viên");

                entity.Property(e => e.RoleId).HasComment("Id nhóm quyền");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.AppUserRoleMappings)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUserRoleMapping_AppUser");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AppUserRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUserRoleMapping_Role");
            });

            modelBuilder.Entity<AuditLogPropertyDAO>(entity =>
            {
                entity.Property(e => e.ActionName).HasMaxLength(255);

                entity.Property(e => e.ClassName).HasMaxLength(255);

                entity.Property(e => e.NewValue)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.OldValue)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Property)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.AuditLogProperties)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_AuditLogProperty_AppUser");
            });

            modelBuilder.Entity<BrandDAO>(entity =>
            {
                //entity.ToTable("Brand", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Status");
            });

            modelBuilder.Entity<BusinessConcentrationLevelDAO>(entity =>
            {
                entity.Property(e => e.Branch).HasMaxLength(200);

                entity.Property(e => e.Manufacturer).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.RevenueInYear).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.BusinessConcentrationLevels)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_BusinessConcentrationLevel_Store");
            });

            modelBuilder.Entity<BusinessTypeDAO>(entity =>
            {
                //entity.ToTable("BusinessType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<CallCategoryDAO>(entity =>
            {
                //entity.ToTable("CallCategory", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CallEmotionDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CallEmotions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallEmotion_Status");
            });

            modelBuilder.Entity<CallLogDAO>(entity =>
            {
                entity.Property(e => e.CallTime).HasColumnType("datetime");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CallLogAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallLog_AppUser");

                entity.HasOne(d => d.CallCategory)
                    .WithMany(p => p.CallLogs)
                    .HasForeignKey(d => d.CallCategoryId)
                    .HasConstraintName("FK_CallLog_CallCategory");

                entity.HasOne(d => d.CallEmotion)
                    .WithMany(p => p.CallLogs)
                    .HasForeignKey(d => d.CallEmotionId)
                    .HasConstraintName("FK_CallLog_CallEmotion");

                entity.HasOne(d => d.CallStatus)
                    .WithMany(p => p.CallLogs)
                    .HasForeignKey(d => d.CallStatusId)
                    .HasConstraintName("FK_CallLog_CallStatus");

                entity.HasOne(d => d.CallType)
                    .WithMany(p => p.CallLogs)
                    .HasForeignKey(d => d.CallTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallLog_CallType");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CallLogCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallLog_AppUser1");

                entity.HasOne(d => d.EntityReference)
                    .WithMany(p => p.CallLogs)
                    .HasForeignKey(d => d.EntityReferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallLog_CallLogEntityType");
            });

            modelBuilder.Entity<CallStatusDAO>(entity =>
            {
                //entity.ToTable("CallStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CallTypeDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ColorCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CallTypes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallType_Status");
            });

            modelBuilder.Entity<CategoryDAO>(entity =>
            {
                //entity.ToTable("Category", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Category_Image");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Category_Category");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Status");
            });

            modelBuilder.Entity<ColorDAO>(entity =>
            {
                //entity.ToTable("Color", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CompanyDAO>(entity =>
            {
                //entity.ToTable("Company", "OPP");

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.EmailOther).HasMaxLength(500);

                entity.Property(e => e.FAX).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(400);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PhoneOther).HasMaxLength(50);

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.Website).HasMaxLength(100);

                entity.Property(e => e.ZIPCode).HasMaxLength(50);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CompanyAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_Company_AppUser");

                entity.HasOne(d => d.CompanyStatus)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CompanyStatusId)
                    .HasConstraintName("FK_Company_CompanyStatus");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CompanyCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_AppUser1");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Company_Currency");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .HasConstraintName("FK_Company_CustomerLead");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Company_District2");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_Company_Nation2");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Company_Comapny");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_Company_Profession");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Company_Province2");
            });

            modelBuilder.Entity<CompanyActivityDAO>(entity =>
            {
                //entity.ToTable("CompanyActivity", "OPP");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.ActivityPriority)
                    .WithMany(p => p.CompanyActivities)
                    .HasForeignKey(d => d.ActivityPriorityId)
                    .HasConstraintName("FK_CompanyActivity_ActivityPriority");

                entity.HasOne(d => d.ActivityStatus)
                    .WithMany(p => p.CompanyActivities)
                    .HasForeignKey(d => d.ActivityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyActivity_ActivityStatus");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.CompanyActivities)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyActivity_ActivityType");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CompanyActivities)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyActivity_AppUser");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyActivities)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyActivity_Company");
            });

            modelBuilder.Entity<CompanyCallLogMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.CallLogId })
                    .HasName("PK_AccountCallLogMapping");

                //entity.ToTable("CompanyCallLogMapping", "OPP");

                entity.HasOne(d => d.CallLog)
                    .WithMany(p => p.CompanyCallLogMappings)
                    .HasForeignKey(d => d.CallLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyCallLogMapping_CallLog");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyCallLogMappings)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyCallLogMapping_Company");
            });

            modelBuilder.Entity<CompanyEmailDAO>(entity =>
            {
                //entity.ToTable("CompanyEmail", "OPP");

                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Reciepient)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyEmails)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyEmail_Company");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CompanyEmails)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyEmail_AppUser");

                entity.HasOne(d => d.EmailStatus)
                    .WithMany(p => p.CompanyEmails)
                    .HasForeignKey(d => d.EmailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyEmail_EmailStatus");
            });

            modelBuilder.Entity<CompanyEmailCCMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CompanyEmailId, e.AppUserId });

                //entity.ToTable("CompanyEmailCCMapping", "OPP");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CompanyEmailCCMappings)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyEmailCCMapping_AppUser");

                entity.HasOne(d => d.CompanyEmail)
                    .WithMany(p => p.CompanyEmailCCMappings)
                    .HasForeignKey(d => d.CompanyEmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyEmailCCMapping_CompanyEmail");
            });

            modelBuilder.Entity<CompanyFileGroupingDAO>(entity =>
            {
                //entity.ToTable("CompanyFileGrouping", "OPP");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyFileGroupings)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyFileGroupMapping_Company1");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CompanyFileGroupings)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyFileGroupMapping_AppUser");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.CompanyFileGroupings)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyFileGroupMapping_FileType");
            });

            modelBuilder.Entity<CompanyFileMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CompanyFileGroupingId, e.FileId })
                    .HasName("PK_AccountFileMapping");

                //entity.ToTable("CompanyFileMapping", "OPP");

                entity.HasOne(d => d.CompanyFileGrouping)
                    .WithMany(p => p.CompanyFileMappings)
                    .HasForeignKey(d => d.CompanyFileGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyFileMapping_Company");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.CompanyFileMappings)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyFileMapping_File");
            });

            modelBuilder.Entity<CompanyStatusDAO>(entity =>
            {
                //entity.ToTable("CompanyStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(500)
                    .HasComment("Mã quận huyện");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ConsultingServiceDAO>(entity =>
            {
                //entity.ToTable("ConsultingService", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ContactDAO>(entity =>
            {
                //entity.ToTable("Contact", "OPP");

                entity.Property(e => e.Address).HasMaxLength(2000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Department).HasMaxLength(500);

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.EmailOther).HasMaxLength(500);

                entity.Property(e => e.FAX).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneHome).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.ZIPCode).HasMaxLength(200);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.ContactAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_Contact_AppUser1");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Contact_Company1");

                entity.HasOne(d => d.ContactStatus)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactStatusId)
                    .HasConstraintName("FK_Contact_ContactStatus");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.ContactCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_AppUser");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .HasConstraintName("FK_Contact_CustomerLead");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Contact_District");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Contact_Image1");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_Contact_Nation");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Contact_Position");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_Contact_Profession");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Contact_Province");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_Contact_Sex");
            });

            modelBuilder.Entity<ContactActivityDAO>(entity =>
            {
                //entity.ToTable("ContactActivity", "OPP");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.ActivityPriority)
                    .WithMany(p => p.ContactActivities)
                    .HasForeignKey(d => d.ActivityPriorityId)
                    .HasConstraintName("FK_ContactActivity_ActivityPriority");

                entity.HasOne(d => d.ActivityStatus)
                    .WithMany(p => p.ContactActivities)
                    .HasForeignKey(d => d.ActivityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactActivity_ActivityStatus");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.ContactActivities)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactActivity_ActivityType");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.ContactActivities)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactActivity_AppUser");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactActivities)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactActivity_Contact");
            });

            modelBuilder.Entity<ContactCallLogMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.CallLogId });

                //entity.ToTable("ContactCallLogMapping", "OPP");

                entity.HasOne(d => d.CallLog)
                    .WithMany(p => p.ContactCallLogMappings)
                    .HasForeignKey(d => d.CallLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactCallLogMapping_CallLog");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactCallLogMappings)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactCallLogMapping_Contact");
            });

            modelBuilder.Entity<ContactEmailDAO>(entity =>
            {
                //entity.ToTable("ContactEmail", "OPP");

                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Reciepient)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactEmails)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactEmail_Contact");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.ContactEmails)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactEmail_AppUser");

                entity.HasOne(d => d.EmailStatus)
                    .WithMany(p => p.ContactEmails)
                    .HasForeignKey(d => d.EmailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactEmail_EmailStatus");
            });

            modelBuilder.Entity<ContactEmailCCMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ContactEmailId, e.AppUserId });

                //entity.ToTable("ContactEmailCCMapping", "OPP");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.ContactEmailCCMappings)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactEmailCCMapping_AppUser");

                entity.HasOne(d => d.ContactEmail)
                    .WithMany(p => p.ContactEmailCCMappings)
                    .HasForeignKey(d => d.ContactEmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactEmailCCMapping_ContactEmail");
            });

            modelBuilder.Entity<ContactFileGroupingDAO>(entity =>
            {
                //entity.ToTable("ContactFileGrouping", "OPP");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactFileGroupings)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactFileGrouping_Contact");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.ContactFileGroupings)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactFileGrouping_AppUser");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.ContactFileGroupings)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactFileGrouping_FileType");
            });

            modelBuilder.Entity<ContactFileMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ContactFileGroupingId, e.FileId });

                //entity.ToTable("ContactFileMapping", "OPP");

                entity.HasOne(d => d.ContactFileGrouping)
                    .WithMany(p => p.ContactFileMappings)
                    .HasForeignKey(d => d.ContactFileGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactFileMapping_ContactFileGrouping");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.ContactFileMappings)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactFileMapping_File");
            });

            modelBuilder.Entity<ContactStatusDAO>(entity =>
            {
                //entity.ToTable("ContactStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ContractDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.DeliveryUnit).HasMaxLength(500);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceAddress)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.InvoiceZipCode).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.ReceiveAddress)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ReceiveZipCode).HasMaxLength(100);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TermAndCondition).HasMaxLength(4000);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalTaxAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalTaxAmountOther).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.ValidityDate).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.ContractAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_AppUser");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Contract_Company");

                entity.HasOne(d => d.ContractStatus)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ContractStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_ContractStatus");

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ContractTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_ContractType");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.ContractCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_AppUser1");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Currency");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.InvoiceDistrict)
                    .WithMany(p => p.ContractInvoiceDistricts)
                    .HasForeignKey(d => d.InvoiceDistrictId)
                    .HasConstraintName("FK_Contract_District");

                entity.HasOne(d => d.InvoiceNation)
                    .WithMany(p => p.ContractInvoiceNations)
                    .HasForeignKey(d => d.InvoiceNationId)
                    .HasConstraintName("FK_Contract_Nation");

                entity.HasOne(d => d.InvoiceProvince)
                    .WithMany(p => p.ContractInvoiceProvinces)
                    .HasForeignKey(d => d.InvoiceProvinceId)
                    .HasConstraintName("FK_Contract_Province");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.OpportunityId)
                    .HasConstraintName("FK_Contract_Opportunity");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Organization");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_PaymentStatus");

                entity.HasOne(d => d.ReceiveDistrict)
                    .WithMany(p => p.ContractReceiveDistricts)
                    .HasForeignKey(d => d.ReceiveDistrictId)
                    .HasConstraintName("FK_Contract_District1");

                entity.HasOne(d => d.ReceiveNation)
                    .WithMany(p => p.ContractReceiveNations)
                    .HasForeignKey(d => d.ReceiveNationId)
                    .HasConstraintName("FK_Contract_Nation1");

                entity.HasOne(d => d.ReceiveProvince)
                    .WithMany(p => p.ContractReceiveProvinces)
                    .HasForeignKey(d => d.ReceiveProvinceId)
                    .HasConstraintName("FK_Contract_Province1");
            });

            modelBuilder.Entity<ContractContactMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ContractId, e.ContactId });

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContractContactMappings)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractContactMapping_Contact");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractContactMappings)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractContactMapping_Contract");
            });

            modelBuilder.Entity<ContractFileGroupingDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractFileGroupings)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFileGrouping_Contract");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.ContractFileGroupings)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFileGrouping_AppUser");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.ContractFileGroupings)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFileGrouping_FileType");
            });

            modelBuilder.Entity<ContractFileMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ContractFileGroupingId, e.FileId });

                entity.HasOne(d => d.ContractFileGrouping)
                    .WithMany(p => p.ContractFileMappings)
                    .HasForeignKey(d => d.ContractFileGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFileMapping_ContractFileGrouping");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.ContractFileMappings)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFileMapping_File");
            });

            modelBuilder.Entity<ContractItemDetailDAO>(entity =>
            {
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PrimaryPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxAmountOther).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TaxPercentageOther).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractItemDetails)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractItemDetail_Contract");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ContractItemDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractItemDetail_Item");

                entity.HasOne(d => d.PrimaryUnitOfMeasure)
                    .WithMany(p => p.ContractItemDetailPrimaryUnitOfMeasures)
                    .HasForeignKey(d => d.PrimaryUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractItemDetail_UnitOfMeasure1");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.ContractItemDetails)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractItemDetail_TaxType");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.ContractItemDetailUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractItemDetail_UnitOfMeasure");
            });

            modelBuilder.Entity<ContractPaymentHistoryDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentMilestone).HasMaxLength(4000);

                entity.Property(e => e.PaymentPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractPaymentHistories)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractPaymentHistory_Contract");
            });

            modelBuilder.Entity<ContractStatusDAO>(entity =>
            {
                //entity.ToTable("ContractStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<ContractTypeDAO>(entity =>
            {
                //entity.ToTable("ContractType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<CooperativeAttitudeDAO>(entity =>
            {
                //entity.ToTable("CooperativeAttitude", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CurrencyDAO>(entity =>
            {
                //entity.ToTable("Currency", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CustomerDAO>(entity =>
            {
                //entity.ToTable("Customer", "CUSTOMER");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Descreption).HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Investment).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Name).HasMaxLength(400);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.RevenueAnnual).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxCode).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.Website).HasMaxLength(500);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CustomerAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_AppUser1");

                entity.HasOne(d => d.BusinessType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BusinessTypeId)
                    .HasConstraintName("FK_Customer_BusinessType");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CustomerCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Customer_Company");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_AppUser");

                entity.HasOne(d => d.CustomerResource)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerResourceId)
                    .HasConstraintName("FK_Customer_CustomerResource");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CustomerType");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Customer_District");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_Customer_Nation");

                entity.HasOne(d => d.ParentCompany)
                    .WithMany(p => p.CustomerParentCompanies)
                    .HasForeignKey(d => d.ParentCompanyId)
                    .HasConstraintName("FK_Customer_Company1");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_Customer_Profession");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Customer_Province");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_Customer_Sex");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Status");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_Customer_Ward");
            });

            modelBuilder.Entity<CustomerCCEmailHistoryDAO>(entity =>
            {
                //entity.ToTable("CustomerCCEmailHistory", "CUSTOMER");

                entity.Property(e => e.CCEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CustomerEmailHistory)
                    .WithMany(p => p.CustomerCCEmailHistories)
                    .HasForeignKey(d => d.CustomerEmailHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCCEmailHistory_CustomerEmailHistory");
            });

            modelBuilder.Entity<CustomerCallLogMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CallLogId });

                //entity.ToTable("CustomerCallLogMapping", "CUSTOMER");

                entity.HasOne(d => d.CallLog)
                    .WithMany(p => p.CustomerCallLogMappings)
                    .HasForeignKey(d => d.CallLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCallLogMapping_CallLog");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCallLogMappings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCallLogMapping_Customer");
            });

            modelBuilder.Entity<CustomerCustomerGroupingMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerGroupingId });

                //entity.ToTable("CustomerCustomerGroupingMapping", "CUSTOMER");

                entity.HasOne(d => d.CustomerGrouping)
                    .WithMany(p => p.CustomerCustomerGroupingMappings)
                    .HasForeignKey(d => d.CustomerGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerGroupingMapping_CustomerGrouping");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCustomerGroupingMappings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerGroupingMapping_Customer");
            });

            modelBuilder.Entity<CustomerEmailDAO>(entity =>
            {
                //entity.ToTable("CustomerEmail", "CUSTOMER");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerEmails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerEmail_Customer");

                entity.HasOne(d => d.EmailType)
                    .WithMany(p => p.CustomerEmails)
                    .HasForeignKey(d => d.EmailTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerEmail_EmailType");
            });

            modelBuilder.Entity<CustomerEmailHistoryDAO>(entity =>
            {
                //entity.ToTable("CustomerEmailHistory", "CUSTOMER");

                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Reciepient)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerEmailHistories)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerEmailHistory_AppUser");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerEmailHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerEmailHistory_Customer");

                entity.HasOne(d => d.EmailStatus)
                    .WithMany(p => p.CustomerEmailHistories)
                    .HasForeignKey(d => d.EmailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerEmailHistory_EmailStatus");
            });

            modelBuilder.Entity<CustomerFeedbackDAO>(entity =>
            {
                //entity.ToTable("CustomerFeedback", "CUSTOMER");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.CustomerFeedbackType)
                    .WithMany(p => p.CustomerFeedbacks)
                    .HasForeignKey(d => d.CustomerFeedbackTypeId)
                    .HasConstraintName("FK_CustomerFeedback_CustomerFeedbackType");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerFeedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CustomerFeedback_Customer");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CustomerFeedbacks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_CustomerFeedback_Status");
            });

            modelBuilder.Entity<CustomerFeedbackTypeDAO>(entity =>
            {
                //entity.ToTable("CustomerFeedbackType", "ENUM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CustomerGroupingDAO>(entity =>
            {
                //entity.ToTable("CustomerGrouping", "MDM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerGroupings)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerGrouping_CustomerType");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_CustomerGrouping_CustomerGrouping");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CustomerGroupings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerGrouping_Status");
            });

            modelBuilder.Entity<CustomerLeadDAO>(entity =>
            {
                //entity.ToTable("CustomerLead", "OPP");

                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.BusinessRegistrationCode).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Fax).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SecondEmail).HasMaxLength(255);

                entity.Property(e => e.TelePhone).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Website).HasMaxLength(255);

                entity.Property(e => e.ZipCode).HasMaxLength(50);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CustomerLeadAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_CustomerLead_AppUser");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CustomerLead_Company");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerLeadCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLead_AppUser1");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_CustomerLead_Currency");

                entity.HasOne(d => d.CustomerLeadLevel)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.CustomerLeadLevelId)
                    .HasConstraintName("FK_CustomerLead_CustomerLeadLevel");

                entity.HasOne(d => d.CustomerLeadSource)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.CustomerLeadSourceId)
                    .HasConstraintName("FK_CustomerLead_CustomerLeadSource");

                entity.HasOne(d => d.CustomerLeadStatus)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.CustomerLeadStatusId)
                    .HasConstraintName("FK_CustomerLead_CustomerLeadStatus");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_CustomerLead_District");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_CustomerLead_Nation");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK_CustomerLead_Profession");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_CustomerLead_Province");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.CustomerLeads)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_CustomerLead_Sex");
            });

            modelBuilder.Entity<CustomerLeadActivityDAO>(entity =>
            {
                //entity.ToTable("CustomerLeadActivity", "OPP");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.ActivityPriority)
                    .WithMany(p => p.CustomerLeadActivities)
                    .HasForeignKey(d => d.ActivityPriorityId)
                    .HasConstraintName("FK_CustomerLeadActivity_ActivityPriority");

                entity.HasOne(d => d.ActivityStatus)
                    .WithMany(p => p.CustomerLeadActivities)
                    .HasForeignKey(d => d.ActivityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadActivity_ActivityStatus");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.CustomerLeadActivities)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadActivity_ActivityType");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CustomerLeadActivities)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadActivity_AppUser");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.CustomerLeadActivities)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadActivity_CustomerLead");
            });

            modelBuilder.Entity<CustomerLeadCallLogMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerLeadId, e.CallLogId });

                //entity.ToTable("CustomerLeadCallLogMapping", "OPP");

                entity.HasOne(d => d.CallLog)
                    .WithMany(p => p.CustomerLeadCallLogMappings)
                    .HasForeignKey(d => d.CallLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadCallLogMapping_CallLog");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.CustomerLeadCallLogMappings)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadCallLogMapping_CustomerLead");
            });

            modelBuilder.Entity<CustomerLeadEmailDAO>(entity =>
            {
                //entity.ToTable("CustomerLeadEmail", "OPP");

                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Reciepient)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerLeadEmails)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadEmailMapping_AppUser");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.CustomerLeadEmails)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadEmail_CustomerLead");

                entity.HasOne(d => d.EmailStatus)
                    .WithMany(p => p.CustomerLeadEmails)
                    .HasForeignKey(d => d.EmailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadEmailMapping_EmailStatus");
            });

            modelBuilder.Entity<CustomerLeadEmailCCMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerLeadEmailId, e.AppUserId })
                    .HasName("PK_CustomerEmailCCMapping");

                //entity.ToTable("CustomerLeadEmailCCMapping", "OPP");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.CustomerLeadEmailCCMappings)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadEmailCCMapping_AppUser");

                entity.HasOne(d => d.CustomerLeadEmail)
                    .WithMany(p => p.CustomerLeadEmailCCMappings)
                    .HasForeignKey(d => d.CustomerLeadEmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadEmailCCMapping_CustomerLeadEmail");
            });

            modelBuilder.Entity<CustomerLeadFileGroupDAO>(entity =>
            {
                //entity.ToTable("CustomerLeadFileGroup", "OPP");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .HasComment("Tên");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasComment("Tên");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerLeadFileGroups)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadFileGroupMapping_AppUser");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.CustomerLeadFileGroups)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadFileGroup_CustomerLead");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.CustomerLeadFileGroups)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadFileGroup_FileType");
            });

            modelBuilder.Entity<CustomerLeadFileMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerLeadFileGroupId, e.FileId });

                //entity.ToTable("CustomerLeadFileMapping", "OPP");

                entity.HasOne(d => d.CustomerLeadFileGroup)
                    .WithMany(p => p.CustomerLeadFileMappings)
                    .HasForeignKey(d => d.CustomerLeadFileGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadFileMapping_CustomerLead");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.CustomerLeadFileMappings)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadFileMapping_File");
            });

            modelBuilder.Entity<CustomerLeadItemMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.CustomerLeadId, e.ItemId });

                //entity.ToTable("CustomerLeadItemMapping", "OPP");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrimaryPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VAT).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VATOther).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.CustomerLeadItemMappings)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadItemMapping_CustomerLead");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CustomerLeadItemMappings)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadItemMapping_Item");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.CustomerLeadItemMappings)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLeadItemMapping_UnitOfMeasure");
            });

            modelBuilder.Entity<CustomerLeadLevelDAO>(entity =>
            {
                //entity.ToTable("CustomerLeadLevel", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerLeadSourceDAO>(entity =>
            {
                //entity.ToTable("CustomerLeadSource", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerLeadStatusDAO>(entity =>
            {
                //entity.ToTable("CustomerLeadStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerLevelDAO>(entity =>
            {
                //entity.ToTable("CustomerLevel", "MDM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Color).HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CustomerLevels)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLevel_Status");
            });

            modelBuilder.Entity<CustomerPhoneDAO>(entity =>
            {
                //entity.ToTable("CustomerPhone", "CUSTOMER");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPhones)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPhone_Customer");

                entity.HasOne(d => d.PhoneType)
                    .WithMany(p => p.CustomerPhones)
                    .HasForeignKey(d => d.PhoneTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPhone_PhoneType");
            });

            modelBuilder.Entity<CustomerPointHistoryDAO>(entity =>
            {
                //entity.ToTable("CustomerPointHistory", "CUSTOMER");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPointHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPointHistory_Customer");
            });

            modelBuilder.Entity<CustomerResourceDAO>(entity =>
            {
                //entity.ToTable("CustomerResource", "MDM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CustomerResources)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerResource_Status");
            });

            modelBuilder.Entity<CustomerSalesOrderDAO>(entity =>
            {
                //entity.ToTable("CustomerSalesOrder", "ORDER");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(4000);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryZIPCode).HasMaxLength(50);

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.InvoiceAddress).HasMaxLength(4000);

                entity.Property(e => e.InvoiceZIPCode).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShippingName).HasMaxLength(500);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalTaxOther).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_CustomerSalesOrder_Contract");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerSalesOrderCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrder_AppUser1");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrder_Customer");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrder_CustomerType");

                entity.HasOne(d => d.DeliveryDistrict)
                    .WithMany(p => p.CustomerSalesOrderDeliveryDistricts)
                    .HasForeignKey(d => d.DeliveryDistrictId)
                    .HasConstraintName("FK_CustomerSalesOrder_District1");

                entity.HasOne(d => d.DeliveryNation)
                    .WithMany(p => p.CustomerSalesOrderDeliveryNations)
                    .HasForeignKey(d => d.DeliveryNationId)
                    .HasConstraintName("FK_CustomerSalesOrder_Nation1");

                entity.HasOne(d => d.DeliveryProvince)
                    .WithMany(p => p.CustomerSalesOrderDeliveryProvinces)
                    .HasForeignKey(d => d.DeliveryProvinceId)
                    .HasConstraintName("FK_CustomerSalesOrder_Province1");

                entity.HasOne(d => d.DeliveryWard)
                    .WithMany(p => p.CustomerSalesOrderDeliveryWards)
                    .HasForeignKey(d => d.DeliveryWardId)
                    .HasConstraintName("FK_CustomerSalesOrder_Ward1");

                entity.HasOne(d => d.EditedPriceStatus)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.EditedPriceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrder_EditedPriceStatus");

                entity.HasOne(d => d.InvoiceDistrict)
                    .WithMany(p => p.CustomerSalesOrderInvoiceDistricts)
                    .HasForeignKey(d => d.InvoiceDistrictId)
                    .HasConstraintName("FK_CustomerSalesOrder_District");

                entity.HasOne(d => d.InvoiceNation)
                    .WithMany(p => p.CustomerSalesOrderInvoiceNations)
                    .HasForeignKey(d => d.InvoiceNationId)
                    .HasConstraintName("FK_CustomerSalesOrder_Nation");

                entity.HasOne(d => d.InvoiceProvince)
                    .WithMany(p => p.CustomerSalesOrderInvoiceProvinces)
                    .HasForeignKey(d => d.InvoiceProvinceId)
                    .HasConstraintName("FK_CustomerSalesOrder_Province");

                entity.HasOne(d => d.InvoiceWard)
                    .WithMany(p => p.CustomerSalesOrderInvoiceWards)
                    .HasForeignKey(d => d.InvoiceWardId)
                    .HasConstraintName("FK_CustomerSalesOrder_Ward");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.OpportunityId)
                    .HasConstraintName("FK_CustomerSalesOrder_Opportunity");

                entity.HasOne(d => d.OrderPaymentStatus)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.OrderPaymentStatusId)
                    .HasConstraintName("FK_CustomerSalesOrder_OrderPaymentStatus");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrder_Organization");

                entity.HasOne(d => d.RequestState)
                    .WithMany(p => p.CustomerSalesOrders)
                    .HasForeignKey(d => d.RequestStateId)
                    .HasConstraintName("FK_CustomerSalesOrder_RequestState");

                entity.HasOne(d => d.SalesEmployee)
                    .WithMany(p => p.CustomerSalesOrderSalesEmployees)
                    .HasForeignKey(d => d.SalesEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrder_AppUser");
            });

            modelBuilder.Entity<CustomerSalesOrderContentDAO>(entity =>
            {
                //entity.ToTable("CustomerSalesOrderContent", "ORDER");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PrimaryPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxAmountOther).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TaxPercentageOther).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.CustomerSalesOrder)
                    .WithMany(p => p.CustomerSalesOrderContents)
                    .HasForeignKey(d => d.CustomerSalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderContent_CustomerSalesOrder");

                entity.HasOne(d => d.EditedPriceStatus)
                    .WithMany(p => p.CustomerSalesOrderContents)
                    .HasForeignKey(d => d.EditedPriceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderContent_EditedPriceStatus");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CustomerSalesOrderContents)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderContent_Item");

                entity.HasOne(d => d.PrimaryUnitOfMeasure)
                    .WithMany(p => p.CustomerSalesOrderContentPrimaryUnitOfMeasures)
                    .HasForeignKey(d => d.PrimaryUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderContent_UnitOfMeasure1");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.CustomerSalesOrderContents)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderContent_TaxType");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.CustomerSalesOrderContentUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderContent_UnitOfMeasure");
            });

            modelBuilder.Entity<CustomerSalesOrderPaymentHistoryDAO>(entity =>
            {
                //entity.ToTable("CustomerSalesOrderPaymentHistory", "ORDER");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PaymentMilestone).HasMaxLength(400);

                entity.Property(e => e.PaymentPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerSalesOrder)
                    .WithMany(p => p.CustomerSalesOrderPaymentHistories)
                    .HasForeignKey(d => d.CustomerSalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderPaymentHistory_CustomerSalesOrder");
            });

            modelBuilder.Entity<CustomerSalesOrderPromotionDAO>(entity =>
            {
                //entity.ToTable("CustomerSalesOrderPromotion", "ORDER");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.CustomerSalesOrder)
                    .WithMany(p => p.CustomerSalesOrderPromotions)
                    .HasForeignKey(d => d.CustomerSalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderPromotion_CustomerSalesOrder");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CustomerSalesOrderPromotions)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderPromotion_Item");

                entity.HasOne(d => d.PrimaryUnitOfMeasure)
                    .WithMany(p => p.CustomerSalesOrderPromotionPrimaryUnitOfMeasures)
                    .HasForeignKey(d => d.PrimaryUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderPromotion_UnitOfMeasure1");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.CustomerSalesOrderPromotionUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSalesOrderPromotion_UnitOfMeasure");
            });

            modelBuilder.Entity<CustomerTypeDAO>(entity =>
            {
                //entity.ToTable("CustomerType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<DirectSalesOrderDAO>(entity =>
            {
                //entity.ToTable("DirectSalesOrder", "ORDER");

                entity.Property(e => e.Id)
                    .HasComment("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuyerStoreId).HasComment("Cửa hàng mua");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("Mã đơn hàng");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(4000)
                    .HasComment("Địa chỉ giao hàng");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày giao hàng");

                entity.Property(e => e.EditedPriceStatusId).HasComment("Sửa giá");

                entity.Property(e => e.GeneralDiscountAmount)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("Số tiền chiết khấu tổng");

                entity.Property(e => e.GeneralDiscountPercentage)
                    .HasColumnType("decimal(8, 2)")
                    .HasComment("% chiết khấu tổng");

                entity.Property(e => e.Note)
                    .HasMaxLength(4000)
                    .HasComment("Ghi chú");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày đặt hàng");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasComment("Số điện thoại");

                entity.Property(e => e.PromotionCode).HasMaxLength(50);

                entity.Property(e => e.PromotionValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RowId).HasComment("Id global cho phê duyệt");

                entity.Property(e => e.SaleEmployeeId).HasComment("Nhân viên kinh doanh");

                entity.Property(e => e.StoreAddress).HasMaxLength(4000);

                entity.Property(e => e.SubTotal)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("Tổng tiền trước thuế");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("Tổng tiền sau thuế");

                entity.Property(e => e.TotalAfterTax).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalTaxAmount)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("Tổng thuế");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.BuyerStore)
                    .WithMany(p => p.DirectSalesOrders)
                    .HasForeignKey(d => d.BuyerStoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrder_Store");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.DirectSalesOrderCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrder_AppUser1");

                entity.HasOne(d => d.EditedPriceStatus)
                    .WithMany(p => p.DirectSalesOrders)
                    .HasForeignKey(d => d.EditedPriceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrder_EditedPriceStatus");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.DirectSalesOrders)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrder_Organization");

                entity.HasOne(d => d.RequestState)
                    .WithMany(p => p.DirectSalesOrders)
                    .HasForeignKey(d => d.RequestStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrder_RequestState");

                entity.HasOne(d => d.SaleEmployee)
                    .WithMany(p => p.DirectSalesOrderSaleEmployees)
                    .HasForeignKey(d => d.SaleEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrder_AppUser");
            });

            modelBuilder.Entity<DirectSalesOrderContentDAO>(entity =>
            {
                //entity.ToTable("DirectSalesOrderContent", "ORDER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PrimaryPrice)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("Giá theo đơn vị lưu kho");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("Giá bán theo đơn vị xuất hàng");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxPercentage).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.DirectSalesOrder)
                    .WithMany(p => p.DirectSalesOrderContents)
                    .HasForeignKey(d => d.DirectSalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderContent_DirectSalesOrder");

                entity.HasOne(d => d.EditedPriceStatus)
                    .WithMany(p => p.DirectSalesOrderContents)
                    .HasForeignKey(d => d.EditedPriceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderContent_EditedPriceStatus");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.DirectSalesOrderContents)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderContent_Item");

                entity.HasOne(d => d.PrimaryUnitOfMeasure)
                    .WithMany(p => p.DirectSalesOrderContentPrimaryUnitOfMeasures)
                    .HasForeignKey(d => d.PrimaryUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderContent_UnitOfMeasure1");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.DirectSalesOrderContentUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderContent_UnitOfMeasure");
            });

            modelBuilder.Entity<DirectSalesOrderPromotionDAO>(entity =>
            {
                //entity.ToTable("DirectSalesOrderPromotion", "ORDER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.DirectSalesOrder)
                    .WithMany(p => p.DirectSalesOrderPromotions)
                    .HasForeignKey(d => d.DirectSalesOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderPromotion_DirectSalesOrder");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.DirectSalesOrderPromotions)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderPromotion_Item");

                entity.HasOne(d => d.PrimaryUnitOfMeasure)
                    .WithMany(p => p.DirectSalesOrderPromotionPrimaryUnitOfMeasures)
                    .HasForeignKey(d => d.PrimaryUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderPromotion_UnitOfMeasure");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.DirectSalesOrderPromotionUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DirectSalesOrderPromotion_UnitOfMeasure1");
            });

            modelBuilder.Entity<DistrictDAO>(entity =>
            {
                //entity.ToTable("District", "MDM");

                entity.Property(e => e.Id)
                    .HasComment("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(500)
                    .HasComment("Mã quận huyện");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("Tên quận huyện");

                entity.Property(e => e.Priority).HasComment("Thứ tự ưu tiên");

                entity.Property(e => e.ProvinceId).HasComment("Tỉnh phụ thuộc");

                entity.Property(e => e.RowId).HasComment("Trường để đồng bộ");

                entity.Property(e => e.StatusId).HasComment("Trạng thái hoạt động");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Status");
            });

            modelBuilder.Entity<EditedPriceStatusDAO>(entity =>
            {
                //entity.ToTable("EditedPriceStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmailStatusDAO>(entity =>
            {
                //entity.ToTable("EmailStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<EmailTypeDAO>(entity =>
            {
                //entity.ToTable("EmailType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EntityReferenceDAO>(entity =>
            {
                //entity.ToTable("EntityReference", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<EventMessageDAO>(entity =>
            {
                //entity.ToTable("EventMessage", "MDM");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RoutingKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<F1_ResourceActionPageMappingDAO>(entity =>
            {
                entity.HasKey(e => e.PageCode);

                entity.Property(e => e.PageCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ActionCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResourceCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FieldDAO>(entity =>
            {
                //entity.ToTable("Field", "PER");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.FieldType)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.FieldTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Field_FieldType");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionField_Menu");
            });

            modelBuilder.Entity<FieldTypeDAO>(entity =>
            {
                //entity.ToTable("FieldType", "PER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FileDAO>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasComment("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasComment("Tên");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasComment("Đường dẫn Url");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_File_AppUser");
            });

            modelBuilder.Entity<FileTypeDAO>(entity =>
            {
                //entity.ToTable("FileType", "ENUM");

                entity.Property(e => e.Id)
                    .HasComment("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("Tên");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("Tên");
            });

            modelBuilder.Entity<ImageDAO>(entity =>
            {
                //entity.ToTable("Image", "MDM");

                entity.Property(e => e.Id)
                    .HasComment("Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasComment("Tên");

                entity.Property(e => e.ThumbnailUrl)
                    .HasMaxLength(4000)
                    .HasComment("Đường dẫn Url");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasComment("Đường dẫn Url");
            });

            modelBuilder.Entity<ImproveQualityServingDAO>(entity =>
            {
                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ImproveQualityServings)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_ImproveQualityServing_Store");
            });

            modelBuilder.Entity<InfulenceLevelMarketDAO>(entity =>
            {
                //entity.ToTable("InfulenceLevelMarket", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<InventoryDAO>(entity =>
            {
                //entity.ToTable("Inventory", "MDM");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Item");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Warehouse");
            });

            modelBuilder.Entity<InventoryHistoryDAO>(entity =>
            {
                //entity.ToTable("InventoryHistory", "MDM");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.InventoryHistories)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryHistory_AppUser");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryHistories)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryHistory_Inventory");
            });

            modelBuilder.Entity<ItemDAO>(entity =>
            {
                //entity.ToTable("Item", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.RetailPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ScanCode).HasMaxLength(4000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Product");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Status");
            });

            modelBuilder.Entity<ItemImageMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.ImageId });

                //entity.ToTable("ItemImageMapping", "MDM");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ItemImageMappings)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemImageMapping_Image");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemImageMappings)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemImageMapping_Item");
            });

            modelBuilder.Entity<KMSStatusDAO>(entity =>
            {
                //entity.ToTable("KMSStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<KnowledgeArticleDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.KnowledgeArticles)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeArticle_AppUser");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.KnowledgeArticles)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeArticle_KnowledgeGroup");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.KnowledgeArticles)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_KnowledgeArticle_Item");

                entity.HasOne(d => d.KMSStatus)
                    .WithMany(p => p.KnowledgeArticles)
                    .HasForeignKey(d => d.KMSStatusId)
                    .HasConstraintName("FK_KnowledgeArticle_KMSStatus");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.KnowledgeArticles)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeArticle_Status");
            });

            modelBuilder.Entity<KnowledgeArticleKeywordDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.KnowledgeArticle)
                    .WithMany(p => p.KnowledgeArticleKeywords)
                    .HasForeignKey(d => d.KnowledgeArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeArticleKeyword_KnowledgeArticle");
            });

            modelBuilder.Entity<KnowledgeArticleOrganizationMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.KnowledgeArticleId, e.OrganizationId });

                entity.HasOne(d => d.KnowledgeArticle)
                    .WithMany(p => p.KnowledgeArticleOrganizationMappings)
                    .HasForeignKey(d => d.KnowledgeArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeArticleOrganizationMapping_KnowledgeArticle");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.KnowledgeArticleOrganizationMappings)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeArticleOrganizationMapping_Organization");
            });

            modelBuilder.Entity<KnowledgeGroupDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.KnowledgeGroups)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_KnowledgeGroup_Status");
            });

            modelBuilder.Entity<KpiCriteriaGeneralDAO>(entity =>
            {
                //entity.ToTable("KpiCriteriaGeneral", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<KpiCriteriaItemDAO>(entity =>
            {
                //entity.ToTable("KpiCriteriaItem", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<KpiGeneralDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.KpiGeneralCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneral_AppUser1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.KpiGeneralEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneral_AppUser");

                entity.HasOne(d => d.KpiYear)
                    .WithMany(p => p.KpiGenerals)
                    .HasForeignKey(d => d.KpiYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneral_KpiYear");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.KpiGenerals)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneral_Organization");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.KpiGenerals)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneral_Status");
            });

            modelBuilder.Entity<KpiGeneralContentDAO>(entity =>
            {
                entity.HasOne(d => d.KpiCriteriaGeneral)
                    .WithMany(p => p.KpiGeneralContents)
                    .HasForeignKey(d => d.KpiCriteriaGeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneralContent_KpiCriteriaGeneral");

                entity.HasOne(d => d.KpiGeneral)
                    .WithMany(p => p.KpiGeneralContents)
                    .HasForeignKey(d => d.KpiGeneralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneralContent_KpiGeneral");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.KpiGeneralContents)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneralContent_Status");
            });

            modelBuilder.Entity<KpiGeneralContentKpiPeriodMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.KpiGeneralContentId, e.KpiPeriodId });

                entity.Property(e => e.Value).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.KpiGeneralContent)
                    .WithMany(p => p.KpiGeneralContentKpiPeriodMappings)
                    .HasForeignKey(d => d.KpiGeneralContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneralContentKpiPeriodMapping_KpiGeneralContent");

                entity.HasOne(d => d.KpiPeriod)
                    .WithMany(p => p.KpiGeneralContentKpiPeriodMappings)
                    .HasForeignKey(d => d.KpiPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiGeneralContentKpiPeriodMapping_KpiPeriod");
            });

            modelBuilder.Entity<KpiItemDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.KpiItemCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItem_AppUser1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.KpiItemEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItem_AppUser");

                entity.HasOne(d => d.KpiPeriod)
                    .WithMany(p => p.KpiItems)
                    .HasForeignKey(d => d.KpiPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItem_KpiPeriod");

                entity.HasOne(d => d.KpiYear)
                    .WithMany(p => p.KpiItems)
                    .HasForeignKey(d => d.KpiYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItem_KpiYear");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.KpiItems)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItem_Organization");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.KpiItems)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItem_Status");
            });

            modelBuilder.Entity<KpiItemContentDAO>(entity =>
            {
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.KpiItemContents)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItemContent_Item");

                entity.HasOne(d => d.KpiItem)
                    .WithMany(p => p.KpiItemContents)
                    .HasForeignKey(d => d.KpiItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItemContent_KpiItem");
            });

            modelBuilder.Entity<KpiItemContentKpiCriteriaItemMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.KpiItemContentId, e.KpiCriteriaItemId });

                entity.HasOne(d => d.KpiCriteriaItem)
                    .WithMany(p => p.KpiItemContentKpiCriteriaItemMappings)
                    .HasForeignKey(d => d.KpiCriteriaItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItemContentKpiCriteriaItemMapping_KpiCriteriaItem");

                entity.HasOne(d => d.KpiItemContent)
                    .WithMany(p => p.KpiItemContentKpiCriteriaItemMappings)
                    .HasForeignKey(d => d.KpiItemContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KpiItemContentKpiCriteriaItemMapping_KpiItemContent");
            });

            modelBuilder.Entity<KpiPeriodDAO>(entity =>
            {
                //entity.ToTable("KpiPeriod", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<KpiYearDAO>(entity =>
            {
                //entity.ToTable("KpiYear", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<LastestEventMessageDAO>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LastestEventMessage");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RoutingKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<MailTemplateDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.MailTemplates)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_MailTemplate_Status");
            });

            modelBuilder.Entity<MarketPriceDAO>(entity =>
            {
                //entity.ToTable("MarketPrice", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MenuDAO>(entity =>
            {
                //entity.ToTable("Menu", "PER");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.Path).HasMaxLength(3000);
            });

            modelBuilder.Entity<NationDAO>(entity =>
            {
                //entity.ToTable("Nation", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("Mã quận huyện");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StatusId).HasComment("Trạng thái hoạt động");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Nations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nation_Status");
            });

            modelBuilder.Entity<NotificationDAO>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.NotificationStatus)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_NotificationStatus");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Notification_Organization");
            });

            modelBuilder.Entity<NotificationStatusDAO>(entity =>
            {
                //entity.ToTable("NotificationStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OpportunityDAO>(entity =>
            {
                //entity.ToTable("Opportunity", "OPP");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ClosingDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.ForecastAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.OpportunityAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opportunity_AppUser");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Opportunity_Company");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.OpportunityCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opportunity_AppUser1");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_Opportunity_Currency");

                entity.HasOne(d => d.CustomerLead)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.CustomerLeadId)
                    .HasConstraintName("FK_Opportunity_CustomerLead");

                entity.HasOne(d => d.LeadSource)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.LeadSourceId)
                    .HasConstraintName("FK_Opportunity_CustomerLeadSource");

                entity.HasOne(d => d.OpportunityResultType)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.OpportunityResultTypeId)
                    .HasConstraintName("FK_Opportunity_OpportunityResultType");

                entity.HasOne(d => d.PotentialResult)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.PotentialResultId)
                    .HasConstraintName("FK_Opportunity_PotentialResult");

                entity.HasOne(d => d.Probability)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.ProbabilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opportunity_Probability");

                entity.HasOne(d => d.SaleStage)
                    .WithMany(p => p.Opportunities)
                    .HasForeignKey(d => d.SaleStageId)
                    .HasConstraintName("FK_Opportunity_SaleStage");
            });

            modelBuilder.Entity<OpportunityActivityDAO>(entity =>
            {
                //entity.ToTable("OpportunityActivity", "OPP");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.ActivityPriority)
                    .WithMany(p => p.OpportunityActivities)
                    .HasForeignKey(d => d.ActivityPriorityId)
                    .HasConstraintName("FK_OpportunityActivity_ActivityPriority");

                entity.HasOne(d => d.ActivityStatus)
                    .WithMany(p => p.OpportunityActivities)
                    .HasForeignKey(d => d.ActivityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityActivity_ActivityStatus");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.OpportunityActivities)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityActivity_ActivityType");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.OpportunityActivities)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityActivity_AppUser");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OpportunityActivities)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityActivity_Opportunity");
            });

            modelBuilder.Entity<OpportunityCallLogMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.OpportunityId, e.CallLogId });

                //entity.ToTable("OpportunityCallLogMapping", "OPP");

                entity.HasOne(d => d.CallLog)
                    .WithMany(p => p.OpportunityCallLogMappings)
                    .HasForeignKey(d => d.CallLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityCallLogMapping_CallLog");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OpportunityCallLogMappings)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityCallLogMapping_Opportunity");
            });

            modelBuilder.Entity<OpportunityContactMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.OpportunityId });

                //entity.ToTable("OpportunityContactMapping", "OPP");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.OpportunityContactMappings)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityContactMapping_Contact");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OpportunityContactMappings)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityContactMapping_Opportunity");
            });

            modelBuilder.Entity<OpportunityEmailDAO>(entity =>
            {
                //entity.ToTable("OpportunityEmail", "OPP");

                entity.Property(e => e.Content).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Reciepient)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.OpportunityEmails)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityEmail_AppUser");

                entity.HasOne(d => d.EmailStatus)
                    .WithMany(p => p.OpportunityEmails)
                    .HasForeignKey(d => d.EmailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityEmail_EmailStatus");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OpportunityEmails)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityEmail_Opportunity");
            });

            modelBuilder.Entity<OpportunityEmailCCMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.OpportunityEmailId, e.AppUserId })
                    .HasName("PK_OpporutunityEmaiCCMapping");

                //entity.ToTable("OpportunityEmailCCMapping", "OPP");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.OpportunityEmailCCMappings)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityEmailCCMapping_AppUser");

                entity.HasOne(d => d.OpportunityEmail)
                    .WithMany(p => p.OpportunityEmailCCMappings)
                    .HasForeignKey(d => d.OpportunityEmailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityEmailCCMapping_OpportunityEmail");
            });

            modelBuilder.Entity<OpportunityFileGroupingDAO>(entity =>
            {
                //entity.ToTable("OpportunityFileGrouping", "OPP");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.OpportunityFileGroupings)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityFileGrouping_AppUser");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.OpportunityFileGroupings)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityFileGrouping_FileType");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OpportunityFileGroupings)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityFileGrouping_Opportunity");
            });

            modelBuilder.Entity<OpportunityFileMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.OpportunityFileGroupingId, e.FileId });

                //entity.ToTable("OpportunityFileMapping", "OPP");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.OpportunityFileMappings)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityFileMapping_File");

                entity.HasOne(d => d.OpportunityFileGrouping)
                    .WithMany(p => p.OpportunityFileMappings)
                    .HasForeignKey(d => d.OpportunityFileGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityFileMapping_OpportunityFileGrouping");
            });

            modelBuilder.Entity<OpportunityItemMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.OpportunityId, e.ItemId });

                //entity.ToTable("OpportunityItemMapping", "OPP");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrimaryPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VAT).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VATOther).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OpportunityItemMappings)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityItemMapping_Item");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OpportunityItemMappings)
                    .HasForeignKey(d => d.OpportunityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityProductMapping_Opportunity");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.OpportunityItemMappings)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpportunityItemMapping_UnitOfMeasure");
            });

            modelBuilder.Entity<OpportunityResultTypeDAO>(entity =>
            {
                //entity.ToTable("OpportunityResultType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderCategoryDAO>(entity =>
            {
                //entity.ToTable("OrderCategory", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderPaymentStatusDAO>(entity =>
            {
                //entity.ToTable("OrderPaymentStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<OrderQuoteDAO>(entity =>
            {
                //entity.ToTable("OrderQuote", "OPP");

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.InvoiceAddress).HasMaxLength(1000);

                entity.Property(e => e.InvoiceZIPCode).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalTaxAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalTaxAmountOther).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(curdate())");

                entity.Property(e => e.ZIPCode).HasMaxLength(255);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.OrderQuoteAppUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuote_AppUser");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.OrderQuotes)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuote_Company");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.OrderQuotes)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuote_Contact");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.OrderQuoteCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuote_AppUser1");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.OrderQuoteDistricts)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_OrderQuote_District1");

                entity.HasOne(d => d.EditedPriceStatus)
                    .WithMany(p => p.OrderQuotes)
                    .HasForeignKey(d => d.EditedPriceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuote_EditedPriceStatus");

                entity.HasOne(d => d.InvoiceDistrict)
                    .WithMany(p => p.OrderQuoteInvoiceDistricts)
                    .HasForeignKey(d => d.InvoiceDistrictId)
                    .HasConstraintName("FK_OrderQuote_District");

                entity.HasOne(d => d.InvoiceNation)
                    .WithMany(p => p.OrderQuoteInvoiceNations)
                    .HasForeignKey(d => d.InvoiceNationId)
                    .HasConstraintName("FK_OrderQuote_Nation1");

                entity.HasOne(d => d.InvoiceProvince)
                    .WithMany(p => p.OrderQuoteInvoiceProvinces)
                    .HasForeignKey(d => d.InvoiceProvinceId)
                    .HasConstraintName("FK_OrderQuote_Province1");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.OrderQuoteNations)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_OrderQuote_Nation");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.OrderQuotes)
                    .HasForeignKey(d => d.OpportunityId)
                    .HasConstraintName("FK_OrderQuote_Opportunity");

                entity.HasOne(d => d.OrderQuoteStatus)
                    .WithMany(p => p.OrderQuotes)
                    .HasForeignKey(d => d.OrderQuoteStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuote_OrderQuoteStatus");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.OrderQuoteProvinces)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_OrderQuote_Province");
            });

            modelBuilder.Entity<OrderQuoteContentDAO>(entity =>
            {
                //entity.ToTable("OrderQuoteContent", "OPP");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.GeneralDiscountAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GeneralDiscountPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PrimaryPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxAmountOther).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TaxPercentage).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TaxPercentageOther).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.EditedPriceStatus)
                    .WithMany(p => p.OrderQuoteContents)
                    .HasForeignKey(d => d.EditedPriceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuoteContent_EditedPriceStatus");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderQuoteContents)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuoteContent_Item");

                entity.HasOne(d => d.OrderQuote)
                    .WithMany(p => p.OrderQuoteContents)
                    .HasForeignKey(d => d.OrderQuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuoteContent_OrderQuote");

                entity.HasOne(d => d.PrimaryUnitOfMeasure)
                    .WithMany(p => p.OrderQuoteContentPrimaryUnitOfMeasures)
                    .HasForeignKey(d => d.PrimaryUnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuoteContent_UnitOfMeasure1");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.OrderQuoteContents)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuoteContent_TaxType");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.OrderQuoteContentUnitOfMeasures)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderQuoteContent_UnitOfMeasure");
            });

            modelBuilder.Entity<OrderQuoteStatusDAO>(entity =>
            {
                //entity.ToTable("OrderQuoteStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OrganizationDAO>(entity =>
            {
                //entity.ToTable("Organization", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Organization_Organization");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organization_Status");
            });

            modelBuilder.Entity<PageDAO>(entity =>
            {
                //entity.ToTable("Page", "PER");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<PaymentStatusDAO>(entity =>
            {
                //entity.ToTable("PaymentStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PermissionDAO>(entity =>
            {
                //entity.ToTable("Permission", "PER");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Role");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Status");
            });

            modelBuilder.Entity<PermissionActionMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ActionId, e.PermissionId })
                    .HasName("PK_ActionPermissionMapping");

                //entity.ToTable("PermissionActionMapping", "PER");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.PermissionActionMappings)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionPermissionMapping_Action");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionActionMappings)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionPermissionMapping_Permission");
            });

            modelBuilder.Entity<PermissionContentDAO>(entity =>
            {
                //entity.ToTable("PermissionContent", "PER");

                entity.Property(e => e.Value).HasMaxLength(500);

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.PermissionContents)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionContent_Field");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionContents)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionContent_Permission");

                entity.HasOne(d => d.PermissionOperator)
                    .WithMany(p => p.PermissionContents)
                    .HasForeignKey(d => d.PermissionOperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionContent_PermissionOperator");
            });

            modelBuilder.Entity<PermissionOperatorDAO>(entity =>
            {
                //entity.ToTable("PermissionOperator", "PER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FieldType)
                    .WithMany(p => p.PermissionOperators)
                    .HasForeignKey(d => d.FieldTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionOperator_FieldType");
            });

            modelBuilder.Entity<PhoneTypeDAO>(entity =>
            {
                //entity.ToTable("PhoneType", "MDM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PhoneTypes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhoneType_Status");
            });

            modelBuilder.Entity<PositionDAO>(entity =>
            {
                //entity.ToTable("Position", "MDM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Position_Status");
            });

            modelBuilder.Entity<PotentialResultDAO>(entity =>
            {
                //entity.ToTable("PotentialResult", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ProbabilityDAO>(entity =>
            {
                //entity.ToTable("Probability", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ProductDAO>(entity =>
            {
                //entity.ToTable("Product", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(3000);

                entity.Property(e => e.ERPCode).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.Note).HasMaxLength(3000);

                entity.Property(e => e.OtherName).HasMaxLength(1000);

                entity.Property(e => e.RetailPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ScanCode).HasMaxLength(500);

                entity.Property(e => e.TechnicalName).HasMaxLength(1000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductType");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Status");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_TaxType");

                entity.HasOne(d => d.UnitOfMeasureGrouping)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitOfMeasureGroupingId)
                    .HasConstraintName("FK_Product_UnitOfMeasureGrouping");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_UnitOfMeasure");

                entity.HasOne(d => d.UsedVariation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UsedVariationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_UsedVariation");
            });

            modelBuilder.Entity<ProductGroupingDAO>(entity =>
            {
                //entity.ToTable("ProductGrouping", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ProductGrouping_ProductGrouping");
            });

            modelBuilder.Entity<ProductImageMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ImageId });

                //entity.ToTable("ProductImageMapping", "MDM");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ProductImageMappings)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImageMapping_Image");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImageMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImageMapping_Product");
            });

            modelBuilder.Entity<ProductProductGroupingMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ProductGroupingId });

                //entity.ToTable("ProductProductGroupingMapping", "MDM");

                entity.HasOne(d => d.ProductGrouping)
                    .WithMany(p => p.ProductProductGroupingMappings)
                    .HasForeignKey(d => d.ProductGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductProductGroupingMapping_ProductGrouping");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProductGroupingMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductProductGroupingMapping_Product");
            });

            modelBuilder.Entity<ProductTypeDAO>(entity =>
            {
                //entity.ToTable("ProductType", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(3000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ProductTypes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductType_Status");
            });

            modelBuilder.Entity<ProfessionDAO>(entity =>
            {
                //entity.ToTable("Profession", "MDM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Professions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profession_Status");
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                //entity.ToTable("Province", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Province_Status");
            });

            modelBuilder.Entity<RatingStatusDAO>(entity =>
            {
                //entity.ToTable("RatingStatus", "ENUM");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<RelationshipCustomerTypeDAO>(entity =>
            {
                //entity.ToTable("RelationshipCustomerType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<RepairStatusDAO>(entity =>
            {
                //entity.ToTable("RepairStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RepairTicketDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.DeviceSerial).HasMaxLength(500);

                entity.Property(e => e.DeviceState)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.ReceiveDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiveUser).HasMaxLength(255);

                entity.Property(e => e.RejectReason).HasMaxLength(4000);

                entity.Property(e => e.RepairAddess).HasMaxLength(500);

                entity.Property(e => e.RepairCost).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RepairDate).HasColumnType("datetime");

                entity.Property(e => e.RepairDueDate).HasColumnType("datetime");

                entity.Property(e => e.RepairSolution).HasMaxLength(4000);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.RepairTickets)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairTicket_AppUser");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RepairTickets)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairTicket_Customer");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.RepairTickets)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_RepairTicket_Item");

                entity.HasOne(d => d.OrderCategory)
                    .WithMany(p => p.RepairTickets)
                    .HasForeignKey(d => d.OrderCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepairTicket_OrderCategory");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.RepairTickets)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_RepairTicket_PaymentStatus");

                entity.HasOne(d => d.RepairStatus)
                    .WithMany(p => p.RepairTickets)
                    .HasForeignKey(d => d.RepairStatusId)
                    .HasConstraintName("FK_RepairTicket_RepairStatus");
            });

            modelBuilder.Entity<RequestStateDAO>(entity =>
            {
                //entity.ToTable("RequestState", "WF");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RequestWorkflowDefinitionMappingDAO>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                //entity.ToTable("RequestWorkflowDefinitionMapping", "WF");

                entity.Property(e => e.RequestId).ValueGeneratedNever();

                entity.HasOne(d => d.RequestState)
                    .WithMany(p => p.RequestWorkflowDefinitionMappings)
                    .HasForeignKey(d => d.RequestStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestWorkflowDefinitionMapping_RequestState");

                entity.HasOne(d => d.WorkflowDefinition)
                    .WithMany(p => p.RequestWorkflowDefinitionMappings)
                    .HasForeignKey(d => d.WorkflowDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestWorkflowDefinitionMapping_WorkflowDefinition");
            });

            modelBuilder.Entity<RequestWorkflowParameterMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.WorkflowParameterId, e.RequestId });

                //entity.ToTable("RequestWorkflowParameterMapping", "WF");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Value).HasMaxLength(500);

                entity.HasOne(d => d.WorkflowParameter)
                    .WithMany(p => p.RequestWorkflowParameterMappings)
                    .HasForeignKey(d => d.WorkflowParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreWorkflowParameterMapping_WorkflowParameter");
            });

            modelBuilder.Entity<RequestWorkflowStepMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.WorkflowStepId });

                //entity.ToTable("RequestWorkflowStepMapping", "WF");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.RequestWorkflowStepMappings)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_StoreWorkflow_AppUser");

                entity.HasOne(d => d.WorkflowState)
                    .WithMany(p => p.RequestWorkflowStepMappings)
                    .HasForeignKey(d => d.WorkflowStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreWorkflow_WorkflowState");

                entity.HasOne(d => d.WorkflowStep)
                    .WithMany(p => p.RequestWorkflowStepMappings)
                    .HasForeignKey(d => d.WorkflowStepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreWorkflow_WorkflowStep");
            });

            modelBuilder.Entity<RoleDAO>(entity =>
            {
                //entity.ToTable("Role", "PER");

                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Status");
            });

            modelBuilder.Entity<SLAAlertDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.MailTemplate)
                    .WithMany(p => p.SLAAlerts)
                    .HasForeignKey(d => d.MailTemplateId)
                    .HasConstraintName("FK_SLAAlert_MailTemplate");

                entity.HasOne(d => d.SmsTemplate)
                    .WithMany(p => p.SLAAlerts)
                    .HasForeignKey(d => d.SmsTemplateId)
                    .HasConstraintName("FK_SLAAlert_SmsTemplate");

                entity.HasOne(d => d.TicketIssueLevel)
                    .WithMany(p => p.SLAAlerts)
                    .HasForeignKey(d => d.TicketIssueLevelId)
                    .HasConstraintName("FK_SLAAlert_TicketIssueLevel");

                entity.HasOne(d => d.TimeUnit)
                    .WithMany(p => p.SLAAlerts)
                    .HasForeignKey(d => d.TimeUnitId)
                    .HasConstraintName("FK_SLAAlert_SLATimeUnit");
            });

            modelBuilder.Entity<SLAAlertFRTDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.MailTemplate)
                    .WithMany(p => p.SLAAlertFRTs)
                    .HasForeignKey(d => d.MailTemplateId)
                    .HasConstraintName("FK_SLAAlertFRT_MailTemplate");

                entity.HasOne(d => d.SmsTemplate)
                    .WithMany(p => p.SLAAlertFRTs)
                    .HasForeignKey(d => d.SmsTemplateId)
                    .HasConstraintName("FK_SLAAlertFRT_SmsTemplate");

                entity.HasOne(d => d.TicketIssueLevel)
                    .WithMany(p => p.SLAAlertFRTs)
                    .HasForeignKey(d => d.TicketIssueLevelId)
                    .HasConstraintName("FK_SLAAlertFRT_TicketIssueLevel");

                entity.HasOne(d => d.TimeUnit)
                    .WithMany(p => p.SLAAlertFRTs)
                    .HasForeignKey(d => d.TimeUnitId)
                    .HasConstraintName("FK_SLAAlertFRT_SLATimeUnit");
            });

            modelBuilder.Entity<SLAAlertFRTMailDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAAlertFRT)
                    .WithMany(p => p.SLAAlertFRTMails)
                    .HasForeignKey(d => d.SLAAlertFRTId)
                    .HasConstraintName("FK_SLAAlertFRTMail_SLAAlertFRT");
            });

            modelBuilder.Entity<SLAAlertFRTPhoneDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAAlertFRT)
                    .WithMany(p => p.SLAAlertFRTPhones)
                    .HasForeignKey(d => d.SLAAlertFRTId)
                    .HasConstraintName("FK_SLAAlertFRTPhone_SLAAlertFRT");
            });

            modelBuilder.Entity<SLAAlertFRTUserDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.SLAAlertFRTUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_SLAAlertFRTUser_AppUser");

                entity.HasOne(d => d.SLAAlertFRT)
                    .WithMany(p => p.SLAAlertFRTUsers)
                    .HasForeignKey(d => d.SLAAlertFRTId)
                    .HasConstraintName("FK_SLAAlertFRTUser_SLAAlertFRT");
            });

            modelBuilder.Entity<SLAAlertMailDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAAlert)
                    .WithMany(p => p.SLAAlertMails)
                    .HasForeignKey(d => d.SLAAlertId)
                    .HasConstraintName("FK_SLAAlertMail_SLAAlert");
            });

            modelBuilder.Entity<SLAAlertPhoneDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAAlert)
                    .WithMany(p => p.SLAAlertPhones)
                    .HasForeignKey(d => d.SLAAlertId)
                    .HasConstraintName("FK_SLAAlertPhone_SLAAlert");
            });

            modelBuilder.Entity<SLAAlertUserDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.SLAAlertUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_SLAAlertUser_AppUser");

                entity.HasOne(d => d.SLAAlert)
                    .WithMany(p => p.SLAAlertUsers)
                    .HasForeignKey(d => d.SLAAlertId)
                    .HasConstraintName("FK_SLAAlertUser_SLAAlert");
            });

            modelBuilder.Entity<SLAEscalationDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.MailTemplate)
                    .WithMany(p => p.SLAEscalations)
                    .HasForeignKey(d => d.MailTemplateId)
                    .HasConstraintName("FK_SLAEscalation_MailTemplate");

                entity.HasOne(d => d.SmsTemplate)
                    .WithMany(p => p.SLAEscalations)
                    .HasForeignKey(d => d.SmsTemplateId)
                    .HasConstraintName("FK_SLAEscalation_SmsTemplate");

                entity.HasOne(d => d.TicketIssueLevel)
                    .WithMany(p => p.SLAEscalations)
                    .HasForeignKey(d => d.TicketIssueLevelId)
                    .HasConstraintName("FK_SLAEscalation_TicketIssueLevel");

                entity.HasOne(d => d.TimeUnit)
                    .WithMany(p => p.SLAEscalations)
                    .HasForeignKey(d => d.TimeUnitId)
                    .HasConstraintName("FK_SLAEscalation_SLATimeUnit");
            });

            modelBuilder.Entity<SLAEscalationFRTDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.MailTemplate)
                    .WithMany(p => p.SLAEscalationFRTs)
                    .HasForeignKey(d => d.MailTemplateId)
                    .HasConstraintName("FK_SLAEscalationFRT_MailTemplate");

                entity.HasOne(d => d.SmsTemplate)
                    .WithMany(p => p.SLAEscalationFRTs)
                    .HasForeignKey(d => d.SmsTemplateId)
                    .HasConstraintName("FK_SLAEscalationFRT_SmsTemplate");

                entity.HasOne(d => d.TicketIssueLevel)
                    .WithMany(p => p.SLAEscalationFRTs)
                    .HasForeignKey(d => d.TicketIssueLevelId)
                    .HasConstraintName("FK_SLAEscalationFRT_TicketIssueLevel");

                entity.HasOne(d => d.TimeUnit)
                    .WithMany(p => p.SLAEscalationFRTs)
                    .HasForeignKey(d => d.TimeUnitId)
                    .HasConstraintName("FK_SLAEscalationFRT_SLATimeUnit");
            });

            modelBuilder.Entity<SLAEscalationFRTMailDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAEscalationFRT)
                    .WithMany(p => p.SLAEscalationFRTMails)
                    .HasForeignKey(d => d.SLAEscalationFRTId)
                    .HasConstraintName("FK_SLAEscalationFRTMail_SLAEscalationFRT");
            });

            modelBuilder.Entity<SLAEscalationFRTPhoneDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAEscalationFRT)
                    .WithMany(p => p.SLAEscalationFRTPhones)
                    .HasForeignKey(d => d.SLAEscalationFRTId)
                    .HasConstraintName("FK_SLAEscalationFRTPhone_SLAEscalationFRT");
            });

            modelBuilder.Entity<SLAEscalationFRTUserDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.SLAEscalationFRTUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_SLAEscalationFRTUser_AppUser");

                entity.HasOne(d => d.SLAEscalationFRT)
                    .WithMany(p => p.SLAEscalationFRTUsers)
                    .HasForeignKey(d => d.SLAEscalationFRTId)
                    .HasConstraintName("FK_SLAEscalationFRTUser_SLAEscalationFRT");
            });

            modelBuilder.Entity<SLAEscalationMailDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SLAEscalation)
                    .WithMany(p => p.SLAEscalationMails)
                    .HasForeignKey(d => d.SLAEscalationId)
                    .HasConstraintName("FK_SLAEscalationMail_SLAEscalation");
            });

            modelBuilder.Entity<SLAEscalationPhoneDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<SLAEscalationUserDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.SLAEscalationUsers)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_SLAEscalationUser_AppUser");

                entity.HasOne(d => d.SLAEscalation)
                    .WithMany(p => p.SLAEscalationUsers)
                    .HasForeignKey(d => d.SLAEscalationId)
                    .HasConstraintName("FK_SLAEscalationUser_SLAEscalation");
            });

            modelBuilder.Entity<SLAPolicyDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.FirstResponseUnit)
                    .WithMany(p => p.SLAPolicyFirstResponseUnits)
                    .HasForeignKey(d => d.FirstResponseUnitId)
                    .HasConstraintName("FK_SLAPolicy_SLATimeUnit");

                entity.HasOne(d => d.ResolveUnit)
                    .WithMany(p => p.SLAPolicyResolveUnits)
                    .HasForeignKey(d => d.ResolveUnitId)
                    .HasConstraintName("FK_SLAPolicy_SLATimeUnit1");

                entity.HasOne(d => d.TicketIssueLevel)
                    .WithMany(p => p.SLAPolicies)
                    .HasForeignKey(d => d.TicketIssueLevelId)
                    .HasConstraintName("FK_SLAPolicy_TicketIssueLevel");

                entity.HasOne(d => d.TicketPriority)
                    .WithMany(p => p.SLAPolicies)
                    .HasForeignKey(d => d.TicketPriorityId)
                    .HasConstraintName("FK_SLAPolicy_TicketPriority");
            });

            modelBuilder.Entity<SLAStatusDAO>(entity =>
            {
                //entity.ToTable("SLAStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ColorCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SLATimeUnitDAO>(entity =>
            {
                //entity.ToTable("SLATimeUnit", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SaleStageDAO>(entity =>
            {
                //entity.ToTable("SaleStage", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ScheduleMasterDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RecurDays).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartDayOfWeek).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ScheduleMasterManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_ScheduleMaster_AppUser2");

                entity.HasOne(d => d.Saler)
                    .WithMany(p => p.ScheduleMasterSalers)
                    .HasForeignKey(d => d.SalerId)
                    .HasConstraintName("FK_ScheduleMaster_AppUser3");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ScheduleMasters)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_ScheduleMaster_Status");
            });

            modelBuilder.Entity<SexDAO>(entity =>
            {
                //entity.ToTable("Sex", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SmsQueueDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.EntityReference)
                    .WithMany(p => p.SmsQueues)
                    .HasForeignKey(d => d.EntityReferenceId)
                    .HasConstraintName("FK_SmsQueue_SmsQueueReference");

                entity.HasOne(d => d.SentByAppUser)
                    .WithMany(p => p.SmsQueues)
                    .HasForeignKey(d => d.SentByAppUserId)
                    .HasConstraintName("FK_SmsQueue_AppUser");

                entity.HasOne(d => d.SmsQueueStatus)
                    .WithMany(p => p.SmsQueues)
                    .HasForeignKey(d => d.SmsQueueStatusId)
                    .HasConstraintName("FK_SmsQueue_SmsQueueStatus");
            });

            modelBuilder.Entity<SmsQueueStatusDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");
            });

            modelBuilder.Entity<SmsTemplateDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày xoá");

                entity.Property(e => e.Name).HasMaxLength(2000);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("Ngày cập nhật");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SmsTemplates)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_SmsTemplate_Status");
            });

            modelBuilder.Entity<SocialChannelTypeDAO>(entity =>
            {
                //entity.ToTable("SocialChannelType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StatusDAO>(entity =>
            {
                //entity.ToTable("Status", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<StoreDAO>(entity =>
            {
                //entity.ToTable("Store", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(3000);

                entity.Property(e => e.Code).HasMaxLength(400);

                entity.Property(e => e.CodeDraft).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(3000);

                entity.Property(e => e.DeliveryLatitude).HasColumnType("decimal(18, 15)");

                entity.Property(e => e.DeliveryLongitude).HasColumnType("decimal(18, 15)");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 15)");

                entity.Property(e => e.LegalEntity).HasMaxLength(500);

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 15)");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.OwnerEmail).HasMaxLength(500);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerPhone)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TaxCode).HasMaxLength(500);

                entity.Property(e => e.Telephone).HasMaxLength(500);

                entity.Property(e => e.UnsignAddress).HasMaxLength(3000);

                entity.Property(e => e.UnsignName).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK_Store_AppUser");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Store_Customer");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Store_District");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Organization");

                entity.HasOne(d => d.ParentStore)
                    .WithMany(p => p.InverseParentStore)
                    .HasForeignKey(d => d.ParentStoreId)
                    .HasConstraintName("FK_Store_Store");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Store_Province");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Status");

                entity.HasOne(d => d.StoreGrouping)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreGroupingId)
                    .HasConstraintName("FK_Store_StoreGrouping");

                entity.HasOne(d => d.StoreStatus)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_StoreStatus");

                entity.HasOne(d => d.StoreType)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_StoreType");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_Store_Ward");
            });

            modelBuilder.Entity<StoreAssetsDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreAssets)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StoreAssets_Store");
            });

            modelBuilder.Entity<StoreConsultingServiceMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ConsultingServiceId });

                entity.HasOne(d => d.ConsultingService)
                    .WithMany(p => p.StoreConsultingServiceMappings)
                    .HasForeignKey(d => d.ConsultingServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreConsultingServiceMapping_ConsultingService");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreConsultingServiceMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreConsultingServiceMapping_Store");
            });

            modelBuilder.Entity<StoreCooperativeAttitudeMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.CooperativeAttitudeId });

                entity.HasOne(d => d.CooperativeAttitude)
                    .WithMany(p => p.StoreCooperativeAttitudeMappings)
                    .HasForeignKey(d => d.CooperativeAttitudeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreCooperativeAttitudeMapping_CooperativeAttitude");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreCooperativeAttitudeMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreCooperativeAttitudeMapping_Store");
            });

            modelBuilder.Entity<StoreCoverageCapacityDAO>(entity =>
            {
                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreCoverageCapacities)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StoreCoverageCapacity_Store");
            });

            modelBuilder.Entity<StoreDeliveryTimeDAO>(entity =>
            {
                //entity.ToTable("StoreDeliveryTime", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StoreDeliveryTimeMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.StoreDeliveryTimeId });

                entity.HasOne(d => d.StoreDeliveryTime)
                    .WithMany(p => p.StoreDeliveryTimeMappings)
                    .HasForeignKey(d => d.StoreDeliveryTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreDeliveryTimeMapping_StoreDeliveryTime");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreDeliveryTimeMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreDeliveryTimeMapping_Store");
            });

            modelBuilder.Entity<StoreExtendDAO>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.StoreId).ValueGeneratedNever();

                entity.Property(e => e.AbilityToPay).IsRequired();

                entity.Property(e => e.AgentContractNumber)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BankAccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BusinessCapital).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BusinessLicense)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DateOfAgentContractNumber).HasColumnType("datetime");

                entity.Property(e => e.DateOfBusinessLicense).HasColumnType("datetime");

                entity.Property(e => e.DistributionAcreage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DistributionArea)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Invest).HasMaxLength(1000);

                entity.Property(e => e.MarketCharacteristics).IsRequired();

                entity.Property(e => e.PhoneOther)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReadyCoordinate).HasMaxLength(1000);

                entity.Property(e => e.StoreAcreage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UrbanizationLevel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WareHouseAcreage).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.BusinessType)
                    .WithMany(p => p.StoreExtends)
                    .HasForeignKey(d => d.BusinessTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreExtend_BusinessType");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.StoreExtends)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreExtend_Currency");

                entity.HasOne(d => d.Store)
                    .WithOne(p => p.StoreExtend)
                    .HasForeignKey<StoreExtendDAO>(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreExtend_Store");
            });

            modelBuilder.Entity<StoreGroupingDAO>(entity =>
            {
                //entity.ToTable("StoreGrouping", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_StoreGrouping_StoreGrouping");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StoreGroupings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreGrouping_Status");
            });

            modelBuilder.Entity<StoreImageMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ImageId });

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.StoreImageMappings)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreImageMapping_Image");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreImageMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreImageMapping_Store");
            });

            modelBuilder.Entity<StoreInfulenceLevelMarketMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.InfulenceLevelMarketId });

                entity.HasOne(d => d.InfulenceLevelMarket)
                    .WithMany(p => p.StoreInfulenceLevelMarketMappings)
                    .HasForeignKey(d => d.InfulenceLevelMarketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreInfulenceLevelMarketMapping_InfulenceLevelMarket");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreInfulenceLevelMarketMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreInfulenceLevelMarketMapping_Store");
            });

            modelBuilder.Entity<StoreMarketPriceMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.MarketPriceId });

                entity.HasOne(d => d.MarketPrice)
                    .WithMany(p => p.StoreMarketPriceMappings)
                    .HasForeignKey(d => d.MarketPriceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreMarketPriceMapping_MarketPrice");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreMarketPriceMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreMarketPriceMapping_Store");
            });

            modelBuilder.Entity<StoreMeansOfDeliveryDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreMeansOfDeliveries)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StoreMeansOfDelivery_Store");
            });

            modelBuilder.Entity<StorePersonnelDAO>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StorePersonnels)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StorePersonnel_Store");
            });

            modelBuilder.Entity<StoreRelationshipCustomerMappingDAO>(entity =>
            {
                entity.HasKey(e => new { e.RelationshipCustomerTypeId, e.StoreId })
                    .HasName("PK_StoreRelationshipCustomer");

                entity.HasOne(d => d.RelationshipCustomerType)
                    .WithMany(p => p.StoreRelationshipCustomerMappings)
                    .HasForeignKey(d => d.RelationshipCustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreRelationshipCustomerMapping_RelationshipCustomerType");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreRelationshipCustomerMappings)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreRelationshipCustomerMapping_Store");
            });

            modelBuilder.Entity<StoreRepresentDAO>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.StoreRepresents)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_StoreRepresent_Position");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreRepresents)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreRepresent_Store");
            });

            modelBuilder.Entity<StoreStatusDAO>(entity =>
            {
                //entity.ToTable("StoreStatus", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<StoreTypeDAO>(entity =>
            {
                //entity.ToTable("StoreType", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.StoreTypes)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_StoreType_Color");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StoreTypes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreType_Status");
            });

            modelBuilder.Entity<StoreWarrantyServiceDAO>(entity =>
            {
                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreWarrantyServices)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreWarrantyService_Store");
            });

            modelBuilder.Entity<SupplierDAO>(entity =>
            {
                //entity.ToTable("Supplier", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(2000);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.TaxCode).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Supplier_District");

                entity.HasOne(d => d.Nation)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK_Supplier_Nation");

                entity.HasOne(d => d.PersonInCharge)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.PersonInChargeId)
                    .HasConstraintName("FK_Supplier_AppUser");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Supplier_Province");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_Status");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_Supplier_Ward");
            });

            modelBuilder.Entity<TaxTypeDAO>(entity =>
            {
                //entity.ToTable("TaxType", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Percentage).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TaxTypes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxType_Status");
            });

            modelBuilder.Entity<TicketDAO>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.FirstResponeTime).HasColumnType("datetime");

                entity.Property(e => e.FirstResponseAt).HasColumnType("datetime");

                entity.Property(e => e.LastHoldingAt).HasColumnType("datetime");

                entity.Property(e => e.LastResponseAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.ProcessDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiveDate).HasColumnType("datetime");

                entity.Property(e => e.ResolveTime).HasColumnType("datetime");

                entity.Property(e => e.ResolvedAt).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TicketNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.closedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AppUserClosed)
                    .WithMany(p => p.TicketAppUserCloseds)
                    .HasForeignKey(d => d.AppUserClosedId)
                    .HasConstraintName("FK_Ticket_AppUser2");

                entity.HasOne(d => d.AppUserResolved)
                    .WithMany(p => p.TicketAppUserResolveds)
                    .HasForeignKey(d => d.AppUserResolvedId)
                    .HasConstraintName("FK_Ticket_AppUser3");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.TicketCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_AppUser1");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Customer");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_CustomerType");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Ticket_Organization");

                entity.HasOne(d => d.EntityReference)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EntityReferenceId)
                    .HasConstraintName("FK_Ticket_TicketReference");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Ticket_Product");

                entity.HasOne(d => d.RelatedCallLog)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.RelatedCallLogId)
                    .HasConstraintName("FK_Ticket_CallLog");

                entity.HasOne(d => d.RelatedTicket)
                    .WithMany(p => p.InverseRelatedTicket)
                    .HasForeignKey(d => d.RelatedTicketId)
                    .HasConstraintName("FK_Ticket_Ticket");

                entity.HasOne(d => d.SLAPolicy)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SLAPolicyId)
                    .HasConstraintName("FK_Ticket_SLAPolicy");

                entity.HasOne(d => d.SLAStatus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SLAStatusId)
                    .HasConstraintName("FK_Ticket_SLAStatus");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Status");

                entity.HasOne(d => d.TicketIssueLevel)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketIssueLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_TicketIssueLevel");

                entity.HasOne(d => d.TicketPriority)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketPriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_TicketPriority");

                entity.HasOne(d => d.TicketResolveType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketResolveTypeId)
                    .HasConstraintName("FK_Ticket_TicketResolveType");

                entity.HasOne(d => d.TicketSource)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_TicketSource");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_TicketStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TicketUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_AppUser");
            });

            modelBuilder.Entity<TicketGeneratedIdDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<TicketGroupDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TicketGroups)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketGroup_Status");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.TicketGroups)
                    .HasForeignKey(d => d.TicketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketGroup_TicketType");
            });

            modelBuilder.Entity<TicketIssueLevelDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TicketIssueLevels)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketIssueLevel_Status");

                entity.HasOne(d => d.TicketGroup)
                    .WithMany(p => p.TicketIssueLevels)
                    .HasForeignKey(d => d.TicketGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketIssueLevel_TicketGroup");
            });

            modelBuilder.Entity<TicketOfDepartmentDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(4000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TicketOfDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketOfDepartment_Organization");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketOfDepartments)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketOfDepartment_Ticket");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.TicketOfDepartments)
                    .HasForeignKey(d => d.TicketStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketOfDepartment_TicketStatus");
            });

            modelBuilder.Entity<TicketOfUserDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(4000);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketOfUsers)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketOfUser_Ticket");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.TicketOfUsers)
                    .HasForeignKey(d => d.TicketStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketOfUser_TicketStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TicketOfUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketOfUser_AppUser");
            });

            modelBuilder.Entity<TicketPriorityDAO>(entity =>
            {
                entity.Property(e => e.ColorCode).HasMaxLength(20);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TicketPriorities)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketPriority_Status");
            });

            modelBuilder.Entity<TicketResolveTypeDAO>(entity =>
            {
                //entity.ToTable("TicketResolveType", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TicketSourceDAO>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TicketSources)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketSource_Status");
            });

            modelBuilder.Entity<TicketStatusDAO>(entity =>
            {
                //entity.ToTable("TicketStatus", "MDM");

                entity.Property(e => e.ColorCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TicketStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStatus_Status");
            });

            modelBuilder.Entity<TicketTypeDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ColorCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TicketTypes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketType_Status");
            });

            modelBuilder.Entity<UnitOfMeasureDAO>(entity =>
            {
                //entity.ToTable("UnitOfMeasure", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(3000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.UnitOfMeasures)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitOfMeasure_Status");
            });

            modelBuilder.Entity<UnitOfMeasureGroupingDAO>(entity =>
            {
                //entity.ToTable("UnitOfMeasureGrouping", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.UnitOfMeasureGroupings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitOfMeasureGrouping_Status");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.UnitOfMeasureGroupings)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitOfMeasureGrouping_UnitOfMeasure");
            });

            modelBuilder.Entity<UnitOfMeasureGroupingContentDAO>(entity =>
            {
                entity.ToTable("UnitOfMeasureGroupingContent", "MDM");

                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.UnitOfMeasureGrouping)
                    .WithMany(p => p.UnitOfMeasureGroupingContents)
                    .HasForeignKey(d => d.UnitOfMeasureGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitOfMeasureGroupingContent_UnitOfMeasureGrouping");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.UnitOfMeasureGroupingContents)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitOfMeasureGroupingContent_UnitOfMeasure");
            });

            modelBuilder.Entity<UsedVariationDAO>(entity =>
            {
                //entity.ToTable("UsedVariation", "ENUM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VariationDAO>(entity =>
            {
                //entity.ToTable("Variation", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.VariationGrouping)
                    .WithMany(p => p.Variations)
                    .HasForeignKey(d => d.VariationGroupingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Variation_VariationGrouping");
            });

            modelBuilder.Entity<VariationGroupingDAO>(entity =>
            {
                //entity.ToTable("VariationGrouping", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.VariationGroupings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VariationGrouping_Product");
            });

            modelBuilder.Entity<WardDAO>(entity =>
            {
                //entity.ToTable("Ward", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_District");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_Status");
            });

            modelBuilder.Entity<WarehouseDAO>(entity =>
            {
                //entity.ToTable("Warehouse", "MDM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Warehouse_District");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehouse_Organization");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Warehouse_Province");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehouse_Status");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_Warehouse_Ward");
            });

            modelBuilder.Entity<WorkflowDefinitionDAO>(entity =>
            {
                //entity.ToTable("WorkflowDefinition", "WF");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.WorkflowDefinitionCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowDefinition_AppUser");

                entity.HasOne(d => d.Modifier)
                    .WithMany(p => p.WorkflowDefinitionModifiers)
                    .HasForeignKey(d => d.ModifierId)
                    .HasConstraintName("FK_WorkflowDefinition_AppUser1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.WorkflowDefinitions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowDefinition_Status");

                entity.HasOne(d => d.WorkflowType)
                    .WithMany(p => p.WorkflowDefinitions)
                    .HasForeignKey(d => d.WorkflowTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowDefinition_WorkflowType");
            });

            modelBuilder.Entity<WorkflowDirectionDAO>(entity =>
            {
                //entity.ToTable("WorkflowDirection", "WF");

                entity.Property(e => e.BodyMailForCreator).HasMaxLength(4000);

                entity.Property(e => e.BodyMailForNextStep).HasMaxLength(4000);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.SubjectMailForCreator).HasMaxLength(500);

                entity.Property(e => e.SubjectMailForNextStep).HasMaxLength(500);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.FromStep)
                    .WithMany(p => p.WorkflowDirectionFromSteps)
                    .HasForeignKey(d => d.FromStepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowDirection_WorkflowStep");

                entity.HasOne(d => d.ToStep)
                    .WithMany(p => p.WorkflowDirectionToSteps)
                    .HasForeignKey(d => d.ToStepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowDirection_WorkflowStep1");

                entity.HasOne(d => d.WorkflowDefinition)
                    .WithMany(p => p.WorkflowDirections)
                    .HasForeignKey(d => d.WorkflowDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowDirection_WorkflowDefinition");
            });

            modelBuilder.Entity<WorkflowParameterDAO>(entity =>
            {
                //entity.ToTable("WorkflowParameter", "WF");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.WorkflowDefinition)
                    .WithMany(p => p.WorkflowParameters)
                    .HasForeignKey(d => d.WorkflowDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowParameter_WorkflowDefinition");
            });

            modelBuilder.Entity<WorkflowStateDAO>(entity =>
            {
                //entity.ToTable("WorkflowState", "WF");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<WorkflowStepDAO>(entity =>
            {
                //entity.ToTable("WorkflowStep", "WF");

                entity.Property(e => e.BodyMailForReject).HasMaxLength(4000);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SubjectMailForReject).HasMaxLength(4000);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.WorkflowSteps)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowStep_Role");

                entity.HasOne(d => d.WorkflowDefinition)
                    .WithMany(p => p.WorkflowSteps)
                    .HasForeignKey(d => d.WorkflowDefinitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowStep_WorkflowDefinition");
            });

            modelBuilder.Entity<WorkflowTypeDAO>(entity =>
            {
                //entity.ToTable("WorkflowType", "WF");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            //modelBuilder.Entity<ActivityPriority>

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
