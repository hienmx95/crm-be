using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MCompany;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProvince;
using CRM.Services.MProfession;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MRatingStatus;
using System.ComponentModel;

namespace CRM.Rpc.company
{
    [DisplayName("Quản lý công ty")]
    public class CompanyRoute : Root
    {
        public const string Parent = Module + "/sales";
        public const string Master = Module + "/sales/company/company-master";
        public const string Detail = Module + "/sales/company/company-detail";
        private const string Default = Rpc + Module + "/company";
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

        public const string FilterListProfession = Default + "/filter-list-profession";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCompanyStatus = Default + "/filter-list-company-status";
        public const string FilterListSaleStage = Default + "/filter-list-sale-stage";
        public const string FilterListOrderQuoteStatus = Default + "/filter-list-order-quote-status";
        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListContact = Default + "/filter-list-contact";
        public const string FilterListRequestState = Default + "/filter-list-request-state";
        public const string FilterListOrderPaymentStatus = Default + "/filter-list-order-payment-status";
        public const string FilterListActivityStatus = Default + "/filter-list-activity-status";
        public const string FilterListActivityType = Default + "/filter-list-activity-type";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListPosition = Default + "/filter-list-position";
        public const string FilterListEmailStatus = Default + "/filter-list-email-status";
        public const string FilterListStore = Default + "/filter-list-store";
        public const string FilterListEditedPriceStatus = Default + "/filter-list-edited-price-status";
        public const string FilterListContractStatus = Default + "/filter-list-contract-status";
        public const string FilterListContractType = Default + "/filter-list-contract-type";
        public const string FilterListCurrency = Default + "/filter-list-currency";
        public const string FilterListPaymentStatus = Default + "/filter-list-payment-status";
        
        public const string SingleListCurrency = Default + "/single-list-currency";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListProfession = Default + "/single-list-profession";
        public const string SingleListCustomerLeadSource = Default + "/single-list-customer-lead-source";
        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListRatingStatus = Default + "/single-list-rating-status";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListCompanyStatus = Default + "/single-list-company-status";
        public const string SingleListSex = Default + "/single-list-sex";
        public const string SingleListContact = Default + "/single-list-contact";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";
        public const string SingleListItem = Default + "/single-list-item";
        public const string SingleListProbability = Default + "/single-list-probability";
        public const string SingleListFileType = Default + "/single-list-file-type";
        public const string SingleListPotentialResult = Default + "/single-list-potential-result";
        public const string SingleListSaleStage = Default + "/single-list-sale-stage";
        public const string SingleListEditedPriceStatus = Default + "/single-list-edited-price-status";
        public const string SingleListOrderQuoteStatus = Default + "/single-list-order-quote-status";
        public const string SingleListTaxType = Default + "/single-list-tax-type";
        public const string SingleListCustomerLeadLevel = Default + "/single-list-customer-lead-level";
        public const string SingleListCustomerLeadStatus = Default + "/single-list-customer-lead-status";
        public const string SingleListActivityStatus = Default + "/single-list-activity-status";
        public const string SingleListActivityType = Default + "/single-list-activity-type";
        public const string SingleListActivityPriority = Default + "/single-list-activity-priority";
        public const string SingleListEmailTemplate = Default + "/single-list-email-template";
        public const string SingleListSmsTemplate = Default + "/single-list-sms-template";
        public const string SingleListProduct = Default + "/single-list-product";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListRequestState = Default + "/single-list-request-state";
        public const string SingleListOrderPaymentStatus = Default + "/single-list-order-payment-status";

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

        public const string CountCallLog = Default + "/count-call-log";
        public const string ListCallLog = Default + "/list-call-log";
        public const string GetCallLog = Default + "/get-call-log";
        public const string DeleteCallLog = Default + "/delete-call-log";

        public const string CountItem = Default + "/count-item";
        public const string ListItem = Default + "/list-item";

        public const string CountAuditLogProperty = Default + "/count-audit-log-property";
        public const string ListAuditLogProperty = Default + "/list-audit-log-property";

        public const string CountOpportunity = Default + "/count-opportunity";
        public const string ListOpportunity = Default + "/list-opportunity";
        public const string GetOpportunity = Default + "/get-opportunity";
        public const string CreateOpportunity = Default + "/create-opportunity";
        public const string UpdateOpportunity = Default + "/update-opportunity";
        public const string DeleteOpportunity = Default + "/delete-opportunity";
        public const string BulkDeleteOpportunity = Default + "/bulk-delete-opportunity";

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

