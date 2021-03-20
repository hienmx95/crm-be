using System.Collections.Generic; 
using CRM.Common; 
using CRM.Entities; 

namespace CRM.Rpc.opportunity
{
    public class OpportunityRoute : Root
    {
        public const string Parent = Module + "/sales/opportunity";
        public const string Master = Module + "/sales/opportunity/opportunity-master";
        public const string Detail = Module + "/sales/opportunity/opportunity-detail";

        private const string Default = Rpc + Module + "/opportunity";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string GetPreview = Default + "/get-preview";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-tempate";
        public const string BulkDelete = Default + "/bulk-delete";
        public const string ChangeStatus = Default + "/change-status";
        public const string UploadFile = Default + "/upload-file";

        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListContact = Default + "/filter-list-contact";
        public const string FilterListSaleStage = Default + "/filter-list-sale-stage";
        public const string FilterListActivityStatus = Default + "/filter-list-activity-status";
        public const string FilterListActivityType = Default + "/filter-list-activity-type";
        public const string FilterListOrderQuoteStatus = Default + "/filter-list-order-quote-status";
        public const string FilterListItem = Default + "/filter-list-item";
        public const string FilterListSupplier = Default + "/filter-list-supplier";
        public const string FilterListProductGrouping = Default + "/filter-list-product-grouping";
        public const string FilterListProductType = Default + "/filter-list-product-type";
        public const string FilterListPosition = Default + "/filter-list-position";
        public const string FilterListEmailStatus = Default + "/filter-list-email-status";

        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListCompanyStatus = Default + "/single-list-company-status";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListContact = Default + "/single-list-contact";
        public const string SingleListContactStatus = Default + "/single-list-contact-status";
        public const string SingleListCustomerLeadSource = Default + "/single-list-customer-lead-source";
        public const string SingleListPotentialResult = Default + "/single-list-potential-result";
        public const string SingleListProbability = Default + "/single-list-probability";
        public const string SingleListSaleStage = Default + "/single-list-sale-stage";
        public const string SingleListPosition = Default + "/single-list-position";
        public const string SingleListProduct = Default + "/single-list-product";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListCurrency = Default + "/single-list-currency";
        public const string SingleListEmailTemplate = Default + "/single-list-email-template";
        public const string SingleListOpportunityResult = Default + "/single-list-opportunity-result";
        public const string SingleListOpportunityResultType = Default + "/single-list-opportunity-result-type";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";
        public const string SingleListItem = Default + "/single-list-item";
        public const string SingleListActivityStatus = Default + "/single-list-activity-status";
        public const string SingleListActivityType = Default + "/single-list-activity-type";
        public const string SingleListCustomerLeadLevel = Default + "/single-list-customer-lead-level";
        public const string SingleListCustomerLeadStatus = Default + "/single-list-customer-lead-status";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListProfession = Default + "/single-list-profession";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListActivityPriority = Default + "/single-list-activity-priority";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListSex = Default + "/single-list-sex";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListFileType = Default + "/single-list-file-type";
        public const string SingleListEditedPriceStatus = Default + "/single-list-edited-price-status";
        public const string SingleListOrderQuoteStatus = Default + "/single-list-order-quote-status";
        public const string SingleListTaxType = Default + "/single-list-tax-type";
        public const string SingleListEmail = Default + "/single-list-email";

        public const string CountActivity = Default + "/count-activity";
        public const string ListActivity = Default + "/list-activity";
        public const string GetActivity = Default + "/get-activity";
        public const string CreateActivity = Default + "/create-activity";
        public const string UpdateActivity = Default + "/update-activity";
        public const string DeleteActivity = Default + "/delete-activity";
        public const string BulkDeleteActivity = Default + "/bulk-delete-activity";

        public const string CountCallLog = Default + "/count-call-log";
        public const string ListCallLog = Default + "/list-call-log";
        public const string GetCallLog = Default + "/get-call-log";
        public const string DeleteCallLog = Default + "/delete-call-log";

        public const string CountItem = Default + "/count-item";
        public const string ListItem = Default + "/list-item";

        public const string CountAuditLogProperty = Default + "/count-audit-log-property";
        public const string ListAuditLogProperty = Default + "/list-audit-log-property";

        public const string CountContact = Default + "/count-contact";
        public const string ListContact = Default + "/list-contact";
        public const string GetContact = Default + "/get-contact";
        public const string CreateContact = Default + "/create-contact";
        public const string UpdateContact = Default + "/update-contact";
        public const string DeleteContact = Default + "/delete-contact";
        public const string BulkDeleteContact = Default + "/bulk-delete-contact";

        public const string CountOrderQuote = Default + "/count-order-quote";
        public const string ListOrderQuote = Default + "/list-order-quote";
        public const string GetOrderQuote = Default + "/get-order-quote";
        public const string CreateOrderQuote = Default + "/create-order-quote";
        public const string UpdateOrderQuote = Default + "/update-order-quote";
        public const string DeleteOrderQuote = Default + "/delete-order-quote";
        public const string BulkDeleteOrderQuote = Default + "/bulk-delete-order-quote";

