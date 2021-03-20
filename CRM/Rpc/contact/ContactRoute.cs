using System.Collections.Generic; 
using CRM.Common; 
using CRM.Entities; 

namespace CRM.Rpc.contact
{
    public class ContactRoute : Root
    {
        public const string Parent = Module + "/sales";
        public const string Master = Module + "/sales/contact/contact-master";
        public const string Detail = Module + "/sales/contact/contact-detail";
        private const string Default = Rpc + Module + "/contact";
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
        public const string UploadFile = Default + "/upload-file";

        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListCustomerLeadSource = Default + "/filter-list-customer-lead-source";
        public const string FilterListContactStatus = Default + "/filter-list-contact-status";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListSaleStage = Default + "/filter-list-sale-stage";
        public const string FilterListActivityType = Default + "/filter-list-activity-type";
        public const string FilterListActivityStatus = Default + "/filter-list-activity-status";
        public const string FilterListContact = Default + "/filter-list-contact";
        public const string FilterListOrderQuoteStatus = Default + "/filter-list-order-quote-status";
        public const string FilterListOrderPaymentStatus = Default + "/filter-list-order-payment-status";
        public const string FilterListRequestState = Default + "/filter-list-request-state";
        public const string FilterListEmailStatus = Default + "/filter-list-email-status";
        
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListProfession = Default + "/single-list-profession";
        public const string SingleListCustomerLeadSource = Default + "/single-list-customer-lead-source";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListContactStatus = Default + "/single-list-contact-status";
        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListSex = Default + "/single-list-sex";
        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListPosition = Default + "/single-list-position";
        public const string SingleListEmailTemplate = Default + "/single-list-email-template";
        public const string SingleListSmsTemplate = Default + "/single-list-sms-template";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";
        public const string SingleListTaxType = Default + "/single-list-tax-type";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListOrderQuoteStatus = Default + "/single-list-order-quote-status";
        public const string SingleListActivityType = Default + "/single-list-activity-type";
        public const string SingleListActivityStatus = Default + "/single-list-activity-status";
        public const string SingleListActivityPriority = Default + "/single-list-activity-priority";
        public const string SingleListFileType = Default + "/single-list-file-type";
        public const string SingleListCompanyStatus = Default + "/single-list-company-status";
        public const string SingleListEditedPriceStatus = Default + "/single-list-edited-price-status";
        public const string SingleListProbability = Default + "/single-list-probability";
        public const string SingleListContact = Default + "/single-list-contact";
        public const string SingleListSaleStage = Default + "/single-sale-stage";

        public const string SendSms = Default + "/send-sms";
        public const string CreateEmail = Default + "/create-email";
        public const string SendEmail = Default + "/send-email";
        public const string Convert = Default + "/convert";

        public const string CountActivity = Default + "/count-activity";
        public const string ListActivity = Default + "/list-activity";
        public const string GetActivity = Default + "/get-activity";
        public const string CreateActivity = Default + "/create-activity";
        public const string UpdateActivity = Default + "/update-activity";
        public const string DeleteActivity = Default + "/delete-activity";
        public const string BulkDeleteActivity = Default + "/bulk-delete-activity";

        public const string ListCallLog = Default + "/list-call-log";
        public const string CountCallLog = Default + "/count-call-log";
        public const string GetCallLog = Default + "/get-call-log";
        public const string DeleteCallLog = Default + "/delete-call-log";

        public const string ListAuditLogProperty = Default + "/list-audit-log-property";
        public const string CountAuditLogProperty = Default + "/count-audit-log-property";
        public const string GetAuditLogProperty = Default + "/get-audit-log-property";

        public const string CountCustomerSalesOrder = Default + "/count-customer-sales-order";
        public const string ListCustomerSalesOrder = Default + "/list-customer-sales-order";
        public const string GetCustomerSalesOrder = Default + "/get-customer-sales-order";

        public const string ListDirectSalesOrder = Default + "/list-direct-sales-order";
        public const string CountDirectSalesOrder = Default + "/count-direct-sales-order";
        public const string GetDirectSalesOrder = Default + "/get-direct-sales-order";