        public const string CountCustomerSalesOrder = Default + "/count-customer-sales-order";
        public const string ListCustomerSalesOrder = Default + "/list-customer-sales-order";
        public const string GetCustomerSalesOrder = Default + "/get-customer-sales-order";

        public const string CountDirectSalesOrder = Default + "/count-direct-sales-order";
        public const string ListDirectSalesOrder = Default + "/list-direct-sales-order";
        public const string GetDirectSalesOrder = Default + "/get-direct-sales-order";

        public const string GetCompanyEmail = Default + "/get-company-email";
        public const string CountCompanyEmail = Default + "/count-company-email";
        public const string ListCompanyEmail = Default + "/list-company-email";

        public const string GetContract = Default + "/get-contract";
        public const string CountContract = Default + "/count-contract";
        public const string ListContract = Default + "/list-contract";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CompanyFilter.AppUserId), FieldTypeEnum.ID.Id }
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization, 
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus
            } },

            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetOpportunity, GetContact, GetOrderQuote, GetCustomerSalesOrder, GetDirectSalesOrder,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, Create, UploadFile,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountActivity, ListActivity, CountCallLog, ListCallLog, CountItem, ListItem, CountAuditLogProperty, ListAuditLogProperty,
                CountOpportunity, ListOpportunity, CountContact, ListContact, CountOrderQuote, ListOrderQuote, CountCustomerSalesOrder, ListCustomerSalesOrder, CountDirectSalesOrder, ListDirectSalesOrder
            } },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCallLog, GetOpportunity, GetContact, GetOrderQuote, GetCustomerSalesOrder, GetDirectSalesOrder,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, Update, UploadFile,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountActivity, ListActivity, CountCallLog, ListCallLog, CountItem, ListItem, CountAuditLogProperty, ListAuditLogProperty,
                CountOpportunity, ListOpportunity, CountContact, ListContact, CountOrderQuote, ListOrderQuote, CountCustomerSalesOrder, ListCustomerSalesOrder, CountDirectSalesOrder, ListDirectSalesOrder 
            } },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Delete 
            } },

            { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                BulkDelete 
            } },

            { "Xuất excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Export 
            } },

            { "Nhập excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                ExportTemplate, Import 
            } },

            { "Thao tác", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetActivity, GetCompanyEmail,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, SendSms, CreateEmail, SendEmail, CreateActivity, UpdateActivity, DeleteActivity, BulkDeleteActivity,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountActivity, ListActivity, CountCompanyEmail, ListCompanyEmail } },

            { "Quản lý lịch sử cuộc gọi", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetCallLog,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, DeleteCallLog,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountCallLog, ListCallLog} },

            { "Quản lý cơ hội", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetOpportunity,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, CreateOpportunity, UpdateOpportunity, DeleteOpportunity, BulkDeleteOpportunity,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountOpportunity, ListOpportunity } },

            { "Quản lý báo giá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetOrderQuote,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, CreateOrderQuote, UpdateOrderQuote, DeleteOrderQuote, BulkDeleteOrderQuote,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountOrderQuote, ListOrderQuote } },

            { "Quản lý liên hệ", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetContact,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, CreateContact, UpdateContact, DeleteContact, BulkDeleteContact,
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountContact, ListContact } },
            { "Quản lý hợp đồng", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, GetContract,
                FilterListProfession, FilterListPosition, FilterListCompany, FilterListAppUser, FilterListCompanyStatus, FilterListActivityStatus, FilterListActivityType,
                FilterListRequestState, FilterListOrderPaymentStatus, FilterListContact, FilterListSaleStage, FilterListOrderQuoteStatus, FilterListOrganization,
                FilterListEmailStatus, FilterListStore, FilterListEditedPriceStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, 
                SingleListDistrict, SingleListNation, SingleListProvince, SingleListProfession, SingleListCustomerLeadSource, SingleListCompany, SingleListRatingStatus,
                SingleListAppUser, SingleListRequestState, SingleListCompanyStatus, SingleListUnitOfMeasure, SingleListCurrency, SingleListActivityStatus, SingleListActivityType,
                SingleListCustomerLeadLevel, SingleListCustomerLeadStatus, SingleListEmailTemplate, SingleListSmsTemplate, SingleListProduct, SingleListActivityPriority,
                SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListProbability, SingleListSex, SingleListContact, SingleListOpportunity,
                SingleListItem, SingleListFileType, SingleListPotentialResult, SingleListEditedPriceStatus, SingleListOrderQuoteStatus, SingleListSaleStage, SingleListTaxType,
                SingleListOrderPaymentStatus,
                CountContract, ListContract } },
        };
    }
}
