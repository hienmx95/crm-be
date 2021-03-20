using System.Collections.Generic; 
using CRM.Common; 
using CRM.Entities; 

namespace CRM.Rpc.order_quote
{
    public class OrderQuoteRoute : Root
    {
        public const string Parent = Module + "/sales/order-quote";
        public const string Master = Module + "/sales/order-quote/order-quote-master";
        public const string Detail = Module + "/sales/order-quote/order-quote-detail";
        private const string Default = Rpc + Module + "/order-quote";
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

        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListContact = Default + "/filter-list-contact";
        public const string FilterListOrderQuoteStatus = Default + "/filter-list-order-quote-status";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        
        public const string SingleListCompany = Default + "/single-list-company";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListContact = Default + "/single-list-contact";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListEditedPriceStatus = Default + "/single-list-edited-price-status";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListOrderQuoteStatus = Default + "/single-list-order-quote-status";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListTaxType = Default + "/single-list-tax-type";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";

        public const string ListItem = Default + "/list-item";
        public const string CountItem = Default + "/count-item";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(OrderQuoteFilter.AppUserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,
                ListItem, CountItem,} },
            { "Thêm", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview, 
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,  
                Detail, Create, 
                SingleListProductGrouping, SingleListProductType, SingleListSupplier, SingleListCompany, SingleListContact, SingleListDistrict, SingleListNation, 
                SingleListProvince, SingleListOpportunity, SingleListOrderQuoteStatus, SingleListAppUser, SingleListEditedPriceStatus, SingleListTaxType, 
                SingleListUnitOfMeasure,
                ListItem, CountItem,  } },

            { "Sửa", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,  
                Detail, Update,
                SingleListProductGrouping, SingleListProductType, SingleListSupplier, SingleListCompany, SingleListContact, SingleListDistrict, SingleListNation,
                SingleListProvince, SingleListOpportunity, SingleListOrderQuoteStatus, SingleListAppUser, SingleListEditedPriceStatus, SingleListTaxType,
                SingleListUnitOfMeasure,
                ListItem, CountItem, } },

            { "Xoá", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,
                Delete } },

            { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,
                BulkDelete } },

            { "Xuất excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,
                Export } },

            { "Nhập excel", new List<string> {
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListCompany, FilterListContact, FilterListOrderQuoteStatus, FilterListAppUser,
                ExportTemplate, Import } },
        };
    }
}