        public const string ListItem = Default + "/list-item";
        public const string CountItem = Default + "/count-item";

        public const string CountOpportunity = Default + "/count-opportunity";
        public const string ListOpportunity = Default + "/list-opportunity";
        public const string GetOpportunity = Default + "/get-opportunity";
        public const string CreateOpportunity = Default + "/create-opportunity";
        public const string UpdateOpportunity = Default + "/update-opportunity";
        public const string DeleteOpportunity = Default + "/delete-opportunity";
        public const string BulkDeleteOpportunity = Default + "/bulk-delete-opportunity";

        public const string ListOrderQuote = Default + "/list-order-quote";
        public const string CountOrderQuote = Default + "/count-order-quote";
        public const string GetOrderQuote = Default + "/get-order-quote";
        public const string CreateOrderQuote = Default + "/create-order-quote";
        public const string UpdateOrderQuote = Default + "/update-order-quote";
        public const string DeleteOrderQuote = Default + "/delete-order-quote";
        public const string BulkDeleteOrderQuote = Default + "/bulk-delete-order-quote";

        public const string GetContactEmail = Default + "/get-contact-email";
        public const string CountContactEmail = Default + "/count-contact-email";
        public const string ListContactEmail = Default + "/list-contact-email";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(ContactFilter.AppUserId), FieldTypeEnum.ID.Id },
            //{ nameof(ContactFilter.UserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType, 
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState
                } },
            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetCustomerSalesOrder, GetDirectSalesOrder, GetOpportunity,
                GetOrderQuote,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, Create, UploadFile,
                SingleListCompany, SingleListProfession, SingleListNation, SingleListCompanyStatus, SingleListProvince, SingleListDistrict, SingleListCustomerLeadSource, 
                SingleListContactStatus, SingleListSex, SingleListOrganization, SingleListPosition, SingleListActivityType, SingleListActivityStatus, SingleListAppUser,
                SingleListEditedPriceStatus, SingleListContact, SingleListOrderQuoteStatus, SingleListOpportunity, SingleListUnitOfMeasure, SingleListTaxType, SingleListSupplier,
                SingleListProductType, SingleListProductGrouping, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProbability, SingleListActivityPriority, SingleListFileType,
                SingleListSaleStage,
                CountActivity, ListActivity, CountCallLog, ListCallLog, ListAuditLogProperty, CountAuditLogProperty, ListCustomerSalesOrder, CountCustomerSalesOrder, CountDirectSalesOrder, ListDirectSalesOrder, ListItem, CountItem,
                CountOpportunity, ListOpportunity, CountOrderQuote, ListOrderQuote } },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetCustomerSalesOrder, GetDirectSalesOrder, GetOpportunity,
                GetOrderQuote,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, Update, UploadFile,
                SingleListCompany, SingleListProfession, SingleListNation, SingleListCompanyStatus, SingleListProvince, SingleListDistrict, SingleListCustomerLeadSource, 
                SingleListContactStatus, SingleListSex, SingleListOrganization, SingleListPosition, SingleListActivityType, SingleListActivityStatus, SingleListAppUser,
                SingleListEditedPriceStatus, SingleListContact, SingleListOrderQuoteStatus, SingleListOpportunity, SingleListUnitOfMeasure, SingleListTaxType, SingleListSupplier,
                SingleListProductType, SingleListProductGrouping, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProbability, SingleListActivityPriority, SingleListFileType,
                SingleListSaleStage,
                CountActivity, ListActivity, CountCallLog, ListCallLog, ListAuditLogProperty, CountAuditLogProperty, ListCustomerSalesOrder, CountCustomerSalesOrder, CountDirectSalesOrder, ListDirectSalesOrder, ListItem, CountItem,
                CountOpportunity, ListOpportunity, CountOrderQuote, ListOrderQuote } },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetCustomerSalesOrder, GetDirectSalesOrder, GetOpportunity,
                GetOrderQuote,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, Delete } },

            { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Parent, 
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, Export } },

            { "Nhập excel", new List<string> {
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, ExportTemplate, Import } },

            { "Thao tác", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetContactEmail,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, SendSms, CreateEmail, SendEmail, CreateActivity, UpdateActivity, DeleteActivity, BulkDeleteActivity,
                SingleListCompany, SingleListProfession, SingleListNation, SingleListCompanyStatus, SingleListProvince, SingleListDistrict, SingleListCustomerLeadSource, 
                SingleListContactStatus, SingleListSex, SingleListOrganization, SingleListPosition, SingleListActivityType, SingleListActivityStatus, SingleListAppUser,
                SingleListEditedPriceStatus, SingleListContact, SingleListOrderQuoteStatus, SingleListOpportunity, SingleListUnitOfMeasure, SingleListTaxType, SingleListSupplier,
                SingleListProductType, SingleListProductGrouping, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProbability, SingleListActivityPriority, SingleListFileType,
                SingleListSaleStage,
                CountActivity, ListActivity, CountContactEmail, ListContactEmail } },

            { "Quản lý lịch sử cuộc gọi", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetCallLog,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, DeleteCallLog,
                SingleListCompany, SingleListProfession, SingleListNation, SingleListCompanyStatus, SingleListProvince, SingleListDistrict, SingleListCustomerLeadSource, 
                SingleListContactStatus, SingleListSex, SingleListOrganization, SingleListPosition, SingleListActivityType, SingleListActivityStatus, SingleListAppUser,
                SingleListEditedPriceStatus, SingleListContact, SingleListOrderQuoteStatus, SingleListOpportunity, SingleListUnitOfMeasure, SingleListTaxType, SingleListSupplier,
                SingleListProductType, SingleListProductGrouping, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProbability, SingleListActivityPriority, SingleListFileType,
                SingleListSaleStage,
                CountCallLog, ListCallLog} },

            { "Quản lý cơ hội", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetOpportunity,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, CreateOpportunity, UpdateOpportunity, DeleteOpportunity, BulkDeleteOpportunity,
                SingleListCompany, SingleListProfession, SingleListNation, SingleListCompanyStatus, SingleListProvince, SingleListDistrict, SingleListCustomerLeadSource, 
                SingleListContactStatus, SingleListSex, SingleListOrganization, SingleListPosition, SingleListActivityType, SingleListActivityStatus, SingleListAppUser,
                SingleListEditedPriceStatus, SingleListContact, SingleListOrderQuoteStatus, SingleListOpportunity, SingleListUnitOfMeasure, SingleListTaxType, SingleListSupplier,
                SingleListProductType, SingleListProductGrouping, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProbability, SingleListActivityPriority, SingleListFileType,
                SingleListSaleStage,
                CountOpportunity, ListOpportunity } },

            { "Quản lý báo giá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetOrderQuote,
                FilterListCompany, FilterListCustomerLeadSource, FilterListContactStatus, FilterListAppUser, FilterListSaleStage, FilterListActivityType,
                FilterListActivityStatus, FilterListEmailStatus, FilterListContact, FilterListOrderQuoteStatus, FilterListOrderPaymentStatus, FilterListRequestState,
                Detail, CreateOrderQuote, UpdateOrderQuote, DeleteOrderQuote, BulkDeleteOrderQuote,
                SingleListCompany, SingleListProfession, SingleListNation, SingleListCompanyStatus, SingleListProvince, SingleListDistrict, SingleListCustomerLeadSource, 
                SingleListContactStatus, SingleListSex, SingleListOrganization, SingleListPosition, SingleListActivityType, SingleListActivityStatus, SingleListAppUser,
                SingleListEditedPriceStatus, SingleListContact, SingleListOrderQuoteStatus, SingleListOpportunity, SingleListUnitOfMeasure, SingleListTaxType, SingleListSupplier,
                SingleListProductType, SingleListProductGrouping, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProbability, SingleListActivityPriority, SingleListFileType,
                SingleListSaleStage,
                CountOrderQuote, ListOrderQuote } },
        };
    }
}
