using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;

namespace CRM.Rpc.direct_sales_order
{
    public class DirectSalesOrderRoute : Root
    {
        public const string Parent = Module + "/sale-order";
        public const string Master = Module + "/sale-order/direct-sales-order/direct-sales-order-master";
        public const string Detail = Module + "/sale-order/direct-sales-order/direct-sales-order-detail/*";
        public const string Mobile = Module + ".direct-sales-order.*";
        private const string Default = Rpc + Module + "/direct-sales-order";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";

        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListStore = Default + "/filter-list-store";
        public const string FilterListStoreStatus = Default + "/filter-list-store-status";
        public const string FilterListUnitOfMeasure = Default + "/filter-list-unit-of-measure";
        public const string FilterListEditedPriceStatus = Default + "/filter-list-edit-price-status";
        public const string FilterListRequestState = Default + "/filter-list-request-state";
        public const string FilterListCustomerAgent = Default + "/filter-list-customer-agent";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(DirectSalesOrderFilter.AppUserId), FieldTypeEnum.ID.Id },
            { nameof(DirectSalesOrderFilter.OrganizationId), FieldTypeEnum.ID.Id },
            { nameof(DirectSalesOrderFilter.UserId), FieldTypeEnum.ID.Id },
        };


        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List, Get,
                FilterListStore,FilterListStoreStatus, FilterListEditedPriceStatus, FilterListRequestState, FilterListAppUser, FilterListUnitOfMeasure, FilterListOrganization, FilterListCustomerAgent} },
        };
    }
}
