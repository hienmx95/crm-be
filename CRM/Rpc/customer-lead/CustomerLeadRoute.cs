using System.Collections.Generic; 
using CRM.Common; 
using CRM.Entities; 

namespace CRM.Rpc.customer_lead
{
    public class CustomerLeadRoute : Root
    {
        public const string Parent = Module + "/sales";
        public const string Master = Module + "/sales/customer-lead/customer-lead-master";
        public const string Detail = Module + "/sales/customer-lead/customer-lead-detail/*";
        public const string GetPreview = Module + "/sales/customer-lead/customer-lead-preview";
        private const string Default = Rpc + Module + "/customer-lead";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-tempate";
        public const string BulkDelete = Default + "/bulk-delete";
        public const string UploadFile = Default + "/upload-file";
        public const string Convert = Default + "/convert";

        public const string FilterListActivityStatus = Default + "/filter-list-activity-status";
        public const string FilterListActivityType = Default + "/filter-list-activity-type";
        public const string FilterListCustomerLeadLevel = Default + "/filter-list-customer-lead-level";
        public const string FilterListCustomerLeadSource = Default + "/filter-list-customer-lead-source";
        public const string FilterListCustomerLeadStatus = Default + "/filter-list-customer-lead-status";
        public const string FilterListDistrict = Default + "/filter-list-district";
        public const string FilterListItem = Default + "/filter-list-item";
        public const string FilterListProfession = Default + "/filter-list-profession";
        public const string FilterListProvince = Default + "/filter-list-province";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListSex = Default + "/filter-list-sex";
        public const string FilterListNation = Default + "/filter-list-nation";
        public const string FilterListSupplier = Default + "/filter-list-supplier";
        public const string FilterListProductGrouping = Default + "/filter-list-product-grouping";
        public const string FilterListProductType = Default + "/filter-list-product-type";
        public const string FilterListEmailStatus = Default + "/filter-list-email-status";

        public const string SingleListActivityStatus = Default + "/single-list-activity-status";
        public const string SingleListActivityType = Default + "/single-list-activity-type";
        public const string SingleListCustomerLeadLevel = Default + "/single-list-customer-lead-level";
        public const string SingleListCustomerLeadSource = Default + "/single-list-customer-lead-source";
        public const string SingleListCustomerLeadStatus = Default + "/single-list-customer-lead-status";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListEmailTemplate = Default + "/single-list-email-template";
        public const string SingleListSmsTemplate = Default + "/single-list-sms-template";
        public const string SingleListProfession = Default + "/single-list-profession";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListProduct = Default + "/single-list-product";
        public const string SingleListActivityPriority = Default + "/single-list-activity-priority";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListProbability = Default + "/single-list-probability";
        public const string SingleListSex = Default + "/single-list-sex";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListContact = Default + "/single-list-contact";
        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";
        public const string SingleListItem = Default + "/single-list-item";
        public const string SingleListFileType = Default + "/single-list-file-type";
        public const string SingleListCurrency = Default + "/single-list-currency";

        public const string CountActivity = Default + "/count-activity";
        public const string ListActivity = Default + "/list-activity";
        public const string GetActivity = Default + "/get-activity";
        public const string CreateActivity = Default + "/create-activity";
        public const string UpdateActivity = Default + "/update-activity";
        public const string DeleteActivity = Default + "/delete-activity";
        public const string BulkDeleteActivity = Default + "/bulk-delete-activity";

        public const string SendSms = Default + "/send-sms";
        public const string CreateEmail = Default + "/create-email";
        public const string SendEmail = Default + "/send-email";
        
        public const string CountCallLog = Default + "/count-call-log";
        public const string ListCallLog = Default + "/list-call-log";
        public const string GetCallLog = Default + "/get-call-log";
        public const string DeleteCallLog = Default + "/delete-call-log";

        public const string CountItem = Default + "/count-item";
        public const string ListItem = Default + "/list-item";

        public const string CountAuditLogProperty = Default + "/count-audit-log-property";
        public const string ListAuditLogProperty = Default + "/list-audit-log-property";

