using CRM.Common;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Repositories;
using System;

namespace CRM.Repositories
{
    public interface IUOW : IServiceScoped, IDisposable
    {
        Task Begin();
        Task Commit();
        Task Rollback();

        IActivityPriorityRepository ActivityPriorityRepository { get; }
        IActivityStatusRepository ActivityStatusRepository { get; }
        IActivityTypeRepository ActivityTypeRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        IAuditLogPropertyRepository AuditLogPropertyRepository { get; }
        IBrandRepository BrandRepository { get; }
        IBusinessConcentrationLevelRepository BusinessConcentrationLevelRepository { get; }
        IBusinessTypeRepository BusinessTypeRepository { get; }
        ICallEmotionRepository CallEmotionRepository { get; }
        ICallLogRepository CallLogRepository { get; }
        ICallCategoryRepository CallCategoryRepository { get; }
        ICallStatusRepository CallStatusRepository { get; }
        ICallTypeRepository CallTypeRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IColorRepository ColorRepository { get; }
        ICompanyActivityRepository CompanyActivityRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICompanyEmailRepository CompanyEmailRepository { get; }
        ICompanyFileGroupingRepository CompanyFileGroupingRepository { get; }
        ICompanyStatusRepository CompanyStatusRepository { get; }
        IConsultingServiceRepository ConsultingServiceRepository { get; }
        IContactActivityRepository ContactActivityRepository { get; }
        IContactRepository ContactRepository { get; }
        IContactEmailRepository ContactEmailRepository { get; }
        IContactFileGroupingRepository ContactFileGroupingRepository { get; }
        IContactStatusRepository ContactStatusRepository { get; }
        IContractRepository ContractRepository { get; }
        IContractFileGroupingRepository ContractFileGroupingRepository { get; }
        IContractPaymentHistoryRepository ContractPaymentHistoryRepository { get; }
        IContractStatusRepository ContractStatusRepository { get; }
        IContractTypeRepository ContractTypeRepository { get; }
        ICooperativeAttitudeRepository CooperativeAttitudeRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        ICustomerCCEmailHistoryRepository CustomerCCEmailHistoryRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ICustomerEmailRepository CustomerEmailRepository { get; }
        ICustomerEmailHistoryRepository CustomerEmailHistoryRepository { get; }
        ICustomerFeedbackRepository CustomerFeedbackRepository { get; }
        ICustomerFeedbackTypeRepository CustomerFeedbackTypeRepository { get; }
        ICustomerGroupingRepository CustomerGroupingRepository { get; }
        ICustomerLeadActivityRepository CustomerLeadActivityRepository { get; }
        ICustomerLeadRepository CustomerLeadRepository { get; }
        ICustomerLeadEmailRepository CustomerLeadEmailRepository { get; }
        ICustomerLeadFileGroupRepository CustomerLeadFileGroupRepository { get; }
        ICustomerLeadLevelRepository CustomerLeadLevelRepository { get; }
        ICustomerLeadSourceRepository CustomerLeadSourceRepository { get; }
        ICustomerLeadStatusRepository CustomerLeadStatusRepository { get; }
        ICustomerLevelRepository CustomerLevelRepository { get; }
        ICustomerPhoneRepository CustomerPhoneRepository { get; }
        ICustomerPointHistoryRepository CustomerPointHistoryRepository { get; }
        ICustomerResourceRepository CustomerResourceRepository { get; }
        ICustomerSalesOrderContentRepository CustomerSalesOrderContentRepository { get; }
        ICustomerSalesOrderRepository CustomerSalesOrderRepository { get; }
        ICustomerSalesOrderPaymentHistoryRepository CustomerSalesOrderPaymentHistoryRepository { get; }
        ICustomerSalesOrderPromotionRepository CustomerSalesOrderPromotionRepository { get; }
        ICustomerTypeRepository CustomerTypeRepository { get; }
        IDirectSalesOrderContentRepository DirectSalesOrderContentRepository { get; }
        IDirectSalesOrderRepository DirectSalesOrderRepository { get; }
        IDirectSalesOrderPromotionRepository DirectSalesOrderPromotionRepository { get; }
        IDistrictRepository DistrictRepository { get; }
        IEditedPriceStatusRepository EditedPriceStatusRepository { get; }
        IEmailStatusRepository EmailStatusRepository { get; }
        IEmailTypeRepository EmailTypeRepository { get; }
        IEntityReferenceRepository EntityReferenceRepository { get; }
        IEventMessageRepository EventMessageRepository { get; }
        IFieldRepository FieldRepository { get; }
        IFileRepository FileRepository { get; }
        IFileTypeRepository FileTypeRepository { get; }
        IImageRepository ImageRepository { get; }
        IImproveQualityServingRepository ImproveQualityServingRepository { get; }
        IInfulenceLevelMarketRepository InfulenceLevelMarketRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IInventoryHistoryRepository InventoryHistoryRepository { get; }
        IItemRepository ItemRepository { get; }
        IKMSStatusRepository KMSStatusRepository { get; }
        IKnowledgeArticleRepository KnowledgeArticleRepository { get; }
        IKnowledgeArticleKeywordRepository KnowledgeArticleKeywordRepository { get; }
        IKnowledgeGroupRepository KnowledgeGroupRepository { get; }
        IKpiCriteriaGeneralRepository KpiCriteriaGeneralRepository { get; }
        IKpiCriteriaItemRepository KpiCriteriaItemRepository { get; }
        IKpiGeneralContentRepository KpiGeneralContentRepository { get; }
        IKpiGeneralRepository KpiGeneralRepository { get; }
        IKpiItemContentRepository KpiItemContentRepository { get; }
        IKpiItemRepository KpiItemRepository { get; }
        IKpiPeriodRepository KpiPeriodRepository { get; }
        IKpiYearRepository KpiYearRepository { get; }
        IMailTemplateRepository MailTemplateRepository { get; }
        IMarketPriceRepository MarketPriceRepository { get; }
        IMenuRepository MenuRepository { get; }
        INationRepository NationRepository { get; }
        INotificationRepository NotificationRepository { get; }
        INotificationStatusRepository NotificationStatusRepository { get; }
        IOpportunityActivityRepository OpportunityActivityRepository { get; }
        IOpportunityRepository OpportunityRepository { get; }
        IOpportunityEmailRepository OpportunityEmailRepository { get; }
        IOpportunityFileGroupingRepository OpportunityFileGroupingRepository { get; }
        IOpportunityResultTypeRepository OpportunityResultTypeRepository { get; }
        IOrderCategoryRepository OrderCategoryRepository { get; }
        IOrderPaymentStatusRepository OrderPaymentStatusRepository { get; }
        IOrderQuoteContentRepository OrderQuoteContentRepository { get; }
        IOrderQuoteRepository OrderQuoteRepository { get; }
        IOrderQuoteStatusRepository OrderQuoteStatusRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }
        IPaymentStatusRepository PaymentStatusRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IPermissionOperatorRepository PermissionOperatorRepository { get; }
        IPhoneTypeRepository PhoneTypeRepository { get; }
        IPositionRepository PositionRepository { get; }
        IPotentialResultRepository PotentialResultRepository { get; }
        IProbabilityRepository ProbabilityRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductGroupingRepository ProductGroupingRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IProfessionRepository ProfessionRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        IRatingStatusRepository RatingStatusRepository { get; }
        IRelationshipCustomerTypeRepository RelationshipCustomerTypeRepository { get; }
        IRepairStatusRepository RepairStatusRepository { get; }
        IRepairTicketRepository RepairTicketRepository { get; }
        IRequestStateRepository RequestStateRepository { get; }
        IRequestWorkflowDefinitionMappingRepository RequestWorkflowDefinitionMappingRepository { get; }
        IRequestWorkflowParameterMappingRepository RequestWorkflowParameterMappingRepository { get; }
        IRequestWorkflowStepMappingRepository RequestWorkflowStepMappingRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISaleStageRepository SaleStageRepository { get; }
        IScheduleMasterRepository ScheduleMasterRepository { get; }
        ISexRepository SexRepository { get; }
        ISLAAlertRepository SLAAlertRepository { get; }
        ISLAAlertFRTRepository SLAAlertFRTRepository { get; }
        ISLAAlertFRTMailRepository SLAAlertFRTMailRepository { get; }
        ISLAAlertFRTPhoneRepository SLAAlertFRTPhoneRepository { get; }
        ISLAAlertFRTUserRepository SLAAlertFRTUserRepository { get; }
        ISLAAlertMailRepository SLAAlertMailRepository { get; }
        ISLAAlertPhoneRepository SLAAlertPhoneRepository { get; }
        ISLAAlertUserRepository SLAAlertUserRepository { get; }
        ISLAEscalationRepository SLAEscalationRepository { get; }
        ISLAEscalationFRTRepository SLAEscalationFRTRepository { get; }
        ISLAEscalationFRTMailRepository SLAEscalationFRTMailRepository { get; }
        ISLAEscalationFRTPhoneRepository SLAEscalationFRTPhoneRepository { get; }
        ISLAEscalationFRTUserRepository SLAEscalationFRTUserRepository { get; }
        ISLAEscalationMailRepository SLAEscalationMailRepository { get; }
        ISLAEscalationPhoneRepository SLAEscalationPhoneRepository { get; }
        ISLAEscalationUserRepository SLAEscalationUserRepository { get; }
        ISLAPolicyRepository SLAPolicyRepository { get; }
        ISLAStatusRepository SLAStatusRepository { get; }
        ISLATimeUnitRepository SLATimeUnitRepository { get; }
        ISmsQueueRepository SmsQueueRepository { get; }
        ISmsQueueStatusRepository SmsQueueStatusRepository { get; }
        ISmsTemplateRepository SmsTemplateRepository { get; }
        ISocialChannelTypeRepository SocialChannelTypeRepository { get; }
        IStatusRepository StatusRepository { get; }
        IStoreAssetsRepository StoreAssetsRepository { get; }
        IStoreCoverageCapacityRepository StoreCoverageCapacityRepository { get; }
        IStoreRepository StoreRepository { get; }
        IStoreDeliveryTimeRepository StoreDeliveryTimeRepository { get; }
        IStoreGroupingRepository StoreGroupingRepository { get; }
        IStoreMeansOfDeliveryRepository StoreMeansOfDeliveryRepository { get; }
        IStorePersonnelRepository StorePersonnelRepository { get; }
        IStoreRepresentRepository StoreRepresentRepository { get; }
        IStoreStatusRepository StoreStatusRepository { get; }
        IStoreTypeRepository StoreTypeRepository { get; }
        IStoreWarrantyServiceRepository StoreWarrantyServiceRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ITaxTypeRepository TaxTypeRepository { get; }
        ITicketRepository TicketRepository { get; }
        ITicketGeneratedIdRepository TicketGeneratedIdRepository { get; }
        ITicketGroupRepository TicketGroupRepository { get; }
        ITicketIssueLevelRepository TicketIssueLevelRepository { get; }
        ITicketOfDepartmentRepository TicketOfDepartmentRepository { get; }
        ITicketOfUserRepository TicketOfUserRepository { get; }
        ITicketPriorityRepository TicketPriorityRepository { get; }
        ITicketResolveTypeRepository TicketResolveTypeRepository { get; }
        ITicketSourceRepository TicketSourceRepository { get; }
        ITicketStatusRepository TicketStatusRepository { get; }
        ITicketTypeRepository TicketTypeRepository { get; }
        IUnitOfMeasureRepository UnitOfMeasureRepository { get; }
        IUnitOfMeasureGroupingContentRepository UnitOfMeasureGroupingContentRepository { get; }
        IUnitOfMeasureGroupingRepository UnitOfMeasureGroupingRepository { get; }
        IUsedVariationRepository UsedVariationRepository { get; }
        IVariationRepository VariationRepository { get; }
        IVariationGroupingRepository VariationGroupingRepository { get; }
        IWardRepository WardRepository { get; }
        IWarehouseRepository WarehouseRepository { get; }
        IWorkflowDefinitionRepository WorkflowDefinitionRepository { get; }
        IWorkflowDirectionRepository WorkflowDirectionRepository { get; }
        IWorkflowParameterRepository WorkflowParameterRepository { get; }
        IWorkflowStateRepository WorkflowStateRepository { get; }
        IWorkflowStepRepository WorkflowStepRepository { get; }
        IWorkflowTypeRepository WorkflowTypeRepository { get; }
    }

    public class UOW : IUOW
    {
        private DataContext DataContext;

        public IActivityPriorityRepository ActivityPriorityRepository { get; private set; }
        public IActivityStatusRepository ActivityStatusRepository { get; private set; }
        public IActivityTypeRepository ActivityTypeRepository { get; private set; }
        public IAppUserRepository AppUserRepository { get; private set; }
        public IAuditLogPropertyRepository AuditLogPropertyRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public IBusinessConcentrationLevelRepository BusinessConcentrationLevelRepository { get; private set; }
        public IBusinessTypeRepository BusinessTypeRepository { get; private set; }
        public ICallEmotionRepository CallEmotionRepository { get; private set; }
        public ICallLogRepository CallLogRepository { get; private set; }
        public ICallCategoryRepository CallCategoryRepository { get; private set; }
        public ICallStatusRepository CallStatusRepository { get; private set; }
        public ICallTypeRepository CallTypeRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IColorRepository ColorRepository { get; private set; }
        public ICompanyActivityRepository CompanyActivityRepository { get; private set; }
        public ICompanyRepository CompanyRepository { get; private set; }
        public ICompanyEmailRepository CompanyEmailRepository { get; private set; }
        public ICompanyFileGroupingRepository CompanyFileGroupingRepository { get; private set; }
        public ICompanyStatusRepository CompanyStatusRepository { get; private set; }
        public IConsultingServiceRepository ConsultingServiceRepository { get; private set; }
        public IContactActivityRepository ContactActivityRepository { get; private set; }
        public IContactRepository ContactRepository { get; private set; }
        public IContactEmailRepository ContactEmailRepository { get; private set; }
        public IContactFileGroupingRepository ContactFileGroupingRepository { get; private set; }
        public IContactStatusRepository ContactStatusRepository { get; private set; }
        public IContractRepository ContractRepository { get; private set; }
        public IContractFileGroupingRepository ContractFileGroupingRepository { get; private set; }
        public IContractPaymentHistoryRepository ContractPaymentHistoryRepository { get; private set; }
        public IContractStatusRepository ContractStatusRepository { get; private set; }
        public IContractTypeRepository ContractTypeRepository { get; private set; }
        public ICooperativeAttitudeRepository CooperativeAttitudeRepository { get; private set; }
        public ICurrencyRepository CurrencyRepository { get; private set; }
        public ICustomerCCEmailHistoryRepository CustomerCCEmailHistoryRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public ICustomerEmailRepository CustomerEmailRepository { get; private set; }
        public ICustomerEmailHistoryRepository CustomerEmailHistoryRepository { get; private set; }
        public ICustomerFeedbackRepository CustomerFeedbackRepository { get; private set; }
        public ICustomerFeedbackTypeRepository CustomerFeedbackTypeRepository { get; private set; }
        public ICustomerGroupingRepository CustomerGroupingRepository { get; private set; }
        public ICustomerLeadActivityRepository CustomerLeadActivityRepository { get; private set; }
        public ICustomerLeadRepository CustomerLeadRepository { get; private set; }
        public ICustomerLeadEmailRepository CustomerLeadEmailRepository { get; private set; }
        public ICustomerLeadFileGroupRepository CustomerLeadFileGroupRepository { get; private set; }
        public ICustomerLeadLevelRepository CustomerLeadLevelRepository { get; private set; }
        public ICustomerLeadSourceRepository CustomerLeadSourceRepository { get; private set; }
        public ICustomerLeadStatusRepository CustomerLeadStatusRepository { get; private set; }
        public ICustomerLevelRepository CustomerLevelRepository { get; private set; }
        public ICustomerPhoneRepository CustomerPhoneRepository { get; private set; }
        public ICustomerPointHistoryRepository CustomerPointHistoryRepository { get; private set; }
        public ICustomerResourceRepository CustomerResourceRepository { get; private set; }
        public ICustomerSalesOrderContentRepository CustomerSalesOrderContentRepository { get; private set; }
        public ICustomerSalesOrderRepository CustomerSalesOrderRepository { get; private set; }
        public ICustomerSalesOrderPaymentHistoryRepository CustomerSalesOrderPaymentHistoryRepository { get; private set; }
        public ICustomerSalesOrderPromotionRepository CustomerSalesOrderPromotionRepository { get; private set; }
        public ICustomerTypeRepository CustomerTypeRepository { get; private set; }
        public IDirectSalesOrderContentRepository DirectSalesOrderContentRepository { get; private set; }
        public IDirectSalesOrderRepository DirectSalesOrderRepository { get; private set; }
        public IDirectSalesOrderPromotionRepository DirectSalesOrderPromotionRepository { get; private set; }
        public IDistrictRepository DistrictRepository { get; private set; }
        public IEditedPriceStatusRepository EditedPriceStatusRepository { get; private set; }
        public IEmailStatusRepository EmailStatusRepository { get; private set; }
        public IEmailTypeRepository EmailTypeRepository { get; private set; }
        public IEntityReferenceRepository EntityReferenceRepository { get; private set; }
        public IEventMessageRepository EventMessageRepository { get; private set; }
        public IFieldRepository FieldRepository { get; private set; }
        public IFileRepository FileRepository { get; private set; }
        public IFileTypeRepository FileTypeRepository { get; private set; }
        public IImageRepository ImageRepository { get; private set; }
        public IImproveQualityServingRepository ImproveQualityServingRepository { get; private set; }
        public IInfulenceLevelMarketRepository InfulenceLevelMarketRepository { get; private set; }
        public IInventoryRepository InventoryRepository { get; private set; }
        public IInventoryHistoryRepository InventoryHistoryRepository { get; private set; }
        public IItemRepository ItemRepository { get; private set; }
        public IKMSStatusRepository KMSStatusRepository { get; private set; }
        public IKnowledgeArticleRepository KnowledgeArticleRepository { get; private set; }
        public IKnowledgeArticleKeywordRepository KnowledgeArticleKeywordRepository { get; private set; }
        public IKnowledgeGroupRepository KnowledgeGroupRepository { get; private set; }
        public IKpiCriteriaGeneralRepository KpiCriteriaGeneralRepository { get; private set; }
        public IKpiCriteriaItemRepository KpiCriteriaItemRepository { get; private set; }
        public IKpiGeneralContentRepository KpiGeneralContentRepository { get; private set; }
        public IKpiGeneralRepository KpiGeneralRepository { get; private set; }
        public IKpiItemContentRepository KpiItemContentRepository { get; private set; }
        public IKpiItemRepository KpiItemRepository { get; private set; }
        public IKpiPeriodRepository KpiPeriodRepository { get; private set; }
        public IKpiYearRepository KpiYearRepository { get; private set; }
        public IMailTemplateRepository MailTemplateRepository { get; private set; }
        public IMarketPriceRepository MarketPriceRepository { get; private set; }
        public IMenuRepository MenuRepository { get; private set; }
        public INationRepository NationRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }
        public INotificationStatusRepository NotificationStatusRepository { get; private set; }
        public IOpportunityActivityRepository OpportunityActivityRepository { get; private set; }
        public IOpportunityRepository OpportunityRepository { get; private set; }
        public IOpportunityEmailRepository OpportunityEmailRepository { get; private set; }
        public IOpportunityFileGroupingRepository OpportunityFileGroupingRepository { get; private set; }
        public IOpportunityResultTypeRepository OpportunityResultTypeRepository { get; private set; }
        public IOrderCategoryRepository OrderCategoryRepository { get; private set; }
        public IOrderPaymentStatusRepository OrderPaymentStatusRepository { get; private set; }
        public IOrderQuoteContentRepository OrderQuoteContentRepository { get; private set; }
        public IOrderQuoteRepository OrderQuoteRepository { get; private set; }
        public IOrderQuoteStatusRepository OrderQuoteStatusRepository { get; private set; }
        public IOrganizationRepository OrganizationRepository { get; private set; }
        public IPaymentStatusRepository PaymentStatusRepository { get; private set; }
        public IPermissionRepository PermissionRepository { get; private set; }
        public IPermissionOperatorRepository PermissionOperatorRepository { get; private set; }
        public IPhoneTypeRepository PhoneTypeRepository { get; private set; }
        public IPositionRepository PositionRepository { get; private set; }
        public IPotentialResultRepository PotentialResultRepository { get; private set; }
        public IProbabilityRepository ProbabilityRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IProductGroupingRepository ProductGroupingRepository { get; private set; }
        public IProductTypeRepository ProductTypeRepository { get; private set; }
        public IProfessionRepository ProfessionRepository { get; private set; }
        public IProvinceRepository ProvinceRepository { get; private set; }
        public IRatingStatusRepository RatingStatusRepository { get; private set; }
        public IRelationshipCustomerTypeRepository RelationshipCustomerTypeRepository { get; private set; }
        public IRepairStatusRepository RepairStatusRepository { get; private set; }
        public IRepairTicketRepository RepairTicketRepository { get; private set; }
        public IRequestStateRepository RequestStateRepository { get; private set; }
        public IRequestWorkflowDefinitionMappingRepository RequestWorkflowDefinitionMappingRepository { get; private set; }
        public IRequestWorkflowParameterMappingRepository RequestWorkflowParameterMappingRepository { get; private set; }
        public IRequestWorkflowStepMappingRepository RequestWorkflowStepMappingRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public ISaleStageRepository SaleStageRepository { get; private set; }
        public IScheduleMasterRepository ScheduleMasterRepository { get; private set; }
        public ISexRepository SexRepository { get; private set; }
        public ISLAAlertRepository SLAAlertRepository { get; private set; }
        public ISLAAlertFRTRepository SLAAlertFRTRepository { get; private set; }
        public ISLAAlertFRTMailRepository SLAAlertFRTMailRepository { get; private set; }
        public ISLAAlertFRTPhoneRepository SLAAlertFRTPhoneRepository { get; private set; }
        public ISLAAlertFRTUserRepository SLAAlertFRTUserRepository { get; private set; }
        public ISLAAlertMailRepository SLAAlertMailRepository { get; private set; }
        public ISLAAlertPhoneRepository SLAAlertPhoneRepository { get; private set; }
        public ISLAAlertUserRepository SLAAlertUserRepository { get; private set; }
        public ISLAEscalationRepository SLAEscalationRepository { get; private set; }
        public ISLAEscalationFRTRepository SLAEscalationFRTRepository { get; private set; }
        public ISLAEscalationFRTMailRepository SLAEscalationFRTMailRepository { get; private set; }
        public ISLAEscalationFRTPhoneRepository SLAEscalationFRTPhoneRepository { get; private set; }
        public ISLAEscalationFRTUserRepository SLAEscalationFRTUserRepository { get; private set; }
        public ISLAEscalationMailRepository SLAEscalationMailRepository { get; private set; }
        public ISLAEscalationPhoneRepository SLAEscalationPhoneRepository { get; private set; }
        public ISLAEscalationUserRepository SLAEscalationUserRepository { get; private set; }
        public ISLAPolicyRepository SLAPolicyRepository { get; private set; }
        public ISLAStatusRepository SLAStatusRepository { get; private set; }
        public ISLATimeUnitRepository SLATimeUnitRepository { get; private set; }
        public ISmsQueueRepository SmsQueueRepository { get; private set; }
        public ISmsQueueStatusRepository SmsQueueStatusRepository { get; private set; }
        public ISmsTemplateRepository SmsTemplateRepository { get; private set; }
        public ISocialChannelTypeRepository SocialChannelTypeRepository { get; private set; }
        public IStatusRepository StatusRepository { get; private set; }
        public IStoreAssetsRepository StoreAssetsRepository { get; private set; }
        public IStoreCoverageCapacityRepository StoreCoverageCapacityRepository { get; private set; }
        public IStoreRepository StoreRepository { get; private set; }
        public IStoreDeliveryTimeRepository StoreDeliveryTimeRepository { get; private set; }
        public IStoreGroupingRepository StoreGroupingRepository { get; private set; }
        public IStoreMeansOfDeliveryRepository StoreMeansOfDeliveryRepository { get; private set; }
        public IStorePersonnelRepository StorePersonnelRepository { get; private set; }
        public IStoreRepresentRepository StoreRepresentRepository { get; private set; }
        public IStoreStatusRepository StoreStatusRepository { get; private set; }
        public IStoreTypeRepository StoreTypeRepository { get; private set; }
        public IStoreWarrantyServiceRepository StoreWarrantyServiceRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ITaxTypeRepository TaxTypeRepository { get; private set; }
        public ITicketRepository TicketRepository { get; private set; }
        public ITicketGeneratedIdRepository TicketGeneratedIdRepository { get; private set; }
        public ITicketGroupRepository TicketGroupRepository { get; private set; }
        public ITicketIssueLevelRepository TicketIssueLevelRepository { get; private set; }
        public ITicketOfDepartmentRepository TicketOfDepartmentRepository { get; private set; }
        public ITicketOfUserRepository TicketOfUserRepository { get; private set; }
        public ITicketPriorityRepository TicketPriorityRepository { get; private set; }
        public ITicketResolveTypeRepository TicketResolveTypeRepository { get; private set; }
        public ITicketSourceRepository TicketSourceRepository { get; private set; }
        public ITicketStatusRepository TicketStatusRepository { get; private set; }
        public ITicketTypeRepository TicketTypeRepository { get; private set; }
        public IUnitOfMeasureRepository UnitOfMeasureRepository { get; private set; }
        public IUnitOfMeasureGroupingContentRepository UnitOfMeasureGroupingContentRepository { get; private set; }
        public IUnitOfMeasureGroupingRepository UnitOfMeasureGroupingRepository { get; private set; }
        public IUsedVariationRepository UsedVariationRepository { get; private set; }
        public IVariationRepository VariationRepository { get; private set; }
        public IVariationGroupingRepository VariationGroupingRepository { get; private set; }
        public IWardRepository WardRepository { get; private set; }
        public IWarehouseRepository WarehouseRepository { get; private set; }
        public IWorkflowDefinitionRepository WorkflowDefinitionRepository { get; private set; }
        public IWorkflowDirectionRepository WorkflowDirectionRepository { get; private set; }
        public IWorkflowParameterRepository WorkflowParameterRepository { get; private set; }
        public IWorkflowStateRepository WorkflowStateRepository { get; private set; }
        public IWorkflowStepRepository WorkflowStepRepository { get; private set; }
        public IWorkflowTypeRepository WorkflowTypeRepository { get; private set; }

        public UOW(DataContext DataContext)
        {
            this.DataContext = DataContext;

            ActivityPriorityRepository = new ActivityPriorityRepository(DataContext);
            ActivityStatusRepository = new ActivityStatusRepository(DataContext);
            ActivityTypeRepository = new ActivityTypeRepository(DataContext);
            AppUserRepository = new AppUserRepository(DataContext);
            AuditLogPropertyRepository = new AuditLogPropertyRepository(DataContext);
            BrandRepository = new BrandRepository(DataContext);
            BusinessConcentrationLevelRepository = new BusinessConcentrationLevelRepository(DataContext);
            BusinessTypeRepository = new BusinessTypeRepository(DataContext);
            CallEmotionRepository = new CallEmotionRepository(DataContext);
            CallLogRepository = new CallLogRepository(DataContext);
            CallCategoryRepository = new CallCategoryRepository(DataContext);
            CallStatusRepository = new CallStatusRepository(DataContext);
            CallTypeRepository = new CallTypeRepository(DataContext);
            CategoryRepository = new CategoryRepository(DataContext);
            ColorRepository = new ColorRepository(DataContext);
            CompanyActivityRepository = new CompanyActivityRepository(DataContext);
            CompanyRepository = new CompanyRepository(DataContext);
            CompanyEmailRepository = new CompanyEmailRepository(DataContext);
            CompanyFileGroupingRepository = new CompanyFileGroupingRepository(DataContext);
            CompanyStatusRepository = new CompanyStatusRepository(DataContext);
            ConsultingServiceRepository = new ConsultingServiceRepository(DataContext);
            ContactActivityRepository = new ContactActivityRepository(DataContext);
            ContactRepository = new ContactRepository(DataContext);
            ContactEmailRepository = new ContactEmailRepository(DataContext);
            ContactFileGroupingRepository = new ContactFileGroupingRepository(DataContext);
            ContactStatusRepository = new ContactStatusRepository(DataContext);
            ContractRepository = new ContractRepository(DataContext);
            ContractFileGroupingRepository = new ContractFileGroupingRepository(DataContext);
            ContractPaymentHistoryRepository = new ContractPaymentHistoryRepository(DataContext);
            ContractStatusRepository = new ContractStatusRepository(DataContext);
            ContractTypeRepository = new ContractTypeRepository(DataContext);
            CooperativeAttitudeRepository = new CooperativeAttitudeRepository(DataContext);
            CurrencyRepository = new CurrencyRepository(DataContext);
            CustomerCCEmailHistoryRepository = new CustomerCCEmailHistoryRepository(DataContext);
            CustomerRepository = new CustomerRepository(DataContext);
            CustomerEmailRepository = new CustomerEmailRepository(DataContext);
            CustomerEmailHistoryRepository = new CustomerEmailHistoryRepository(DataContext);
            CustomerFeedbackRepository = new CustomerFeedbackRepository(DataContext);
            CustomerFeedbackTypeRepository = new CustomerFeedbackTypeRepository(DataContext);
            CustomerGroupingRepository = new CustomerGroupingRepository(DataContext);
            CustomerLeadActivityRepository = new CustomerLeadActivityRepository(DataContext);
            CustomerLeadRepository = new CustomerLeadRepository(DataContext);
            CustomerLeadEmailRepository = new CustomerLeadEmailRepository(DataContext);
            CustomerLeadFileGroupRepository = new CustomerLeadFileGroupRepository(DataContext);
            CustomerLeadLevelRepository = new CustomerLeadLevelRepository(DataContext);
            CustomerLeadSourceRepository = new CustomerLeadSourceRepository(DataContext);
            CustomerLeadStatusRepository = new CustomerLeadStatusRepository(DataContext);
            CustomerLevelRepository = new CustomerLevelRepository(DataContext);
            CustomerPhoneRepository = new CustomerPhoneRepository(DataContext);
            CustomerPointHistoryRepository = new CustomerPointHistoryRepository(DataContext);
            CustomerResourceRepository = new CustomerResourceRepository(DataContext);
            CustomerSalesOrderContentRepository = new CustomerSalesOrderContentRepository(DataContext);
            CustomerSalesOrderRepository = new CustomerSalesOrderRepository(DataContext);
            CustomerSalesOrderPaymentHistoryRepository = new CustomerSalesOrderPaymentHistoryRepository(DataContext);
            CustomerSalesOrderPromotionRepository = new CustomerSalesOrderPromotionRepository(DataContext);
            CustomerTypeRepository = new CustomerTypeRepository(DataContext);
            DirectSalesOrderContentRepository = new DirectSalesOrderContentRepository(DataContext);
            DirectSalesOrderRepository = new DirectSalesOrderRepository(DataContext);
            DirectSalesOrderPromotionRepository = new DirectSalesOrderPromotionRepository(DataContext);
            DistrictRepository = new DistrictRepository(DataContext);
            EditedPriceStatusRepository = new EditedPriceStatusRepository(DataContext);
            EmailStatusRepository = new EmailStatusRepository(DataContext);
            EmailTypeRepository = new EmailTypeRepository(DataContext);
            EntityReferenceRepository = new EntityReferenceRepository(DataContext);
            EventMessageRepository = new EventMessageRepository(DataContext);
            FieldRepository = new FieldRepository(DataContext);
            FileRepository = new FileRepository(DataContext);
            FileTypeRepository = new FileTypeRepository(DataContext);
            ImageRepository = new ImageRepository(DataContext);
            ImproveQualityServingRepository = new ImproveQualityServingRepository(DataContext);
            InfulenceLevelMarketRepository = new InfulenceLevelMarketRepository(DataContext);
            InventoryRepository = new InventoryRepository(DataContext);
            InventoryHistoryRepository = new InventoryHistoryRepository(DataContext);
            ItemRepository = new ItemRepository(DataContext);
            KMSStatusRepository = new KMSStatusRepository(DataContext);
            KnowledgeArticleRepository = new KnowledgeArticleRepository(DataContext);
            KnowledgeArticleKeywordRepository = new KnowledgeArticleKeywordRepository(DataContext);
            KnowledgeGroupRepository = new KnowledgeGroupRepository(DataContext);
            KpiCriteriaGeneralRepository = new KpiCriteriaGeneralRepository(DataContext);
            KpiCriteriaItemRepository = new KpiCriteriaItemRepository(DataContext);
            KpiGeneralContentRepository = new KpiGeneralContentRepository(DataContext);
            KpiGeneralRepository = new KpiGeneralRepository(DataContext);
            KpiItemContentRepository = new KpiItemContentRepository(DataContext);
            KpiItemRepository = new KpiItemRepository(DataContext);
            KpiPeriodRepository = new KpiPeriodRepository(DataContext);
            KpiYearRepository = new KpiYearRepository(DataContext);
            MailTemplateRepository = new MailTemplateRepository(DataContext);
            MarketPriceRepository = new MarketPriceRepository(DataContext);
            MenuRepository = new MenuRepository(DataContext);
            NationRepository = new NationRepository(DataContext);
            NotificationRepository = new NotificationRepository(DataContext);
            NotificationStatusRepository = new NotificationStatusRepository(DataContext);
            OpportunityActivityRepository = new OpportunityActivityRepository(DataContext);
            OpportunityRepository = new OpportunityRepository(DataContext);
            OpportunityEmailRepository = new OpportunityEmailRepository(DataContext);
            OpportunityFileGroupingRepository = new OpportunityFileGroupingRepository(DataContext);
            OpportunityResultTypeRepository = new OpportunityResultTypeRepository(DataContext);
            OrderCategoryRepository = new OrderCategoryRepository(DataContext);
            OrderPaymentStatusRepository = new OrderPaymentStatusRepository(DataContext);
            OrderQuoteContentRepository = new OrderQuoteContentRepository(DataContext);
            OrderQuoteRepository = new OrderQuoteRepository(DataContext);
            OrderQuoteStatusRepository = new OrderQuoteStatusRepository(DataContext);
            OrganizationRepository = new OrganizationRepository(DataContext);
            PaymentStatusRepository = new PaymentStatusRepository(DataContext);
            PermissionRepository = new PermissionRepository(DataContext);
            PermissionOperatorRepository = new PermissionOperatorRepository(DataContext);
            PhoneTypeRepository = new PhoneTypeRepository(DataContext);
            PositionRepository = new PositionRepository(DataContext);
            PotentialResultRepository = new PotentialResultRepository(DataContext);
            ProbabilityRepository = new ProbabilityRepository(DataContext);
            ProductRepository = new ProductRepository(DataContext);
            ProductGroupingRepository = new ProductGroupingRepository(DataContext);
            ProductTypeRepository = new ProductTypeRepository(DataContext);
            ProfessionRepository = new ProfessionRepository(DataContext);
            ProvinceRepository = new ProvinceRepository(DataContext);
            RatingStatusRepository = new RatingStatusRepository(DataContext);
            RelationshipCustomerTypeRepository = new RelationshipCustomerTypeRepository(DataContext);
            RepairStatusRepository = new RepairStatusRepository(DataContext);
            RepairTicketRepository = new RepairTicketRepository(DataContext);
            RequestStateRepository = new RequestStateRepository(DataContext);
            RequestWorkflowDefinitionMappingRepository = new RequestWorkflowDefinitionMappingRepository(DataContext);
            RequestWorkflowParameterMappingRepository = new RequestWorkflowParameterMappingRepository(DataContext);
            RequestWorkflowStepMappingRepository = new RequestWorkflowStepMappingRepository(DataContext);
            RoleRepository = new RoleRepository(DataContext);
            SaleStageRepository = new SaleStageRepository(DataContext);
            ScheduleMasterRepository = new ScheduleMasterRepository(DataContext);
            SexRepository = new SexRepository(DataContext);
            SLAAlertRepository = new SLAAlertRepository(DataContext);
            SLAAlertFRTRepository = new SLAAlertFRTRepository(DataContext);
            SLAAlertFRTMailRepository = new SLAAlertFRTMailRepository(DataContext);
            SLAAlertFRTPhoneRepository = new SLAAlertFRTPhoneRepository(DataContext);
            SLAAlertFRTUserRepository = new SLAAlertFRTUserRepository(DataContext);
            SLAAlertMailRepository = new SLAAlertMailRepository(DataContext);
            SLAAlertPhoneRepository = new SLAAlertPhoneRepository(DataContext);
            SLAAlertUserRepository = new SLAAlertUserRepository(DataContext);
            SLAEscalationRepository = new SLAEscalationRepository(DataContext);
            SLAEscalationFRTRepository = new SLAEscalationFRTRepository(DataContext);
            SLAEscalationFRTMailRepository = new SLAEscalationFRTMailRepository(DataContext);
            SLAEscalationFRTPhoneRepository = new SLAEscalationFRTPhoneRepository(DataContext);
            SLAEscalationFRTUserRepository = new SLAEscalationFRTUserRepository(DataContext);
            SLAEscalationMailRepository = new SLAEscalationMailRepository(DataContext);
            SLAEscalationPhoneRepository = new SLAEscalationPhoneRepository(DataContext);
            SLAEscalationUserRepository = new SLAEscalationUserRepository(DataContext);
            SLAPolicyRepository = new SLAPolicyRepository(DataContext);
            SLAStatusRepository = new SLAStatusRepository(DataContext);
            SLATimeUnitRepository = new SLATimeUnitRepository(DataContext);
            SmsQueueRepository = new SmsQueueRepository(DataContext);
            SmsQueueStatusRepository = new SmsQueueStatusRepository(DataContext);
            SmsTemplateRepository = new SmsTemplateRepository(DataContext);
            SocialChannelTypeRepository = new SocialChannelTypeRepository(DataContext);
            StatusRepository = new StatusRepository(DataContext);
            StoreAssetsRepository = new StoreAssetsRepository(DataContext);
            StoreCoverageCapacityRepository = new StoreCoverageCapacityRepository(DataContext);
            StoreRepository = new StoreRepository(DataContext);
            StoreDeliveryTimeRepository = new StoreDeliveryTimeRepository(DataContext);
            StoreGroupingRepository = new StoreGroupingRepository(DataContext);
            StoreMeansOfDeliveryRepository = new StoreMeansOfDeliveryRepository(DataContext);
            StorePersonnelRepository = new StorePersonnelRepository(DataContext);
            StoreRepresentRepository = new StoreRepresentRepository(DataContext);
            StoreStatusRepository = new StoreStatusRepository(DataContext);
            StoreTypeRepository = new StoreTypeRepository(DataContext);
            StoreWarrantyServiceRepository = new StoreWarrantyServiceRepository(DataContext);
            SupplierRepository = new SupplierRepository(DataContext);
            TaxTypeRepository = new TaxTypeRepository(DataContext);
            TicketRepository = new TicketRepository(DataContext);
            TicketGeneratedIdRepository = new TicketGeneratedIdRepository(DataContext);
            TicketGroupRepository = new TicketGroupRepository(DataContext);
            TicketIssueLevelRepository = new TicketIssueLevelRepository(DataContext);
            TicketOfDepartmentRepository = new TicketOfDepartmentRepository(DataContext);
            TicketOfUserRepository = new TicketOfUserRepository(DataContext);
            TicketPriorityRepository = new TicketPriorityRepository(DataContext);
            TicketResolveTypeRepository = new TicketResolveTypeRepository(DataContext);
            TicketSourceRepository = new TicketSourceRepository(DataContext);
            TicketStatusRepository = new TicketStatusRepository(DataContext);
            TicketTypeRepository = new TicketTypeRepository(DataContext);
            UnitOfMeasureRepository = new UnitOfMeasureRepository(DataContext);
            UnitOfMeasureGroupingContentRepository = new UnitOfMeasureGroupingContentRepository(DataContext);
            UnitOfMeasureGroupingRepository = new UnitOfMeasureGroupingRepository(DataContext);
            UsedVariationRepository = new UsedVariationRepository(DataContext);
            VariationRepository = new VariationRepository(DataContext);
            VariationGroupingRepository = new VariationGroupingRepository(DataContext);
            WardRepository = new WardRepository(DataContext);
            WarehouseRepository = new WarehouseRepository(DataContext);
            WorkflowDefinitionRepository = new WorkflowDefinitionRepository(DataContext);
            WorkflowDirectionRepository = new WorkflowDirectionRepository(DataContext);
            WorkflowParameterRepository = new WorkflowParameterRepository(DataContext);
            WorkflowStateRepository = new WorkflowStateRepository(DataContext);
            WorkflowStepRepository = new WorkflowStepRepository(DataContext);
            WorkflowTypeRepository = new WorkflowTypeRepository(DataContext);
        }
        public async Task Begin()
        {
            return;
            await DataContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            //DataContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            //DataContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.DataContext == null)
            {
                return;
            }

            this.DataContext.Dispose();
            this.DataContext = null;
        }
    }
}