        public const string CreateEmail = Default + "/create-email";
        public const string SendEmail = Default + "/send-email";
        public const string GetOpportunityEmail = Default + "/get-opportunity-email";
        public const string CountOpportunityEmail = Default + "/count-opportunity-email";
        public const string ListOpportunityEmail = Default + "/list-opportunity-email";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(OpportunityFilter.AppUserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType, 
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition, } },
            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetContact, GetOrderQuote,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Detail, Create, UploadFile, 
                SingleListCompany, SingleListCompanyStatus, SingleListAppUser, SingleListContact, SingleListContactStatus, SingleListCustomerLeadSource, SingleListPotentialResult,
                SingleListProbability, SingleListSaleStage, SingleListPosition, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListCurrency, SingleListOpportunityResult,
                SingleListUnitOfMeasure, SingleListItem, SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListDistrict,
                SingleListProfession, SingleListProvince, SingleListActivityPriority, SingleListProductGrouping, SingleListSex, SingleListNation, SingleListOpportunity, SingleListFileType,
                SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListTaxType, SingleListOpportunityResultType, SingleListEmailTemplate,
                CountActivity, ListActivity, CountCallLog, ListCallLog, CountItem, ListItem, CountAuditLogProperty, ListAuditLogProperty,
                CountContact, ListContact, CountOrderQuote, ListOrderQuote} },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetContact, GetOrderQuote,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Detail, Update, ChangeStatus, UploadFile,
                SingleListCompany, SingleListCompanyStatus, SingleListAppUser, SingleListContact, SingleListContactStatus, SingleListCustomerLeadSource, SingleListPotentialResult,
                SingleListProbability, SingleListSaleStage, SingleListPosition, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListCurrency, SingleListOpportunityResult,
                SingleListUnitOfMeasure, SingleListItem, SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListDistrict,
                SingleListProfession, SingleListProvince, SingleListActivityPriority, SingleListProductGrouping, SingleListSex, SingleListNation, SingleListOpportunity, SingleListFileType,
                SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListTaxType, SingleListOpportunityResultType, SingleListEmailTemplate, 
                CountActivity, ListActivity, CountCallLog, ListCallLog, CountItem, ListItem, CountAuditLogProperty, ListAuditLogProperty,
                CountContact, ListContact, CountOrderQuote, ListOrderQuote} },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, GetPreview,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Delete} },

            { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List, GetPreview,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Export } },

            { "Nhập excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                ExportTemplate, Import, } },

            { "Thao tác", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetOpportunityEmail,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Detail, CreateActivity, UpdateActivity, DeleteActivity, BulkDeleteActivity, CreateEmail, SendEmail,
                SingleListCompany, SingleListCompanyStatus, SingleListAppUser, SingleListContact, SingleListContactStatus, SingleListCustomerLeadSource, SingleListPotentialResult,
                SingleListProbability, SingleListSaleStage, SingleListPosition, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListCurrency, SingleListOpportunityResult,
                SingleListUnitOfMeasure, SingleListItem, SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListDistrict,
                SingleListProfession, SingleListProvince, SingleListActivityPriority, SingleListProductGrouping, SingleListSex, SingleListNation, SingleListOpportunity, SingleListFileType,
                SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListTaxType, SingleListOpportunityResultType, SingleListEmailTemplate, SingleListEmail,
                CountActivity, ListActivity, CountOpportunityEmail, ListOpportunityEmail} },

            { "Quản lý báo giá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetOrderQuote,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Detail, CreateOrderQuote, UpdateOrderQuote, DeleteOrderQuote, BulkDeleteOrderQuote,
                SingleListCompany, SingleListCompanyStatus, SingleListAppUser, SingleListContact, SingleListContactStatus, SingleListCustomerLeadSource, SingleListPotentialResult,
                SingleListProbability, SingleListSaleStage, SingleListPosition, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListCurrency, SingleListOpportunityResult,
                SingleListUnitOfMeasure, SingleListItem, SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListDistrict,
                SingleListProfession, SingleListProvince, SingleListActivityPriority, SingleListProductGrouping, SingleListSex, SingleListNation, SingleListOpportunity, SingleListFileType,
                SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListTaxType, SingleListOpportunityResultType, SingleListEmailTemplate,
                CountOrderQuote, ListOrderQuote,} },

            { "Quản lý liên hệ", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetContact,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Detail, CreateContact, UpdateContact, DeleteContact, BulkDeleteContact,
                SingleListCompany, SingleListCompanyStatus, SingleListAppUser, SingleListContact, SingleListContactStatus, SingleListCustomerLeadSource, SingleListPotentialResult,
                SingleListProbability, SingleListSaleStage, SingleListPosition, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListCurrency, SingleListOpportunityResult,
                SingleListUnitOfMeasure, SingleListItem, SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListDistrict,
                SingleListProfession, SingleListProvince, SingleListActivityPriority, SingleListProductGrouping, SingleListSex, SingleListNation, SingleListOpportunity, SingleListFileType,
                SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListTaxType, SingleListOpportunityResultType, SingleListEmailTemplate,
                CountContact, ListContact,} },

            { "Quản lý lịch sử cuộc gọi", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetCallLog,
                FilterListCompany, FilterListAppUser, FilterListContact, FilterListSaleStage, FilterListEmailStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListOrderQuoteStatus, FilterListPosition,
                Detail, DeleteCallLog,
                SingleListCompany, SingleListCompanyStatus, SingleListAppUser, SingleListContact, SingleListContactStatus, SingleListCustomerLeadSource, SingleListPotentialResult,
                SingleListProbability, SingleListSaleStage, SingleListPosition, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListCurrency, SingleListOpportunityResult,
                SingleListUnitOfMeasure, SingleListItem, SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListDistrict,
                SingleListProfession, SingleListProvince, SingleListActivityPriority, SingleListProductGrouping, SingleListSex, SingleListNation, SingleListOpportunity, SingleListFileType,
                SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListTaxType, SingleListOpportunityResultType, SingleListEmailTemplate,
                CountCallLog, ListCallLog} },
        };
    }
}
