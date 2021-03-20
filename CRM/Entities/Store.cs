using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Store : DataEntity,  IEquatable<Store>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string CodeDraft { get; set; }
        public string Name { get; set; }
        public string UnsignName { get; set; }
        public long? ParentStoreId { get; set; }
        public long OrganizationId { get; set; }
        public long StoreTypeId { get; set; }
        public long? StoreGroupingId { get; set; }
        public string Telephone { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? WardId { get; set; }
        public string Address { get; set; }
        public string UnsignAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal? DeliveryLatitude { get; set; }
        public decimal? DeliveryLongitude { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }
        public string TaxCode { get; set; }
        public string LegalEntity { get; set; }
        public long? AppUserId { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public long StoreStatusId { get; set; }
        public long? CustomerId { get; set; }
        public AppUser AppUser { get; set; }
        public District District { get; set; }
        public Organization Organization { get; set; }
        public Store ParentStore { get; set; }
        public Province Province { get; set; }
        public Status Status { get; set; }
        public StoreGrouping StoreGrouping { get; set; }
        public StoreStatus StoreStatus { get; set; }
        public StoreType StoreType { get; set; }
        public Ward Ward { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }
        public StoreExtend StoreExtend { get; set; }
        public List<StoreImageMapping> StoreImageMappings { get; set; }
        //public List<StoreRepresent> StoreRepresents { get; set; }
        //public List<StorePersonnel> StorePersonnels { get; set; }
        //public List<StoreAssets> StoreAssetses { get; set; }
        //public List<StoreDeliveryTimeMapping> StoreDeliveryTimeMappings { get; set; }
        //public List<StoreRelationshipCustomerMapping> StoreRelationshipCustomerMappings { get; set; }
        //public List<StoreInfulenceLevelMarketMapping> StoreInfulenceLevelMarketMappings { get; set; }
        //public List<StoreMarketPriceMapping> StoreMarketPriceMappings { get; set; } 
        //public List<StoreConsultingServiceMapping> StoreConsultingServiceMappings { get; set; }
        //public List<StoreCooperativeAttitudeMapping> StoreCooperativeAttitudeMappings { get; set; }
        //public List<StoreWarrantyService> StoreWarrantyServices { get; set; }
        //public List<StoreMeansOfDelivery> StoreMeansOfDeliveries { get; set; }
        //public List<ImproveQualityServing> ImproveQualityServings { get; set; }
        //public List<BusinessConcentrationLevel> BusinessConcentrationLevels { get; set; }
        //public List<StoreCoverageCapacity> StoreCoverageCapacities { get; set; }

        public bool Equals(Store other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class StoreFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter CodeDraft { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter UnsignName { get; set; }
        public IdFilter ParentStoreId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter StoreTypeId { get; set; }
        public IdFilter StoreGroupingId { get; set; }
        public StringFilter Telephone { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter WardId { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter UnsignAddress { get; set; }
        public StringFilter DeliveryAddress { get; set; }
        public DecimalFilter Latitude { get; set; }
        public DecimalFilter Longitude { get; set; }
        public DecimalFilter DeliveryLatitude { get; set; }
        public DecimalFilter DeliveryLongitude { get; set; }
        public StringFilter OwnerName { get; set; }
        public StringFilter OwnerPhone { get; set; }
        public StringFilter OwnerEmail { get; set; }
        public StringFilter TaxCode { get; set; }
        public StringFilter LegalEntity { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter StoreStatusId { get; set; }
        public IdFilter CustomerId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public bool? isSelected { get; set; }
        public List<StoreFilter> OrFilter { get; set; }
        public StoreOrder OrderBy {get; set;}
        public StoreSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreOrder
    {
        Id = 0,
        Code = 1,
        CodeDraft = 2,
        Name = 3,
        UnsignName = 4,
        ParentStore = 5,
        Organization = 6,
        StoreType = 7,
        StoreGrouping = 8,
        Reseller = 9,
        Telephone = 10,
        Province = 11,
        District = 12,
        Ward = 13,
        Address = 14,
        UnsignAddress = 15,
        DeliveryAddress = 16,
        Latitude = 17,
        Longitude = 18,
        DeliveryLatitude = 19,
        DeliveryLongitude = 20,
        OwnerName = 21,
        OwnerPhone = 22,
        OwnerEmail = 23,
        TaxCode = 24,
        LegalEntity = 25,
        AppUser = 26,
        Status = 27,
        Used = 31,
        StoreStatus = 33,
        Customer = 34,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum StoreSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        CodeDraft = E._2,
        Name = E._3,
        UnsignName = E._4,
        ParentStore = E._5,
        Organization = E._6,
        StoreType = E._7,
        StoreGrouping = E._8,
        Reseller = E._9,
        Telephone = E._10,
        Province = E._11,
        District = E._12,
        Ward = E._13,
        Address = E._14,
        UnsignAddress = E._15,
        DeliveryAddress = E._16,
        Latitude = E._17,
        Longitude = E._18,
        DeliveryLatitude = E._19,
        DeliveryLongitude = E._20,
        OwnerName = E._21,
        OwnerPhone = E._22,
        OwnerEmail = E._23,
        TaxCode = E._24,
        LegalEntity = E._25,
        AppUser = E._26,
        Status = E._27,
        Used = E._31,
        StoreStatus = E._33,
        Customer = E._34,
    }
}
