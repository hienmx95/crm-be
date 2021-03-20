using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;

namespace CRM.Rpc.customer_sales_order
{
    public class CustomerSalesOrderRoute : Root
    {
        public const string Parent = Module + "/sale-order/customer-sales-order";
        public const string Master = Module + "/sale-order/customer-sales-order/customer-sales-order-master";
        public const string Detail = Module + "/sale-order/customer-sales-order/customer-sales-order-detail";
        private const string Default = Rpc + Module + "/customer-sales-order";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string GetPreview = Default + "/get-preview";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string ExportTemplate = Default + "/export-template";
        public const string BulkDelete = Default + "/bulk-delete";
        
        public const string FilterListContract = Default + "/filter-list-contract";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListCustomerType = Default + "/filter-list-customer-type";
        public const string FilterListDistrict = Default + "/filter-list-district";
        public const string FilterListNation = Default + "/filter-list-nation";
        public const string FilterListProvince = Default + "/filter-list-province";
        public const string FilterListWard = Default + "/filter-list-ward";
        public const string FilterListEditedPriceStatus = Default + "/filter-list-edited-price-status";
        public const string FilterListOpportunity = Default + "/filter-list-opportunity";
        public const string FilterListOrderPaymentStatus = Default + "/filter-list-order-payment-status";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListRequestState = Default + "/filter-list-request-state";
        public const string FilterListItem = Default + "/filter-list-item";
        public const string FilterListUnitOfMeasure = Default + "/filter-list-unit-of-measure";

        public const string SingleListContract = Default + "/single-list-contract";
        public const string SingleListAppUser = Default + "/single-list-app-user";
        public const string SingleListCustomer = Default + "/single-list-customer";
        public const string SingleListCustomerType = Default + "/single-list-customer-type";
        public const string SingleListDistrict = Default + "/single-list-district";
        public const string SingleListNation = Default + "/single-list-nation";
        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListWard = Default + "/single-list-ward";
        public const string SingleListEditedPriceStatus = Default + "/single-list-edited-price-status";
        public const string SingleListOpportunity = Default + "/single-list-opportunity";
        public const string SingleListOrderPaymentStatus = Default + "/single-list-order-payment-status";
        public const string SingleListOrganization = Default + "/single-list-organization";
        public const string SingleListRequestState = Default + "/single-list-request-state";
        public const string SingleListItem = Default + "/single-list-item";
        public const string SingleListUnitOfMeasure = Default + "/single-list-unit-of-measure";
        public const string SingleListSupplier = Default + "/single-list-supplier";
        public const string SingleListProductGrouping = Default + "/single-list-product-grouping";
        public const string SingleListProductType = Default + "/single-list-product-type";
        public const string SingleListTaxType = Default + "/single-list-tax-type";
        
        public const string CountItem = Default + "/count-item";
        public const string ListItem = Default + "/list-item";
        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerSalesOrderFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.CustomerTypeId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.CustomerId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.OpportunityId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.ContractId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.OrderPaymentStatusId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.RequestStateId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.EditedPriceStatusId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.ShippingName), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.OrderDate), FieldTypeEnum.DATE.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryDate), FieldTypeEnum.DATE.Id },
            { nameof(CustomerSalesOrderFilter.SalesEmployeeId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.Note), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.InvoiceAddress), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.InvoiceNationId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.InvoiceProvinceId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.InvoiceDistrictId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.InvoiceWardId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.InvoiceZIPCode), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryAddress), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryNationId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryProvinceId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryDistrictId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryWardId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.DeliveryZIPCode), FieldTypeEnum.STRING.Id },
            { nameof(CustomerSalesOrderFilter.SubTotal), FieldTypeEnum.DECIMAL.Id },
            { nameof(CustomerSalesOrderFilter.GeneralDiscountPercentage), FieldTypeEnum.DECIMAL.Id },
            { nameof(CustomerSalesOrderFilter.GeneralDiscountAmount), FieldTypeEnum.DECIMAL.Id },
            { nameof(CustomerSalesOrderFilter.TotalTaxOther), FieldTypeEnum.DECIMAL.Id },
            { nameof(CustomerSalesOrderFilter.TotalTax), FieldTypeEnum.DECIMAL.Id },
            { nameof(CustomerSalesOrderFilter.Total), FieldTypeEnum.DECIMAL.Id },
            { nameof(CustomerSalesOrderFilter.CreatorId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.OrganizationId), FieldTypeEnum.ID.Id },
            { nameof(CustomerSalesOrderFilter.RowId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Parent,
                Master, Count, List,
                Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince, 
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState, 
                FilterListItem, FilterListUnitOfMeasure, } },
            { "Thêm", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince, 
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState, 
                FilterListItem, FilterListUnitOfMeasure,  
                Detail, Create, 
                SingleListContract, SingleListAppUser, SingleListCustomer, SingleListCustomerType, SingleListDistrict, SingleListNation, SingleListProvince, 
                SingleListWard, SingleListEditedPriceStatus, SingleListOpportunity, SingleListOrderPaymentStatus, SingleListOrganization, SingleListRequestState, 
                SingleListItem, SingleListUnitOfMeasure, SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListTaxType,
                CountItem, ListItem } },

            { "Sửa", new List<string> { 
                Parent,            
                Master, Count, List, Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince,
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState,
                FilterListItem, FilterListUnitOfMeasure,
                Detail, Update,
                SingleListContract, SingleListAppUser, SingleListCustomer, SingleListCustomerType, SingleListDistrict, SingleListNation, SingleListProvince,
                SingleListWard, SingleListEditedPriceStatus, SingleListOpportunity, SingleListOrderPaymentStatus, SingleListOrganization, SingleListRequestState,
                SingleListItem, SingleListUnitOfMeasure, SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListTaxType,
                CountItem, ListItem } },

            { "Xoá", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince,
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState,
                FilterListItem, FilterListUnitOfMeasure,
                Delete,
                SingleListContract, SingleListAppUser, SingleListCustomer, SingleListCustomerType, SingleListDistrict, SingleListNation, SingleListProvince,
                SingleListWard, SingleListEditedPriceStatus, SingleListOpportunity, SingleListOrderPaymentStatus, SingleListOrganization, SingleListRequestState,
                SingleListItem, SingleListUnitOfMeasure, SingleListSupplier, SingleListProductGrouping, SingleListProductType, SingleListTaxType, } },

            { "Xoá nhiều", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince,
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState,
                FilterListItem, FilterListUnitOfMeasure,
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince,
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState,
                FilterListItem, FilterListUnitOfMeasure,
                Export } },

            { "Nhập excel", new List<string> { 
                Parent,
                Master, Count, List, Get, GetPreview,
                FilterListContract, FilterListAppUser, FilterListCustomer, FilterListCustomerType, FilterListDistrict, FilterListNation, FilterListProvince,
                FilterListWard, FilterListEditedPriceStatus, FilterListOpportunity, FilterListOrderPaymentStatus, FilterListOrganization, FilterListRequestState,
                FilterListItem, FilterListUnitOfMeasure,
                ExportTemplate, Import } },
        };
    }
}
