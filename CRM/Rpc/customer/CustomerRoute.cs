using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;

namespace CRM.Rpc.customer
{
    public class CustomerRoute : Root
    {
        public const string Parent = Module + "/master-data/customer";
        public const string Master = Module + "/master-data/customer/customer-master";
        public const string Detail = Module + "/master-data/customer/customer-detail/*";
        public const string Preview = Module + "/master-data/customer/customer-preview/*";
        private const string Default = Rpc + Module + "/customer";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-template";
        public const string BulkDelete = Default + "/bulk-delete";

        public const string FilterListBusinessType = Default + "/filter-list-business-type";
        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCustomerResource = Default + "/filter-list-customer-resource";
        public const string FilterListCustomerGrouping = Default + "/filter-list-customer-grouping";
        public const string FilterListCustomerType = Default + "/filter-list-customer-type";
        public const string FilterListDistrict = Default + "/filter-list-district";
        public const string FilterListNation = Default + "/filter-list-nation";
        public const string FilterListProfession = Default + "/filter-list-profession";
        public const string FilterListProvince = Default + "/filter-list-province";
        public const string FilterListSex = Default + "/filter-list-sex";
        public const string FilterListStatus = Default + "/filter-list-status";
        public const string FilterListWard = Default + "/filter-list-ward";
        public const string FilterListEmailType = Default + "/filter-list-email-type";
        public const string FilterListCustomerFeedback = Default + "/filter-list-customer-feedback";
        public const string FilterListCustomerFeedbackType = Default + "/filter-list-customer-feedback-type";
        public const string FilterListPhoneType = Default + "/filter-list-phone-type";
        public const string FilterListCallCategory = Default + "/filter-list-call-category";
        public const string FilterListCallStatus = Default + "/filter-list-call-status";
        public const string FilterListCallType = Default + "/filter-list-call-type";
        public const string FilterListEmailStatus = Default + "/filter-list-email-status";
        public const string FilterListContractStatus = Default + "/filter-list-contract-status";
        public const string FilterListContractType = Default + "/filter-list-contract-type";
        public const string FilterListCurrency = Default + "/filter-list-currency";
        public const string FilterListPaymentStatus = Default + "/filter-list-payment-status";
        public const string FilterListOrderCategory = Default + "/filter-list-order-category";
        public const string FilterListRepairStatus = Default + "/filter-list-repair-status";
        public const string FilterListRequestState = Default + "/filter-list-request-state";
        public const string FilterListOpportunity = Default + "/filter-list-opportunity";
        public const string FilterListContract = Default + "/filter-list-contract";
        public const string FilterListStore = Default + "/filter-list-store";
        
        public const string SingleListBusinessType = Default + "/single-list-business-type";
        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListCustomerResource = Default + "/single-list-customer-resource";
        public const string SingleListCustomerGrouping = Default + "/single-list-customer-grouping";
        public const string SingleListCustomerType = Default + "/single-list-customer-type";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListProfession = Default + "/single-list-profession";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListSex = Default + "/single-list-sex";
        public const string SingleListStatus = Default + "/single-list-status";
        public const string SingleListWard = Default + "/single-list-ward";
        public const string SingleListEmailType = Default + "/single-list-email-type";
        public const string SingleListCustomerFeedback = Default + "/single-list-customer-feedback";
        public const string SingleListCustomerFeedbackType = Default + "/single-list-customer-feedback-type";
        public const string SingleListPhoneType = Default + "/single-list-phone-type";
        public const string SingleListCallCategory = Default + "/single-list-call-category";
        public const string SingleListCallStatus = Default + "/single-list-call-status";
        public const string SingleListCallType = Default + "/single-list-call-type";
        public const string SingleListEmailStatus = Default + "/single-list-email-status";
        public const string SingleListContractStatus = Default + "/single-list-contract-status";
        public const string SingleListContractType = Default + "/single-list-contract-type";
        public const string SingleListCurrency = Default + "/single-list-currency";
        public const string SingleListPaymentStatus = Default + "/single-list-payment-status";
        public const string SingleListOrderCategory = Default + "/single-list-order-category";
        public const string SingleListRepairStatus = Default + "/single-list-repair-status";
        public const string SingleListTicketIssueLevel = Default + "/single-list-ticket-issue-level";
        public const string SingleListTicketPriority = Default + "/single-list-ticket-priority";
        public const string SingleListTicketSource = Default + "/single-list-ticket-source";
        public const string SingleListTicketStatus = Default + "/single-list-ticket-status";
        public const string SingleListTicketGroup = Default + "/single-list-ticket-group";
        public const string SingleListTicketType = Default + "/single-list-ticket-type";
        public const string SingleListTicketResolveType = Default + "/single-list-ticket-resolve-type";
        public const string SingleListMailTemplate = Default + "/single-list-mail-template";
        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListEditedPriceStatus = Default + "/single-list-edited-price-status";