        public const string GetCustomerLeadEmail = Default + "/get-customer-lead-email";
        public const string CountCustomerLeadEmail = Default + "/count-customer-lead-email";
        public const string ListCustomerLeadEmail = Default + "/list-customer-lead-email";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerLeadFilter.AppUserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict, 
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus } },

            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, 
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Detail, Create, UploadFile,
                SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadSource, SingleListCustomerLeadStatus, SingleListDistrict, SingleListEmailTemplate,
                SingleListProfession, SingleListProvince, SingleListAppUser, SingleListProduct, SingleListAppUser, SingleListProduct, SingleListActivityPriority, SingleListSupplier, SingleListProductType,
                SingleListProbability, SingleListSex, SingleListNation, SingleListContact, SingleListCompany, SingleListOpportunity, SingleListUnitOfMeasure, SingleListItem, SingleListFileType,
                SingleListProductGrouping, SingleListSmsTemplate, SingleListCurrency,
                CountActivity, ListActivity, CountCallLog, ListCallLog, CountItem, ListItem, CountAuditLogProperty, ListAuditLogProperty } },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, 
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Detail, Update, UploadFile,
                SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadSource, SingleListCustomerLeadStatus, SingleListDistrict, SingleListEmailTemplate,
                SingleListProfession, SingleListProvince, SingleListAppUser, SingleListProduct, SingleListAppUser, SingleListProduct, SingleListActivityPriority, SingleListSupplier, SingleListProductType,
                SingleListProbability, SingleListSex, SingleListNation, SingleListContact, SingleListCompany, SingleListOpportunity, SingleListUnitOfMeasure, SingleListItem, SingleListFileType,
                SingleListProductGrouping, SingleListSmsTemplate, SingleListCurrency, 
                CountActivity, ListActivity, CountCallLog, ListCallLog, CountItem, ListItem, CountAuditLogProperty, ListAuditLogProperty } },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Delete,} },

            { "Xoá nhiều", new List<string> {
                Master, Count, List, Get, GetPreview, 
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Master, Count, List, Get,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Export } },

            { "Nhập excel", new List<string> {
                Master, Count, List, Get,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                ExportTemplate, Import } },

            { "Thao tác", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCustomerLeadEmail,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Detail, SendSms, CreateEmail, SendEmail, CreateActivity, UpdateActivity, DeleteActivity, BulkDeleteActivity,
                SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadSource, SingleListCustomerLeadStatus, SingleListDistrict, SingleListEmailTemplate,
                SingleListProfession, SingleListProvince, SingleListAppUser, SingleListProduct, SingleListAppUser, SingleListProduct, SingleListActivityPriority, SingleListSupplier, SingleListProductType,
                SingleListProbability, SingleListSex, SingleListNation, SingleListContact, SingleListCompany, SingleListOpportunity, SingleListUnitOfMeasure, SingleListItem, SingleListFileType,
                SingleListProductGrouping, SingleListSmsTemplate, SingleListCurrency, 
                CountActivity, ListActivity, CountCustomerLeadEmail, ListCustomerLeadEmail} },

            { "Chuyển đổi", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Detail, Convert,
                SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadSource, SingleListCustomerLeadStatus, SingleListDistrict, SingleListEmailTemplate,
                SingleListProfession, SingleListProvince, SingleListAppUser, SingleListProduct, SingleListAppUser, SingleListProduct, SingleListActivityPriority, SingleListSupplier, SingleListProductType,
                SingleListProbability, SingleListSex, SingleListNation, SingleListContact, SingleListCompany, SingleListOpportunity, SingleListUnitOfMeasure, SingleListItem, SingleListFileType,
                SingleListProductGrouping, SingleListSmsTemplate, SingleListCurrency } },

            { "Quản lý lịch sử cuộc gọi", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetCallLog,
                FilterListActivityStatus, FilterListActivityType, FilterListCustomerLeadLevel, FilterListCustomerLeadSource, FilterListCustomerLeadStatus, FilterListDistrict,
                FilterListProfession, FilterListProvince, FilterListAppUser, FilterListSex, FilterListNation, FilterListItem, FilterListSupplier, FilterListProductGrouping, FilterListProductType, FilterListEmailStatus,
                Detail, DeleteCallLog,
                SingleListActivityStatus, SingleListActivityType, SingleListCustomerLeadLevel, SingleListCustomerLeadSource, SingleListCustomerLeadStatus, SingleListDistrict, SingleListEmailTemplate,
                SingleListProfession, SingleListProvince, SingleListAppUser, SingleListProduct, SingleListAppUser, SingleListProduct, SingleListActivityPriority, SingleListSupplier, SingleListProductType,
                SingleListProbability, SingleListSex, SingleListNation, SingleListContact, SingleListCompany, SingleListOpportunity, SingleListUnitOfMeasure, SingleListItem, SingleListFileType,
                SingleListProductGrouping, SingleListSmsTemplate, SingleListCurrency,
                CountCallLog, ListCallLog} },

        };
    }
}
