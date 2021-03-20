using System.Collections.Generic; 
using CRM.Common; 
using CRM.Entities; 

namespace CRM.Rpc.contract
{
    public class ContractRoute : Root
    {
        public const string Parent = Module + "/sales";
        public const string Master = Module + "/sales/contract/contract-master";
        public const string Detail = Module + "/sales/contract/contract-detail";
        private const string Default = Rpc + Module + "/contract";
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

        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListContractStatus = Default + "/filter-list-contract-status";
        public const string FilterListContractType = Default + "/filter-list-contract-type";
        public const string FilterListCurrency = Default + "/filter-list-currency";
        public const string FilterListPaymentStatus = Default + "/filter-list-payment-status";

        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListContact = Default + "/single-list-contact";
        public const string SingleListContractStatus = Default + "/single-list-contract-status";
        public const string SingleListContractType = Default + "/single-list-contract-type";
        public const string SingleListCurrency = Default + "/single-list-currency";
        public const string SingleListPaymentStatus = Default + "/single-list-payment-status";
        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListProduct = Default + "/single-list-product";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListTaxType = Default + "/single-list-tax-type";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";
        public const string SingleListFileType = Default + "/single-list-file-type";

        public const string CountContact = Default + "/count-contact";
        public const string ListContact = Default + "/list-contact";
        public const string CountItem = Default + "/count-item";
        public const string ListItem = Default + "/list-item";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(ContractFilter.AppUserId), FieldTypeEnum.ID.Id },
            { nameof(ContractFilter.CompanyId), FieldTypeEnum.ID.Id },
            { nameof(ContractFilter.ContractTypeId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                } },
            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, 
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, Create, UploadFile,
                SingleListCompany, SingleListAppUser, SingleListContact, SingleListContractStatus, SingleListContractType, SingleListCurrency,
                SingleListCustomer, SingleListDistrict, SingleListNation, SingleListProvince, SingleListOpportunity, SingleListOrganization, 
                SingleListPaymentStatus, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListUnitOfMeasure, SingleListProductGrouping,
                SingleListTaxType, SingleListFileType,
                CountItem, ListItem, CountContact, ListContact } },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, Update, UploadFile,
                SingleListCompany, SingleListAppUser, SingleListContact, SingleListContractStatus, SingleListContractType, SingleListCurrency,
                SingleListCustomer, SingleListDistrict, SingleListNation, SingleListProvince, SingleListOpportunity, SingleListOrganization,
                SingleListPaymentStatus, SingleListProduct, SingleListSupplier, SingleListProductType, SingleListUnitOfMeasure, SingleListProductGrouping,
                SingleListTaxType, SingleListFileType,
                CountItem, ListItem, CountContact, ListContact } },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, Delete } },

            { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, BulkDelete } },

            { "Xuất excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Export } },

            { "Nhập excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListCustomer, FilterListContractStatus, FilterListContractType, FilterListCurrency, FilterListPaymentStatus,
                Detail, ExportTemplate, Import } },

        };
    }
}