        public const string CreateContract = Default + "/create-contract";
        public const string UpdateContract = Default + "/update-contract";
        public const string DeleteContract = Default + "/delete-contract";
        public const string BulkDeleteContract = Default + "/bulk-delete-contract";
        public const string CountContract = Default + "/count-contract";
        public const string ListContract = Default + "/list-contract";
        public const string GetContract = Default + "/get-contract";

        public const string CountCustomerSalesOrder = Default + "/count-customer-sales-order";
        public const string ListCustomerSalesOrder = Default + "/list-customer-sales-order";
        public const string GetCustomerSalesOrder = Default + "/get-customer-sales-order";

        public const string CountDirectSalesOrder = Default + "/count-direct-sales-order";
        public const string ListDirectSalesOrder = Default + "/list-direct-sales-order";
        public const string GetDirectSalesOrder = Default + "/get-direct-sales-order";

        public const string CreateEmail = Default + "/create-email";
        public const string UpdateEmail = Default + "/update-email";
        public const string SendEmail = Default + "/send-email";
        public const string GetCustomerEmailHistory = Default + "/get-customer-email-history";
        public const string CountCustomerEmailHistory = Default + "/count-customer-email-history";
        public const string ListCustomerEmailHistory = Default + "/list-customer-email-history";

        public const string CreateRepairTicket = Default + "/create-repair-ticket";
        public const string UpdateRepairTicket = Default + "/update-repair-ticket";
        public const string DeleteRepairTicket = Default + "/delete-repair-ticket";
        public const string BulkDeleteRepairTicket = Default + "/bulk-delete-repair-ticket";
        public const string CountRepairTicket = Default + "/count-repair-ticket";
        public const string ListRepairTicket = Default + "/list-repair-ticket";
        public const string GetRepairTicket = Default + "/get-repair-ticket";

        public const string CountStore = Default + "/count-store";
        public const string ListStore = Default + "/list-store";
        public const string GetStoreProfile = Default + "/list-store-profile";
        public const string CreateStoreProfile = Default + "/create-store-profile";
        public const string UpdateStoreProfile = Default + "/update-store-profile";
        public const string BulkMergeStore = Default + "/bulk-merge-store";
        public const string DeleteStore = Default + "/delete-store";
        public const string BulkDeleteStore = Default + "/bulk-delete-store";

        public const string CreateCustomerPointHistory = Default + "/create-customer-point-history";
        public const string CountCustomerPointHistory = Default + "/count-customer-point-history";
        public const string ListCustomerPointHistory = Default + "/list-customer-point-history";
        public const string GetCustomerPointHistory = Default + "/get-customer-point-history";

