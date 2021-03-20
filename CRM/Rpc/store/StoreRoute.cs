using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;

namespace CRM.Rpc.store
{
    public class StoreRoute : Root
    {
        public const string Master = Module + "/sales/store/store-master";
        public const string Detail = Module + "/sales/store/store-detail";
        private const string Default = Rpc + Module + "/store";
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
        
        
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        
        public const string FilterListDistrict = Default + "/filter-list-district";
        
        public const string FilterListOrganization = Default + "/filter-list-organization";
        
        public const string FilterListStore = Default + "/filter-list-store";
        
        public const string FilterListProvince = Default + "/filter-list-province";
        
        public const string FilterListStatus = Default + "/filter-list-status";
        
        public const string FilterListStoreGrouping = Default + "/filter-list-store-grouping";
        
        public const string FilterListStoreStatus = Default + "/filter-list-store-status";
        
        public const string FilterListStoreType = Default + "/filter-list-store-type";
        
        public const string FilterListWard = Default + "/filter-list-ward";
        

        
        public const string SingleListAppUser = Default + "/single-list-app-user";
        
        public const string SingleListDistrict = Default + "/single-list-district";
        
        public const string SingleListOrganization = Default + "/single-list-organization";
        
        public const string SingleListStore = Default + "/single-list-store";
        
        public const string SingleListProvince = Default + "/single-list-province";
        
        public const string SingleListStatus = Default + "/single-list-status";
        
        public const string SingleListStoreGrouping = Default + "/single-list-store-grouping";
        
        public const string SingleListStoreStatus = Default + "/single-list-store-status";
        
        public const string SingleListStoreType = Default + "/single-list-store-type";
        
        public const string SingleListWard = Default + "/single-list-ward";


        public const string SingleListBusinessType = Default + "/single-list-business-type";
        public const string SingleListPosition = Default + "/single-list-position";
        public const string SingleListStoreDeliveryTime = Default + "/single-list-store-delivery-time";
        public const string SingleListRelationshipCustomerType = Default + "/single-list-relationship-customer";
        public const string SingleListInfulenceLevelMarket = Default + "/single-list-infulence-level-market";
        public const string SingleListMarketPrice = Default + "/single-list-market-price";
        public const string SingleListConsultingService = Default + "/single-list-consulting-service";
        public const string SingleListCooperativeAttitude = Default + "/single-list-cooperative-attitude";
        public const string SingleListCurrency = Default + "/single-list-currency";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(StoreFilter.Id), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.Code), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.CodeDraft), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.Name), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.UnsignName), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.ParentStoreId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.OrganizationId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.StoreTypeId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.StoreGroupingId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.Telephone), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.ProvinceId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.DistrictId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.WardId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.Address), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.UnsignAddress), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.DeliveryAddress), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.Latitude), FieldTypeEnum.DECIMAL.Id },
            { nameof(StoreFilter.Longitude), FieldTypeEnum.DECIMAL.Id },
            { nameof(StoreFilter.DeliveryLatitude), FieldTypeEnum.DECIMAL.Id },
            { nameof(StoreFilter.DeliveryLongitude), FieldTypeEnum.DECIMAL.Id },
            { nameof(StoreFilter.OwnerName), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.OwnerPhone), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.OwnerEmail), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.TaxCode), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.LegalEntity), FieldTypeEnum.STRING.Id },
            { nameof(StoreFilter.AppUserId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.StatusId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.StoreStatusId), FieldTypeEnum.ID.Id },
            { nameof(StoreFilter.CustomerId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard, 
            } },
            { "Thêm", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard, Detail, Create, 
                SingleListAppUser, SingleListDistrict, SingleListOrganization, SingleListStore, SingleListProvince, 
                SingleListStatus, SingleListStoreGrouping, SingleListStoreStatus, SingleListStoreType, SingleListWard,
                SingleListBusinessType,SingleListPosition,SingleListStoreDeliveryTime,SingleListRelationshipCustomerType,SingleListInfulenceLevelMarket,
                SingleListMarketPrice,SingleListConsultingService,SingleListCooperativeAttitude,SingleListCurrency
            } },

            { "Sửa", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard,  
                Detail, Update, 
                SingleListAppUser, SingleListDistrict, SingleListOrganization, SingleListStore, SingleListProvince, 
                SingleListStatus, SingleListStoreGrouping, SingleListStoreStatus, SingleListStoreType, SingleListWard,
                SingleListBusinessType,SingleListPosition,SingleListStoreDeliveryTime,SingleListRelationshipCustomerType,SingleListInfulenceLevelMarket,
                SingleListMarketPrice,SingleListConsultingService,SingleListCooperativeAttitude,SingleListCurrency
                } },

            { "Xoá", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard,  
                Delete, 
                SingleListAppUser, SingleListDistrict, SingleListOrganization, SingleListStore, SingleListProvince, 
                SingleListStatus, SingleListStoreGrouping, SingleListStoreStatus, SingleListStoreType, SingleListWard,  } },

            { "Xoá nhiều", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard,  
                BulkDelete } },

            { "Xuất excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard,  
                Export } },

            { "Nhập excel", new List<string> { 
                Master, Count, List, Get, GetPreview,
                FilterListAppUser, FilterListDistrict, FilterListOrganization, FilterListStore, FilterListProvince, 
                FilterListStatus, FilterListStoreGrouping, FilterListStoreStatus, FilterListStoreType, FilterListWard,  
                ExportTemplate, Import } },
        };
    }
}