        public const string CreateTicket = Default + "/create-ticket";
        public const string UpdateTicket = Default + "/update-ticket";
        public const string DeleteTicket = Default + "/delete-ticket";
        public const string CountTicket = Default + "/count-ticket";
        public const string ListTicket = Default + "/list-ticket";
        public const string GetTicket = Default + "/get-ticket";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerFilter.CustomerTypeId), FieldTypeEnum.ID.Id },
            { nameof(CustomerFilter.CustomerGroupingId), FieldTypeEnum.DATE.Id },
            { nameof(CustomerFilter.AppUserId), FieldTypeEnum.STRING.Id },
            { nameof(CustomerFilter.CreatorId), FieldTypeEnum.ID.Id },
            { nameof(CustomerFilter.OrganizationId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListCustomerGrouping,
                FilterListNation, FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, 
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCallCategory, FilterListCallStatus, FilterListCallType, FilterListEmailStatus, FilterListContractStatus,
                FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus,FilterListRequestState,
                FilterListOpportunity, FilterListContract, FilterListStore} },
            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview, 
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListCustomerGrouping,
                FilterListNation, FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, 
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCallCategory, FilterListCallStatus, FilterListCallType, FilterListEmailStatus, FilterListContractStatus,
                FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus, FilterListRequestState,
                FilterListOpportunity, FilterListContract, FilterListStore,
                Detail, Create,
                SingleListBusinessType, SingleListCompany, SingleListAppUser, SingleListCustomerResource, SingleListCustomerType, SingleListDistrict, SingleListNation, 
                SingleListProfession, SingleListProvince, SingleListSex, SingleListStatus, SingleListWard, SingleListEmailType, SingleListCustomerFeedback, 
                SingleListCustomerFeedbackType, SingleListPhoneType, SingleListCustomerGrouping, SingleListCallCategory, SingleListCallStatus, SingleListCallType,
                SingleListEmailStatus, SingleListContractStatus, SingleListContractType, SingleListCurrency, SingleListPaymentStatus, SingleListOrderCategory, SingleListRepairStatus,
                SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListTicketGroup, SingleListTicketType, 
                SingleListTicketResolveType, SingleListMailTemplate, SingleListCustomer, SingleListEditedPriceStatus, } },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview, 
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListNation, 
                FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, 
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCustomerGrouping, FilterListCallCategory, FilterListCallStatus, FilterListCallType, FilterListContractStatus,
                FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListEmailStatus, FilterListOrderCategory, FilterListRepairStatus, FilterListRequestState,
                FilterListOpportunity, FilterListContract, FilterListStore,
                Detail, Update,
                SingleListBusinessType, SingleListCompany, SingleListAppUser, SingleListCustomerResource, SingleListCustomerType, SingleListDistrict, SingleListNation,
                SingleListProfession, SingleListProvince, SingleListSex, SingleListStatus, SingleListWard, SingleListEmailType, SingleListCustomerFeedback,
                SingleListCustomerFeedbackType, SingleListPhoneType, SingleListCustomerGrouping, SingleListCallCategory, SingleListCallStatus, SingleListCallType,
                SingleListEmailStatus, SingleListContractStatus, SingleListContractType, SingleListCurrency, SingleListPaymentStatus, SingleListOrderCategory, SingleListRepairStatus,
                SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListTicketGroup, SingleListTicketType,
                SingleListTicketResolveType, SingleListMailTemplate, SingleListCustomer, SingleListEditedPriceStatus, } },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListNation, 
                FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, FilterListRequestState,
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCustomerGrouping, FilterListCallCategory, FilterListCallStatus, FilterListCallType,
                FilterListEmailStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus,
                FilterListOpportunity, FilterListContract, FilterListStore,
                Delete,
                SingleListBusinessType, SingleListCompany, SingleListAppUser, SingleListCustomerResource, SingleListCustomerType, SingleListDistrict, SingleListNation,
                SingleListProfession, SingleListProvince, SingleListSex, SingleListStatus, SingleListWard, SingleListEmailType, SingleListCustomerFeedback,
                SingleListCustomerFeedbackType, SingleListPhoneType, SingleListCustomerGrouping, SingleListCallCategory, SingleListCallStatus, SingleListCallType,
                SingleListEmailStatus, SingleListContractStatus, SingleListContractType, SingleListCurrency, SingleListPaymentStatus, SingleListOrderCategory, SingleListRepairStatus,
                SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListTicketGroup, SingleListTicketType,
                SingleListTicketResolveType, SingleListMailTemplate, SingleListCustomer, SingleListEditedPriceStatus,} },

            { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListNation, 
                FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, FilterListRequestState,
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCustomerGrouping, FilterListCallCategory, FilterListCallStatus, FilterListCallType,
                FilterListEmailStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus,
                FilterListOpportunity, FilterListContract, FilterListStore,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListNation, 
                FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, FilterListStore,
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCustomerGrouping, FilterListCallCategory, FilterListCallStatus, FilterListCallType,
                FilterListEmailStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus,
                Export } },

            { "Nhập excel", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListNation, 
                FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback, FilterListRequestState,
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCustomerGrouping, FilterListCallCategory, FilterListCallStatus, FilterListCallType,
                FilterListEmailStatus, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus,
                FilterListOpportunity, FilterListContract,
                ExportTemplate, Import } },

            { "Quản lý 360", new List<string> {
                Parent,
                Master, Count, List, 
                Get, Preview, GetContract, GetCustomerSalesOrder, GetDirectSalesOrder, GetRepairTicket, GetStoreProfile, GetCustomerEmailHistory, GetCustomerPointHistory,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListCustomerGrouping,
                FilterListNation, FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback,
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCallCategory, FilterListCallStatus, FilterListCallType, FilterListEmailStatus, FilterListContractStatus,
                FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus, FilterListRequestState,
                FilterListOpportunity, FilterListContract, FilterListStore,
                Detail,
                CreateContract, UpdateContract, DeleteContract, BulkDeleteContract,
                CreateRepairTicket, UpdateRepairTicket, DeleteRepairTicket, BulkDeleteRepairTicket,
                BulkMergeStore, UpdateStoreProfile, DeleteStore, BulkDeleteStore,
                CreateEmail, UpdateEmail, SendEmail,
                SingleListBusinessType, SingleListCompany, SingleListAppUser, SingleListCustomerResource, SingleListCustomerType, SingleListDistrict, SingleListNation,
                SingleListProfession, SingleListProvince, SingleListSex, SingleListStatus, SingleListWard, SingleListEmailType, SingleListCustomerFeedback,
                SingleListCustomerFeedbackType, SingleListPhoneType, SingleListCustomerGrouping, SingleListCallCategory, SingleListCallStatus, SingleListCallType,
                SingleListEmailStatus, SingleListContractStatus, SingleListContractType, SingleListCurrency, SingleListPaymentStatus, SingleListOrderCategory, SingleListRepairStatus,
                SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListTicketGroup, SingleListTicketType,
                SingleListTicketResolveType, SingleListMailTemplate, SingleListCustomer, SingleListEditedPriceStatus,
                CountContract, ListContract, CountCustomerSalesOrder, ListCustomerSalesOrder, CountDirectSalesOrder, ListDirectSalesOrder,CountRepairTicket, ListRepairTicket, 
                CountStore, ListStore, CountCustomerEmailHistory, ListCustomerEmailHistory, CountCustomerPointHistory, ListCustomerPointHistory
                } },

            { "Thao tác", new List<string> {
                Parent,
                Master, Count, List,
                Get, Preview, GetTicket, GetCustomerEmailHistory,
                FilterListBusinessType, FilterListCompany, FilterListAppUser, FilterListCustomerResource, FilterListCustomerType, FilterListDistrict, FilterListCustomerGrouping,
                FilterListNation, FilterListProfession, FilterListProvince, FilterListSex, FilterListStatus, FilterListWard, FilterListEmailType, FilterListCustomerFeedback,
                FilterListCustomerFeedbackType, FilterListPhoneType, FilterListCallCategory, FilterListCallStatus, FilterListCallType, FilterListEmailStatus, FilterListContractStatus,
                FilterListContractType, FilterListCurrency, FilterListPaymentStatus, FilterListOrderCategory, FilterListRepairStatus, FilterListRequestState,
                FilterListOpportunity, FilterListContract, FilterListStore,
                Detail,
                CreateEmail, UpdateEmail, SendEmail,
                CreateTicket, UpdateTicket, DeleteTicket,
                SingleListBusinessType, SingleListCompany, SingleListAppUser, SingleListCustomerResource, SingleListCustomerType, SingleListDistrict, SingleListNation,
                SingleListProfession, SingleListProvince, SingleListSex, SingleListStatus, SingleListWard, SingleListEmailType, SingleListCustomerFeedback,
                SingleListCustomerFeedbackType, SingleListPhoneType, SingleListCustomerGrouping, SingleListCallCategory, SingleListCallStatus, SingleListCallType,
                SingleListEmailStatus, SingleListContractStatus, SingleListContractType, SingleListCurrency, SingleListPaymentStatus, SingleListOrderCategory, SingleListRepairStatus,
                SingleListTicketIssueLevel, SingleListTicketPriority, SingleListTicketSource, SingleListTicketStatus, SingleListTicketGroup, SingleListTicketType,
                SingleListTicketResolveType, SingleListMailTemplate, SingleListCustomer, SingleListEditedPriceStatus,
                CountTicket, ListTicket, } },
        };
    }
}
